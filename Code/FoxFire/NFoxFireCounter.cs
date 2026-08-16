using Godot;
using MegaCrit.Sts2.Core.Entities.Players;

namespace Zhao.FoxFire;

/// <summary>
/// 狐火计数器 UI:0.107.1 星辉计数器同架构(参考 NStarCounter)。
///  - 独立计数器节点(图标 + 数字),不是 Power 图标 + 层数;
///  - 挂在与星辉计数器相同的位置:能量计数器上方、底部居中锚定,尺寸/偏移与 star_counter.tscn 一致;
///  - 订阅 FoxFireResource.AmountChanged 刷新数字与可见性(对应 NStarCounter 订阅 StarsChanged);
///  - 由 ZhaoCombatUiPatch 在 NCombatUi.Activate 后创建(对应原版 Activate 内 _starCounter.Initialize/Reparent)。
/// ⚠️ 图标当前为模组占位图(zhao/images/foxfire/foxfire_icon.png),正式美术待用户提供。
/// </summary>
public sealed partial class NFoxFireCounter : Control
{
    private const string IconPath = "res://zhao/images/foxfire/foxfire_icon.png";

    private Player? _player;
    private FoxFireResource? _resource;
    private Label? _countLabel;
    private int _displayedAmount = -1;

    private NFoxFireCounter()
    {
        MouseFilter = MouseFilterEnum.Ignore;
        Size = new Vector2(128f, 128f);

        // 与 star_counter.tscn 相同的放置:底部居中锚定 + 偏移 (64, -212, 192, -84),scale 0.8
        AnchorLeft = 0.5f;
        AnchorTop = 1f;
        AnchorRight = 0.5f;
        AnchorBottom = 1f;
        OffsetLeft = 64f;
        OffsetTop = -212f;
        OffsetRight = 192f;
        OffsetBottom = -84f;
        Scale = new Vector2(0.8f, 0.8f);

        // 图标(占位)
        var icon = new TextureRect
        {
            Name = "Icon",
            Texture = GD.Load<Texture2D>(IconPath),
            ExpandMode = TextureRect.ExpandModeEnum.IgnoreSize,
            StretchMode = TextureRect.StretchModeEnum.KeepAspectCentered,
            MouseFilter = MouseFilterEnum.Ignore,
        };
        icon.SetAnchorsPreset(LayoutPreset.FullRect);
        AddChild(icon);

        // 数量文本(与 star_counter.tscn 的 CountLabel 相同布局:顶部留白 41)
        _countLabel = new Label
        {
            Name = "CountLabel",
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Center,
            MouseFilter = MouseFilterEnum.Ignore,
        };
        _countLabel.SetAnchorsPreset(LayoutPreset.FullRect);
        _countLabel.OffsetTop = 41f;
        _countLabel.AddThemeFontSizeOverride("font_size", 36);
        _countLabel.AddThemeColorOverride("font_color", new Color(1f, 0.9647f, 0.8863f));
        _countLabel.AddThemeColorOverride("font_outline_color", new Color(0f, 0.2453f, 0.46f));
        _countLabel.AddThemeConstantOverride("outline_size", 14);
        AddChild(_countLabel);

        Visible = false;
        SetCountText(0);
    }

    /// <summary>创建并绑定到指定玩家的狐火计数器。</summary>
    public static NFoxFireCounter Create(Player player)
    {
        var counter = new NFoxFireCounter();
        counter.Initialize(player);
        return counter;
    }

    private void Initialize(Player player)
    {
        _player = player;
        _resource = FoxFireBank.For(player);
        _resource.AmountChanged += OnAmountChanged;
        RefreshVisibility();
    }

    public override void _ExitTree()
    {
        base._ExitTree();
        if (_resource != null)
        {
            _resource.AmountChanged -= OnAmountChanged;
            _resource = null;
        }
        _player = null;
    }

    private void OnAmountChanged(int oldAmount, int newAmount)
    {
        SetCountText(newAmount);
        RefreshVisibility();
    }

    private void RefreshVisibility()
    {
        if (_resource == null)
        {
            Visible = false;
            return;
        }
        // 对应 NStarCounter.RefreshVisibility:数量为 0 时隐藏
        Visible = Visible || _resource.Amount > 0;
    }

    private void SetCountText(int amount)
    {
        if (_displayedAmount == amount || _countLabel == null)
        {
            return;
        }
        _displayedAmount = amount;
        _countLabel.Text = amount.ToString();
    }
}
