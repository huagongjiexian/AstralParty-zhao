using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.Nodes.CommonUi;

namespace Zhao.Cards;

/// <summary>
/// 段落卡转化助手:用本体 CardCmd.TransformTo 把当前卡替换为目标卡(同堆替换、同位置),
/// 并继承升级等级(基础→基础 / +→+ / ++→++)。
/// 战斗内转化使用 CardPreviewStyle.None(不弹转化预览 UI)。
/// ⚠️ 生命周期约束(0.0.6):禁止在 OnPlay 内转化"正在打出的这张卡"——那会跳过原版结果牌堆移动,
/// 导致 NCard 遗留在 PlayContainer(屏幕中央)。请一律经 ZhaoCardModel.OnTransformAfterPlay
/// (Played 事件 → 结果堆移动之后)调用本助手。
/// </summary>
public static class TransformHelper
{
    public static async Task TransformInto<T>(CardModel original) where T : CardModel
    {
        var result = await CardCmd.TransformTo<T>(original, CardPreviewStyle.None);
        if (result is not { success: true } r || r.cardAdded == null)
        {
            return;
        }

        // 继承升级等级(基础→基础 / +→+ / ++→++):
        // 使用本体公开升级命令 CardCmd.Upgrade(原版 Charge 转化后升级的先例,CardPreviewStyle.None 不弹预览),
        // 保证走完整公开流程(UpgradeInternal + FinalizeUpgradeInternal),不再借用解放的可逆升级执行器。
        for (int i = 0; i < original.CurrentUpgradeLevel; i++)
        {
            CardCmd.Upgrade(r.cardAdded, CardPreviewStyle.None);
        }
    }
}
