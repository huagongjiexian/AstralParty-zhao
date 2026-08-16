using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Cards;
using Zhao.Forms;
using Zhao.Powers;

namespace Zhao.Cards;

/// <summary>
/// 照小姐就是我们的光！(初始卡1张)。技能牌(⚠️ 类型为默认解释,用户对"光"系卡回答为技能)。
/// 基础:1费,进入淑女形态,获得1层光。淑女形态时不可使用。
/// +:0费,2层光。++:0费,3层光。本卡不会转化为下一张卡。
/// </summary>
public sealed class LightCard : ZhaoCardModel
{
    protected override IEnumerable<DynamicVar> CanonicalVars => new DynamicVar[]
    {
        new IntVar("Light", 1m),
    };

    public LightCard() : base(1, CardType.Skill, CardRarity.Basic, TargetType.Self)
    {
    }

    public override int MaxUpgradeLevel => 2;

    protected override bool IsPlayable =>
        base.IsPlayable &&
        FormSystem.GetCurrentForm(base.Owner.Creature) != ZhaoForm.Lady;

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        var creature = base.Owner.Creature;
        await FormSystem.SwitchForm(choiceContext, creature, ZhaoForm.Lady);
        await PowerCmd.Apply<LightPower>(
            choiceContext, creature,
            base.DynamicVars["Light"].IntValue,
            creature, this);
    }

    protected override void OnUpgrade()
    {
        if (base.CurrentUpgradeLevel == 1)
        {
            // 基础1费 → +0费(++保持0费)
            base.EnergyCost.UpgradeBy(-1);
        }
        base.DynamicVars["Light"].UpgradeValueBy(1m);
    }
}
