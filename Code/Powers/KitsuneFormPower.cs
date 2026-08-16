using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using Zhao.Forms;
using Zhao.FoxFire;

namespace Zhao.Powers;

public class KitsuneFormPower : ZhaoFormPower
{
	[StructLayout((LayoutKind)3)]
	[CompilerGenerated]
	private struct _003CAfterCardPlayed_003Ed__0 : IAsyncStateMachine
	{
		public int _003C_003E1__state;

		public AsyncTaskMethodBuilder _003C_003Et__builder;

		public CardPlay cardPlay;

		public KitsuneFormPower _003C_003E4__this;

		private TaskAwaiter _003C_003Eu__1;

		private void MoveNext()
		{
			//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
			//IL_00f3: Unknown result type (might be due to invalid IL or missing references)
			//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
			//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
			//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
			//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
			//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
			int num = _003C_003E1__state;
			KitsuneFormPower kitsuneFormPower = _003C_003E4__this;
			try
			{
				TaskAwaiter val;
				if (num == 0)
				{
					val = _003C_003Eu__1;
					_003C_003Eu__1 = default(TaskAwaiter);
					num = (_003C_003E1__state = -1);
					goto IL_010a;
				}
				CardModel card = cardPlay.Card;
				int num2;
				if (card != null)
				{
					Player owner = card.Owner;
					num2 = ((((owner != null) ? owner.Creature : null) != ((PowerModel)kitsuneFormPower).Owner) ? 1 : 0);
				}
				else
				{
					num2 = 1;
				}
				if (num2 == 0 && cardPlay.IsFirstInSeries)
				{
					int canonical = card.EnergyCost.Canonical;
					SectionPower section = FormSystem.GetSection(((PowerModel)kitsuneFormPower).Owner);
					bool flag = section != null && section.Stage == SectionStage.Intro;
					int withModifiers = card.EnergyCost.GetWithModifiers((CostModifiers)(-1));
					if (canonical == 3 || (flag && withModifiers == 2))
					{
						Player val2 = FormSystem.PlayerFor(((PowerModel)kitsuneFormPower).Owner);
						if (val2 != null)
						{
							val = FoxFireCmd.Gain(1, val2).GetAwaiter();
							if (!((TaskAwaiter)(ref val)).IsCompleted)
							{
								num = (_003C_003E1__state = 0);
								_003C_003Eu__1 = val;
								((AsyncTaskMethodBuilder)(ref _003C_003Et__builder)).AwaitUnsafeOnCompleted<TaskAwaiter, _003CAfterCardPlayed_003Ed__0>(ref val, ref this);
								return;
							}
							goto IL_010a;
						}
					}
				}
				goto end_IL_000e;
				IL_010a:
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

	[AsyncStateMachine(typeof(_003CAfterCardPlayed_003Ed__0))]
	public override global::System.Threading.Tasks.Task AfterCardPlayed(PlayerChoiceContext choiceContext, CardPlay cardPlay)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		_003CAfterCardPlayed_003Ed__0 _003CAfterCardPlayed_003Ed__1 = default(_003CAfterCardPlayed_003Ed__0);
		_003CAfterCardPlayed_003Ed__1._003C_003Et__builder = AsyncTaskMethodBuilder.Create();
		_003CAfterCardPlayed_003Ed__1._003C_003E4__this = this;
		_003CAfterCardPlayed_003Ed__1.cardPlay = cardPlay;
		_003CAfterCardPlayed_003Ed__1._003C_003E1__state = -1;
		((AsyncTaskMethodBuilder)(ref _003CAfterCardPlayed_003Ed__1._003C_003Et__builder)).Start<_003CAfterCardPlayed_003Ed__0>(ref _003CAfterCardPlayed_003Ed__1);
		return ((AsyncTaskMethodBuilder)(ref _003CAfterCardPlayed_003Ed__1._003C_003Et__builder)).Task;
	}
}
