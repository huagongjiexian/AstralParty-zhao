using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Cards;

namespace Zhao.Powers;

/// <summary>
/// 解放的临时强化执行器:通过本体公开升级流程(UpgradeInternal/DowngradeInternal/FinalizeUpgradeInternal)实现可逆强化,
/// 不破坏永久升级(规格:基础→临时+→恢复基础;永久+→临时++→恢复永久+)。
/// 注意:这些方法在本体 CardModel 中是 public(已从反编译源码核实),直接调用获得编译期检查。
/// </summary>
public static class TemporaryUpgrade
{
    /// <summary>临时提升一级(升级 + 清预览态,等效 CardCmd.Upgrade 的无预览路径)。</summary>
    public static void ApplyOneLevel(CardModel card)
    {
        card.UpgradeInternal();
        card.FinalizeUpgradeInternal();
    }

    /// <summary>回退到原等级:先 DowngradeInternal(归零重建),再按原等级重新升级。</summary>
    public static void RevertToLevel(CardModel card, int originalLevel)
    {
        card.DowngradeInternal();
        for (int i = 0; i < originalLevel; i++)
        {
            card.UpgradeInternal();
            card.FinalizeUpgradeInternal();
        }
    }
}
