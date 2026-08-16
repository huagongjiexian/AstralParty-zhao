using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Cards;
using Zhao.Forms;
using Zhao.Powers;

namespace Zhao.Cards;

/// <summary>
/// セクション(段落)·サビ(副歌)。由主歌卡转化而来。技能牌(⚠️ 默认解释)。
/// 基础:1费;副歌+/副歌++:0费。
/// 使用:进入副歌,获得解放;然后副歌卡从本场战斗移除(不进入弃牌堆/消耗堆,不算消耗,不触发消耗相关效果)。
/// 战斗结束以后依旧正常属于牌组(战斗卡为牌库克隆,本场移除天然不影响牌库)。
/// Rarity=Token(转化专用,不进普通奖励池):按原版 MinionDiveBomb/Soul 先例,
/// 由 ModHelper.AddModelToPool 注册进共享 TokenCardPool。
/// </summary>
public sealed class SectionChorus : ZhaoCardModel
{
    public SectionChorus() : base(1, CardType.Skill, CardRarity.Token, TargetType.Self)
    {
    }

    public override int MaxUpgradeLevel => 2;

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        var creature = base.Owner.Creature;

        // 进入副歌
        await FormSystem.SetStage(choiceContext, creature, SectionStage.Chorus);

        // 获得解放(副歌结束时消失 —— SectionPower 在进入间奏时移除)
        await PowerCmd.Apply<LiberationPower>(choiceContext, creature, 1m, creature, this);
    }

    /// <summary>副歌卡:本场战斗移除(不是消耗)。</summary>
    protected override PileType GetResultPileTypeForCardPlay() => PileType.None;

    protected override void OnUpgrade()
    {
        if (base.CurrentUpgradeLevel == 1)
        {
            // 基础1费 → +0费(++保持0费)
            base.EnergyCost.UpgradeBy(-1);
        }
    }
}
