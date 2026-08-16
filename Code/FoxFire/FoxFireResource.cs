using System;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Zhao.FoxFire;

public sealed class FoxFireResource
{
	private int _amount;

	[CompilerGenerated]
	private Action<int, int>? m_AmountChanged;

	public int Amount => _amount;

	public event Action<int, int>? AmountChanged
	{
		[CompilerGenerated]
		add
		{
			Action<int, int> val = this.m_AmountChanged;
			Action<int, int> val2;
			do
			{
				val2 = val;
				Action<int, int> val3 = (Action<int, int>)(object)global::System.Delegate.Combine((global::System.Delegate)(object)val2, (global::System.Delegate)(object)value);
				val = Interlocked.CompareExchange<Action<int, int>>(ref this.m_AmountChanged, val3, val2);
			}
			while (val != val2);
		}
		[CompilerGenerated]
		remove
		{
			Action<int, int> val = this.m_AmountChanged;
			Action<int, int> val2;
			do
			{
				val2 = val;
				Action<int, int> val3 = (Action<int, int>)(object)global::System.Delegate.Remove((global::System.Delegate)(object)val2, (global::System.Delegate)(object)value);
				val = Interlocked.CompareExchange<Action<int, int>>(ref this.m_AmountChanged, val3, val2);
			}
			while (val != val2);
		}
	}

	public void Gain(int amount)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		if (amount < 0)
		{
			throw new ArgumentException("Must not be negative.", "amount");
		}
		Set(_amount + amount);
	}

	public void Lose(int amount)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
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
			int amount = _amount;
			_amount = value;
			this.AmountChanged?.Invoke(amount, _amount);
		}
	}
}
