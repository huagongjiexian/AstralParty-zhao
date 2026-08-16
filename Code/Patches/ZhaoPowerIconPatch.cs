using System.Reflection;
using HarmonyLib;
using MegaCrit.Sts2.Core.Models;

namespace Zhao.Patches;

/// <summary>
/// Zhao Power 图标占位:PackedIconPath 在原版是非 virtual 属性(默认指向 power_atlas.sprites/{entry}.tres),
/// 模组 Power 无法覆写,导致本体图集缺失并回退 BETA 占位图。
/// 此补丁把 Zhao.Powers 命名空间下所有 Power 的图标指向模组自带占位图(⚠️ 明确占位状态,正式美术待用户提供)。
/// </summary>
[HarmonyPatch]
public static class ZhaoPowerIconPatch
{
    private const string PlaceholderPath = "res://zhao/images/powers/zhao_power_placeholder.png";

    private static IEnumerable<MethodBase> TargetMethods()
    {
        yield return AccessTools.PropertyGetter(typeof(PowerModel), nameof(PowerModel.PackedIconPath));
    }

    private static bool Prefix(PowerModel __instance, ref string __result)
    {
        if (__instance.GetType().Namespace == "Zhao.Powers")
        {
            __result = PlaceholderPath;
            return false;
        }
        return true;
    }
}
