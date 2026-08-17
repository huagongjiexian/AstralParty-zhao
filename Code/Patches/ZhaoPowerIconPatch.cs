using System.Collections.Generic;
using System.Reflection;
using HarmonyLib;
using MegaCrit.Sts2.Core.Models;

namespace Zhao.Patches;

/// <summary>
/// Power 图标兼容补丁。
///
/// 0.0.17.3 起不再覆盖 PowerModel.PackedIconPath。
/// 所有照模组 Power 按原版 PowerModel 规则从
/// res://images/atlases/power_atlas.sprites/{power_id}.tres 读取小图标。
/// LightPower 对应 light_power.tres（照小姐就是我的光！专属图标）；
/// 其他自定义 Power 的标准路径均提供占位图资源。
/// </summary>
[HarmonyPatch]
public static class ZhaoPowerIconPatch
{
    private static IEnumerable<MethodBase> TargetMethods()
    {
        yield return AccessTools.PropertyGetter(typeof(PowerModel), nameof(PowerModel.PackedIconPath));
    }

    /// <summary>
    /// 返回 true，完全放行原版 PackedIconPath getter。
    /// 保留该补丁壳仅用于和旧存档/旧程序集结构兼容。
    /// </summary>
    private static bool Prefix(PowerModel __instance, ref string __result) => true;
}
