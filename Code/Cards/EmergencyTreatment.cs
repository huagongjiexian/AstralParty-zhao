using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using Zhao.Forms;
using Zhao.Powers;

namespace Zhao.Cards;

public sealed class EmergencyTreatment : ZhaoCardModel
{
	[StructLayout((LayoutKind)3)]
	[CompilerGenerated]
	private struct _003COnPlay_003Ed__3 : IAsyncStateMachine
	{
		public int _003C_003E1__state;

		public AsyncTaskMethodBuilder _003C_003Et__builder;

		public EmergencyTreatment _003C_003E4__this;

		public PlayerChoiceContext choiceContext;

		private Creature _003Ccreature_003E5__2;

		private TaskAwaiter _003C_003Eu__1;

		private TaskAwaiter<HealingPower?> _003C_003Eu__2;

		public void MoveNext()
		{
			//IL_0076: Unknown result type (might be due to invalid IL or missing references)
			//IL_007b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0082: Unknown result type (might be due to invalid IL or missing references)
			//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
			//IL_00f3: Unknown result type (might be due to invalid IL or missing references)
			//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
			//IL_0153: Unknown result type (might be due to invalid IL or missing references)
			//IL_0158: Unknown result type (might be due to invalid IL or missing references)
			//IL_015f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0043: Unknown result type (might be due to invalid IL or missing references)
			//IL_0048: Unknown result type (might be due to invalid IL or missing references)
			//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
			//IL_00bb: Expected O, but got Unknown
			//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
			//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
			//IL_0123: Unknown result type (might be due to invalid IL or missing references)
			//IL_0128: Unknown result type (might be due to invalid IL or missing references)
			//IL_005c: Unknown result type (might be due to invalid IL or missing references)
			//IL_005d: Unknown result type (might be due to invalid IL or missing references)
			//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
			//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
			//IL_013c: Unknown result type (might be due to invalid IL or missing references)
			//IL_013d: Unknown result type (might be due to invalid IL or missing references)
			int num = _003C_003E1__state;
			EmergencyTreatment emergencyTreatment = _003C_003E4__this;
			try
			{
				TaskAwaiter val;
				TaskAwaiter<HealingPower> val2;
				switch (num)
				{
				default:
					_003Ccreature_003E5__2 = ((CardModel)emergencyTreatment).Owner.Creature;
					val = FormSystem.SwitchForm(choiceContext, _003Ccreature_003E5__2, ZhaoForm.Nurse).GetAwaiter();
					if (!val.IsCompleted)
					{
						num = (_003C_003E1__state = 0);
						_003C_003Eu__1 = val;
						_003C_003Et__builder.AwaitUnsafeOnCompleted<TaskAwaiter, _003COnPlay_003Ed__3>(ref val, ref this);
						return;
					}
					goto IL_0091;
				case 0:
					val = _003C_003Eu__1;
					_003C_003Eu__1 = default(TaskAwaiter);
					num = (_003C_003E1__state = -1);
					goto IL_0091;
				case 1:
					val2 = _003C_003Eu__2;
					_003C_003Eu__2 = default(TaskAwaiter<HealingPower>);
					num = (_003C_003E1__state = -1);
					goto IL_0109;
				case 2:
					{
						val = _003C_003Eu__1;
						_003C_003Eu__1 = default(TaskAwaiter);
						num = (_003C_003E1__state = -1);
						break;
					}
					IL_0109:
					val2.GetResult();
					val = CreatureCmd.Heal(_003Ccreature_003E5__2, 5m, true).GetAwaiter();
					if (!val.IsCompleted)
					{
						num = (_003C_003E1__state = 2);
						_003C_003Eu__1 = val;
						_003C_003Et__builder.AwaitUnsafeOnCompleted<TaskAwaiter, _003COnPlay_003Ed__3>(ref val, ref this);
						return;
					}
					break;
					IL_0091:
					val.GetResult();
					val2 = PowerCmd.Apply<HealingPower>(choiceContext, _003Ccreature_003E5__2, 1m, _003Ccreature_003E5__2, (CardModel)emergencyTreatment, false).GetAwaiter();
					if (!val2.IsCompleted)
					{
						num = (_003C_003E1__state = 1);
						_003C_003Eu__2 = val2;
						_003C_003Et__builder.AwaitUnsafeOnCompleted<TaskAwaiter<HealingPower>, _003COnPlay_003Ed__3>(ref val2, ref this);
						return;
					}
					goto IL_0109;
				}
				val.GetResult();
			}
			catch (global::System.Exception exception)
			{
				_003C_003E1__state = -2;
				_003Ccreature_003E5__2 = null;
				_003C_003Et__builder.SetException(exception);
				return;
			}
			_003C_003E1__state = -2;
			_003Ccreature_003E5__2 = null;
			_003C_003Et__builder.SetResult();
		}

		[DebuggerHidden]
		public void SetStateMachine(IAsyncStateMachine stateMachine)
		{
			_003C_003Et__builder.SetStateMachine(stateMachine);
		}
	}

	protected override bool IsPlayable
	{
		get
		{
			if (base.IsPlayable)
			{
				return FormSystem.GetCurrentForm(((CardModel)this).Owner.Creature) != ZhaoForm.Nurse;
			}
			return false;
		}
	}

	public EmergencyTreatment()
		: base(0, CardType.Skill, CardRarity.Basic, TargetType.Self)
	{
	}

	[AsyncStateMachine(typeof(_003COnPlay_003Ed__3))]
	protected override global::System.Threading.Tasks.Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		_003COnPlay_003Ed__3 _003COnPlay_003Ed__4 = default(_003COnPlay_003Ed__3);
		_003COnPlay_003Ed__4._003C_003Et__builder = AsyncTaskMethodBuilder.Create();
		_003COnPlay_003Ed__4._003C_003E4__this = this;
		_003COnPlay_003Ed__4.choiceContext = choiceContext;
		_003COnPlay_003Ed__4._003C_003E1__state = -1;
		_003COnPlay_003Ed__4._003C_003Et__builder.Start<_003COnPlay_003Ed__3>(ref _003COnPlay_003Ed__4);
		return _003COnPlay_003Ed__4._003C_003Et__builder.Task;
	}

	protected override void OnUpgrade()
	{
	}
}
