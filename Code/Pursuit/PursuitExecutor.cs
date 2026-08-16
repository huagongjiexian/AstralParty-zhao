using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;

namespace Zhao.Pursuit;

public static class PursuitExecutor
{
	[StructLayout((LayoutKind)3)]
	[CompilerGenerated]
	private struct _003CChase_003Ed__1 : IAsyncStateMachine
	{
		public int _003C_003E1__state;

		public AsyncTaskMethodBuilder _003C_003Et__builder;

		public int hitCount;

		public decimal damagePerHit;

		public Creature target;

		public Player player;

		public PlayerChoiceContext choiceContext;

		private int _003Ci_003E5__2;

		private TaskAwaiter<global::System.Collections.Generic.IEnumerable<DamageResult>> _003C_003Eu__1;

		private void MoveNext()
		{
			//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
			//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
			//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
			//IL_007d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0082: Unknown result type (might be due to invalid IL or missing references)
			//IL_0096: Unknown result type (might be due to invalid IL or missing references)
			//IL_0097: Unknown result type (might be due to invalid IL or missing references)
			int num = _003C_003E1__state;
			try
			{
				TaskAwaiter<global::System.Collections.Generic.IEnumerable<DamageResult>> val;
				if (num == 0)
				{
					val = _003C_003Eu__1;
					_003C_003Eu__1 = default(TaskAwaiter<global::System.Collections.Generic.IEnumerable<DamageResult>>);
					num = (_003C_003E1__state = -1);
					goto IL_00c8;
				}
				if (hitCount > 0 && !(damagePerHit <= 0m))
				{
					_003Ci_003E5__2 = 0;
					goto IL_00e0;
				}
				goto end_IL_0007;
				IL_00e0:
				Creature val2;
				if (_003Ci_003E5__2 < hitCount)
				{
					val2 = target;
					if (val2 != null && !val2.IsDead)
					{
						goto IL_005d;
					}
					val2 = PickRandomLivingEnemy(player);
					if (val2 != null)
					{
						goto IL_005d;
					}
				}
				goto end_IL_0007;
				IL_00c8:
				val.GetResult();
				_003Ci_003E5__2++;
				goto IL_00e0;
				IL_005d:
				val = CreatureCmd.Damage(choiceContext, val2, damagePerHit, (ValueProp)40, player.Creature, (CardModel)null).GetAwaiter();
				if (!val.IsCompleted)
				{
					num = (_003C_003E1__state = 0);
					_003C_003Eu__1 = val;
					((AsyncTaskMethodBuilder)(ref _003C_003Et__builder)).AwaitUnsafeOnCompleted<TaskAwaiter<global::System.Collections.Generic.IEnumerable<DamageResult>>, _003CChase_003Ed__1>(ref val, ref this);
					return;
				}
				goto IL_00c8;
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

	public const ValueProp PursuitFlag = (ValueProp)32;

	[AsyncStateMachine(typeof(_003CChase_003Ed__1))]
	public static global::System.Threading.Tasks.Task Chase(PlayerChoiceContext choiceContext, Player player, int hitCount, decimal damagePerHit, Creature? target = null)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		_003CChase_003Ed__1 _003CChase_003Ed__2 = default(_003CChase_003Ed__1);
		_003CChase_003Ed__2._003C_003Et__builder = AsyncTaskMethodBuilder.Create();
		_003CChase_003Ed__2.choiceContext = choiceContext;
		_003CChase_003Ed__2.player = player;
		_003CChase_003Ed__2.hitCount = hitCount;
		_003CChase_003Ed__2.damagePerHit = damagePerHit;
		_003CChase_003Ed__2.target = target;
		_003CChase_003Ed__2._003C_003E1__state = -1;
		((AsyncTaskMethodBuilder)(ref _003CChase_003Ed__2._003C_003Et__builder)).Start<_003CChase_003Ed__1>(ref _003CChase_003Ed__2);
		return ((AsyncTaskMethodBuilder)(ref _003CChase_003Ed__2._003C_003Et__builder)).Task;
	}

	private static Creature? PickRandomLivingEnemy(Player player)
	{
		ICombatState combatState = player.Creature.CombatState;
		global::System.Collections.Generic.IReadOnlyList<Creature> readOnlyList = ((combatState != null) ? combatState.HittableEnemies : null);
		if (readOnlyList == null)
		{
			return null;
		}
		List<Creature> val = Enumerable.ToList<Creature>(Enumerable.Where<Creature>((global::System.Collections.Generic.IEnumerable<Creature>)readOnlyList, (Func<Creature, bool>)((Creature e) => !e.IsDead)));
		if (val.Count == 0)
		{
			return null;
		}
		return player.RunState.Rng.CombatCardSelection.NextItem<Creature>((global::System.Collections.Generic.IEnumerable<Creature>)val);
	}
}
