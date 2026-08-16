using MegaCrit.Sts2.Core.Entities.Players;

namespace Zhao.FoxFire;

/// <summary>
/// 狐火命令层。0.107.1 星辉同架构(参考 PlayerCmd.GainStars / PlayerCmd.LoseStars):
/// 卡牌/遗物/形态系统只通过命令层增减狐火,不直接操作数值。
/// </summary>
public static class FoxFireCmd
{
    /// <summary>获得狐火。对应 PlayerCmd.GainStars(amount, player)。</summary>
    public static Task Gain(int amount, Player player)
    {
        FoxFireBank.For(player).Gain(amount);
        return Task.CompletedTask;
    }

    /// <summary>失去狐火。对应 PlayerCmd.LoseStars(amount, player)。</summary>
    public static Task Lose(int amount, Player player)
    {
        FoxFireBank.For(player).Lose(amount);
        return Task.CompletedTask;
    }

    /// <summary>
    /// 支付卡牌狐火成本。对应原版 CardModel.SpendStars → PlayerCombatState.LoseStars 的支付语义,
    /// 由 ZhaoFoxFireCombatHooks.BeforeCardPlayed 在原版出牌管线中"效果执行前"调用。
    /// </summary>
    public static Task Spend(int amount, Player player) => Lose(amount, player);

    /// <summary>查询当前狐火数量。</summary>
    public static int Get(Player player) => FoxFireBank.Get(player);
}
