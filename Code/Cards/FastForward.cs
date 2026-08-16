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
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using Zhao.Forms;
using Zhao.Powers;

namespace Zhao.Cards;

public sealed class FastForward : ZhaoCardModel
{
	[StructLayout((LayoutKind)3)]
	[CompilerGenerated]
	private struct _003COnPlay_003Ed__5 : IAsyncStateMachine
	{
		public int _003C_003E1__state;

		public AsyncTaskMethodBuilder _003C_003Et__builder;

		public FastForward _003C_003E4__this;

		public PlayerChoiceContext choiceContext;

		private Creature _003Ccreature_003E5__2;

		private TaskAwaiter<VulnerablePower?> _003C_003Eu__1;

		private TaskAwaiter<WeakPower?> _003C_003Eu__2;

		private TaskAwaiter _003C_003Eu__3;

		private void MoveNext()
		{
			//IL_008c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0091: Unknown result type (might be due to invalid IL or missing references)
			//IL_0098: Unknown result type (might be due to invalid IL or missing references)
			//IL_0108: Unknown result type (might be due to invalid IL or missing references)
			//IL_010d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0115: Unknown result type (might be due to invalid IL or missing references)
			//IL_0193: Unknown result type (might be due to invalid IL or missing references)
			//IL_0198: Unknown result type (might be due to invalid IL or missing references)
			//IL_01a0: Unknown result type (might be due to invalid IL or missing references)
			//IL_01fc: Unknown result type (might be due to invalid IL or missing references)
			//IL_0201: Unknown result type (might be due to invalid IL or missing references)
			//IL_0209: Unknown result type (might be due to invalid IL or missing references)
			//IL_004e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0059: Expected O, but got Unknown
			//IL_0059: Unknown result type (might be due to invalid IL or missing references)
			//IL_005e: Unknown result type (might be due to invalid IL or missing references)
			//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
			//IL_00d3: Expected O, but got Unknown
			//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
			//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
			//IL_0072: Unknown result type (might be due to invalid IL or missing references)
			//IL_0073: Unknown result type (might be due to invalid IL or missing references)
			//IL_00ed: Unknown result type (might be due to invalid IL or missing references)
			//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
			//IL_01ca: Unknown result type (might be due to invalid IL or missing references)
			//IL_01cf: Unknown result type (might be due to invalid IL or missing references)
			//IL_015e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0163: Unknown result type (might be due to invalid IL or missing references)
			//IL_01e4: Unknown result type (might be due to invalid IL or missing references)
			//IL_01e6: Unknown result type (might be due to invalid IL or missing references)
			//IL_0178: Unknown result type (might be due to invalid IL or missing references)
			//IL_017a: Unknown result type (might be due to invalid IL or missing references)
			int num = _003C_003E1__state;
			FastForward fastForward = _003C_003E4__this;
			try
			{
				TaskAwaiter<VulnerablePower> val3;
				TaskAwaiter<WeakPower> val2;
				TaskAwaiter val;
				SectionPower section;
				switch (num)
				{
				default:
					_003Ccreature_003E5__2 = ((CardModel)fastForward).Owner.Creature;
					val3 = PowerCmd.Apply<VulnerablePower>(choiceContext, _003Ccreature_003E5__2, 2m, _003Ccreature_003E5__2, (CardModel)fastForward, false).GetAwaiter();
					if (!val3.IsCompleted)
					{
						num = (_003C_003E1__state = 0);
						_003C_003Eu__1 = val3;
						((AsyncTaskMethodBuilder)(ref _003C_003Et__builder)).AwaitUnsafeOnCompleted<TaskAwaiter<VulnerablePower>, _003COnPlay_003Ed__5>(ref val3, ref this);
						return;
					}
					goto IL_00a7;
				case 0:
					val3 = _003C_003Eu__1;
					_003C_003Eu__1 = default(TaskAwaiter<VulnerablePower>);
					num = (_003C_003E1__state = -1);
					goto IL_00a7;
				case 1:
					val2 = _003C_003Eu__2;
					_003C_003Eu__2 = default(TaskAwaiter<WeakPower>);
					num = (_003C_003E1__state = -1);
					goto IL_0124;
				case 2:
					val = _003C_003Eu__3;
					_003C_003Eu__3 = default(TaskAwaiter);
					num = (_003C_003E1__state = -1);
					goto IL_01af;
				case 3:
					{
						val = _003C_003Eu__3;
						_003C_003Eu__3 = default(TaskAwaiter);
						num = (_003C_003E1__state = -1);
						break;
					}
					IL_0124:
					val2.GetResult();
					section = FormSystem.GetSection(_003Ccreature_003E5__2);
					if (section != null)
					{
						SectionStage stage = (SectionStage)Math.Min((int)(section.Stage + 1), 5);
						val = FormSystem.SetStage(choiceContext, _003Ccreature_003E5__2, stage).GetAwaiter();
						if (!((TaskAwaiter)(ref val)).IsCompleted)
						{
							num = (_003C_003E1__state = 2);
							_003C_003Eu__3 = val;
							((AsyncTaskMethodBuilder)(ref _003C_003Et__builder)).AwaitUnsafeOnCompleted<TaskAwaiter, _003COnPlay_003Ed__5>(ref val, ref this);
							return;
						}
						goto IL_01af;
					}
					val = FormSystem.SetStage(choiceContext, _003Ccreature_003E5__2, SectionStage.Intro).GetAwaiter();
					if (!((TaskAwaiter)(ref val)).IsCompleted)
					{
						num = (_003C_003E1__state = 3);
						_003C_003Eu__3 = val;
						((AsyncTaskMethodBuilder)(ref _003C_003Et__builder)).AwaitUnsafeOnCompleted<TaskAwaiter, _003COnPlay_003Ed__5>(ref val, ref this);
						return;
					}
					break;
					IL_00a7:
					val3.GetResult();
					val2 = PowerCmd.Apply<WeakPower>(choiceContext, _003Ccreature_003E5__2, 2m, _003Ccreature_003E5__2, (CardModel)fastForward, false).GetAwaiter();
					if (!val2.IsCompleted)
					{
						num = (_003C_003E1__state = 1);
						_003C_003Eu__2 = val2;
						((AsyncTaskMethodBuilder)(ref _003C_003Et__builder)).AwaitUnsafeOnCompleted<TaskAwaiter<WeakPower>, _003COnPlay_003Ed__5>(ref val2, ref this);
						return;
					}
					goto IL_0124;
					IL_01af:
					((TaskAwaiter)(ref val)).GetResult();
					goto end_IL_000e;
				}
				((TaskAwaiter)(ref val)).GetResult();
				end_IL_000e:;
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

	public override global::System.Collections.Generic.IEnumerable<CardKeyword> CanonicalKeywords => (global::System.Collections.Generic.IEnumerable<CardKeyword>)(object)new CardKeyword[1] { (CardKeyword)1 };

	public override int MaxUpgradeLevel => 2;

	public FastForward()
		: base(2, (CardType)2, (CardRarity)3, (TargetType)1)
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
		if (((CardModel)this).CurrentUpgradeLevel == 1)
		{
			((CardModel)this).BaseReplayCount = ((CardModel)this).BaseReplayCount + 1;
		}
	}
}
