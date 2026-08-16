using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.Models;
using Zhao.FoxFire;

namespace Zhao.Cards;

/// <summary>
/// 「照」全部正式卡牌的基类。0.107.1 原版机制映射:
///  - FoxFireCost:狐火支付成本,对应原版 CanonicalStarCost(默认 0=不支付,由 ZhaoFoxFireCombatHooks 在出牌管线中支付);
///  - IsPlayable:狐火不足时卡牌不可使用,对应原版 PlayerCombatState.HasEnoughResourcesFor 的星辉闸门(UnplayableReason.StarCostTooHigh);
///  - OnTransformAfterPlay:打出结算完成(结果牌堆移动之后)再执行的转化 —— 0.107.1 没有"打出过程中自转化"先例
///    (原版 Charge/Seance 只转化其他卡):若在 OnPlay 内转化当前卡,原卡被 RemoveFromCurrentPile + RemoveFromState,
///    OnPlayWrapper 尾部的结果牌堆移动被跳过,NCard 遗留在 PlayContainer(屏幕中央)造成"出牌后悬浮"。
///    因此借助原版 Played 事件(OnPlayWrapper 末尾、结果堆移动之后触发)执行转化,与原版生命周期一致。
///  - PortraitPath/BetaPortraitPath:指向模组占位卡图(⚠️ 正式美术待用户提供),替代原版 card_atlas 缺失时的 BETA 占位。
/// </summary>
public abstract class ZhaoCardModel : CardModel
{
    /// <summary>模组卡图占位(未提供正式美术,明确占位状态)。</summary>
    private const string CardPortraitPlaceholder = "res://zhao/images/cards/zhao_card_placeholder.png";

    protected ZhaoCardModel(int energyCost, CardType type, CardRarity rarity, TargetType target)
        : base(energyCost, type, rarity, target)
    {
    }

    /// <summary>
    /// 战斗实例创建后订阅 Played 事件(转化时机钩子)。
    /// ⚠️ 不能在构造函数里订阅:本体 CardModel.DeepCloneFields 会清空克隆体的事件(Played = null),
    /// 构造函数订阅只存在于 canonical 上,真正打出的战斗克隆收不到事件。AfterCreated 在
    /// CombatState 创建战斗卡后调用(0.107.1 反编译 CombatState.CreateCard → cardModel.AfterCreated()),
    /// 此时订阅绑定的是实际打出的实例。
    /// </summary>
    public override void AfterCreated()
    {
        base.AfterCreated();
        Played += OnPlayedFinalize;
    }

    /// <summary>狐火支付成本(0=不支付)。对应原版 CanonicalStarCost(默认 -1=不参与)。</summary>
    public virtual int FoxFireCost => 0;

    /// <summary>
    /// 狐火不足时不可使用。对应原版星辉:PlayerCombatState.HasEnoughResourcesFor →
    /// UnplayableReason.StarCostTooHigh。子类覆写 IsPlayable 时必须与 base.IsPlayable 结合。
    /// </summary>
    protected override bool IsPlayable
    {
        get
        {
            if (Owner != null && FoxFireCost > 0 && FoxFireCmd.Get(Owner) < FoxFireCost)
            {
                return false;
            }
            return base.IsPlayable;
        }
    }

    /// <summary>
    /// 打出结算完成后要执行的转化任务(默认 null=不转化)。
    /// 由 Played 事件触发,此时原卡已完成原版"手牌→打出→结果牌堆"视觉生命周期。
    /// </summary>
    protected virtual Task? OnTransformAfterPlay() => null;

    private void OnPlayedFinalize()
    {
        if (CombatManager.Instance.IsOverOrEnding)
        {
            return;
        }
        TaskHelper.RunSafely(RunTransformAfterPlay());
    }

    private async Task RunTransformAfterPlay()
    {
        var task = OnTransformAfterPlay();
        if (task != null)
        {
            await task;
        }
    }

    // ---------- 卡图占位(替代原版 card_atlas 缺失时的 BETA 占位图) ----------
    public override string PortraitPath => CardPortraitPlaceholder;

    public override string BetaPortraitPath => CardPortraitPlaceholder;
}
