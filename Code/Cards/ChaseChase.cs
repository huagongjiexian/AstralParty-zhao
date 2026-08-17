using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using Zhao.Forms;
using Zhao.Pursuit;

namespace Zhao.Cards;

public sealed class ChaseChase : ZhaoCardModel
{
	[StructLayout((LayoutKind)3)]
	[CompilerGenerated]
	private struct _003COnPlay_003Ed__7 : IAsyncStateMachine
	{
		public int _003C_003E1__state;

		public AsyncTaskMethodBuilder _003C_003Et__builder;

		public CardPlay cardPlay;

		public ChaseChase _003C_003E4__this;

		public PlayerChoiceContext choiceContext;

		private TaskAwaiter _003C_003Eu__1;

		public void MoveNext()
		{
			//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
			//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
			//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
			//IL_0096: Unknown result type (might be due to invalid IL or missing references)
			//IL_009b: Unknown result type (might be due to invalid IL or missing references)
			//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
			//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
			int num = _003C_003E1__state;
			ChaseChase chaseChase = _003C_003E4__this;
			try
			{
				TaskAwaiter val;
				if (num != 0)
				{
					// 修复:目标为空/死亡时由 PursuitExecutor.Chase 回退到随机存活敌人,不再硬抛异常
					Creature creature = ((CardModel)chaseChase).Owner.Creature;
					int num2 = ((CardModel)chaseChase).DynamicVars["Hits"].IntValue;
					decimal baseValue = ((CardModel)chaseChase).DynamicVars["ChaseDamage"].BaseValue;
					if (FormSystem.GetCurrentForm(creature) == ZhaoForm.Kitsune)
					{
						num2 += ((((CardModel)chaseChase).CurrentUpgradeLevel < 2) ? 1 : 2);
					}
					val = PursuitExecutor.Chase(choiceContext, ((CardModel)chaseChase).Owner, num2, baseValue, cardPlay.Target).GetAwaiter();
					if (!val.IsCompleted)
					{
						num = (_003C_003E1__state = 0);
						_003C_003Eu__1 = val;
						_003C_003Et__builder.AwaitUnsafeOnCompleted<TaskAwaiter, _003COnPlay_003Ed__7>(ref val, ref this);
						return;
					}
				}
				else
				{
					val = _003C_003Eu__1;
					_003C_003Eu__1 = default(TaskAwaiter);
					num = (_003C_003E1__state = -1);
				}
				val.GetResult();
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

	protected override global::System.Collections.Generic.IEnumerable<DynamicVar> CanonicalVars => (global::System.Collections.Generic.IEnumerable<DynamicVar>)(object)new DynamicVar[3]
	{
		(DynamicVar)new IntVar("Foxfire", 2m),
		(DynamicVar)new IntVar("Hits", 2m),
		(DynamicVar)new IntVar("ChaseDamage", 6m)
	};

	public override int MaxUpgradeLevel => 2;

	public override int FoxFireCost => ((CardModel)this).DynamicVars["Foxfire"].IntValue;

	public ChaseChase()
		: base(2, CardType.Attack, CardRarity.Common, TargetType.AnyEnemy)
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
		_003COnPlay_003Ed__8.cardPlay = cardPlay;
		_003COnPlay_003Ed__8._003C_003E1__state = -1;
		_003COnPlay_003Ed__8._003C_003Et__builder.Start<_003COnPlay_003Ed__7>(ref _003COnPlay_003Ed__8);
		return _003COnPlay_003Ed__8._003C_003Et__builder.Task;
	}

	protected override void OnUpgrade()
	{
		if (((CardModel)this).CurrentUpgradeLevel == 1)
		{
			// 基础→+:效果与基础相同(不得擅自加强),不改任何数值
		}
		else if (((CardModel)this).CurrentUpgradeLevel == 2)
		{
			// ++:3费,狐火2→1,4次追击×8伤害(巫女形态额外+2次,总计6次)
			((CardModel)this).EnergyCost.UpgradeBy(1);
			((CardModel)this).DynamicVars["Foxfire"].UpgradeValueBy(-1m);
			((CardModel)this).DynamicVars["Hits"].UpgradeValueBy(2m);
			((CardModel)this).DynamicVars["ChaseDamage"].UpgradeValueBy(2m);
		}
	}
}
