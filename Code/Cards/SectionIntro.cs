using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using Zhao.Forms;

namespace Zhao.Cards;

/// <summary>
/// セクション(段落)·イントロ(前奏)(初始卡1张)。技能牌(⚠️ 类型为默认解释)。
/// 基础:1费,歌姬形态时不可使用。使用:进入歌姬形态,获得段落·前奏(抽1张牌),该卡转化为主歌。
/// 强化:前奏 1费→主歌;前奏+ 0费→主歌+;前奏++ 0费→主歌++。
/// 转化时机(0.0.6 修复):不在 OnPlay 内转化"正在打出的这张卡"——那会跳过原版结果牌堆移动,
/// 使 NCard 遗留在 PlayContainer 屏幕中央。改为经 ZhaoCardModel.OnTransformAfterPlay,
/// 在本体 Played 事件(结果堆移动之后)触发,与原版出牌视觉生命周期一致。
/// </summary>
public sealed class SectionIntro : ZhaoCardModel
{
    public SectionIntro() : base(1, CardType.Skill, CardRarity.Basic, TargetType.Self)
    {
    }

    public override int MaxUpgradeLevel => 2;

    protected override bool IsPlayable =>
        base.IsPlayable &&
        FormSystem.GetCurrentForm(base.Owner.Creature) != ZhaoForm.Diva;

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        var creature = base.Owner.Creature;

        // 进入歌姬形态
        await FormSystem.SwitchForm(choiceContext, creature, ZhaoForm.Diva);

        // 获得段落·前奏
        await FormSystem.SetStage(choiceContext, creature, SectionStage.Intro);

        // 获得前奏时:抽1张牌(前奏Buff)
        await CardPileCmd.Draw(choiceContext, 1, base.Owner);
    }

    protected override Task? OnTransformAfterPlay() => TransformHelper.TransformInto<SectionMain>(this);

    protected override void OnUpgrade()
    {
        if (base.CurrentUpgradeLevel == 1)
        {
            // 基础1费 → +0费(++保持0费)
            base.EnergyCost.UpgradeBy(-1);
        }
    }
}
