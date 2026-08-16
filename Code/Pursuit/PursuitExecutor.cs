using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.ValueProps;

namespace Zhao.Pursuit;

/// <summary>
/// 追击:独立的伤害行为,与普通伤害在代码层区分。
/// 区分方式:ValueProp 自定义标志位 0x20(本体 ValueProp 为 [Flags],本标志位未被本体占用)。
/// 规则:不存在统一的"追击基础伤害"——每次追击的次数/伤害/是否消耗狐火,由调用方(效果)自行指定。
/// </summary>
public static class PursuitExecutor
{
    /// <summary>追击伤害标志位(0x20)。</summary>
    public const ValueProp PursuitFlag = (ValueProp)0x20;

    /// <summary>
    /// 执行追击:hitCount 次、每次 damagePerHit 伤害。
    /// 目标规则:显式 target 优先;否则随机选择一名存活敌人(⚠️ 规格未定义追击目标,此为默认实现,待确认)。
    /// 追击不自动消耗狐火——是否消耗由调用方决定(规格)。
    /// </summary>
    public static async Task Chase(
        PlayerChoiceContext choiceContext,
        Player player,
        int hitCount,
        decimal damagePerHit,
        Creature? target = null)
    {
        if (hitCount <= 0 || damagePerHit <= 0m) return;

        for (int i = 0; i < hitCount; i++)
        {
            var t = target;
            if (t == null || t.IsDead)
            {
                t = PickRandomLivingEnemy(player);
                if (t == null) return;
            }

            await CreatureCmd.Damage(
                choiceContext,
                t,
                damagePerHit,
                ValueProp.Move | PursuitFlag,
                player.Creature,
                null);
        }
    }

    private static Creature? PickRandomLivingEnemy(Player player)
    {
        var enemies = player.Creature.CombatState?.HittableEnemies;
        if (enemies == null) return null;

        var list = enemies.Where(e => !e.IsDead).ToList();
        if (list.Count == 0) return null;

        var rng = player.RunState.Rng;
        var pick = rng.CombatCardSelection.NextItem(list);
        return pick;
    }
}
