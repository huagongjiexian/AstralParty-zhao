using System.Runtime.CompilerServices;
using MegaCrit.Sts2.Core.Entities.Players;

namespace Zhao.FoxFire;

public static class FoxFireBank
{
	private static readonly ConditionalWeakTable<Player, FoxFireResource> _states = new ConditionalWeakTable<Player, FoxFireResource>();

	public static FoxFireResource For(Player player)
	{
		return _states.GetValue(player, (CreateValueCallback<Player, FoxFireResource>)((Player _) => new FoxFireResource()));
	}

	public static int Get(Player player)
	{
		return For(player).Amount;
	}

	public static void ClearCombat()
	{
		_states.Clear();
	}
}
