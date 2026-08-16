using Godot;
using HarmonyLib;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Models;
using Zhao.Character;

namespace Zhao.Patches;

/// <summary>
/// 巫女动作只由「照」的攻击牌触发。
/// 本体的 AttackCommand 也会被技能牌用于造成伤害，不能直接把所有 Attack 请求都映射成巫女攻击动画。
/// </summary>
[HarmonyPatch(typeof(CardModel), nameof(CardModel.OnPlayWrapper))]
public static class ZhaoCardAnimationPatch
{
    private static void Prefix(CardModel __instance)
    {
        var owner = __instance.Owner;
        if (__instance.Type != CardType.Attack || owner.Character is not ZhaoCharacter)
        {
            return;
        }

        ZhaoCombatAnimation.PlayAttack(owner.Creature);
    }
}

internal static class ZhaoCombatAnimation
{
    private static readonly StringName AttackAnimation = "Attack";
    private static readonly StringName IdleAnimation = "Idle";
    private static readonly Dictionary<ulong, (AnimatedSprite2D Sprite, Callable Callback)> FinishCallbacks = new();

    public static void PlayAttack(MegaCrit.Sts2.Core.Entities.Creatures.Creature creature)
    {
        var creatureNode = MegaCrit.Sts2.Core.Nodes.Rooms.NCombatRoom.Instance?.GetCreatureNode(creature);
        if (creatureNode?.Visuals.GetCurrentBody() is not AnimatedSprite2D sprite ||
            !GodotObject.IsInstanceValid(sprite))
        {
            return;
        }

        EnsureFinishCallback(sprite);
        sprite.Stop();
        sprite.Animation = AttackAnimation;
        sprite.Frame = 0;
        sprite.Play();
    }

    public static void CleanupCombat()
    {
        foreach (var (sprite, callback) in FinishCallbacks.Values)
        {
            if (GodotObject.IsInstanceValid(sprite) &&
                sprite.IsConnected(AnimatedSprite2D.SignalName.AnimationFinished, callback))
            {
                sprite.Disconnect(AnimatedSprite2D.SignalName.AnimationFinished, callback);
            }
        }
        FinishCallbacks.Clear();
    }

    private static void EnsureFinishCallback(AnimatedSprite2D sprite)
    {
        ulong id = sprite.GetInstanceId();
        if (FinishCallbacks.ContainsKey(id))
        {
            return;
        }

        var callback = Callable.From(() => OnAnimationFinished(sprite));
        FinishCallbacks[id] = (sprite, callback);
        sprite.Connect(AnimatedSprite2D.SignalName.AnimationFinished, callback);
    }

    private static void OnAnimationFinished(AnimatedSprite2D sprite)
    {
        if (GodotObject.IsInstanceValid(sprite) && sprite.Animation == AttackAnimation)
        {
            sprite.Play(IdleAnimation);
        }
    }
}
