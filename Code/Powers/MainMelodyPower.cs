using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;

namespace Zhao.Powers;

/// <summary>
/// 主歌+ Buff:直到下一回合开始前,自身造成的伤害+1、自身受到的伤害-1(仅属于主歌+/++)。
/// 持续时间:施加于打出主歌+的回合,下一次玩家侧回合开始时移除。
/// </summary>
public class MainMelodyPower : PowerModel
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Single;

    public override decimal ModifyDamageAdditive(Creature? target, decimal amount, ValueProp props, Creature? dealer, CardModel? cardSource)
    {
        if (!props.IsPoweredAttack())
            return 0m;

        if (base.Owner == dealer)
            return 1m;   // 自身造成的伤害+1

        if (base.Owner == target)
            return -1m;  // 自身受到的伤害-1

        return 0m;
    }

    public override async Task AfterSideTurnStart(CombatSide side, IReadOnlyList<Creature> participants, ICombatState state)
    {
        if (side != CombatSide.Player || !participants.Contains(base.Owner))
            return;
        await PowerCmd.Remove(this);
    }
}

/// <summary>
/// 小护士→主歌:此后每使用1张技能牌获得1层治愈。
/// ⚠️ 持续时间规格未确定,默认本场战斗(普通 Power 生命周期)。
/// </summary>
public class NurseMainHealingPower : PowerModel
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Single;
    public override int DisplayAmount => 0;

    public override async Task AfterCardPlayed(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        if (cardPlay.Card == null || cardPlay.Card.Type != CardType.Skill)
            return;
        if (cardPlay.Card.Owner?.Creature != base.Owner)
            return;

        await PowerCmd.Apply<HealingPower>(choiceContext, base.Owner, 1m, base.Owner, cardPlay.Card);
    }
}
