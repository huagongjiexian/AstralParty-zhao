using System;
using System.Runtime.CompilerServices;
using Godot;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Nodes.Combat;
using MegaCrit.Sts2.Core.Nodes.Rooms;

namespace Zhao.Forms;

public static class DivaVideoBackground
{
	[CompilerGenerated]
	private static class _003C_003EO
	{
		public static Action _003C0_003E__OnVideoResized;
	}

	private const string VideoPath = "res://zhao/video/diva_bg.ogv";

	private const float NativeVideoWidth = 1024f;

	private static VideoStreamPlayer? _player;

	private static VideoStreamTheora? _stream;

	private static Creature? _lastCreature;

	public static void ShowForDivaForm(Creature creature)
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Expected O, but got Unknown
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Expected O, but got Unknown
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Expected O, but got Unknown
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Expected O, but got Unknown
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Expected O, but got Unknown
		//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fb: Expected O, but got Unknown
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e3: Expected O, but got Unknown
		NCombatRoom instance = NCombatRoom.Instance;
		if (instance == null || !GodotObject.IsInstanceValid((GodotObject)instance))
		{
			return;
		}
		Control backCombatVfxContainer = instance.BackCombatVfxContainer;
		if (backCombatVfxContainer == null || !GodotObject.IsInstanceValid((GodotObject)backCombatVfxContainer))
		{
			return;
		}
		_lastCreature = creature;
		if (_player == null || !GodotObject.IsInstanceValid((GodotObject)_player))
		{
			if (_stream == null)
			{
				_stream = GD.Load<VideoStreamTheora>("res://zhao/video/diva_bg.ogv");
			}
			if (_stream == null)
			{
				return;
			}
			_player = new VideoStreamPlayer
			{
				Stream = (VideoStream)_stream,
				Loop = true,
				MouseFilter = (MouseFilterEnum)2,
				Visible = false
			};
			((Control)_player).SetAnchorsPreset((LayoutPreset)0, false);
			((Control)_player).GrowHorizontal = (GrowDirection)1;
			((Control)_player).GrowVertical = (GrowDirection)1;
			VideoStreamPlayer? player = _player;
			object obj = _003C_003EO._003C0_003E__OnVideoResized;
			if (obj == null)
			{
				Action val = OnVideoResized;
				_003C_003EO._003C0_003E__OnVideoResized = val;
				obj = (object)val;
			}
			((Control)player).Resized += (Action)obj;
			((Node)backCombatVfxContainer).AddChild((Node)_player, false, (InternalMode)0);
		}
		UpdatePosition(creature);
		((CanvasItem)_player).Visible = true;
		if (!_player.IsPlaying())
		{
			_player.Play();
		}
	}

	private static void OnVideoResized()
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Expected O, but got Unknown
		if (_lastCreature != null && GodotObject.IsInstanceValid((GodotObject)_player))
		{
			UpdatePosition(_lastCreature);
		}
	}

	private static void UpdatePosition(Creature creature)
	{
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Expected O, but got Unknown
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Expected O, but got Unknown
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Expected O, but got Unknown
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		if (_player != null && GodotObject.IsInstanceValid((GodotObject)_player))
		{
			NCombatRoom instance = NCombatRoom.Instance;
			Control val = ((instance != null) ? instance.BackCombatVfxContainer : null);
			NCreature val2 = ((instance != null) ? instance.GetCreatureNode(creature) : null);
			if (instance != null && val != null && GodotObject.IsInstanceValid((GodotObject)val) && val2 != null && GodotObject.IsInstanceValid((GodotObject)val2))
			{
				Transform2D globalTransform = ((CanvasItem)val).GetGlobalTransform();
				Vector2 val3 = ((Transform2D)(ref globalTransform)).AffineInverse() * ((Control)val2).GlobalPosition;
				float num = ((((Control)_player).Size.X > 0f) ? ((Control)_player).Size.X : 1024f);
				((Control)_player).Position = new Vector2(val3.X - num * 0.5f, 0f);
			}
		}
	}

	public static void HideFromDivaForm()
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Expected O, but got Unknown
		if (_player != null && GodotObject.IsInstanceValid((GodotObject)_player))
		{
			_player.Paused = true;
			((CanvasItem)_player).Visible = false;
		}
	}

	public static void CleanupCombat()
	{
		_player = null;
		_stream = null;
		_lastCreature = null;
	}
}
