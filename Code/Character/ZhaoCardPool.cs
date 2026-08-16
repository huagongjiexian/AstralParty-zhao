using Godot;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Cards;
using Zhao.Cards;

namespace Zhao.Character;

/// <summary>
/// 「照」的卡池。
/// 原版规则(0.107.1):每个 CardModel 都必须能通过 CardModel.Pool 找到归属池,
/// 否则战斗抽牌 NCard.Create → Reload → EnergyIcon → VisualCardPool → Pool 会一直查不到,
/// 最终落入 MockCardPool 检查并抛出 InvalidOperationException("You monster!")。
/// 初始卡组卡同样必须入池 —— 参考原版:StrikeIronclad/DefendIronclad 位于 IroncladCardPool,
/// 其 Rarity=Basic,由奖励系统(CardFactory.CreateForReward)天然排除出奖励。
/// 本池内容:
///  - 初始卡组 4 张(Basic):狐火打击 / 照小姐就是我们的光 / 前奏 / 紧急治疗;
///  - 可获取奖励卡:追击追击(普通)、快进(罕见)、尾声(稀有)、光よ！(稀有)。
/// 段落转化牌(主歌/副歌)不在此池 —— 按原版 MinionDiveBomb/Soul 的先例,
/// 它们是 Rarity=Token 的转化专用卡,通过 ModHelper.AddModelToPool 注册进原版共享 TokenCardPool。
/// </summary>
public class ZhaoCardPool : CardPoolModel
{
    public override string Title => "zhao";
    public override string EnergyColorName => "ironclad";              // ⚠️ 占位
    public override string CardFrameMaterialPath => "card_frame_red";  // ⚠️ 占位(铁甲红框)
    public override Color DeckEntryCardColor => new("FFB300");
    public override bool IsColorless => false;

    protected override CardModel[] GenerateAllCards() => new CardModel[]
    {
        // 初始卡组卡(Basic,奖励系统不发放;参考原版 Strike/Defend 入 IroncladCardPool)
        ModelDb.Card<KitsuneFireStrike>(),
        ModelDb.Card<LightCard>(),
        ModelDb.Card<SectionIntro>(),
        ModelDb.Card<EmergencyTreatment>(),
        // 可获取奖励卡
        ModelDb.Card<ChaseChase>(),
        ModelDb.Card<FastForward>(),
        ModelDb.Card<OutroCard>(),
        ModelDb.Card<LightYo>(),
    };
}
