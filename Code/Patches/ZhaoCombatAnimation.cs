using System;
using System.Collections.Generic;
using Godot;
using MegaCrit.Sts2.Core.Nodes.Combat;

namespace Zhao.Patches;

internal static class ZhaoCombatAnimation
{
	private static readonly StringName AttackAnimation = new StringName("Attack");

	private static readonly StringName IdleAnimation = new StringName("Idle");

	private static readonly Dictionary<ulong, ValueTuple<AnimatedSprite2D, Callable>> FinishCallbacks = new Dictionary<ulong, ValueTuple<AnimatedSprite2D, Callable>>();

	public static void PlayAttack(NCreature creatureNode)
	{
		if (creatureNode != null && GodotObject.IsInstanceValid((GodotObject)(object)creatureNode))
		{
			NCreatureVisuals visuals = creatureNode.Visuals;
			Node2D obj = ((visuals != null) ? visuals.GetCurrentBody() : null);
			AnimatedSprite2D val = (AnimatedSprite2D)(object)((obj is AnimatedSprite2D) ? obj : null);
			if (val != null && GodotObject.IsInstanceValid((GodotObject)(object)val) && val.SpriteFrames != null && val.SpriteFrames.HasAnimation(AttackAnimation))
			{
				EnsureFinishCallback(val);
				val.Stop();
				val.Animation = AttackAnimation;
				val.Frame = 0;
				val.Play((StringName)null, 1f, false);
			}
		}
	}

	public static void CleanupCombat()
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		global::System.Collections.Generic.Dictionary<ulong, ValueTuple<AnimatedSprite2D, Callable>>.ValueCollection.Enumerator enumerator = FinishCallbacks.Values.GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				ValueTuple<AnimatedSprite2D, Callable> current = enumerator.Current;
				AnimatedSprite2D item = current.Item1;
				Callable item2 = current.Item2;
				if (GodotObject.IsInstanceValid((GodotObject)(object)item) && ((GodotObject)item).IsConnected(AnimatedSprite2D.SignalName.AnimationFinished, item2))
				{
					((GodotObject)item).Disconnect(AnimatedSprite2D.SignalName.AnimationFinished, item2);
				}
			}
		}
		finally
		{
			((global::System.IDisposable)enumerator/*cast due to .constrained prefix*/).Dispose();
		}
		FinishCallbacks.Clear();
	}

	private static void EnsureFinishCallback(AnimatedSprite2D sprite)
	{
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Expected O, but got Unknown
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		ulong instanceId = ((GodotObject)sprite).GetInstanceId();
		if (!FinishCallbacks.ContainsKey(instanceId))
		{
			Callable val = Callable.From((Action)delegate
			{
				OnAnimationFinished(sprite);
			});
			FinishCallbacks[instanceId] = new ValueTuple<AnimatedSprite2D, Callable>(sprite, val);
			((GodotObject)sprite).Connect(AnimatedSprite2D.SignalName.AnimationFinished, val, 0u);
		}
	}

	private static void OnAnimationFinished(AnimatedSprite2D sprite)
	{
		if (GodotObject.IsInstanceValid((GodotObject)(object)sprite) && !(sprite.Animation != AttackAnimation) && sprite.SpriteFrames != null && sprite.SpriteFrames.HasAnimation(IdleAnimation))
		{
			sprite.Play(IdleAnimation, 1f, false);
		}
	}
}
