using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Entities.Players;

namespace Zhao.FoxFire;

public static class FoxFireCmd
{
	public static global::System.Threading.Tasks.Task Gain(int amount, Player player)
	{
		FoxFireBank.For(player).Gain(amount);
		return global::System.Threading.Tasks.Task.CompletedTask;
	}

	public static global::System.Threading.Tasks.Task Lose(int amount, Player player)
	{
		FoxFireBank.For(player).Lose(amount);
		return global::System.Threading.Tasks.Task.CompletedTask;
	}

	public static global::System.Threading.Tasks.Task Spend(int amount, Player player)
	{
		return Lose(amount, player);
	}

	public static int Get(Player player)
	{
		return FoxFireBank.Get(player);
	}
}
