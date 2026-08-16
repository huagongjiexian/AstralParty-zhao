using System.Linq;
using HarmonyLib;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Context;
using MegaCrit.Sts2.Core.Nodes.Combat;
using Zhao.Character;
using Zhao.FoxFire;

namespace Zhao.Patches;

/// <summary>
/// 狐火计数器 UI 挂载:0.107.1 星辉计数器同架构(参考 NCombatUi.Activate 内的
/// _starCounter.Initialize(me) → Reparent 到能量计数器)。
/// 在 Activate 完成后为「照」创建 NFoxFireCounter 并挂到能量计数器上(与星辉计数器同位置、同层级)。
/// </summary>
[HarmonyPatch(typeof(NCombatUi), nameof(NCombatUi.Activate))]
public static class ZhaoCombatUiPatch
{
    private static void Postfix(NCombatUi __instance, CombatState state)
    {
        var me = LocalContext.GetMe(state);
        if (me?.Character is not ZhaoCharacter)
        {
            return;
        }
        var container = __instance.EnergyCounterContainer;
        var energyCounter = container?.GetChildren().OfType<NEnergyCounter>().FirstOrDefault();
        if (energyCounter == null)
        {
            return;
        }
        // 防重复创建:NCombatUi.Activate 可能多次执行(重复进入战斗 UI),已有计数器则跳过
        if (energyCounter.GetChildren().OfType<NFoxFireCounter>().Any())
        {
            return;
        }
        var counter = NFoxFireCounter.Create(me);
        energyCounter.AddChild(counter);
    }
}
