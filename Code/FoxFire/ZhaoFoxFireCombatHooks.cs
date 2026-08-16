using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Models;
using Zhao.Cards;

namespace Zhao.FoxFire;

public sealed class ZhaoFoxFireCombatHooks : AbstractModel
{
	[StructLayout((LayoutKind)3)]
	[CompilerGenerated]
	private struct _003CBeforeCardPlayed_003Ed__2 : IAsyncStateMachine
	{
		public int _003C_003E1__state;

		public AsyncTaskMethodBuilder _003C_003Et__builder;

		public CardPlay cardPlay;

		private TaskAwaiter _003C_003Eu__1;

		private void MoveNext()
		{
			//IL_007f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0084: Unknown result type (might be due to invalid IL or missing references)
			//IL_008b: Unknown result type (might be due to invalid IL or missing references)
			//IL_004f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0054: Unknown result type (might be due to invalid IL or missing references)
			//IL_0068: Unknown result type (might be due to invalid IL or missing references)
			//IL_0069: Unknown result type (might be due to invalid IL or missing references)
			int num = _003C_003E1__state;
			try
			{
				TaskAwaiter val;
				if (num == 0)
				{
					val = _003C_003Eu__1;
					_003C_003Eu__1 = default(TaskAwaiter);
					num = (_003C_003E1__state = -1);
					goto IL_009a;
				}
				if (cardPlay.IsFirstInSeries && cardPlay.Card is ZhaoCardModel { FoxFireCost: >0 } zhaoCardModel)
				{
					Player owner = ((CardModel)zhaoCardModel).Owner;
					if (owner != null)
					{
						val = FoxFireCmd.Spend(zhaoCardModel.FoxFireCost, owner).GetAwaiter();
						if (!((TaskAwaiter)(ref val)).IsCompleted)
						{
							num = (_003C_003E1__state = 0);
							_003C_003Eu__1 = val;
							((AsyncTaskMethodBuilder)(ref _003C_003Et__builder)).AwaitUnsafeOnCompleted<TaskAwaiter, _003CBeforeCardPlayed_003Ed__2>(ref val, ref this);
							return;
						}
						goto IL_009a;
					}
				}
				goto end_IL_0007;
				IL_009a:
				((TaskAwaiter)(ref val)).GetResult();
				end_IL_0007:;
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

	public override bool ShouldReceiveCombatHooks => true;

	[AsyncStateMachine(typeof(_003CBeforeCardPlayed_003Ed__2))]
	public override global::System.Threading.Tasks.Task BeforeCardPlayed(CardPlay cardPlay)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		_003CBeforeCardPlayed_003Ed__2 _003CBeforeCardPlayed_003Ed__3 = default(_003CBeforeCardPlayed_003Ed__2);
		_003CBeforeCardPlayed_003Ed__3._003C_003Et__builder = AsyncTaskMethodBuilder.Create();
		_003CBeforeCardPlayed_003Ed__3.cardPlay = cardPlay;
		_003CBeforeCardPlayed_003Ed__3._003C_003E1__state = -1;
		((AsyncTaskMethodBuilder)(ref _003CBeforeCardPlayed_003Ed__3._003C_003Et__builder)).Start<_003CBeforeCardPlayed_003Ed__2>(ref _003CBeforeCardPlayed_003Ed__3);
		return ((AsyncTaskMethodBuilder)(ref _003CBeforeCardPlayed_003Ed__3._003C_003Et__builder)).Task;
	}
}
