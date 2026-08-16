using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.Models.Powers;
using Zhao.Forms;
using Zhao.Powers;

namespace Zhao.Cards;

/// <summary>
/// 快进。技能卡,罕见,2费。卡牌专属属性:消耗(原生 CardKeyword.Exhaust)。
/// 普通效果:自身获得2层易伤、2层虚弱;
///  若已拥有段落 → 使当前段落进入下一阶段;
///  若无段落 → 获得段落(⚠️ 用户决定"获得段落";起点=前奏,此为默认解释,待确认)。
/// 快进+:额外获得卡牌专属效果"重放"(原生 BaseReplayCount)。
/// 快进++:规格未定义 → TODO。
/// </summary>
public sealed class FastForward : ZhaoCardModel
{
    public override IEnumerable<CardKeyword> CanonicalKeywords =>
        new[] { CardKeyword.Exhaust };

    public FastForward() : base(2, CardType.Skill, CardRarity.Uncommon, TargetType.Self)
    {
    }

    public override int MaxUpgradeLevel => 2;

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        var creature = base.Owner.Creature;

        // 自身获得2层易伤、2层虚弱
        await PowerCmd.Apply<VulnerablePower>(choiceContext, creature, 2m, creature, this);
        await PowerCmd.Apply<WeakPower>(choiceContext, creature, 2m, creature, this);

        // 已有段落 → 下一阶段;无段落 → 获得段落(默认从序列起点=前奏,⚠️ 待确认)
        var section = FormSystem.GetSection(creature);
        if (section != null)
        {
            var next = (SectionStage)Math.Min((int)section.Stage + 1, (int)SectionStage.Outro);
            await FormSystem.SetStage(choiceContext, creature, next);
        }
        else
        {
            await FormSystem.SetStage(choiceContext, creature, SectionStage.Intro);
        }
    }

    protected override void OnUpgrade()
    {
        if (base.CurrentUpgradeLevel == 1)
        {
            // 快进+:额外获得"重放"(原生重放计数,UI 自动渲染)
            base.BaseReplayCount += 1;
        }
        // TODO: 快进++ 效果 —— 用户未给,不得自行补。
    }
}
