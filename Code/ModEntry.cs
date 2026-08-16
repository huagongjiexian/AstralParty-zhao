using HarmonyLib;
using MegaCrit.Sts2.Core.Modding;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.CardPools;
using Zhao.Cards;
using Zhao.FoxFire;

namespace Zhao;

/// <summary>
/// 模组入口。官方约定:标注 [ModInitializer] 的类,加载模组时调用 initializerMethod。
/// </summary>
[ModInitializer(nameof(Initialize))]
public static class ModEntry
{
    public const string ModId = "zhao";

    public static void Initialize()
    {
        // 段落转化专用卡(主歌/副歌)注册进原版共享 TokenCardPool。
        // 这是官方模组 API(ModHelper.AddModelToPool),与 CardPoolModel.AllCards 的
        // ModHelper.ConcatModelsFromMods 消费端配套;必须在游戏初始化(ModelDb.Preload)之前调用。
        // 原版先例:MinionDiveBomb/Soul(Charge/Seance 的转化产物,Rarity=Token)同样位于 TokenCardPool,
        // 从而拥有合法 CardModel.Pool / VisualCardPool / EnergyIcon,且永远不会作为角色普通奖励出现。
        ModHelper.AddModelToPool<TokenCardPool, SectionMain>();
        ModHelper.AddModelToPool<TokenCardPool, SectionChorus>();

        // 狐火支付钩子:官方模组战斗钩子订阅(对应 CombatState.IterateHookListeners 的 ModHelper 消费端)。
        // ZhaoFoxFireCombatHooks.BeforeCardPlayed 在原版出牌管线中完成狐火成本支付
        // (时序对应原版星辉的 SpendResources → SpendStars)。
        ModHelper.SubscribeForCombatStateHooks(
            "zhao.foxfire",
            _ => new[] { ModelDb.GetById<AbstractModel>(ModelDb.GetId(typeof(ZhaoFoxFireCombatHooks))) });

        Harmony harmony = new(ModId);
        harmony.PatchAll();
    }
}
