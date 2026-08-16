using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Models;
using Zhao.Cards;

namespace Zhao.FoxFire;

/// <summary>
/// 狐火支付钩子模型:挂接原版出牌管线完成狐火成本支付。
/// 0.107.1 星辉同架构:星辉支付发生在 CardModel.SpendResources(由 PlayCardAction 在 OnPlayWrapper 之前调用)。
/// 模组无法扩展本体支付管线,故经官方模组钩子订阅(ModHelper.SubscribeForCombatStateHooks)注册本模型,
/// 在 Hook.BeforeCardPlayed(OnPlayWrapper 内、卡牌效果 OnPlay 之前、普通能量已支付之后)扣取狐火 —— 时序与星辉等价。
/// 可支付性由 ZhaoCardModel.IsPlayable 检查(对应 PlayerCombatState.HasEnoughResourcesFor 的星辉闸门)。
/// </summary>
public sealed class ZhaoFoxFireCombatHooks : AbstractModel
{
    public override bool ShouldReceiveCombatHooks => true;

    public override async Task BeforeCardPlayed(CardPlay cardPlay)
    {
        // 重放只支付一次(原版 SpendResources 也只执行一次)
        if (!cardPlay.IsFirstInSeries)
        {
            return;
        }
        if (cardPlay.Card is not ZhaoCardModel zhaoCard || zhaoCard.FoxFireCost <= 0)
        {
            return;
        }
        var owner = zhaoCard.Owner;
        if (owner == null)
        {
            return;
        }
        await FoxFireCmd.Spend(zhaoCard.FoxFireCost, owner);
    }
}
