using System.Runtime.CompilerServices;
using MegaCrit.Sts2.Core.Entities.Players;

namespace Zhao.FoxFire;

/// <summary>
/// 狐火银行:按玩家存放每场战斗的 FoxFireResource。
/// 0.107.1 星辉同架构:星辉是 PlayerCombatState 上的 int 字段,模组无法给本体类加字段,
/// 故以"每玩家每场战斗的资源对象注册表"等价实现(键为 Player 引用,战斗结束整体清理)。
/// </summary>
public static class FoxFireBank
{
    private static readonly ConditionalWeakTable<Player, FoxFireResource> _states = new();

    /// <summary>取(或惰性创建)该玩家的狐火资源对象。</summary>
    public static FoxFireResource For(Player player) => _states.GetValue(player, static _ => new FoxFireResource());

    /// <summary>查询当前狐火数量。</summary>
    public static int Get(Player player) => For(player).Amount;

    /// <summary>战斗结束清理(等价于原版星辉随 PlayerCombatState 一起丢弃、下场归零)。</summary>
    public static void ClearCombat()
    {
        _states.Clear();
    }
}
