using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using Zhao.Forms;
using Zhao.Powers;

namespace Zhao.Cards;

public sealed class OutroCard : ZhaoCardModel
{
	[StructLayout((LayoutKind)3)]
	[CompilerGenerated]
	private struct _003COnPlay_003Ed__5 : IAsyncStateMachine
	{
		public int _003C_003E1__state;

		public AsyncTaskMethodBuilder _003C_003Et__builder;

		public OutroCard _003C_003E4__this;

		public PlayerChoiceContext choiceContext;

		private TaskAwaiter _003C_003Eu__1;

		private void MoveNext()
		{
			//IL_0072: Unknown result type (might be due to invalid IL or missing references)
			//IL_0077: Unknown result type (might be due to invalid IL or missing references)
			//IL_007f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0040: Unknown result type (might be due to invalid IL or missing references)
			//IL_0045: Unknown result type (might be due to invalid IL or missing references)
			//IL_005a: Unknown result type (might be due to invalid IL or missing references)
			//IL_005c: Unknown result type (might be due to invalid IL or missing references)
			int num = _003C_003E1__state;
			OutroCard outroCard = _003C_003E4__this;
			try
			{
				TaskAwaiter val;
				if (num != 0)
				{
					Creature creature = ((CardModel)outroCard).Owner.Creature;
					SectionPower section = FormSystem.GetSection(creature);
					if (section != null)
					{
						section.OutroLevel = ((CardModel)outroCard).CurrentUpgradeLevel;
					}
					val = FormSystem.SetStage(choiceContext, creature, SectionStage.Outro).GetAwaiter();
					if (!((TaskAwaiter)(ref val)).IsCompleted)
					{
						num = (_003C_003E1__state = 0);
						_003C_003Eu__1 = val;
						((AsyncTaskMethodBuilder)(ref _003C_003Et__builder)).AwaitUnsafeOnCompleted<TaskAwaiter, _003COnPlay_003Ed__5>(ref val, ref this);
						return;
					}
				}
				else
				{
					val = _003C_003Eu__1;
					_003C_003Eu__1 = default(TaskAwaiter);
					num = (_003C_003E1__state = -1);
				}
				((TaskAwaiter)(ref val)).GetResult();
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

	public override int MaxUpgradeLevel => 2;

	protected override bool IsPlayable
	{
		get
		{
			int num;
			if (base.IsPlayable)
			{
				SectionPower section = FormSystem.GetSection(((CardModel)this).Owner.Creature);
				num = ((section != null && section.Stage == SectionStage.Interlude) ? 1 : 0);
			}
			else
			{
				num = 0;
			}
			return (byte)num != 0;
		}
	}

	public OutroCard()
		: base(3, (CardType)2, (CardRarity)4, (TargetType)1)
	{
	}

	[AsyncStateMachine(typeof(_003COnPlay_003Ed__5))]
	protected override global::System.Threading.Tasks.Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		_003COnPlay_003Ed__5 _003COnPlay_003Ed__6 = default(_003COnPlay_003Ed__5);
		_003COnPlay_003Ed__6._003C_003Et__builder = AsyncTaskMethodBuilder.Create();
		_003COnPlay_003Ed__6._003C_003E4__this = this;
		_003COnPlay_003Ed__6.choiceContext = choiceContext;
		_003COnPlay_003Ed__6._003C_003E1__state = -1;
		((AsyncTaskMethodBuilder)(ref _003COnPlay_003Ed__6._003C_003Et__builder)).Start<_003COnPlay_003Ed__5>(ref _003COnPlay_003Ed__6);
		return ((AsyncTaskMethodBuilder)(ref _003COnPlay_003Ed__6._003C_003Et__builder)).Task;
	}

	protected override void OnUpgrade()
	{
	}
}
