using Godot;
using HarmonyLib;
using MegaCrit.Sts2.Core.Nodes.Screens.CharacterSelect;
using Zhao.Character;

namespace Zhao.Patches;

/// <summary>
/// 选角页「照」按钮专属选人框(用户提供 4 张 420x684 竖版框图,按 1→2→3→4 顺序循环)。
/// 挂接点:NCharacterSelectButton.Init(角色按钮初始化;原版在此设置图标/锁定态)。
/// 做法:为 Zhao 按钮隐藏原版 outline(原版选中时显示的框),改挂自建 ZhaoSelectFrame:
///  - 按原版 100x148 按钮的高度等比缩放，保持宽高比居中、不拉伸变形;
///  - 默认显示第 1 张作为角色按钮卡面；本按钮被选中(IsSelected)时才开始轮播;
///  - 4 张图按 1→2→3→4 每 2.5 秒切换、循环(AnimationPlayer,纯代码创建,不引入场景脚本)。
/// </summary>
[HarmonyPatch(typeof(NCharacterSelectButton), nameof(NCharacterSelectButton.Init))]
public static class ZhaoSelectButtonPatch
{
    /// <summary>
    /// 用 __args 注入参数(不依赖参数名匹配),对任何编译器元数据都可靠。
    /// </summary>
    private static void Postfix(NCharacterSelectButton __instance, object[] __args)
    {
        if (__args.Length == 0 || __args[0] is not ZhaoCharacter)
        {
            return;
        }
        var frame = ZhaoSelectFrame.Create();
        __instance.AddChild(frame);
        __instance.MoveChild(frame, 0); // 放到按钮最底层:原版图标/遮罩仍在框之上
        ConfigureInput(__instance);
    }

    private static void ConfigureInput(NCharacterSelectButton button)
    {
        button.MouseFilter = Control.MouseFilterEnum.Stop;
        IgnoreChildControls(button);
        button.Connect(Control.SignalName.GuiInput,
            Callable.From<InputEvent>(inputEvent => SelectOnClick(button, inputEvent)));
    }

    private static void IgnoreChildControls(Node parent)
    {
        foreach (Node child in parent.GetChildren())
        {
            if (child is Control control)
            {
                control.MouseFilter = Control.MouseFilterEnum.Ignore;
            }
            IgnoreChildControls(child);
        }
    }

    private static void SelectOnClick(NCharacterSelectButton button, InputEvent inputEvent)
    {
        if (button.IsEnabled && !button.IsLocked && inputEvent is InputEventMouseButton
            {
                ButtonIndex: MouseButton.Left,
                Pressed: true,
            })
        {
            button.Select();
        }
    }
}

/// <summary>
/// 「照」选角按钮框(代码创建节点,不进入任何 .tscn)。
/// 关键:用 Sprite2D(Node2D)而不是 Control —— 触屏设备上 Control 即使 mouse_filter=Ignore
/// 也可能参与 GUI 命中判定吞掉点击;Sprite2D 完全不在 GUI 输入路径中,点击百分百落到按钮上。
/// 每帧按父按钮选中态启停轮播(与选角背景轮播同步),并隐藏原版三态 outline(避免双框叠影)。
/// </summary>
public sealed partial class ZhaoSelectFrame : Sprite2D
{
    private const string Frame1Path = "res://zhao/images/char_select_frame/1.png";
    private const string Frame2Path = "res://zhao/images/char_select_frame/2.png";
    private const string Frame3Path = "res://zhao/images/char_select_frame/3.png";
    private const string Frame4Path = "res://zhao/images/char_select_frame/4.png";
    private static readonly StringName FrameAnimationName = "ZhaoFrame";
    private static readonly StringName BgAnimationName = "Carousel";

    /// <summary>每张框图停留时长(秒),与选角背景轮播保持一致以实现同步。</summary>
    private const float FrameDuration = 2.5f;

    /// <summary>框图为 420x684，按原版 100x148 按钮的高度等比缩放，避免覆盖选角界面。</summary>
    private const float FrameScale = 148f / 684f;

    /// <summary>按钮中心(原版 char_select_button.tscn:custom_minimum_size 100x148,pivot 50,74)。</summary>
    private static readonly Vector2 ButtonCenter = new(50f, 74f);

    /// <summary>选角背景场景根节点名(原版 NCharacterSelectScreen 按 "{Id.Entry}_bg" 命名)。</summary>
    private const string BgRootName = "ZHAO_CHARACTER_bg";

    private AnimationPlayer? _animPlayer;
    private AnimationPlayer? _bgAnimPlayer;
    private bool _wasSelected;

    public static ZhaoSelectFrame Create()
    {
        var frame = new ZhaoSelectFrame
        {
            Name = "ZhaoSelectFrame",
            Texture = GD.Load<Texture2D>(Frame1Path),
            Centered = true,
            Position = ButtonCenter,
            Scale = Vector2.One * FrameScale,
            Visible = true, // 默认显示第1张框图(选中后才开始轮播)
        };

        // 1→2→3→4 轮播动画(不自动播放:选中后才由 _Process 启动,与背景轮播同步)
        var animation = new Animation
        {
            Length = FrameDuration * 4f,
            LoopMode = Animation.LoopModeEnum.Linear,
        };
        int track = animation.AddTrack(Animation.TrackType.Value, 0);
        animation.TrackSetPath(track, new NodePath(".:texture"));
        animation.ValueTrackSetUpdateMode(track, Animation.UpdateMode.Discrete);
        animation.TrackInsertKey(track, 0f * FrameDuration, GD.Load<Texture2D>(Frame1Path));
        animation.TrackInsertKey(track, 1f * FrameDuration, GD.Load<Texture2D>(Frame2Path));
        animation.TrackInsertKey(track, 2f * FrameDuration, GD.Load<Texture2D>(Frame3Path));
        animation.TrackInsertKey(track, 3f * FrameDuration, GD.Load<Texture2D>(Frame4Path));

        var library = new AnimationLibrary();
        library.AddAnimation("ZhaoFrame", animation);
        var player = new AnimationPlayer
        {
            Name = "ZhaoFrameAnim",
            RootNode = new NodePath(".."),
        };
        player.AddAnimationLibrary("", library);
        frame._animPlayer = player;
        frame.AddChild(player);

        return frame;
    }

    public override void _Process(double delta)
    {
        var button = GetParentOrNull<NCharacterSelectButton>();
        if (button == null)
        {
            return;
        }

        // 选中态边沿:选中 → 框图与背景轮播同步从第1张开始播放;取消选中 → 双双停回第1张
        bool selected = button.IsSelected;
        if (selected != _wasSelected)
        {
            _wasSelected = selected;
            if (selected)
            {
                _bgAnimPlayer = FindBgCarouselPlayer(button);
                PlayBoth(_animPlayer, _bgAnimPlayer);
            }
            else
            {
                ResetBoth(_animPlayer, _bgAnimPlayer);
                _bgAnimPlayer = null;
            }
        }

        // 背景由原版 SelectCharacter 动态重建；如果首帧尚未入树，后续帧继续查找并启动。
        if (selected && (_bgAnimPlayer == null || !GodotObject.IsInstanceValid(_bgAnimPlayer)))
        {
            _bgAnimPlayer = FindBgCarouselPlayer(button);
            PlayAnimation(_bgAnimPlayer, BgAnimationName);
        }

        // Zhao 按钮隐藏原版三态 outline,避免与自建框叠影
        HideVanillaOutline(button, "%OutlineLocal");
        HideVanillaOutline(button, "%OutlineRemote");
        HideVanillaOutline(button, "%OutlineMixed");
        // 隐藏原版图标层(铁甲战士占位图/遮罩/阴影),让用户的框图完整显示,不被遮挡
        HideVanillaOutline(button, "%Icon");
        HideVanillaOutline(button, "%IconAdd");
        HideVanillaOutline(button, "%Shadow");
        HideVanillaOutline(button, "MarginContainer/Mask");
    }

    /// <summary>在场景树中定位 Zhao 选角背景场景的轮播播放器(背景随悬停/选中可能重建,每次边沿重新查找)。</summary>
    private static AnimationPlayer? FindBgCarouselPlayer(NCharacterSelectButton button)
    {
        var tree = button.GetTree();
        if (tree == null)
        {
            return null;
        }
        var bg = tree.Root?.FindChild(BgRootName, recursive: true, owned: false);
        return bg?.GetNodeOrNull<AnimationPlayer>("CarouselAnim");
    }

    private static void PlayBoth(AnimationPlayer? framePlayer, AnimationPlayer? bgPlayer)
    {
        PlayAnimation(framePlayer, FrameAnimationName);
        PlayAnimation(bgPlayer, BgAnimationName);
    }

    private static void ResetBoth(AnimationPlayer? framePlayer, AnimationPlayer? bgPlayer)
    {
        ResetAnimation(framePlayer, FrameAnimationName);
        ResetAnimation(bgPlayer, BgAnimationName);
    }

    private static void PlayAnimation(AnimationPlayer? player, StringName animationName)
    {
        if (!GodotObject.IsInstanceValid(player))
        {
            return;
        }
        player!.Play(animationName);
        player.Seek(0.0, true);
    }

    private static void ResetAnimation(AnimationPlayer? player, StringName animationName)
    {
        if (!GodotObject.IsInstanceValid(player))
        {
            return;
        }
        player!.Play(animationName);
        player.Seek(0.0, true);
        player.Pause();
    }

    private static void HideVanillaOutline(NCharacterSelectButton button, string nodePath)
    {
        var outline = button.GetNodeOrNull<Control>(nodePath);
        if (outline != null && outline.Visible)
        {
            outline.Visible = false;
        }
    }
}
