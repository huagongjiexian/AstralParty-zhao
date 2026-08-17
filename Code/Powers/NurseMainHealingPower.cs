using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;

namespace Zhao.Powers;

public class NurseMainHealingPower : PowerModel
{
	[StructLayout((LayoutKind)3)]
	[CompilerGenerated]
	private struct _003CAfterCardPlayed_003Ed__6 : IAsyncStateMachine
	{
		public int _003C_003E1__state;

		public AsyncTaskMethodBuilder _003C_003Et__builder;

		public CardPlay cardPlay;

		public NurseMainHealingPower _003C_003E4__this;

		public PlayerChoiceContext choiceContext;

		private TaskAwaiter<HealingPower?> _003C_003Eu__1;

		public void MoveNext()
		{
			//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
			//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
			//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
			//IL_002f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0035: Invalid comparison between Unknown and I4
			//IL_0087: Unknown result type (might be due to invalid IL or missing references)
			//IL_008c: Unknown result type (might be due to invalid IL or missing references)
			//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
			//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
			int num = _003C_003E1__state;
			NurseMainHealingPower nurseMainHealingPower = _003C_003E4__this;
			try
			{
				TaskAwaiter<HealingPower> val;
				if (num == 0)
				{
					val = _003C_003Eu__1;
					_003C_003Eu__1 = default(TaskAwaiter<HealingPower>);
					num = (_003C_003E1__state = -1);
					goto IL_00d2;
				}
				if (cardPlay.Card != null && (int)cardPlay.Card.Type == 2)
				{
					Player owner = cardPlay.Card.Owner;
					if (((owner != null) ? owner.Creature : null) == ((PowerModel)nurseMainHealingPower).Owner)
					{
						val = PowerCmd.Apply<HealingPower>(choiceContext, ((PowerModel)nurseMainHealingPower).Owner, 1m, ((PowerModel)nurseMainHealingPower).Owner, cardPlay.Card, false).GetAwaiter();
						if (!val.IsCompleted)
						{
							num = (_003C_003E1__state = 0);
							_003C_003Eu__1 = val;
							_003C_003Et__builder.AwaitUnsafeOnCompleted<TaskAwaiter<HealingPower>, _003CAfterCardPlayed_003Ed__6>(ref val, ref this);
							return;
						}
						goto IL_00d2;
					}
				}
				goto end_IL_000e;
				IL_00d2:
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

	public override int DisplayAmount => 0;

	[AsyncStateMachine(typeof(_003CAfterCardPlayed_003Ed__6))]
	public override global::System.Threading.Tasks.Task AfterCardPlayed(PlayerChoiceContext choiceContext, CardPlay cardPlay)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		_003CAfterCardPlayed_003Ed__6 _003CAfterCardPlayed_003Ed__7 = default(_003CAfterCardPlayed_003Ed__6);
		_003CAfterCardPlayed_003Ed__7._003C_003Et__builder = AsyncTaskMethodBuilder.Create();
		_003CAfterCardPlayed_003Ed__7._003C_003E4__this = this;
		_003CAfterCardPlayed_003Ed__7.choiceContext = choiceContext;
		_003CAfterCardPlayed_003Ed__7.cardPlay = cardPlay;
		_003CAfterCardPlayed_003Ed__7._003C_003E1__state = -1;
		_003CAfterCardPlayed_003Ed__7._003C_003Et__builder.Start<_003CAfterCardPlayed_003Ed__6>(ref _003CAfterCardPlayed_003Ed__7);
		return _003CAfterCardPlayed_003Ed__7._003C_003Et__builder.Task;
	}
}
