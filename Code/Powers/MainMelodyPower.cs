using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;

namespace Zhao.Powers;

public class MainMelodyPower : PowerModel
{
	[StructLayout((LayoutKind)3)]
	[CompilerGenerated]
	private struct _003CAfterSideTurnStart_003Ed__5 : IAsyncStateMachine
	{
		public int _003C_003E1__state;

		public AsyncTaskMethodBuilder _003C_003Et__builder;

		public CombatSide side;

		public global::System.Collections.Generic.IReadOnlyList<Creature> participants;

		public MainMelodyPower _003C_003E4__this;

		private TaskAwaiter _003C_003Eu__1;

		public void MoveNext()
		{
			//IL_0068: Unknown result type (might be due to invalid IL or missing references)
			//IL_006d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0074: Unknown result type (might be due to invalid IL or missing references)
			//IL_0012: Unknown result type (might be due to invalid IL or missing references)
			//IL_0018: Invalid comparison between Unknown and I4
			//IL_002e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0038: Expected O, but got Unknown
			//IL_0038: Unknown result type (might be due to invalid IL or missing references)
			//IL_003d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0051: Unknown result type (might be due to invalid IL or missing references)
			//IL_0052: Unknown result type (might be due to invalid IL or missing references)
			int num = _003C_003E1__state;
			MainMelodyPower mainMelodyPower = _003C_003E4__this;
			try
			{
				TaskAwaiter val;
				if (num == 0)
				{
					val = _003C_003Eu__1;
					_003C_003Eu__1 = default(TaskAwaiter);
					num = (_003C_003E1__state = -1);
					goto IL_0083;
				}
				if (side == CombatSide.Player && Enumerable.Contains<Creature>((global::System.Collections.Generic.IEnumerable<Creature>)participants, ((PowerModel)mainMelodyPower).Owner))
				{
					val = PowerCmd.Remove((PowerModel)mainMelodyPower).GetAwaiter();
					if (!val.IsCompleted)
					{
						num = (_003C_003E1__state = 0);
						_003C_003Eu__1 = val;
						_003C_003Et__builder.AwaitUnsafeOnCompleted<TaskAwaiter, _003CAfterSideTurnStart_003Ed__5>(ref val, ref this);
						return;
					}
					goto IL_0083;
				}
				goto end_IL_000e;
				IL_0083:
				val.GetResult();
				end_IL_000e:;
			}
			catch (global::System.Exception exception)
			{
				_003C_003E1__state = -2;
				_003C_003Et__builder.SetException(exception);
				return;
			}
			_003C_003E1__state = -2;
			_003C_003Et__builder.SetResult();
		}

		[DebuggerHidden]
		public void SetStateMachine(IAsyncStateMachine stateMachine)
		{
			_003C_003Et__builder.SetStateMachine(stateMachine);
		}
	}

	public override PowerType Type => PowerType.Buff;

	public override PowerStackType StackType => PowerStackType.Single;

	public override decimal ModifyDamageAdditive(Creature? target, decimal amount, ValueProp props, Creature? dealer, CardModel? cardSource)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		if (!ValuePropExtensions.IsPoweredAttack(props))
		{
			return 0m;
		}
		if (((PowerModel)this).Owner == dealer)
		{
			return 1m;
		}
		if (((PowerModel)this).Owner == target)
		{
			return -1m;
		}
		return 0m;
	}

	[AsyncStateMachine(typeof(_003CAfterSideTurnStart_003Ed__5))]
	public override global::System.Threading.Tasks.Task AfterSideTurnStart(CombatSide side, global::System.Collections.Generic.IReadOnlyList<Creature> participants, ICombatState state)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		_003CAfterSideTurnStart_003Ed__5 _003CAfterSideTurnStart_003Ed__6 = default(_003CAfterSideTurnStart_003Ed__5);
		_003CAfterSideTurnStart_003Ed__6._003C_003Et__builder = AsyncTaskMethodBuilder.Create();
		_003CAfterSideTurnStart_003Ed__6._003C_003E4__this = this;
		_003CAfterSideTurnStart_003Ed__6.side = side;
		_003CAfterSideTurnStart_003Ed__6.participants = participants;
		_003CAfterSideTurnStart_003Ed__6._003C_003E1__state = -1;
		_003CAfterSideTurnStart_003Ed__6._003C_003Et__builder.Start<_003CAfterSideTurnStart_003Ed__5>(ref _003CAfterSideTurnStart_003Ed__6);
		return _003CAfterSideTurnStart_003Ed__6._003C_003Et__builder.Task;
	}
}
