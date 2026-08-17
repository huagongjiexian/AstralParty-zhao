using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Commands.Builders;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;
using Zhao.FoxFire;
using Zhao.Pursuit;

namespace Zhao.Cards;

public sealed class KitsuneFireStrike : ZhaoCardModel
{
	[StructLayout((LayoutKind)3)]
	[CompilerGenerated]
	private struct _003COnPlay_003Ed__5 : IAsyncStateMachine
	{
		public int _003C_003E1__state;

		public AsyncTaskMethodBuilder _003C_003Et__builder;

		public CardPlay cardPlay;

		public KitsuneFireStrike _003C_003E4__this;

		public PlayerChoiceContext choiceContext;

		private TaskAwaiter<AttackCommand> _003C_003Eu__1;

		private TaskAwaiter _003C_003Eu__2;

		public void MoveNext()
		{
			//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
			//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
			//IL_00af: Unknown result type (might be due to invalid IL or missing references)
			//IL_0119: Unknown result type (might be due to invalid IL or missing references)
			//IL_011e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0125: Unknown result type (might be due to invalid IL or missing references)
			//IL_0197: Unknown result type (might be due to invalid IL or missing references)
			//IL_019c: Unknown result type (might be due to invalid IL or missing references)
			//IL_01a3: Unknown result type (might be due to invalid IL or missing references)
			//IL_004b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0055: Expected O, but got Unknown
			//IL_0070: Unknown result type (might be due to invalid IL or missing references)
			//IL_0075: Unknown result type (might be due to invalid IL or missing references)
			//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
			//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
			//IL_0089: Unknown result type (might be due to invalid IL or missing references)
			//IL_008a: Unknown result type (might be due to invalid IL or missing references)
			//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
			//IL_0100: Unknown result type (might be due to invalid IL or missing references)
			//IL_0167: Unknown result type (might be due to invalid IL or missing references)
			//IL_016c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0180: Unknown result type (might be due to invalid IL or missing references)
			//IL_0181: Unknown result type (might be due to invalid IL or missing references)
			int num = _003C_003E1__state;
			KitsuneFireStrike kitsuneFireStrike = _003C_003E4__this;
			try
			{
				TaskAwaiter<AttackCommand> val2;
				TaskAwaiter val;
				switch (num)
				{
				default:
					ArgumentNullException.ThrowIfNull((object)cardPlay.Target, "cardPlay.Target");
					val2 = DamageCmd.Attack(((DynamicVar)((CardModel)kitsuneFireStrike).DynamicVars.Damage).BaseValue).FromCard((CardModel)kitsuneFireStrike).Targeting(cardPlay.Target)
						.Execute(choiceContext)
						.GetAwaiter();
					if (!val2.IsCompleted)
					{
						num = (_003C_003E1__state = 0);
						_003C_003Eu__1 = val2;
						_003C_003Et__builder.AwaitUnsafeOnCompleted<TaskAwaiter<AttackCommand>, _003COnPlay_003Ed__5>(ref val2, ref this);
						return;
					}
					goto IL_00be;
				case 0:
					val2 = _003C_003Eu__1;
					_003C_003Eu__1 = default(TaskAwaiter<AttackCommand>);
					num = (_003C_003E1__state = -1);
					goto IL_00be;
				case 1:
					val = _003C_003Eu__2;
					_003C_003Eu__2 = default(TaskAwaiter);
					num = (_003C_003E1__state = -1);
					goto IL_0134;
				case 2:
					{
						val = _003C_003Eu__2;
						_003C_003Eu__2 = default(TaskAwaiter);
						num = (_003C_003E1__state = -1);
						break;
					}
					IL_0134:
					val.GetResult();
					if (((CardModel)kitsuneFireStrike).CurrentUpgradeLevel >= 2)
					{
						val = PursuitExecutor.Chase(choiceContext, ((CardModel)kitsuneFireStrike).Owner, 1, 6m, cardPlay.Target).GetAwaiter();
						if (!val.IsCompleted)
						{
							num = (_003C_003E1__state = 2);
							_003C_003Eu__2 = val;
							_003C_003Et__builder.AwaitUnsafeOnCompleted<TaskAwaiter, _003COnPlay_003Ed__5>(ref val, ref this);
							return;
						}
						break;
					}
					goto end_IL_000e;
					IL_00be:
					val2.GetResult();
					val = FoxFireCmd.Gain(((CardModel)kitsuneFireStrike).DynamicVars["Foxfire"].IntValue, ((CardModel)kitsuneFireStrike).Owner).GetAwaiter();
					if (!val.IsCompleted)
					{
						num = (_003C_003E1__state = 1);
						_003C_003Eu__2 = val;
						_003C_003Et__builder.AwaitUnsafeOnCompleted<TaskAwaiter, _003COnPlay_003Ed__5>(ref val, ref this);
						return;
					}
					goto IL_0134;
				}
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

	protected override global::System.Collections.Generic.IEnumerable<DynamicVar> CanonicalVars => (global::System.Collections.Generic.IEnumerable<DynamicVar>)(object)new DynamicVar[2]
	{
		(DynamicVar)new DamageVar(6m, ValueProp.Move),
		(DynamicVar)new IntVar("Foxfire", 1m)
	};

	public override int MaxUpgradeLevel => 2;

	public KitsuneFireStrike()
		: base(1, CardType.Attack, CardRarity.Basic, TargetType.AnyEnemy)
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
		_003COnPlay_003Ed__6.cardPlay = cardPlay;
		_003COnPlay_003Ed__6._003C_003E1__state = -1;
		_003COnPlay_003Ed__6._003C_003Et__builder.Start<_003COnPlay_003Ed__5>(ref _003COnPlay_003Ed__6);
		return _003COnPlay_003Ed__6._003C_003Et__builder.Task;
	}

	protected override void OnUpgrade()
	{
		((DynamicVar)((CardModel)this).DynamicVars.Damage).UpgradeValueBy(2m);
		((CardModel)this).DynamicVars["Foxfire"].UpgradeValueBy(1m);
	}
}
