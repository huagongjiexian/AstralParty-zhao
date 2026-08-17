using System;
using System.Collections.Generic;
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
	public const string ModId = "zhao";

	private static readonly CombatHookSubscriptionDelegate FoxFireCombatHooks =
		(CombatState _) => new AbstractModel[]
		{
			ModelDb.GetById<AbstractModel>(ModelDb.GetId(typeof(ZhaoFoxFireCombatHooks)))
		};

	public static void Initialize()
	{
		ModHelper.AddModelToPool<TokenCardPool, SectionMain>();
		ModHelper.AddModelToPool<TokenCardPool, SectionChorus>();
		ModHelper.SubscribeForCombatStateHooks("zhao.foxfire", FoxFireCombatHooks);

		// 容错:补丁目标解析失败(游戏更新/模组冲突)时仅记录错误,不让模组初始化直接抛异常
		try
		{
			new Harmony("zhao").PatchAll();
		}
		catch (Exception ex)
		{
			Godot.GD.PrintErr("zhao mod: Harmony PatchAll failed: " + ex);
		}
	}
}
