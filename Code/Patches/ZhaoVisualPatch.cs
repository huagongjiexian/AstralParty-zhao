using System.Reflection;
using HarmonyLib;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.Models;
using Zhao.Character;

namespace Zhao.Patches;

/// <summary>
/// 「照」的占位视觉:本体下列属性不是 virtual,无法在子类覆写,
/// 用 Harmony 补丁把 getter 结果替换为铁甲战士资源(正式美术待动作确认后替换)。
/// </summary>
[HarmonyPatch]
public static class ZhaoVisualPatch
{
    private static readonly Dictionary<string, string> PathOverrides = new()
    {
        // 正式视觉场景(四形态 AnimatedSprite2D 资源;战斗中随形态切换视觉=待办)
        ["get_VisualsPath"] = SceneHelper.GetScenePath("creature_visuals/zhao"),
        ["get_TrailPath"] = SceneHelper.GetScenePath("vfx/card_trail_ironclad"),
        // 铁甲战士图片占位已移除:改用模组自带占位图(正式美术待用户提供)
        ["get_IconTexturePath"] = "res://zhao/images/ui/zhao_character_placeholder.png",
        ["get_IconOutlineTexturePath"] = "res://zhao/images/ui/zhao_character_placeholder.png",
        ["get_EnergyCounterPath"] = SceneHelper.GetScenePath("combat/energy_counters/ironclad_energy_counter"),
        ["get_RestSiteAnimPath"] = SceneHelper.GetScenePath("rest_site/characters/ironclad_rest_site"),
        ["get_MerchantAnimPath"] = SceneHelper.GetScenePath("merchant/characters/ironclad_merchant"),
        ["get_CharacterSelectBg"] = SceneHelper.GetScenePath("screens/char_select/char_select_bg_zhao"),
        ["get_CharacterSelectTransitionPath"] = "res://materials/transitions/ironclad_transition_mat.tres",
        ["get_AttackSfx"] = "event:/sfx/characters/ironclad/ironclad_attack",
        ["get_CastSfx"] = "event:/sfx/characters/ironclad/ironclad_cast",
        ["get_DeathSfx"] = "event:/sfx/characters/ironclad/ironclad_die",
        ["get_ArmPointingTexturePath"] = "res://zhao/images/ui/zhao_character_placeholder.png",
        ["get_ArmRockTexturePath"] = "res://zhao/images/ui/zhao_character_placeholder.png",
        ["get_ArmPaperTexturePath"] = "res://zhao/images/ui/zhao_character_placeholder.png",
        ["get_ArmScissorsTexturePath"] = "res://zhao/images/ui/zhao_character_placeholder.png",
    };

    private static IEnumerable<MethodBase> TargetMethods()
    {
        foreach (var name in PathOverrides.Keys)
        {
            var method = AccessTools.PropertyGetter(typeof(CharacterModel), name.Substring(4));
            if (method != null)
            {
                yield return method;
            }
        }
    }

    private static bool Prefix(CharacterModel __instance, MethodInfo __originalMethod, ref string? __result)
    {
        if (__instance is not ZhaoCharacter)
            return true;

        if (PathOverrides.TryGetValue(__originalMethod.Name, out var path))
        {
            __result = path;
            return false;
        }
        return true;
    }
}
