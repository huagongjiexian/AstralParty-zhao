using System;
using System.Collections.Generic;
using System.Linq;
using HarmonyLib;
using MegaCrit.Sts2.Core.Models;
using Zhao.Character;

namespace Zhao.Patches;

[HarmonyPatch(/*Could not decode attribute arguments.*/)]
public static class CharacterRegistryPatch
{
	private static void Postfix(ref global::System.Collections.Generic.IEnumerable<CharacterModel> __result)
	{
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Expected O, but got Unknown
		if (!Enumerable.Any<CharacterModel>(__result, (Func<CharacterModel, bool>)((CharacterModel c) => c is ZhaoCharacter)))
		{
			__result = Enumerable.Append<CharacterModel>(__result, (CharacterModel)ModelDb.Character<ZhaoCharacter>());
		}
	}
}
