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
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;

namespace Zhao.Powers;

public class HealingPower : PowerModel
{
	[StructLayout((LayoutKind)3)]
	[CompilerGenerated]
	private struct _003CAfterSideTurnStart_003Ed__4 : IAsyncStateMachine
	{
		public int _003C_003E1__state;

		public AsyncTaskMethodBuilder _003C_003Et__builder;

		public CombatSide side;

		public global::System.Collections.Generic.IReadOnlyList<Creature> participants;

		public HealingPower _003C_003E4__this;

		private TaskAwaiter _003C_003Eu__1;

		private void MoveNext()
		{
			//IL_0084: Unknown result type (might be due to invalid IL or missing references)
			//IL_0089: Unknown result type (might be due to invalid IL or missing references)
			//IL_0090: Unknown result type (might be due to invalid IL or missing references)
			//IL_0012: Unknown result type (might be due to invalid IL or missing references)
			//IL_0018: Invalid comparison between Unknown and I4
			//IL_0054: Unknown result type (might be due to invalid IL or missing references)
			//IL_0059: Unknown result type (might be due to invalid IL or missing references)
			//IL_006d: Unknown result type (might be due to invalid IL or missing references)
			//IL_006e: Unknown result type (might be due to invalid IL or missing references)
			int num = _003C_003E1__state;
			HealingPower healingPower = _003C_003E4__this;
			try
			{
				TaskAwaiter val;
				if (num == 0)
				{
					val = _003C_003Eu__1;
					_003C_003Eu__1 = default(TaskAwaiter);
					num = (_003C_003E1__state = -1);
					goto IL_009f;
				}
				if ((int)side == 1 && Enumerable.Contains<Creature>((global::System.Collections.Generic.IEnumerable<Creature>)participants, ((PowerModel)healingPower).Owner) && !((PowerModel)healingPower).Owner.IsDead)
				{
					val = CreatureCmd.Heal(((PowerModel)healingPower).Owner, decimal.op_Implicit(((PowerModel)healingPower).Amount), true).GetAwaiter();
					if (!((TaskAwaiter)(ref val)).IsCompleted)
					{
						num = (_003C_003E1__state = 0);
						_003C_003Eu__1 = val;
						((AsyncTaskMethodBuilder)(ref _003C_003Et__builder)).AwaitUnsafeOnCompleted<TaskAwaiter, _003CAfterSideTurnStart_003Ed__4>(ref val, ref this);
						return;
					}
					goto IL_009f;
				}
				goto end_IL_000e;
				IL_009f:
				((TaskAwaiter)(ref val)).GetResult();
				end_IL_000e:;
			}
			catch (global::System.Exception exception)
			{
				_003C_003E1__state = -2;
				((AsyncTaskMethodBuilder)(ref _003C_003Et__builder)).SetException(exception);
				return;
			}
			_003C_003E1__state = -2;
			((AsyncTaskMethodBuilder)(ref _003C_003Et__builder)).SetResult();
		}

		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
			((AsyncTaskMethodBuilder)(ref _003C_003Et__builder)).SetStateMachine(stateMachine);
		}
	}

	[StructLayout((LayoutKind)3)]
	[CompilerGenerated]
	private struct _003CBeforeSideTurnEnd_003Ed__5 : IAsyncStateMachine
	{
		public int _003C_003E1__state;

		public AsyncTaskMethodBuilder _003C_003Et__builder;

		public CombatSide side;

		public global::System.Collections.Generic.IEnumerable<Creature> participants;

		public HealingPower _003C_003E4__this;

		public PlayerChoiceContext choiceContext;

		private TaskAwaiter<int> _003C_003Eu__1;

		private void MoveNext()
		{
			//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
			//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
			//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
			//IL_0015: Unknown result type (might be due to invalid IL or missing references)
			//IL_001b: Invalid comparison between Unknown and I4
			//IL_006e: Unknown result type (might be due to invalid IL or missing references)
			//IL_008d: Expected O, but got Unknown
			//IL_008d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0092: Unknown result type (might be due to invalid IL or missing references)
			//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
			//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
			int num = _003C_003E1__state;
			HealingPower healingPower = _003C_003E4__this;
			try
			{
				TaskAwaiter<int> val;
				if (num == 0)
				{
					val = _003C_003Eu__1;
					_003C_003Eu__1 = default(TaskAwaiter<int>);
					num = (_003C_003E1__state = -1);
					goto IL_00d8;
				}
				if ((int)side == 1 && Enumerable.Contains<Creature>(participants, ((PowerModel)healingPower).Owner) && !((PowerModel)healingPower).Owner.IsDead)
				{
					int num2 = (int)decimal.Floor(decimal.op_Implicit(((PowerModel)healingPower).Amount) / 2m);
					val = PowerCmd.ModifyAmount(choiceContext, (PowerModel)healingPower, decimal.op_Implicit(num2 - ((PowerModel)healingPower).Amount), ((PowerModel)healingPower).Owner, (CardModel)null, false).GetAwaiter();
					if (!val.IsCompleted)
					{
						num = (_003C_003E1__state = 0);
						_003C_003Eu__1 = val;
						((AsyncTaskMethodBuilder)(ref _003C_003Et__builder)).AwaitUnsafeOnCompleted<TaskAwaiter<int>, _003CBeforeSideTurnEnd_003Ed__5>(ref val, ref this);
						return;
					}
					goto IL_00d8;
				}
				goto end_IL_000e;
				IL_00d8:
				val.GetResult();
				end_IL_000e:;
			}
			catch (global::System.Exception exception)
			{
				_003C_003E1__state = -2;
				((AsyncTaskMethodBuilder)(ref _003C_003Et__builder)).SetException(exception);
				return;
			}
			_003C_003E1__state = -2;
			((AsyncTaskMethodBuilder)(ref _003C_003Et__builder)).SetResult();
		}

		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
			((AsyncTaskMethodBuilder)(ref _003C_003Et__builder)).SetStateMachine(stateMachine);
		}
	}

	public override PowerType Type => (PowerType)1;

	public override PowerStackType StackType => (PowerStackType)1;

	[AsyncStateMachine(typeof(_003CAfterSideTurnStart_003Ed__4))]
	public override global::System.Threading.Tasks.Task AfterSideTurnStart(CombatSide side, global::System.Collections.Generic.IReadOnlyList<Creature> participants, ICombatState state)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		_003CAfterSideTurnStart_003Ed__4 _003CAfterSideTurnStart_003Ed__5 = default(_003CAfterSideTurnStart_003Ed__4);
		_003CAfterSideTurnStart_003Ed__5._003C_003Et__builder = AsyncTaskMethodBuilder.Create();
		_003CAfterSideTurnStart_003Ed__5._003C_003E4__this = this;
		_003CAfterSideTurnStart_003Ed__5.side = side;
		_003CAfterSideTurnStart_003Ed__5.participants = participants;
		_003CAfterSideTurnStart_003Ed__5._003C_003E1__state = -1;
		((AsyncTaskMethodBuilder)(ref _003CAfterSideTurnStart_003Ed__5._003C_003Et__builder)).Start<_003CAfterSideTurnStart_003Ed__4>(ref _003CAfterSideTurnStart_003Ed__5);
		return ((AsyncTaskMethodBuilder)(ref _003CAfterSideTurnStart_003Ed__5._003C_003Et__builder)).Task;
	}

	[AsyncStateMachine(typeof(_003CBeforeSideTurnEnd_003Ed__5))]
	public override global::System.Threading.Tasks.Task BeforeSideTurnEnd(PlayerChoiceContext choiceContext, CombatSide side, global::System.Collections.Generic.IEnumerable<Creature> participants)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		_003CBeforeSideTurnEnd_003Ed__5 _003CBeforeSideTurnEnd_003Ed__6 = default(_003CBeforeSideTurnEnd_003Ed__5);
		_003CBeforeSideTurnEnd_003Ed__6._003C_003Et__builder = AsyncTaskMethodBuilder.Create();
		_003CBeforeSideTurnEnd_003Ed__6._003C_003E4__this = this;
		_003CBeforeSideTurnEnd_003Ed__6.choiceContext = choiceContext;
		_003CBeforeSideTurnEnd_003Ed__6.side = side;
		_003CBeforeSideTurnEnd_003Ed__6.participants = participants;
		_003CBeforeSideTurnEnd_003Ed__6._003C_003E1__state = -1;
		((AsyncTaskMethodBuilder)(ref _003CBeforeSideTurnEnd_003Ed__6._003C_003Et__builder)).Start<_003CBeforeSideTurnEnd_003Ed__5>(ref _003CBeforeSideTurnEnd_003Ed__6);
		return ((AsyncTaskMethodBuilder)(ref _003CBeforeSideTurnEnd_003Ed__6._003C_003Et__builder)).Task;
	}
}
