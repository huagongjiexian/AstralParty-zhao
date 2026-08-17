using Godot;
using MegaCrit.Sts2.Core.Entities.Players;

namespace Zhao.FoxFire;

/// <summary>
/// 狐火独立计数器 UI(非 Power 图标):显示当前狐火层数,层数大于 0 时可见。
/// </summary>
public partial class NFoxFireCounter : Control
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
		AnchorLeft = 0.5f;
		AnchorTop = 1f;
		AnchorRight = 0.5f;
		AnchorBottom = 1f;
		OffsetLeft = 64f;
		OffsetTop = -212f;
		OffsetRight = 192f;
		OffsetBottom = -84f;
		Scale = new Vector2(0.8f, 0.8f);

		TextureRect icon = new TextureRect
		{
			Name = "Icon",
			Texture = GD.Load<Texture2D>(IconPath),
			ExpandMode = ExpandModeEnum.IgnoreSize,
			StretchMode = StretchModeEnum.KeepAspectCentered,
			MouseFilter = MouseFilterEnum.Ignore
		};
		icon.SetAnchorsPreset(LayoutPreset.FullRect, false);
		AddChild(icon, false, InternalMode.Disabled);

		_countLabel = new Label
		{
			Name = "CountLabel",
			HorizontalAlignment = HorizontalAlignment.Center,
			VerticalAlignment = VerticalAlignment.Center,
			MouseFilter = MouseFilterEnum.Ignore
		};
		_countLabel.SetAnchorsPreset(LayoutPreset.FullRect, false);
		_countLabel.OffsetTop = 41f;
		_countLabel.AddThemeFontSizeOverride("font_size", 36);
		_countLabel.AddThemeColorOverride("font_color", new Color(1f, 0.9647f, 0.8863f, 1f));
		_countLabel.AddThemeColorOverride("font_outline_color", new Color(0f, 0.2453f, 0.46f, 1f));
		_countLabel.AddThemeConstantOverride("outline_size", 14);
		AddChild(_countLabel, false, InternalMode.Disabled);
		Visible = false;
		SetCountText(0);
	}

	public static NFoxFireCounter Create(Player player)
	{
		NFoxFireCounter counter = new NFoxFireCounter();
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
		// 防御:节点被释放但事件未退订时直接忽略回调,避免 ObjectDisposedException 闪退
		if (!GodotObject.IsInstanceValid(this))
		{
			return;
		}
		SetCountText(newAmount);
		RefreshVisibility();
	}

	private void RefreshVisibility()
	{
		if (_resource == null)
		{
			Visible = false;
		}
		else
		{
			// 修复:原反编译代码为 Visible || Amount>0,导致显示后永不隐藏
			Visible = _resource.Amount > 0;
		}
	}

	private void SetCountText(int amount)
	{
		if (_displayedAmount != amount && _countLabel != null)
		{
			_displayedAmount = amount;
			_countLabel.Text = amount.ToString();
		}
	}
}
