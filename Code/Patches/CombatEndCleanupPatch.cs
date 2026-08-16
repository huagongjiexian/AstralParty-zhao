using HarmonyLib;
using MegaCrit.Sts2.Core.Hooks;
using Zhao.Forms;
using Zhao.FoxFire;

namespace Zhao.Patches;

/// <summary>
/// 战斗结束钩子:
///  - 清理歌姬视频背景的静态引用(节点本身随战斗房间销毁,不产生重复播放器);
///  - 清空狐火银行(0.107.1 星辉同架构:星辉随 PlayerCombatState 每场新建而清零,狐火以整场清理等价实现)。
/// </summary>
[HarmonyPatch(typeof(Hook), nameof(Hook.AfterCombatEnd))]
public static class CombatEndCleanupPatch
{
    private static void Postfix()
    {
        DivaVideoBackground.CleanupCombat();
        FoxFireBank.ClearCombat();
        ZhaoCombatAnimation.CleanupCombat();
    }
}
