using HarmonyLib;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Nodes.Combat;
using Zhao.Character;

namespace Zhao.Patches;

[HarmonyPatch(typeof(NCreature), "SetAnimationTrigger")]
public static class ZhaoCardAnimationPatch
{
	private static void Postfix(NCreature __instance, string trigger)
	{
		if (!(trigger != "Attack"))
		{
			Creature entity = __instance.Entity;
			object obj;
			if (entity == null)
			{
				obj = null;
			}
			else
			{
				Player player = entity.Player;
				obj = ((player != null) ? player.Character : null);
			}
			if (obj is ZhaoCharacter)
			{
				ZhaoCombatAnimation.PlayAttack(__instance);
			}
		}
	}
}
