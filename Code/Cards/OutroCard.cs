using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Cards;
using Zhao.Forms;

namespace Zhao.Cards;

/// <summary>
/// 尾声(セクション·アウトロ 的触发卡)。技能卡,稀有,3费,仅在间奏期间可以使用。
/// 卡牌本身只使间奏进入尾声;资源结算属于"进入尾声阶段时的效果"(见 SectionPower.OutroSettlement)。
/// 尾声++:规格未确认新数值,不得自行提高 → 数值与尾声+相同(见结算实现)。
/// </summary>
public sealed class OutroCard : ZhaoCardModel
{
    public OutroCard() : base(3, CardType.Skill, CardRarity.Rare, TargetType.Self)
    {
    }

    public override int MaxUpgradeLevel => 2;

    protected override bool IsPlayable =>
        base.IsPlayable &&
        FormSystem.GetSection(base.Owner.Creature)?.Stage == SectionStage.Interlude;

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        var creature = base.Owner.Creature;

        // 记录本卡的升级等级(结算数值用),然后使间奏进入尾声;
        // 资源结算由 SectionPower 检测到进入尾声时执行
        var section = FormSystem.GetSection(creature);
        if (section != null)
        {
            section.OutroLevel = base.CurrentUpgradeLevel;
        }
        await FormSystem.SetStage(choiceContext, creature, SectionStage.Outro);
    }

    protected override void OnUpgrade()
    {
        // TODO: 尾声+ 的数值提升在结算逻辑中体现(追击次数+1/4、每次7、光/治愈+1/4);
        //       尾声++ 规格未确认新数值 → 与 + 相同。
    }
}
