using Godot;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Nodes.Rooms;

namespace Zhao.Forms;

/// <summary>
/// 歌姬形态常态视频背景。
/// - 位置:以「照」角色的稳定战斗锚点(NCreature 节点在战斗画布中的位置)为水平参考,
///   视频水平中心与角色视觉中心对齐,整体悬挂在角色正上方(屏幕顶部);不写死任何屏幕像素坐标,
///   经画布变换(GetGlobalTransform)转换,随分辨率/镜头缩放自适应;
/// - 挂载点:本体战斗房间的 BackCombatVfxContainer(后层特效容器)——位于战斗 UI 与角色之下,
///   不遮挡卡牌/能量/血量/Buff/敌人状态;ZIndex 未改;
/// - 尺寸:保持视频原生大小(ExpandKeepSize),不放大、不裁切、不改长宽比;
/// - 循环播放:VideoStreamPlayer.Loop = true;
/// - 生命周期:单一播放器节点,进入歌姬时显示并继续播放,离开歌姬时暂停并隐藏(不重建、不重头播放);
///   战斗结束随战斗房间一起销毁,静态引用经 Hook.AfterCombatEnd 补丁清理。
/// - 视频资源:res://zhao/video/diva_bg.ogv(Theora;源为哈希包内 USM,未修改内容,仅容器转码)。
/// </summary>
public static class DivaVideoBackground
{
    private const string VideoPath = "res://zhao/video/diva_bg.ogv";

    /// <summary>视频内容的原生宽度(源 USM 重建产物,固定内容尺寸,非屏幕分辨率)。</summary>
    private const float NativeVideoWidth = 1024f;

    private static VideoStreamPlayer? _player;
    private static VideoStreamTheora? _stream;

    /// <summary>最近一次进入歌姬形态时的角色引用(视频纹理定尺寸后重算位置用)。</summary>
    private static Creature? _lastCreature;

    /// <summary>进入歌姬形态:显示并(继续)循环播放。重复调用安全,不会创建第二个播放器。</summary>
    public static void ShowForDivaForm(Creature creature)
    {
        var room = NCombatRoom.Instance;
        if (room == null || !GodotObject.IsInstanceValid(room))
        {
            return;
        }
        var container = room.BackCombatVfxContainer;
        if (container == null || !GodotObject.IsInstanceValid(container))
        {
            return;
        }

        _lastCreature = creature;

        if (_player == null || !GodotObject.IsInstanceValid(_player))
        {
            _stream ??= GD.Load<VideoStreamTheora>(VideoPath);
            if (_stream == null)
            {
                return;
            }

            _player = new VideoStreamPlayer
            {
                Stream = _stream,
                Loop = true,
                MouseFilter = Control.MouseFilterEnum.Ignore,
                Visible = false,
            };
            // 左上锚定(anchors 0/0),位置完全由 UpdatePosition 按角色锚点计算
            _player.SetAnchorsPreset(Control.LayoutPreset.TopLeft);
            _player.GrowHorizontal = Control.GrowDirection.End;
            _player.GrowVertical = Control.GrowDirection.End;
            // 视频纹理定尺寸后重算一次位置(此时 Size.X 为真实原生宽,不再依赖预置常量)
            _player.Resized += OnVideoResized;

            container.AddChild(_player);
        }

        UpdatePosition(creature);

        _player.Visible = true;
        if (!_player.IsPlaying())
        {
            _player.Play();
        }
    }

    /// <summary>视频尺寸确定后按角色锚点重算位置。</summary>
    private static void OnVideoResized()
    {
        if (_lastCreature == null || !GodotObject.IsInstanceValid(_player))
        {
            return;
        }
        UpdatePosition(_lastCreature);
    }

    /// <summary>
    /// 按角色稳定战斗锚点定位视频:
    /// 读取 Zhao 玩家 NCreature 节点在战斗画布中的位置(稳定基准,不逐帧跟随攻击动画),
    /// 经容器画布变换转换到 BackCombatVfxContainer 局部坐标,
    /// 视频水平中心与角色视觉中心对齐;垂直方向保持在屏幕顶部(悬挂于角色上方,不遮挡主体)。
    /// </summary>
    private static void UpdatePosition(Creature creature)
    {
        if (_player == null || !GodotObject.IsInstanceValid(_player))
        {
            return;
        }
        var room = NCombatRoom.Instance;
        var container = room?.BackCombatVfxContainer;
        var creatureNode = room?.GetCreatureNode(creature);
        if (room == null || container == null || !GodotObject.IsInstanceValid(container) ||
            creatureNode == null || !GodotObject.IsInstanceValid(creatureNode))
        {
            return;
        }

        // 角色战斗锚点(画布坐标)→ 视频容器局部坐标
        Vector2 anchorInContainer = container.GetGlobalTransform().AffineInverse() * creatureNode.GlobalPosition;

        // 视频原生宽度(尺寸已布局时用实际值,内容尺寸固定)
        float videoWidth = _player.Size.X > 0f ? _player.Size.X : NativeVideoWidth;

        // 水平中心与角色视觉中心对齐;顶部悬挂(与之前的垂直位置保持一致)
        _player.Position = new Vector2(anchorInContainer.X - videoWidth * 0.5f, 0f);
    }

    /// <summary>离开歌姬形态:暂停并隐藏(保留播放位置,再次进入时继续,不重建播放器)。</summary>
    public static void HideFromDivaForm()
    {
        if (_player == null || !GodotObject.IsInstanceValid(_player))
        {
            return;
        }
        _player.Paused = true;
        _player.Visible = false;
    }

    /// <summary>战斗结束:清理静态引用(节点本身随战斗房间销毁)。</summary>
    public static void CleanupCombat()
    {
        _player = null;
        _stream = null;
        _lastCreature = null;
    }
}
