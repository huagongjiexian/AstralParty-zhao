using HarmonyLib;
using MegaCrit.Sts2.Core.Models;
using Zhao.Character;

namespace Zhao.Patches;

/// <summary>
/// 本体 ModelDb.AllCharacters 是硬编码数组(不扫描模组程序集)。
/// 此处用 Harmony 后置补丁把角色「照」注入返回结果。
/// </summary>
[HarmonyPatch(typeof(ModelDb), nameof(ModelDb.AllCharacters), MethodType.Getter)]
public static class CharacterRegistryPatch
{
    private static void Postfix(ref IEnumerable<CharacterModel> __result)
    {
        if (__result.Any(c => c is ZhaoCharacter))
            return;

        __result = __result.Append(ModelDb.Character<ZhaoCharacter>());
    }
}
