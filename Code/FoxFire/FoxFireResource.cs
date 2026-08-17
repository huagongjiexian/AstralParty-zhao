using System;

namespace Zhao.FoxFire;

/// <summary>
/// 狐火资源计数:内部 int,提供 Gain / Lose 与变化事件(旧值, 新值)。
/// </summary>
public sealed class FoxFireResource
{
	private int _amount;

	public int Amount => _amount;

	public event Action<int, int>? AmountChanged;

	public void Gain(int amount)
	{
		if (amount < 0)
		{
			throw new ArgumentException("Must not be negative.", "amount");
		}
		Set(_amount + amount);
	}

	public void Lose(int amount)
	{
		if (amount < 0)
		{
			throw new ArgumentException("Must not be negative.", "amount");
		}
		Set(Math.Max(_amount - amount, 0));
	}

	private void Set(int value)
	{
		if (_amount != value)
		{
			int oldAmount = _amount;
			_amount = value;
			AmountChanged?.Invoke(oldAmount, _amount);
		}
	}
}
