using Godot;
using MegaCrit.Sts2.Core.Nodes.Screens.CharacterSelect;

namespace Zhao.Patches;

/// <summary>
/// 选角按钮边框(Sprite2D):默认显示第 1 张框图;选中后按 1-2-3-4 循环,并与背景轮播同步。
/// </summary>
public partial class ZhaoSelectFrame : Sprite2D
{
	private const string Frame1Path = "res://zhao/images/char_select_frame/1.png";
	private const string Frame2Path = "res://zhao/images/char_select_frame/2.png";
	private const string Frame3Path = "res://zhao/images/char_select_frame/3.png";
	private const string Frame4Path = "res://zhao/images/char_select_frame/4.png";

	private static readonly StringName FrameAnimationName = new StringName("ZhaoFrame");
	private static readonly StringName BgAnimationName = new StringName("Carousel");

	private const float FrameDuration = 2.5f;
	private const float FrameScale = 37f / 171f;

	private static readonly Vector2 ButtonCenter = new Vector2(50f, 74f);

	private const string BgRootName = "ZHAO_CHARACTER_bg";

	private AnimationPlayer? _animPlayer;
	private AnimationPlayer? _bgAnimPlayer;
	private bool _wasSelected;

	public static ZhaoSelectFrame Create()
	{
		ZhaoSelectFrame frame = new ZhaoSelectFrame
		{
			Name = "ZhaoSelectFrame",
			Texture = GD.Load<Texture2D>(Frame1Path),
			Centered = true,
			Position = ButtonCenter,
			Scale = Vector2.One * FrameScale,
			Visible = true
		};

		Animation anim = new Animation
		{
			Length = 10f,
			LoopMode = Animation.LoopModeEnum.Linear
		};
		int track = anim.AddTrack(Animation.TrackType.Value, 0);
		anim.TrackSetPath(track, new NodePath(".:texture"));
		anim.ValueTrackSetUpdateMode(track, Animation.UpdateMode.Continuous);

		// 防御:资源缺失时不把 null 塞进动画轨,避免播放时崩溃
		Texture2D? t1 = GD.Load<Texture2D>(Frame1Path);
		Texture2D? t2 = GD.Load<Texture2D>(Frame2Path);
		Texture2D? t3 = GD.Load<Texture2D>(Frame3Path);
		Texture2D? t4 = GD.Load<Texture2D>(Frame4Path);
		if (t1 != null && t2 != null && t3 != null && t4 != null)
		{
			anim.TrackInsertKey(track, 0.0, t1, 1f);
			anim.TrackInsertKey(track, FrameDuration, t2, 1f);
			anim.TrackInsertKey(track, FrameDuration * 2f, t3, 1f);
			anim.TrackInsertKey(track, FrameDuration * 3f, t4, 1f);
		}

		AnimationLibrary lib = new AnimationLibrary();
		lib.AddAnimation(FrameAnimationName, anim);

		AnimationPlayer player = new AnimationPlayer
		{
			Name = "ZhaoFrameAnim",
			RootNode = new NodePath("..")
		};
		player.AddAnimationLibrary("", lib);
		frame._animPlayer = player;
		frame.AddChild(player, false, InternalMode.Disabled);
		return frame;
	}

	public override void _Process(double delta)
	{
		NCharacterSelectButton? button = GetParentOrNull<NCharacterSelectButton>();
		if (button == null)
		{
			return;
		}
		bool isSelected = button.IsSelected;
		if (isSelected != _wasSelected)
		{
			_wasSelected = isSelected;
			if (isSelected)
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
		if (isSelected && (_bgAnimPlayer == null || !GodotObject.IsInstanceValid((GodotObject)_bgAnimPlayer)))
		{
			_bgAnimPlayer = FindBgCarouselPlayer(button);
			PlayAnimation(_bgAnimPlayer, BgAnimationName);
		}
		HideVanillaOutline(button, "%OutlineLocal");
		HideVanillaOutline(button, "%OutlineRemote");
		HideVanillaOutline(button, "%OutlineMixed");
		HideVanillaOutline(button, "%Icon");
		HideVanillaOutline(button, "%IconAdd");
		HideVanillaOutline(button, "%Shadow");
		HideVanillaOutline(button, "MarginContainer/Mask");
	}

	private static AnimationPlayer? FindBgCarouselPlayer(NCharacterSelectButton button)
	{
		Window root = button.GetTree().Root;
		Node? node = root.FindChild(BgRootName, true, false);
		return node?.GetNodeOrNull<AnimationPlayer>(new NodePath("CarouselAnim"));
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
		if (GodotObject.IsInstanceValid((GodotObject)player))
		{
			player.Play(animationName, -1.0, 1f, false);
			player.Seek(0.0, true);
		}
	}

	private static void ResetAnimation(AnimationPlayer? player, StringName animationName)
	{
		if (GodotObject.IsInstanceValid((GodotObject)player))
		{
			player.Play(animationName, -1.0, 1f, false);
			player.Seek(0.0, true);
			player.Pause();
		}
	}

	private static void HideVanillaOutline(NCharacterSelectButton button, string nodePath)
	{
		Control? node = button.GetNodeOrNull<Control>(new NodePath(nodePath));
		if (node != null && node.Visible)
		{
			node.Visible = false;
		}
	}
}
