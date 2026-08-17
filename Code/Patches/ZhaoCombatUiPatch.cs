using System.Collections;
using System.Linq;
using Godot;
using HarmonyLib;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Context;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Nodes.Combat;
using Zhao.Character;
using Zhao.FoxFire;

namespace Zhao.Patches;

[HarmonyPatch(typeof(NCombatUi), "Activate")]
public static class ZhaoCombatUiPatch
{
	private static void Postfix(NCombatUi __instance, CombatState state)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Expected O, but got Unknown
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Expected O, but got Unknown
		Player me = LocalContext.GetMe((ICombatState)state);
		if (((me != null) ? me.Character : null) is ZhaoCharacter)
		{
			Control energyCounterContainer = __instance.EnergyCounterContainer;
			NEnergyCounter val = ((energyCounterContainer != null) ? Enumerable.FirstOrDefault<NEnergyCounter>(Enumerable.OfType<NEnergyCounter>((global::System.Collections.IEnumerable)((Node)energyCounterContainer).GetChildren(false))) : null);
			if (val != null && !Enumerable.Any<NFoxFireCounter>(Enumerable.OfType<NFoxFireCounter>((global::System.Collections.IEnumerable)((Node)val).GetChildren(false))))
			{
				NFoxFireCounter nFoxFireCounter = NFoxFireCounter.Create(me);
				((Node)val).AddChild((Node)nFoxFireCounter, false, InternalMode.Disabled);
			}
		}
	}
}
