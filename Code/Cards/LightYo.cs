using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.ValueProps;
using Zhao.Forms;
using Zhao.Powers;

namespace Zhao.Cards;

/// <summary>
/// 光よ！稀有,3费。用户决定:技能牌。仅淑女形态可以使用。
/// 消耗所有光,造成(消耗光数量 × 2)的伤害。
/// ⚠️ 目标未定义:默认可指定敌人(TargetType.AnyEnemy),伤害打所选目标。
/// +/++:规格未定义 → 暂无效果(TODO)。
/// </summary>
public sealed class LightYo : ZhaoCardModel
{
    public LightYo() : base(3, CardType.Skill, CardRarity.Rare, TargetType.AnyEnemy)
    {
    }

    protected override bool IsPlayable =>
        base.IsPlayable &&
        FormSystem.GetCurrentForm(base.Owner.Creature) == ZhaoForm.Lady;

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
        var creature = base.Owner.Creature;

        // 消耗所有光
        int light = creature.GetPowerAmount<LightPower>();
        if (light > 0)
        {
            await PowerCmd.Remove<LightPower>(creature);
        }

        // 伤害 = 消耗光数量 × 2
        if (light > 0)
        {
            await CreatureCmd.Damage(choiceContext, cardPlay.Target, light * 2m, ValueProp.Move, creature, this);
        }
    }

    // TODO: 光よ!+/++ 效果 —— 用户未给数值,不得自行补。
    protected override void OnUpgrade()
    {
    }
}
