using System.Collections.Generic;
using System.ComponentModel;
using Godot;
using Godot.Bridge;
using Godot.NativeInterop;
using MegaCrit.Sts2.Core.Nodes.Screens.CharacterSelect;

namespace Zhao.Patches;

public sealed class ZhaoSelectFrame : Sprite2D
{
	public class MethodName : MethodName
	{
		public static readonly StringName Create = StringName.op_Implicit("Create");

		public static readonly StringName _Process = StringName.op_Implicit("_Process");

		public static readonly StringName FindBgCarouselPlayer = StringName.op_Implicit("FindBgCarouselPlayer");

		public static readonly StringName PlayBoth = StringName.op_Implicit("PlayBoth");

		public static readonly StringName ResetBoth = StringName.op_Implicit("ResetBoth");

		public static readonly StringName PlayAnimation = StringName.op_Implicit("PlayAnimation");

		public static readonly StringName ResetAnimation = StringName.op_Implicit("ResetAnimation");

		public static readonly StringName HideVanillaOutline = StringName.op_Implicit("HideVanillaOutline");
	}

	public class PropertyName : PropertyName
	{
		public static readonly StringName _animPlayer = StringName.op_Implicit("_animPlayer");

		public static readonly StringName _bgAnimPlayer = StringName.op_Implicit("_bgAnimPlayer");

		public static readonly StringName _wasSelected = StringName.op_Implicit("_wasSelected");
	}

	public class SignalName : SignalName
	{
	}

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
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Expected O, but got Unknown
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Expected O, but got Unknown
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Expected O, but got Unknown
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_0114: Unknown result type (might be due to invalid IL or missing references)
		//IL_0124: Unknown result type (might be due to invalid IL or missing references)
		//IL_012a: Expected O, but got Unknown
		//IL_0130: Unknown result type (might be due to invalid IL or missing references)
		//IL_013b: Expected O, but got Unknown
		//IL_0136: Unknown result type (might be due to invalid IL or missing references)
		//IL_013c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0141: Unknown result type (might be due to invalid IL or missing references)
		//IL_0147: Unknown result type (might be due to invalid IL or missing references)
		//IL_0151: Expected O, but got Unknown
		//IL_0151: Unknown result type (might be due to invalid IL or missing references)
		//IL_0157: Unknown result type (might be due to invalid IL or missing references)
		//IL_0161: Expected O, but got Unknown
		//IL_0162: Expected O, but got Unknown
		//IL_0168: Unknown result type (might be due to invalid IL or missing references)
		//IL_0173: Expected O, but got Unknown
		//IL_016e: Unknown result type (might be due to invalid IL or missing references)
		//IL_017d: Unknown result type (might be due to invalid IL or missing references)
		//IL_018a: Expected O, but got Unknown
		ZhaoSelectFrame zhaoSelectFrame = new ZhaoSelectFrame();
		((Node)zhaoSelectFrame).Name = new StringName("ZhaoSelectFrame");
		((Sprite2D)zhaoSelectFrame).Texture = GD.Load<Texture2D>("res://zhao/images/char_select_frame/1.png");
		((Sprite2D)zhaoSelectFrame).Centered = true;
		((Node2D)zhaoSelectFrame).Position = ButtonCenter;
		((Node2D)zhaoSelectFrame).Scale = Vector2.One * (37f / 171f);
		((CanvasItem)zhaoSelectFrame).Visible = true;
		Animation val = new Animation
		{
			Length = 10f,
			LoopMode = (LoopModeEnum)1
		};
		int num = val.AddTrack((TrackType)0, 0);
		val.TrackSetPath(num, new NodePath(".:texture"));
		val.ValueTrackSetUpdateMode(num, (UpdateMode)1);
		val.TrackInsertKey(num, 0.0, Variant.op_Implicit((GodotObject)(object)GD.Load<Texture2D>("res://zhao/images/char_select_frame/1.png")), 1f);
		val.TrackInsertKey(num, 2.5, Variant.op_Implicit((GodotObject)(object)GD.Load<Texture2D>("res://zhao/images/char_select_frame/2.png")), 1f);
		val.TrackInsertKey(num, 5.0, Variant.op_Implicit((GodotObject)(object)GD.Load<Texture2D>("res://zhao/images/char_select_frame/3.png")), 1f);
		val.TrackInsertKey(num, 7.5, Variant.op_Implicit((GodotObject)(object)GD.Load<Texture2D>("res://zhao/images/char_select_frame/4.png")), 1f);
		AnimationLibrary val2 = new AnimationLibrary();
		val2.AddAnimation(new StringName("ZhaoFrame"), val);
		AnimationPlayer val3 = new AnimationPlayer
		{
			Name = new StringName("ZhaoFrameAnim"),
			RootNode = new NodePath("..")
		};
		((AnimationMixer)val3).AddAnimationLibrary(new StringName(""), val2);
		zhaoSelectFrame._animPlayer = val3;
		((Node)zhaoSelectFrame).AddChild((Node)val3, false, (InternalMode)0);
		return zhaoSelectFrame;
	}

	public override void _Process(double delta)
	{
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Expected O, but got Unknown
		NCharacterSelectButton parentOrNull = ((Node)this).GetParentOrNull<NCharacterSelectButton>();
		if (parentOrNull == null)
		{
			return;
		}
		bool isSelected = parentOrNull.IsSelected;
		if (isSelected != _wasSelected)
		{
			_wasSelected = isSelected;
			if (isSelected)
			{
				_bgAnimPlayer = FindBgCarouselPlayer(parentOrNull);
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
			_bgAnimPlayer = FindBgCarouselPlayer(parentOrNull);
			PlayAnimation(_bgAnimPlayer, BgAnimationName);
		}
		HideVanillaOutline(parentOrNull, "%OutlineLocal");
		HideVanillaOutline(parentOrNull, "%OutlineRemote");
		HideVanillaOutline(parentOrNull, "%OutlineMixed");
		HideVanillaOutline(parentOrNull, "%Icon");
		HideVanillaOutline(parentOrNull, "%IconAdd");
		HideVanillaOutline(parentOrNull, "%Shadow");
		HideVanillaOutline(parentOrNull, "MarginContainer/Mask");
	}

	private static AnimationPlayer? FindBgCarouselPlayer(NCharacterSelectButton button)
	{
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Expected O, but got Unknown
		SceneTree tree = ((Node)button).GetTree();
		if (tree == null)
		{
			return null;
		}
		Window root = tree.Root;
		Node val = ((root != null) ? ((Node)root).FindChild("ZHAO_CHARACTER_bg", true, false) : null);
		if (val == null)
		{
			return null;
		}
		return val.GetNodeOrNull<AnimationPlayer>(new NodePath("CarouselAnim"));
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
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Expected O, but got Unknown
		if (GodotObject.IsInstanceValid((GodotObject)player))
		{
			player.Play(animationName, -1.0, 1f, false);
			player.Seek(0.0, true);
		}
	}

	private static void ResetAnimation(AnimationPlayer? player, StringName animationName)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Expected O, but got Unknown
		if (GodotObject.IsInstanceValid((GodotObject)player))
		{
			player.Play(animationName, -1.0, 1f, false);
			player.Seek(0.0, true);
			player.Pause();
		}
	}

	private static void HideVanillaOutline(NCharacterSelectButton button, string nodePath)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Expected O, but got Unknown
		Control nodeOrNull = ((Node)button).GetNodeOrNull<Control>(new NodePath(nodePath));
		if (nodeOrNull != null && ((CanvasItem)nodeOrNull).Visible)
		{
			((CanvasItem)nodeOrNull).Visible = false;
		}
	}

	[EditorBrowsable(/*Could not decode attribute arguments.*/)]
	internal static List<MethodInfo> GetGodotMethodList()
	{
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Expected O, but got Unknown
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c1: Expected O, but got Unknown
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f0: Expected O, but got Unknown
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_011c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0145: Unknown result type (might be due to invalid IL or missing references)
		//IL_0150: Expected O, but got Unknown
		//IL_014b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0171: Unknown result type (might be due to invalid IL or missing references)
		//IL_017c: Expected O, but got Unknown
		//IL_0177: Unknown result type (might be due to invalid IL or missing references)
		//IL_0182: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dc: Expected O, but got Unknown
		//IL_01d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0208: Expected O, but got Unknown
		//IL_0203: Unknown result type (might be due to invalid IL or missing references)
		//IL_020e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0234: Unknown result type (might be due to invalid IL or missing references)
		//IL_025d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0268: Expected O, but got Unknown
		//IL_0263: Unknown result type (might be due to invalid IL or missing references)
		//IL_0285: Unknown result type (might be due to invalid IL or missing references)
		//IL_0290: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02df: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ea: Expected O, but got Unknown
		//IL_02e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0307: Unknown result type (might be due to invalid IL or missing references)
		//IL_0312: Unknown result type (might be due to invalid IL or missing references)
		//IL_0338: Unknown result type (might be due to invalid IL or missing references)
		//IL_0361: Unknown result type (might be due to invalid IL or missing references)
		//IL_036c: Expected O, but got Unknown
		//IL_0367: Unknown result type (might be due to invalid IL or missing references)
		//IL_0388: Unknown result type (might be due to invalid IL or missing references)
		//IL_0393: Unknown result type (might be due to invalid IL or missing references)
		List<MethodInfo> obj = new List<MethodInfo>(8);
		obj.Add(new MethodInfo(MethodName.Create, new PropertyInfo((Type)24, StringName.op_Implicit(""), (PropertyHint)0, "", (PropertyUsageFlags)6, new StringName("Sprite2D"), false), (MethodFlags)33, (List<PropertyInfo>)null, (List<Variant>)null));
		StringName process = MethodName._Process;
		PropertyInfo val = new PropertyInfo((Type)0, StringName.op_Implicit(""), (PropertyHint)0, "", (PropertyUsageFlags)6, false);
		long num = 1L;
		List<PropertyInfo> obj2 = new List<PropertyInfo>();
		obj2.Add(new PropertyInfo((Type)3, StringName.op_Implicit("delta"), (PropertyHint)0, "", (PropertyUsageFlags)6, false));
		obj.Add(new MethodInfo(process, val, (MethodFlags)num, obj2, (List<Variant>)null));
		StringName findBgCarouselPlayer = MethodName.FindBgCarouselPlayer;
		PropertyInfo val2 = new PropertyInfo((Type)24, StringName.op_Implicit(""), (PropertyHint)0, "", (PropertyUsageFlags)6, new StringName("AnimationPlayer"), false);
		long num2 = 33L;
		List<PropertyInfo> obj3 = new List<PropertyInfo>();
		obj3.Add(new PropertyInfo((Type)24, StringName.op_Implicit("button"), (PropertyHint)0, "", (PropertyUsageFlags)6, new StringName("Control"), false));
		obj.Add(new MethodInfo(findBgCarouselPlayer, val2, (MethodFlags)num2, obj3, (List<Variant>)null));
		StringName playBoth = MethodName.PlayBoth;
		PropertyInfo val3 = new PropertyInfo((Type)0, StringName.op_Implicit(""), (PropertyHint)0, "", (PropertyUsageFlags)6, false);
		long num3 = 33L;
		List<PropertyInfo> obj4 = new List<PropertyInfo>();
		obj4.Add(new PropertyInfo((Type)24, StringName.op_Implicit("framePlayer"), (PropertyHint)0, "", (PropertyUsageFlags)6, new StringName("AnimationPlayer"), false));
		obj4.Add(new PropertyInfo((Type)24, StringName.op_Implicit("bgPlayer"), (PropertyHint)0, "", (PropertyUsageFlags)6, new StringName("AnimationPlayer"), false));
		obj.Add(new MethodInfo(playBoth, val3, (MethodFlags)num3, obj4, (List<Variant>)null));
		StringName resetBoth = MethodName.ResetBoth;
		PropertyInfo val4 = new PropertyInfo((Type)0, StringName.op_Implicit(""), (PropertyHint)0, "", (PropertyUsageFlags)6, false);
		long num4 = 33L;
		List<PropertyInfo> obj5 = new List<PropertyInfo>();
		obj5.Add(new PropertyInfo((Type)24, StringName.op_Implicit("framePlayer"), (PropertyHint)0, "", (PropertyUsageFlags)6, new StringName("AnimationPlayer"), false));
		obj5.Add(new PropertyInfo((Type)24, StringName.op_Implicit("bgPlayer"), (PropertyHint)0, "", (PropertyUsageFlags)6, new StringName("AnimationPlayer"), false));
		obj.Add(new MethodInfo(resetBoth, val4, (MethodFlags)num4, obj5, (List<Variant>)null));
		StringName playAnimation = MethodName.PlayAnimation;
		PropertyInfo val5 = new PropertyInfo((Type)0, StringName.op_Implicit(""), (PropertyHint)0, "", (PropertyUsageFlags)6, false);
		long num5 = 33L;
		List<PropertyInfo> obj6 = new List<PropertyInfo>();
		obj6.Add(new PropertyInfo((Type)24, StringName.op_Implicit("player"), (PropertyHint)0, "", (PropertyUsageFlags)6, new StringName("AnimationPlayer"), false));
		obj6.Add(new PropertyInfo((Type)21, StringName.op_Implicit("animationName"), (PropertyHint)0, "", (PropertyUsageFlags)6, false));
		obj.Add(new MethodInfo(playAnimation, val5, (MethodFlags)num5, obj6, (List<Variant>)null));
		StringName resetAnimation = MethodName.ResetAnimation;
		PropertyInfo val6 = new PropertyInfo((Type)0, StringName.op_Implicit(""), (PropertyHint)0, "", (PropertyUsageFlags)6, false);
		long num6 = 33L;
		List<PropertyInfo> obj7 = new List<PropertyInfo>();
		obj7.Add(new PropertyInfo((Type)24, StringName.op_Implicit("player"), (PropertyHint)0, "", (PropertyUsageFlags)6, new StringName("AnimationPlayer"), false));
		obj7.Add(new PropertyInfo((Type)21, StringName.op_Implicit("animationName"), (PropertyHint)0, "", (PropertyUsageFlags)6, false));
		obj.Add(new MethodInfo(resetAnimation, val6, (MethodFlags)num6, obj7, (List<Variant>)null));
		StringName hideVanillaOutline = MethodName.HideVanillaOutline;
		PropertyInfo val7 = new PropertyInfo((Type)0, StringName.op_Implicit(""), (PropertyHint)0, "", (PropertyUsageFlags)6, false);
		long num7 = 33L;
		List<PropertyInfo> obj8 = new List<PropertyInfo>();
		obj8.Add(new PropertyInfo((Type)24, StringName.op_Implicit("button"), (PropertyHint)0, "", (PropertyUsageFlags)6, new StringName("Control"), false));
		obj8.Add(new PropertyInfo((Type)4, StringName.op_Implicit("nodePath"), (PropertyHint)0, "", (PropertyUsageFlags)6, false));
		obj.Add(new MethodInfo(hideVanillaOutline, val7, (MethodFlags)num7, obj8, (List<Variant>)null));
		return obj;
	}

	[EditorBrowsable(/*Could not decode attribute arguments.*/)]
	protected override bool InvokeGodotClassMethod(in godot_string_name method, NativeVariantPtrArgs args, out godot_variant ret)
	{
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_010d: Unknown result type (might be due to invalid IL or missing references)
		//IL_014c: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_018b: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ca: Unknown result type (might be due to invalid IL or missing references)
		if ((ref method) == MethodName.Create && ((NativeVariantPtrArgs)(ref args)).Count == 0)
		{
			ZhaoSelectFrame zhaoSelectFrame = Create();
			ret = VariantUtils.CreateFrom<ZhaoSelectFrame>(ref zhaoSelectFrame);
			return true;
		}
		if ((ref method) == MethodName._Process && ((NativeVariantPtrArgs)(ref args)).Count == 1)
		{
			((Node)this)._Process(VariantUtils.ConvertTo<double>(ref ((NativeVariantPtrArgs)(ref args))[0]));
			ret = default(godot_variant);
			return true;
		}
		if ((ref method) == MethodName.FindBgCarouselPlayer && ((NativeVariantPtrArgs)(ref args)).Count == 1)
		{
			AnimationPlayer val = FindBgCarouselPlayer(VariantUtils.ConvertTo<NCharacterSelectButton>(ref ((NativeVariantPtrArgs)(ref args))[0]));
			ret = VariantUtils.CreateFrom<AnimationPlayer>(ref val);
			return true;
		}
		if ((ref method) == MethodName.PlayBoth && ((NativeVariantPtrArgs)(ref args)).Count == 2)
		{
			PlayBoth(VariantUtils.ConvertTo<AnimationPlayer>(ref ((NativeVariantPtrArgs)(ref args))[0]), VariantUtils.ConvertTo<AnimationPlayer>(ref ((NativeVariantPtrArgs)(ref args))[1]));
			ret = default(godot_variant);
			return true;
		}
		if ((ref method) == MethodName.ResetBoth && ((NativeVariantPtrArgs)(ref args)).Count == 2)
		{
			ResetBoth(VariantUtils.ConvertTo<AnimationPlayer>(ref ((NativeVariantPtrArgs)(ref args))[0]), VariantUtils.ConvertTo<AnimationPlayer>(ref ((NativeVariantPtrArgs)(ref args))[1]));
			ret = default(godot_variant);
			return true;
		}
		if ((ref method) == MethodName.PlayAnimation && ((NativeVariantPtrArgs)(ref args)).Count == 2)
		{
			PlayAnimation(VariantUtils.ConvertTo<AnimationPlayer>(ref ((NativeVariantPtrArgs)(ref args))[0]), VariantUtils.ConvertTo<StringName>(ref ((NativeVariantPtrArgs)(ref args))[1]));
			ret = default(godot_variant);
			return true;
		}
		if ((ref method) == MethodName.ResetAnimation && ((NativeVariantPtrArgs)(ref args)).Count == 2)
		{
			ResetAnimation(VariantUtils.ConvertTo<AnimationPlayer>(ref ((NativeVariantPtrArgs)(ref args))[0]), VariantUtils.ConvertTo<StringName>(ref ((NativeVariantPtrArgs)(ref args))[1]));
			ret = default(godot_variant);
			return true;
		}
		if ((ref method) == MethodName.HideVanillaOutline && ((NativeVariantPtrArgs)(ref args)).Count == 2)
		{
			HideVanillaOutline(VariantUtils.ConvertTo<NCharacterSelectButton>(ref ((NativeVariantPtrArgs)(ref args))[0]), VariantUtils.ConvertTo<string>(ref ((NativeVariantPtrArgs)(ref args))[1]));
			ret = default(godot_variant);
			return true;
		}
		return ((Sprite2D)this).InvokeGodotClassMethod(ref method, args, ref ret);
	}

	[EditorBrowsable(/*Could not decode attribute arguments.*/)]
	internal static bool InvokeGodotClassStaticMethod(in godot_string_name method, NativeVariantPtrArgs args, out godot_variant ret)
	{
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00da: Unknown result type (might be due to invalid IL or missing references)
		//IL_0119: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0158: Unknown result type (might be due to invalid IL or missing references)
		//IL_0197: Unknown result type (might be due to invalid IL or missing references)
		if ((ref method) == MethodName.Create && ((NativeVariantPtrArgs)(ref args)).Count == 0)
		{
			ZhaoSelectFrame zhaoSelectFrame = Create();
			ret = VariantUtils.CreateFrom<ZhaoSelectFrame>(ref zhaoSelectFrame);
			return true;
		}
		if ((ref method) == MethodName.FindBgCarouselPlayer && ((NativeVariantPtrArgs)(ref args)).Count == 1)
		{
			AnimationPlayer val = FindBgCarouselPlayer(VariantUtils.ConvertTo<NCharacterSelectButton>(ref ((NativeVariantPtrArgs)(ref args))[0]));
			ret = VariantUtils.CreateFrom<AnimationPlayer>(ref val);
			return true;
		}
		if ((ref method) == MethodName.PlayBoth && ((NativeVariantPtrArgs)(ref args)).Count == 2)
		{
			PlayBoth(VariantUtils.ConvertTo<AnimationPlayer>(ref ((NativeVariantPtrArgs)(ref args))[0]), VariantUtils.ConvertTo<AnimationPlayer>(ref ((NativeVariantPtrArgs)(ref args))[1]));
			ret = default(godot_variant);
			return true;
		}
		if ((ref method) == MethodName.ResetBoth && ((NativeVariantPtrArgs)(ref args)).Count == 2)
		{
			ResetBoth(VariantUtils.ConvertTo<AnimationPlayer>(ref ((NativeVariantPtrArgs)(ref args))[0]), VariantUtils.ConvertTo<AnimationPlayer>(ref ((NativeVariantPtrArgs)(ref args))[1]));
			ret = default(godot_variant);
			return true;
		}
		if ((ref method) == MethodName.PlayAnimation && ((NativeVariantPtrArgs)(ref args)).Count == 2)
		{
			PlayAnimation(VariantUtils.ConvertTo<AnimationPlayer>(ref ((NativeVariantPtrArgs)(ref args))[0]), VariantUtils.ConvertTo<StringName>(ref ((NativeVariantPtrArgs)(ref args))[1]));
			ret = default(godot_variant);
			return true;
		}
		if ((ref method) == MethodName.ResetAnimation && ((NativeVariantPtrArgs)(ref args)).Count == 2)
		{
			ResetAnimation(VariantUtils.ConvertTo<AnimationPlayer>(ref ((NativeVariantPtrArgs)(ref args))[0]), VariantUtils.ConvertTo<StringName>(ref ((NativeVariantPtrArgs)(ref args))[1]));
			ret = default(godot_variant);
			return true;
		}
		if ((ref method) == MethodName.HideVanillaOutline && ((NativeVariantPtrArgs)(ref args)).Count == 2)
		{
			HideVanillaOutline(VariantUtils.ConvertTo<NCharacterSelectButton>(ref ((NativeVariantPtrArgs)(ref args))[0]), VariantUtils.ConvertTo<string>(ref ((NativeVariantPtrArgs)(ref args))[1]));
			ret = default(godot_variant);
			return true;
		}
		ret = default(godot_variant);
		return false;
	}

	[EditorBrowsable(/*Could not decode attribute arguments.*/)]
	protected override bool HasGodotClassMethod(in godot_string_name method)
	{
		if ((ref method) == MethodName.Create)
		{
			return true;
		}
		if ((ref method) == MethodName._Process)
		{
			return true;
		}
		if ((ref method) == MethodName.FindBgCarouselPlayer)
		{
			return true;
		}
		if ((ref method) == MethodName.PlayBoth)
		{
			return true;
		}
		if ((ref method) == MethodName.ResetBoth)
		{
			return true;
		}
		if ((ref method) == MethodName.PlayAnimation)
		{
			return true;
		}
		if ((ref method) == MethodName.ResetAnimation)
		{
			return true;
		}
		if ((ref method) == MethodName.HideVanillaOutline)
		{
			return true;
		}
		return ((Sprite2D)this).HasGodotClassMethod(ref method);
	}

	[EditorBrowsable(/*Could not decode attribute arguments.*/)]
	protected override bool SetGodotClassPropertyValue(in godot_string_name name, in godot_variant value)
	{
		if ((ref name) == PropertyName._animPlayer)
		{
			_animPlayer = VariantUtils.ConvertTo<AnimationPlayer>(ref value);
			return true;
		}
		if ((ref name) == PropertyName._bgAnimPlayer)
		{
			_bgAnimPlayer = VariantUtils.ConvertTo<AnimationPlayer>(ref value);
			return true;
		}
		if ((ref name) == PropertyName._wasSelected)
		{
			_wasSelected = VariantUtils.ConvertTo<bool>(ref value);
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
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		if ((ref name) == PropertyName._animPlayer)
		{
			value = VariantUtils.CreateFrom<AnimationPlayer>(ref _animPlayer);
			return true;
		}
		if ((ref name) == PropertyName._bgAnimPlayer)
		{
			value = VariantUtils.CreateFrom<AnimationPlayer>(ref _bgAnimPlayer);
			return true;
		}
		if ((ref name) == PropertyName._wasSelected)
		{
			value = VariantUtils.CreateFrom<bool>(ref _wasSelected);
			return true;
		}
		return ((GodotObject)this).GetGodotClassPropertyValue(ref name, ref value);
	}

	[EditorBrowsable(/*Could not decode attribute arguments.*/)]
	internal static List<PropertyInfo> GetGodotPropertyList()
	{
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		List<PropertyInfo> obj = new List<PropertyInfo>();
		obj.Add(new PropertyInfo((Type)24, PropertyName._animPlayer, (PropertyHint)0, "", (PropertyUsageFlags)4096, false));
		obj.Add(new PropertyInfo((Type)24, PropertyName._bgAnimPlayer, (PropertyHint)0, "", (PropertyUsageFlags)4096, false));
		obj.Add(new PropertyInfo((Type)1, PropertyName._wasSelected, (PropertyHint)0, "", (PropertyUsageFlags)4096, false));
		return obj;
	}

	[EditorBrowsable(/*Could not decode attribute arguments.*/)]
	protected override void SaveGodotObjectData(GodotSerializationInfo info)
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		((GodotObject)this).SaveGodotObjectData(info);
		info.AddProperty(PropertyName._animPlayer, Variant.From<AnimationPlayer>(ref _animPlayer));
		info.AddProperty(PropertyName._bgAnimPlayer, Variant.From<AnimationPlayer>(ref _bgAnimPlayer));
		info.AddProperty(PropertyName._wasSelected, Variant.From<bool>(ref _wasSelected));
	}

	[EditorBrowsable(/*Could not decode attribute arguments.*/)]
	protected override void RestoreGodotObjectData(GodotSerializationInfo info)
	{
		((GodotObject)this).RestoreGodotObjectData(info);
		Variant val = default(Variant);
		if (info.TryGetProperty(PropertyName._animPlayer, ref val))
		{
			_animPlayer = ((Variant)(ref val)).As<AnimationPlayer>();
		}
		Variant val2 = default(Variant);
		if (info.TryGetProperty(PropertyName._bgAnimPlayer, ref val2))
		{
			_bgAnimPlayer = ((Variant)(ref val2)).As<AnimationPlayer>();
		}
		Variant val3 = default(Variant);
		if (info.TryGetProperty(PropertyName._wasSelected, ref val3))
		{
			_wasSelected = ((Variant)(ref val3)).As<bool>();
		}
	}
}
