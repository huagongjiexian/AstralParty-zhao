using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Cards;
using Zhao.Forms;
using Zhao.Powers;

namespace Zhao.Cards;

/// <summary>
/// 紧急治疗(初始卡1张)。用户决定:技能牌。
/// 0费,小护士形态时不可使用。使用:进入小护士形态,获得1层治愈,回复5点生命。
/// +/++:规格未定义 → 暂无效果(TODO 等用户给数值)。
/// </summary>
public sealed class EmergencyTreatment : ZhaoCardModel
{
    public EmergencyTreatment() : base(0, CardType.Skill, CardRarity.Basic, TargetType.Self)
    {
    }

    protected override bool IsPlayable =>
        base.IsPlayable &&
        FormSystem.GetCurrentForm(base.Owner.Creature) != ZhaoForm.Nurse;

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        var creature = base.Owner.Creature;

        await FormSystem.SwitchForm(choiceContext, creature, ZhaoForm.Nurse);
        await PowerCmd.Apply<HealingPower>(choiceContext, creature, 1m, creature, this);
        await CreatureCmd.Heal(creature, 5m);
    }

    // TODO: 紧急治疗+/++ 效果 —— 用户未给数值,不得自行补。
    protected override void OnUpgrade()
    {
    }
}
