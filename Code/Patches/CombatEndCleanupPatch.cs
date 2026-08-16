using HarmonyLib;
using MegaCrit.Sts2.Core.Hooks;
using Zhao.Forms;
using Zhao.FoxFire;

namespace Zhao.Patches;

[HarmonyPatch(typeof(Hook), "AfterCombatEnd")]
public static class CombatEndCleanupPatch
{
	private static void Postfix()
	{
		DivaVideoBackground.CleanupCombat();
		FoxFireBank.ClearCombat();
		ZhaoCombatAnimation.CleanupCombat();
	}
}
