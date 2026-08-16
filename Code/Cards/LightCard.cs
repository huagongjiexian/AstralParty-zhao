using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using Zhao.Forms;
using Zhao.Powers;

namespace Zhao.Cards;

public sealed class LightCard : ZhaoCardModel
{
	[StructLayout((LayoutKind)3)]
	[CompilerGenerated]
	private struct _003COnPlay_003Ed__7 : IAsyncStateMachine
	{
		public int _003C_003E1__state;

		public AsyncTaskMethodBuilder _003C_003Et__builder;

		public LightCard _003C_003E4__this;

		public PlayerChoiceContext choiceContext;

		private Creature _003Ccreature_003E5__2;

		private TaskAwaiter _003C_003Eu__1;

		private TaskAwaiter<LightPower?> _003C_003Eu__2;

		private void MoveNext()
		{
			//IL_006e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0073: Unknown result type (might be due to invalid IL or missing references)
			//IL_007a: Unknown result type (might be due to invalid IL or missing references)
			//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
			//IL_00c8: Expected O, but got Unknown
			//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
			//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
			//IL_00f8: Unknown result type (might be due to invalid IL or missing references)
			//IL_00fd: Unknown result type (might be due to invalid IL or missing references)
			//IL_0104: Unknown result type (might be due to invalid IL or missing references)
			//IL_003b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0040: Unknown result type (might be due to invalid IL or missing references)
			//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
			//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
			//IL_0054: Unknown result type (might be due to invalid IL or missing references)
			//IL_0055: Unknown result type (might be due to invalid IL or missing references)
			int num = _003C_003E1__state;
			LightCard lightCard = _003C_003E4__this;
			try
			{
				TaskAwaiter<LightPower> val;
				TaskAwaiter val2;
				if (num != 0)
				{
					if (num == 1)
					{
						val = _003C_003Eu__2;
						_003C_003Eu__2 = default(TaskAwaiter<LightPower>);
						num = (_003C_003E1__state = -1);
						goto IL_0113;
					}
					_003Ccreature_003E5__2 = ((CardModel)lightCard).Owner.Creature;
					val2 = FormSystem.SwitchForm(choiceContext, _003Ccreature_003E5__2, ZhaoForm.Lady).GetAwaiter();
					if (!((TaskAwaiter)(ref val2)).IsCompleted)
					{
						num = (_003C_003E1__state = 0);
						_003C_003Eu__1 = val2;
						((AsyncTaskMethodBuilder)(ref _003C_003Et__builder)).AwaitUnsafeOnCompleted<TaskAwaiter, _003COnPlay_003Ed__7>(ref val2, ref this);
						return;
					}
				}
				else
				{
					val2 = _003C_003Eu__1;
					_003C_003Eu__1 = default(TaskAwaiter);
					num = (_003C_003E1__state = -1);
				}
				((TaskAwaiter)(ref val2)).GetResult();
				val = PowerCmd.Apply<LightPower>(choiceContext, _003Ccreature_003E5__2, decimal.op_Implicit(((CardModel)lightCard).DynamicVars["Light"].IntValue), _003Ccreature_003E5__2, (CardModel)lightCard, false).GetAwaiter();
				if (!val.IsCompleted)
				{
					num = (_003C_003E1__state = 1);
					_003C_003Eu__2 = val;
					((AsyncTaskMethodBuilder)(ref _003C_003Et__builder)).AwaitUnsafeOnCompleted<TaskAwaiter<LightPower>, _003COnPlay_003Ed__7>(ref val, ref this);
					return;
				}
				goto IL_0113;
				IL_0113:
				val.GetResult();
			}
			catch (global::System.Exception exception)
			{
				_003C_003E1__state = -2;
				_003Ccreature_003E5__2 = null;
				((AsyncTaskMethodBuilder)(ref _003C_003Et__builder)).SetException(exception);
				return;
			}
			_003C_003E1__state = -2;
			_003Ccreature_003E5__2 = null;
			((AsyncTaskMethodBuilder)(ref _003C_003Et__builder)).SetResult();
		}

		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
			((AsyncTaskMethodBuilder)(ref _003C_003Et__builder)).SetStateMachine(stateMachine);
		}
	}

	protected override global::System.Collections.Generic.IEnumerable<DynamicVar> CanonicalVars => (global::System.Collections.Generic.IEnumerable<DynamicVar>)(object)new DynamicVar[1] { (DynamicVar)new IntVar("Light", 1m) };

	public override int MaxUpgradeLevel => 2;

	protected override bool IsPlayable
	{
		get
		{
			if (base.IsPlayable)
			{
				return FormSystem.GetCurrentForm(((CardModel)this).Owner.Creature) != ZhaoForm.Lady;
			}
			return false;
		}
	}

	public LightCard()
		: base(1, (CardType)2, (CardRarity)1, (TargetType)1)
	{
	}

	[AsyncStateMachine(typeof(_003COnPlay_003Ed__7))]
	protected override global::System.Threading.Tasks.Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		_003COnPlay_003Ed__7 _003COnPlay_003Ed__8 = default(_003COnPlay_003Ed__7);
		_003COnPlay_003Ed__8._003C_003Et__builder = AsyncTaskMethodBuilder.Create();
		_003COnPlay_003Ed__8._003C_003E4__this = this;
		_003COnPlay_003Ed__8.choiceContext = choiceContext;
		_003COnPlay_003Ed__8._003C_003E1__state = -1;
		((AsyncTaskMethodBuilder)(ref _003COnPlay_003Ed__8._003C_003Et__builder)).Start<_003COnPlay_003Ed__7>(ref _003COnPlay_003Ed__8);
		return ((AsyncTaskMethodBuilder)(ref _003COnPlay_003Ed__8._003C_003Et__builder)).Task;
	}

	protected override void OnUpgrade()
	{
		if (((CardModel)this).CurrentUpgradeLevel == 1)
		{
			((CardModel)this).EnergyCost.UpgradeBy(-1);
		}
		((CardModel)this).DynamicVars["Light"].UpgradeValueBy(1m);
	}
}
