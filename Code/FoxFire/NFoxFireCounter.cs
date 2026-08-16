using System.Collections.Generic;
using System.ComponentModel;
using Godot;
using Godot.Bridge;
using Godot.NativeInterop;
using MegaCrit.Sts2.Core.Entities.Players;

namespace Zhao.FoxFire;

public sealed class NFoxFireCounter : Control
{
	public class MethodName : MethodName
	{
		public static readonly StringName _ExitTree = StringName.op_Implicit("_ExitTree");

		public static readonly StringName OnAmountChanged = StringName.op_Implicit("OnAmountChanged");

		public static readonly StringName RefreshVisibility = StringName.op_Implicit("RefreshVisibility");

		public static readonly StringName SetCountText = StringName.op_Implicit("SetCountText");
	}

	public class PropertyName : PropertyName
	{
		public static readonly StringName _countLabel = StringName.op_Implicit("_countLabel");

		public static readonly StringName _displayedAmount = StringName.op_Implicit("_displayedAmount");
	}

	public class SignalName : SignalName
	{
	}

	private const string IconPath = "res://zhao/images/foxfire/foxfire_icon.png";

	private Player? _player;

	private FoxFireResource? _resource;

	private Label? _countLabel;

	private int _displayedAmount = -1;

	private NFoxFireCounter()
	{
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Expected O, but got Unknown
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d5: Expected O, but got Unknown
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Expected O, but got Unknown
		//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0104: Expected O, but got Unknown
		//IL_0104: Unknown result type (might be due to invalid IL or missing references)
		//IL_010c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0114: Unknown result type (might be due to invalid IL or missing references)
		//IL_0121: Expected O, but got Unknown
		//IL_014b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0157: Expected O, but got Unknown
		//IL_0162: Unknown result type (might be due to invalid IL or missing references)
		//IL_017b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0185: Expected O, but got Unknown
		//IL_0190: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b3: Expected O, but got Unknown
		//IL_01be: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ca: Expected O, but got Unknown
		//IL_01d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01de: Expected O, but got Unknown
		((Control)this).MouseFilter = (MouseFilterEnum)2;
		((Control)this).Size = new Vector2(128f, 128f);
		((Control)this).AnchorLeft = 0.5f;
		((Control)this).AnchorTop = 1f;
		((Control)this).AnchorRight = 0.5f;
		((Control)this).AnchorBottom = 1f;
		((Control)this).OffsetLeft = 64f;
		((Control)this).OffsetTop = -212f;
		((Control)this).OffsetRight = 192f;
		((Control)this).OffsetBottom = -84f;
		((Control)this).Scale = new Vector2(0.8f, 0.8f);
		TextureRect val = new TextureRect
		{
			Name = new StringName("Icon"),
			Texture = GD.Load<Texture2D>("res://zhao/images/foxfire/foxfire_icon.png"),
			ExpandMode = (ExpandModeEnum)1,
			StretchMode = (StretchModeEnum)5,
			MouseFilter = (MouseFilterEnum)2
		};
		((Control)val).SetAnchorsPreset((LayoutPreset)15, false);
		((Node)this).AddChild((Node)val, false, (InternalMode)0);
		_countLabel = new Label
		{
			Name = new StringName("CountLabel"),
			HorizontalAlignment = (HorizontalAlignment)1,
			VerticalAlignment = (VerticalAlignment)1,
			MouseFilter = (MouseFilterEnum)2
		};
		((Control)_countLabel).SetAnchorsPreset((LayoutPreset)15, false);
		((Control)_countLabel).OffsetTop = 41f;
		((Control)_countLabel).AddThemeFontSizeOverride(new StringName("font_size"), 36);
		((Control)_countLabel).AddThemeColorOverride(new StringName("font_color"), new Color(1f, 0.9647f, 0.8863f, 1f));
		((Control)_countLabel).AddThemeColorOverride(new StringName("font_outline_color"), new Color(0f, 0.2453f, 0.46f, 1f));
		((Control)_countLabel).AddThemeConstantOverride(new StringName("outline_size"), 14);
		((Node)this).AddChild((Node)_countLabel, false, (InternalMode)0);
		((CanvasItem)this).Visible = false;
		SetCountText(0);
	}

	public static NFoxFireCounter Create(Player player)
	{
		NFoxFireCounter nFoxFireCounter = new NFoxFireCounter();
		nFoxFireCounter.Initialize(player);
		return nFoxFireCounter;
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
		((Node)this)._ExitTree();
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
			((CanvasItem)this).Visible = false;
		}
		else
		{
			((CanvasItem)this).Visible = ((CanvasItem)this).Visible || _resource.Amount > 0;
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

	[EditorBrowsable(/*Could not decode attribute arguments.*/)]
	internal static List<MethodInfo> GetGodotMethodList()
	{
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0118: Unknown result type (might be due to invalid IL or missing references)
		//IL_0123: Unknown result type (might be due to invalid IL or missing references)
		List<MethodInfo> obj = new List<MethodInfo>(4);
		obj.Add(new MethodInfo(MethodName._ExitTree, new PropertyInfo((Type)0, StringName.op_Implicit(""), (PropertyHint)0, "", (PropertyUsageFlags)6, false), (MethodFlags)1, (List<PropertyInfo>)null, (List<Variant>)null));
		StringName onAmountChanged = MethodName.OnAmountChanged;
		PropertyInfo val = new PropertyInfo((Type)0, StringName.op_Implicit(""), (PropertyHint)0, "", (PropertyUsageFlags)6, false);
		long num = 1L;
		List<PropertyInfo> obj2 = new List<PropertyInfo>();
		obj2.Add(new PropertyInfo((Type)2, StringName.op_Implicit("oldAmount"), (PropertyHint)0, "", (PropertyUsageFlags)6, false));
		obj2.Add(new PropertyInfo((Type)2, StringName.op_Implicit("newAmount"), (PropertyHint)0, "", (PropertyUsageFlags)6, false));
		obj.Add(new MethodInfo(onAmountChanged, val, (MethodFlags)num, obj2, (List<Variant>)null));
		obj.Add(new MethodInfo(MethodName.RefreshVisibility, new PropertyInfo((Type)0, StringName.op_Implicit(""), (PropertyHint)0, "", (PropertyUsageFlags)6, false), (MethodFlags)1, (List<PropertyInfo>)null, (List<Variant>)null));
		StringName setCountText = MethodName.SetCountText;
		PropertyInfo val2 = new PropertyInfo((Type)0, StringName.op_Implicit(""), (PropertyHint)0, "", (PropertyUsageFlags)6, false);
		long num2 = 1L;
		List<PropertyInfo> obj3 = new List<PropertyInfo>();
		obj3.Add(new PropertyInfo((Type)2, StringName.op_Implicit("amount"), (PropertyHint)0, "", (PropertyUsageFlags)6, false));
		obj.Add(new MethodInfo(setCountText, val2, (MethodFlags)num2, obj3, (List<Variant>)null));
		return obj;
	}

	[EditorBrowsable(/*Could not decode attribute arguments.*/)]
	protected override bool InvokeGodotClassMethod(in godot_string_name method, NativeVariantPtrArgs args, out godot_variant ret)
	{
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		if ((ref method) == MethodName._ExitTree && ((NativeVariantPtrArgs)(ref args)).Count == 0)
		{
			((Node)this)._ExitTree();
			ret = default(godot_variant);
			return true;
		}
		if ((ref method) == MethodName.OnAmountChanged && ((NativeVariantPtrArgs)(ref args)).Count == 2)
		{
			OnAmountChanged(VariantUtils.ConvertTo<int>(ref ((NativeVariantPtrArgs)(ref args))[0]), VariantUtils.ConvertTo<int>(ref ((NativeVariantPtrArgs)(ref args))[1]));
			ret = default(godot_variant);
			return true;
		}
		if ((ref method) == MethodName.RefreshVisibility && ((NativeVariantPtrArgs)(ref args)).Count == 0)
		{
			RefreshVisibility();
			ret = default(godot_variant);
			return true;
		}
		if ((ref method) == MethodName.SetCountText && ((NativeVariantPtrArgs)(ref args)).Count == 1)
		{
			SetCountText(VariantUtils.ConvertTo<int>(ref ((NativeVariantPtrArgs)(ref args))[0]));
			ret = default(godot_variant);
			return true;
		}
		return ((Control)this).InvokeGodotClassMethod(ref method, args, ref ret);
	}

	[EditorBrowsable(/*Could not decode attribute arguments.*/)]
	protected override bool HasGodotClassMethod(in godot_string_name method)
	{
		if ((ref method) == MethodName._ExitTree)
		{
			return true;
		}
		if ((ref method) == MethodName.OnAmountChanged)
		{
			return true;
		}
		if ((ref method) == MethodName.RefreshVisibility)
		{
			return true;
		}
		if ((ref method) == MethodName.SetCountText)
		{
			return true;
		}
		return ((Control)this).HasGodotClassMethod(ref method);
	}

	[EditorBrowsable(/*Could not decode attribute arguments.*/)]
	protected override bool SetGodotClassPropertyValue(in godot_string_name name, in godot_variant value)
	{
		if ((ref name) == PropertyName._countLabel)
		{
			_countLabel = VariantUtils.ConvertTo<Label>(ref value);
			return true;
		}
		if ((ref name) == PropertyName._displayedAmount)
		{
			_displayedAmount = VariantUtils.ConvertTo<int>(ref value);
			return true;
		}
		return ((GodotObject)this).SetGodotClassPropertyValue(ref name, ref value);
	}

	[EditorBrowsable(/*Could not decode attribute arguments.*/)]
	protected override bool GetGodotClassPropertyValue(in godot_string_name name, out godot_variant value)
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		if ((ref name) == PropertyName._countLabel)
		{
			value = VariantUtils.CreateFrom<Label>(ref _countLabel);
			return true;
		}
		if ((ref name) == PropertyName._displayedAmount)
		{
			value = VariantUtils.CreateFrom<int>(ref _displayedAmount);
			return true;
		}
		return ((GodotObject)this).GetGodotClassPropertyValue(ref name, ref value);
	}

	[EditorBrowsable(/*Could not decode attribute arguments.*/)]
	internal static List<PropertyInfo> GetGodotPropertyList()
	{
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		List<PropertyInfo> obj = new List<PropertyInfo>();
		obj.Add(new PropertyInfo((Type)24, PropertyName._countLabel, (PropertyHint)0, "", (PropertyUsageFlags)4096, false));
		obj.Add(new PropertyInfo((Type)2, PropertyName._displayedAmount, (PropertyHint)0, "", (PropertyUsageFlags)4096, false));
		return obj;
	}

	[EditorBrowsable(/*Could not decode attribute arguments.*/)]
	protected override void SaveGodotObjectData(GodotSerializationInfo info)
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		((GodotObject)this).SaveGodotObjectData(info);
		info.AddProperty(PropertyName._countLabel, Variant.From<Label>(ref _countLabel));
		info.AddProperty(PropertyName._displayedAmount, Variant.From<int>(ref _displayedAmount));
	}

	[EditorBrowsable(/*Could not decode attribute arguments.*/)]
	protected override void RestoreGodotObjectData(GodotSerializationInfo info)
	{
		((GodotObject)this).RestoreGodotObjectData(info);
		Variant val = default(Variant);
		if (info.TryGetProperty(PropertyName._countLabel, ref val))
		{
			_countLabel = ((Variant)(ref val)).As<Label>();
		}
		Variant val2 = default(Variant);
		if (info.TryGetProperty(PropertyName._displayedAmount, ref val2))
		{
			_displayedAmount = ((Variant)(ref val2)).As<int>();
		}
	}
}
