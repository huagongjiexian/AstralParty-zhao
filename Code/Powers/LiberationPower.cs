using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.Models;

namespace Zhao.Powers;

public class LiberationPower : PowerModel
{
	[StructLayout((LayoutKind)3)]
	[CompilerGenerated]
	private struct _003CAfterApplied_003Ed__5 : IAsyncStateMachine
	{
		public int _003C_003E1__state;

		public AsyncTaskMethodBuilder _003C_003Et__builder;

		public LiberationPower _003C_003E4__this;

		public Creature applier;

		public CardModel cardSource;

		private TaskAwaiter _003C_003Eu__1;

		private void MoveNext()
		{
			//IL_0056: Unknown result type (might be due to invalid IL or missing references)
			//IL_005b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0062: Unknown result type (might be due to invalid IL or missing references)
			//IL_0023: Unknown result type (might be due to invalid IL or missing references)
			//IL_0028: Unknown result type (might be due to invalid IL or missing references)
			//IL_003c: Unknown result type (might be due to invalid IL or missing references)
			//IL_003d: Unknown result type (might be due to invalid IL or missing references)
			//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
			int num = _003C_003E1__state;
			LiberationPower liberationPower = _003C_003E4__this;
			try
			{
				TaskAwaiter val;
				if (num != 0)
				{
					val = liberationPower._003C_003En__0(applier, cardSource).GetAwaiter();
					if (!((TaskAwaiter)(ref val)).IsCompleted)
					{
						num = (_003C_003E1__state = 0);
						_003C_003Eu__1 = val;
						((AsyncTaskMethodBuilder)(ref _003C_003Et__builder)).AwaitUnsafeOnCompleted<TaskAwaiter, _003CAfterApplied_003Ed__5>(ref val, ref this);
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
				liberationPower._bumped.Clear();
				Player val2 = liberationPower.PlayerForOwner();
				if (val2 != null)
				{
					global::System.Collections.Generic.IEnumerator<CardModel> enumerator = AllCombatCards(val2).GetEnumerator();
					try
					{
						while (((global::System.Collections.IEnumerator)enumerator).MoveNext())
						{
							CardModel current = enumerator.Current;
							if (current.CurrentUpgradeLevel < current.MaxUpgradeLevel)
							{
								liberationPower._bumped.Add(new ValueTuple<CardModel, int>(current, current.CurrentUpgradeLevel));
								TemporaryUpgrade.ApplyOneLevel(current);
							}
						}
					}
					finally
					{
						if (num < 0)
						{
							((global::System.IDisposable)enumerator)?.Dispose();
						}
					}
				}
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

	[StructLayout((LayoutKind)3)]
	[CompilerGenerated]
	private struct _003CAfterRemoved_003Ed__6 : IAsyncStateMachine
	{
		public int _003C_003E1__state;

		public AsyncTaskMethodBuilder _003C_003Et__builder;

		public LiberationPower _003C_003E4__this;

		public Creature oldOwner;

		private TaskAwaiter _003C_003Eu__1;

		private void MoveNext()
		{
			//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
			//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
			//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
			//IL_001a: Unknown result type (might be due to invalid IL or missing references)
			//IL_001f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0024: Unknown result type (might be due to invalid IL or missing references)
			//IL_0029: Unknown result type (might be due to invalid IL or missing references)
			//IL_0073: Unknown result type (might be due to invalid IL or missing references)
			//IL_0078: Unknown result type (might be due to invalid IL or missing references)
			//IL_008d: Unknown result type (might be due to invalid IL or missing references)
			//IL_008f: Unknown result type (might be due to invalid IL or missing references)
			int num = _003C_003E1__state;
			LiberationPower liberationPower = _003C_003E4__this;
			try
			{
				TaskAwaiter val;
				if (num != 0)
				{
					Enumerator<ValueTuple<CardModel, int>> enumerator = liberationPower._bumped.GetEnumerator();
					try
					{
						while (enumerator.MoveNext())
						{
							ValueTuple<CardModel, int> current = enumerator.Current;
							CardModel item = current.Item1;
							int item2 = current.Item2;
							TemporaryUpgrade.RevertToLevel(item, item2);
						}
					}
					finally
					{
						if (num < 0)
						{
							((global::System.IDisposable)enumerator/*cast due to .constrained prefix*/).Dispose();
						}
					}
					liberationPower._bumped.Clear();
					val = liberationPower._003C_003En__1(oldOwner).GetAwaiter();
					if (!((TaskAwaiter)(ref val)).IsCompleted)
					{
						num = (_003C_003E1__state = 0);
						_003C_003Eu__1 = val;
						((AsyncTaskMethodBuilder)(ref _003C_003Et__builder)).AwaitUnsafeOnCompleted<TaskAwaiter, _003CAfterRemoved_003Ed__6>(ref val, ref this);
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

	private readonly List<ValueTuple<CardModel, int>> _bumped = new List<ValueTuple<CardModel, int>>();

	public override PowerType Type => (PowerType)1;

	public override PowerStackType StackType => (PowerStackType)2;

	[AsyncStateMachine(typeof(_003CAfterApplied_003Ed__5))]
	public override global::System.Threading.Tasks.Task AfterApplied(Creature? applier, CardModel? cardSource)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		_003CAfterApplied_003Ed__5 _003CAfterApplied_003Ed__6 = default(_003CAfterApplied_003Ed__5);
		_003CAfterApplied_003Ed__6._003C_003Et__builder = AsyncTaskMethodBuilder.Create();
		_003CAfterApplied_003Ed__6._003C_003E4__this = this;
		_003CAfterApplied_003Ed__6.applier = applier;
		_003CAfterApplied_003Ed__6.cardSource = cardSource;
		_003CAfterApplied_003Ed__6._003C_003E1__state = -1;
		((AsyncTaskMethodBuilder)(ref _003CAfterApplied_003Ed__6._003C_003Et__builder)).Start<_003CAfterApplied_003Ed__5>(ref _003CAfterApplied_003Ed__6);
		return ((AsyncTaskMethodBuilder)(ref _003CAfterApplied_003Ed__6._003C_003Et__builder)).Task;
	}

	[AsyncStateMachine(typeof(_003CAfterRemoved_003Ed__6))]
	public override global::System.Threading.Tasks.Task AfterRemoved(Creature oldOwner)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		_003CAfterRemoved_003Ed__6 _003CAfterRemoved_003Ed__7 = default(_003CAfterRemoved_003Ed__6);
		_003CAfterRemoved_003Ed__7._003C_003Et__builder = AsyncTaskMethodBuilder.Create();
		_003CAfterRemoved_003Ed__7._003C_003E4__this = this;
		_003CAfterRemoved_003Ed__7.oldOwner = oldOwner;
		_003CAfterRemoved_003Ed__7._003C_003E1__state = -1;
		((AsyncTaskMethodBuilder)(ref _003CAfterRemoved_003Ed__7._003C_003Et__builder)).Start<_003CAfterRemoved_003Ed__6>(ref _003CAfterRemoved_003Ed__7);
		return ((AsyncTaskMethodBuilder)(ref _003CAfterRemoved_003Ed__7._003C_003Et__builder)).Task;
	}

	private Player? PlayerForOwner()
	{
		ICombatState combatState = ((PowerModel)this).Owner.CombatState;
		if (combatState == null)
		{
			return null;
		}
		return Enumerable.FirstOrDefault<Player>((global::System.Collections.Generic.IEnumerable<Player>)combatState.Players, (Func<Player, bool>)([CompilerGenerated] (Player p) => p.Creature == ((PowerModel)this).Owner));
	}

	private static global::System.Collections.Generic.IEnumerable<CardModel> AllCombatCards(Player player)
	{
		PlayerCombatState playerCombatState = player.PlayerCombatState;
		if (playerCombatState == null)
		{
			return global::System.Array.Empty<CardModel>();
		}
		return Enumerable.Concat<CardModel>(Enumerable.Concat<CardModel>(Enumerable.Concat<CardModel>((global::System.Collections.Generic.IEnumerable<CardModel>)playerCombatState.Hand.Cards, (global::System.Collections.Generic.IEnumerable<CardModel>)playerCombatState.DrawPile.Cards), (global::System.Collections.Generic.IEnumerable<CardModel>)playerCombatState.DiscardPile.Cards), (global::System.Collections.Generic.IEnumerable<CardModel>)playerCombatState.ExhaustPile.Cards);
	}

	[CompilerGenerated]
	[DebuggerHidden]
	private global::System.Threading.Tasks.Task _003C_003En__0(Creature? applier, CardModel? cardSource)
	{
		return ((PowerModel)this).AfterApplied(applier, cardSource);
	}

	[CompilerGenerated]
	[DebuggerHidden]
	private global::System.Threading.Tasks.Task _003C_003En__1(Creature oldOwner)
	{
		return ((PowerModel)this).AfterRemoved(oldOwner);
	}
}
