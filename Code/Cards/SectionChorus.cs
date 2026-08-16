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

public sealed class SectionChorus : ZhaoCardModel
{
	[StructLayout((LayoutKind)3)]
	[CompilerGenerated]
	private struct _003COnPlay_003Ed__3 : IAsyncStateMachine
	{
		public int _003C_003E1__state;

		public AsyncTaskMethodBuilder _003C_003Et__builder;

		public SectionChorus _003C_003E4__this;

		public PlayerChoiceContext choiceContext;

		private Creature _003Ccreature_003E5__2;

		private TaskAwaiter _003C_003Eu__1;

		private TaskAwaiter<LiberationPower?> _003C_003Eu__2;

		private void MoveNext()
		{
			//IL_006e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0073: Unknown result type (might be due to invalid IL or missing references)
			//IL_007a: Unknown result type (might be due to invalid IL or missing references)
			//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
			//IL_00b3: Expected O, but got Unknown
			//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
			//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
			//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
			//IL_00e8: Unknown result type (might be due to invalid IL or missing references)
			//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
			//IL_003b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0040: Unknown result type (might be due to invalid IL or missing references)
			//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
			//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
			//IL_0054: Unknown result type (might be due to invalid IL or missing references)
			//IL_0055: Unknown result type (might be due to invalid IL or missing references)
			int num = _003C_003E1__state;
			SectionChorus sectionChorus = _003C_003E4__this;
			try
			{
				TaskAwaiter<LiberationPower> val;
				TaskAwaiter val2;
				if (num != 0)
				{
					if (num == 1)
					{
						val = _003C_003Eu__2;
						_003C_003Eu__2 = default(TaskAwaiter<LiberationPower>);
						num = (_003C_003E1__state = -1);
						goto IL_00fe;
					}
					_003Ccreature_003E5__2 = ((CardModel)sectionChorus).Owner.Creature;
					val2 = FormSystem.SetStage(choiceContext, _003Ccreature_003E5__2, SectionStage.Chorus).GetAwaiter();
					if (!((TaskAwaiter)(ref val2)).IsCompleted)
					{
						num = (_003C_003E1__state = 0);
						_003C_003Eu__1 = val2;
						((AsyncTaskMethodBuilder)(ref _003C_003Et__builder)).AwaitUnsafeOnCompleted<TaskAwaiter, _003COnPlay_003Ed__3>(ref val2, ref this);
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
				val = PowerCmd.Apply<LiberationPower>(choiceContext, _003Ccreature_003E5__2, 1m, _003Ccreature_003E5__2, (CardModel)sectionChorus, false).GetAwaiter();
				if (!val.IsCompleted)
				{
					num = (_003C_003E1__state = 1);
					_003C_003Eu__2 = val;
					((AsyncTaskMethodBuilder)(ref _003C_003Et__builder)).AwaitUnsafeOnCompleted<TaskAwaiter<LiberationPower>, _003COnPlay_003Ed__3>(ref val, ref this);
					return;
				}
				goto IL_00fe;
				IL_00fe:
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

	public override int MaxUpgradeLevel => 2;

	public SectionChorus()
		: base(1, (CardType)2, (CardRarity)7, (TargetType)1)
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
		((AsyncTaskMethodBuilder)(ref _003COnPlay_003Ed__4._003C_003Et__builder)).Start<_003COnPlay_003Ed__3>(ref _003COnPlay_003Ed__4);
		return ((AsyncTaskMethodBuilder)(ref _003COnPlay_003Ed__4._003C_003Et__builder)).Task;
	}

	protected override PileType GetResultPileTypeForCardPlay()
	{
		return (PileType)0;
	}

	protected override void OnUpgrade()
	{
		if (((CardModel)this).CurrentUpgradeLevel == 1)
		{
			((CardModel)this).EnergyCost.UpgradeBy(-1);
		}
	}
}
