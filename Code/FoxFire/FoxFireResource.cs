namespace Zhao.FoxFire;

/// <summary>
/// 狐火特殊资源:每名玩家、每场战斗的持有量对象。
/// 0.107.1 星辉同架构(参考 PlayerCombatState.Stars 段):
///  - 数值存在战斗状态侧对象上,不是 Power/Buff、不进 Buff 栏;
///  - int 持有量 + AmountChanged(int old, int new) 变更事件(对应 StarsChanged);
///  - Gain/Lose 带非负参数校验并夹在 0 以上(对应 GainStars/LoseStars);
///  - 战斗结束整场清零(对应 PlayerCombatState 每场战斗新建、星辉随对象丢弃)。
/// </summary>
public sealed class FoxFireResource
{
    private int _amount;

    /// <summary>当前狐火数量(无上限,与 0.107.1 星辉一致)。</summary>
    public int Amount => _amount;

    /// <summary>数量变更事件(旧值, 新值)。对应 PlayerCombatState.StarsChanged。</summary>
    public event Action<int, int>? AmountChanged;

    /// <summary>获得狐火。对应 PlayerCombatState.GainStars。</summary>
    public void Gain(int amount)
    {
        if (amount < 0)
        {
            throw new ArgumentException("Must not be negative.", nameof(amount));
        }
        Set(_amount + amount);
    }

    /// <summary>失去/支付狐火(不扣成负数)。对应 PlayerCombatState.LoseStars。</summary>
    public void Lose(int amount)
    {
        if (amount < 0)
        {
            throw new ArgumentException("Must not be negative.", nameof(amount));
        }
        Set(Math.Max(_amount - amount, 0));
    }

    private void Set(int value)
    {
        if (_amount == value)
        {
            return;
        }
        int old = _amount;
        _amount = value;
        AmountChanged?.Invoke(old, _amount);
    }
}
