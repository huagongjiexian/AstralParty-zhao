using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using HarmonyLib;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Modding;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.CardPools;
using Zhao.Cards;
using Zhao.FoxFire;

namespace Zhao;

[ModInitializer("Initialize")]
public static class ModEntry
{
	[Serializable]
	[CompilerGenerated]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9 = new _003C_003Ec();

		public static CombatHookSubscriptionDelegate _003C_003E9__1_0;

		internal global::System.Collections.Generic.IEnumerable<AbstractModel> _003CInitialize_003Eb__1_0(CombatState _)
		{
			return (global::System.Collections.Generic.IEnumerable<AbstractModel>)(object)new AbstractModel[1] { ModelDb.GetById<AbstractModel>(ModelDb.GetId(typeof(ZhaoFoxFireCombatHooks))) };
		}
	}

	[Serializable]
	[CompilerGenerated]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9 = new _003C_003Ec();

		public static CombatHookSubscriptionDelegate _003C_003E9__2_0;

		internal global::System.Collections.Generic.IEnumerable<AbstractModel> _003CInitialize_003Eb__2_0(CombatState _)
		{
			return (global::System.Collections.Generic.IEnumerable<AbstractModel>)(object)new AbstractModel[1] { ModelDb.GetById<AbstractModel>(ModelDb.GetId(typeof(ZhaoFoxFireCombatHooks))) };
		}
	}

	public const string ModId = "zhao";

	public static void Initialize()
	{
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Expected O, but got Unknown
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Expected O, but got Unknown
		ModHelper.AddModelToPool<TokenCardPool, SectionMain>();
		ModHelper.AddModelToPool<TokenCardPool, SectionChorus>();
		object obj = _003C_003Ec._003C_003E9__1_0;
		if (obj == null)
		{
			object obj2 = _003C_003Ec._003C_003E9__2_0;
			if (obj2 == null)
			{
				CombatHookSubscriptionDelegate val = (CombatState _) => (global::System.Collections.Generic.IEnumerable<AbstractModel>)(object)new AbstractModel[1] { ModelDb.GetById<AbstractModel>(ModelDb.GetId(typeof(ZhaoFoxFireCombatHooks))) };
				_003C_003Ec._003C_003E9__2_0 = val;
				obj2 = (object)val;
			}
			_003C_003Ec._003C_003E9__1_0 = (CombatHookSubscriptionDelegate)obj2;
			obj = obj2;
		}
		ModHelper.SubscribeForCombatStateHooks("zhao.foxfire", (CombatHookSubscriptionDelegate)obj);
		new Harmony("zhao").PatchAll();
	}
}
