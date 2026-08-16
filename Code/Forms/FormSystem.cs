using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization;
using MegaCrit.Sts2.Core.Models;
using Zhao.Cards;
using Zhao.FoxFire;
using Zhao.Powers;
using Zhao.Pursuit;

namespace Zhao.Forms;

public static class FormSystem
{
	[StructLayout((LayoutKind)3)]
	[CompilerGenerated]
	private struct _003CEnterInterlude_003Ed__7 : IAsyncStateMachine
	{
		public int _003C_003E1__state;

		public AsyncTaskMethodBuilder _003C_003Et__builder;

		public Creature creature;

		public PlayerChoiceContext choiceContext;

		private TaskAwaiter<global::System.Collections.Generic.IEnumerable<CardModel>> _003C_003Eu__1;

		private TaskAwaiter _003C_003Eu__2;

		private void MoveNext()
		{
			//IL_012b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0130: Unknown result type (might be due to invalid IL or missing references)
			//IL_0138: Unknown result type (might be due to invalid IL or missing references)
			//IL_019c: Unknown result type (might be due to invalid IL or missing references)
			//IL_01a1: Unknown result type (might be due to invalid IL or missing references)
			//IL_01a9: Unknown result type (might be due to invalid IL or missing references)
			//IL_016a: Unknown result type (might be due to invalid IL or missing references)
			//IL_016f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0184: Unknown result type (might be due to invalid IL or missing references)
			//IL_0186: Unknown result type (might be due to invalid IL or missing references)
			//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
			//IL_00c8: Expected O, but got Unknown
			//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
			//IL_00f6: Unknown result type (might be due to invalid IL or missing references)
			//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
			//IL_0110: Unknown result type (might be due to invalid IL or missing references)
			//IL_0112: Unknown result type (might be due to invalid IL or missing references)
			int num = _003C_003E1__state;
			try
			{
				TaskAwaiter<global::System.Collections.Generic.IEnumerable<CardModel>> val;
				if (num == 0)
				{
					val = _003C_003Eu__1;
					_003C_003Eu__1 = default(TaskAwaiter<global::System.Collections.Generic.IEnumerable<CardModel>>);
					num = (_003C_003E1__state = -1);
					goto IL_0147;
				}
				TaskAwaiter val2;
				if (num == 1)
				{
					val2 = _003C_003Eu__2;
					_003C_003Eu__2 = default(TaskAwaiter);
					num = (_003C_003E1__state = -1);
					goto IL_01b8;
				}
				Player val3 = PlayerFor(creature);
				if (val3 != null)
				{
					PlayerCombatState playerCombatState = val3.PlayerCombatState;
					if (playerCombatState != null)
					{
						CardPile val4 = null;
						if (Enumerable.Any<CardModel>((global::System.Collections.Generic.IEnumerable<CardModel>)playerCombatState.DrawPile.Cards, (Func<CardModel, bool>)((CardModel c) => _003CEnterInterlude_003Eg__IsFormCard_007C7_2(c) && c.CanPlay())))
						{
							val4 = playerCombatState.DrawPile;
						}
						else if (Enumerable.Any<CardModel>((global::System.Collections.Generic.IEnumerable<CardModel>)playerCombatState.DiscardPile.Cards, (Func<CardModel, bool>)((CardModel c) => _003CEnterInterlude_003Eg__IsFormCard_007C7_2(c) && c.CanPlay())))
						{
							val4 = playerCombatState.DiscardPile;
						}
						if (val4 != null)
						{
							CardSelectorPrefs val5 = default(CardSelectorPrefs);
							((CardSelectorPrefs)(ref val5))._002Ector(new LocString("characters", "ZHAO_CHARACTER.interludeCardPrompt"), 1);
							val = CardSelectCmd.FromCombatPile(choiceContext, val4, val3, val5, (Func<CardModel, bool>)((CardModel c) => _003CEnterInterlude_003Eg__IsFormCard_007C7_2(c) && c.CanPlay())).GetAwaiter();
							if (!val.IsCompleted)
							{
								num = (_003C_003E1__state = 0);
								_003C_003Eu__1 = val;
								((AsyncTaskMethodBuilder)(ref _003C_003Et__builder)).AwaitUnsafeOnCompleted<TaskAwaiter<global::System.Collections.Generic.IEnumerable<CardModel>>, _003CEnterInterlude_003Ed__7>(ref val, ref this);
								return;
							}
							goto IL_0147;
						}
					}
				}
				goto end_IL_0007;
				IL_0147:
				CardModel val6 = Enumerable.FirstOrDefault<CardModel>(val.GetResult());
				if (val6 != null)
				{
					val2 = CardCmd.AutoPlay(choiceContext, val6, (Creature)null, (AutoPlayType)1, false, false).GetAwaiter();
					if (!((TaskAwaiter)(ref val2)).IsCompleted)
					{
						num = (_003C_003E1__state = 1);
						_003C_003Eu__2 = val2;
						((AsyncTaskMethodBuilder)(ref _003C_003Et__builder)).AwaitUnsafeOnCompleted<TaskAwaiter, _003CEnterInterlude_003Ed__7>(ref val2, ref this);
						return;
					}
					goto IL_01b8;
				}
				goto end_IL_0007;
				IL_01b8:
				((TaskAwaiter)(ref val2)).GetResult();
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

	[StructLayout((LayoutKind)3)]
	[CompilerGenerated]
	private struct _003CLeaveKitsuneForm_003Ed__2 : IAsyncStateMachine
	{
		public int _003C_003E1__state;

		public AsyncTaskMethodBuilder _003C_003Et__builder;

		public Creature creature;

		public PlayerChoiceContext choiceContext;

		private Player _003Cplayer_003E5__2;

		private int _003Cfire_003E5__3;

		private TaskAwaiter _003C_003Eu__1;

		private void MoveNext()
		{
			//IL_0093: Unknown result type (might be due to invalid IL or missing references)
			//IL_0098: Unknown result type (might be due to invalid IL or missing references)
			//IL_009f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0110: Unknown result type (might be due to invalid IL or missing references)
			//IL_0115: Unknown result type (might be due to invalid IL or missing references)
			//IL_011c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0189: Unknown result type (might be due to invalid IL or missing references)
			//IL_018e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0195: Unknown result type (might be due to invalid IL or missing references)
			//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
			//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
			//IL_00f6: Unknown result type (might be due to invalid IL or missing references)
			//IL_00f7: Unknown result type (might be due to invalid IL or missing references)
			//IL_0159: Unknown result type (might be due to invalid IL or missing references)
			//IL_015e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0060: Unknown result type (might be due to invalid IL or missing references)
			//IL_0065: Unknown result type (might be due to invalid IL or missing references)
			//IL_0172: Unknown result type (might be due to invalid IL or missing references)
			//IL_0173: Unknown result type (might be due to invalid IL or missing references)
			//IL_0079: Unknown result type (might be due to invalid IL or missing references)
			//IL_007a: Unknown result type (might be due to invalid IL or missing references)
			int num = _003C_003E1__state;
			try
			{
				TaskAwaiter val;
				switch (num)
				{
				default:
					_003Cplayer_003E5__2 = PlayerFor(creature);
					if (_003Cplayer_003E5__2 != null)
					{
						_003Cfire_003E5__3 = FoxFireCmd.Get(_003Cplayer_003E5__2);
						if (_003Cfire_003E5__3 > 0)
						{
							val = FoxFireCmd.Lose(1, _003Cplayer_003E5__2).GetAwaiter();
							if (!((TaskAwaiter)(ref val)).IsCompleted)
							{
								num = (_003C_003E1__state = 0);
								_003C_003Eu__1 = val;
								((AsyncTaskMethodBuilder)(ref _003C_003Et__builder)).AwaitUnsafeOnCompleted<TaskAwaiter, _003CLeaveKitsuneForm_003Ed__2>(ref val, ref this);
								return;
							}
							goto IL_00ae;
						}
					}
					goto end_IL_0007;
				case 0:
					val = _003C_003Eu__1;
					_003C_003Eu__1 = default(TaskAwaiter);
					num = (_003C_003E1__state = -1);
					goto IL_00ae;
				case 1:
					val = _003C_003Eu__1;
					_003C_003Eu__1 = default(TaskAwaiter);
					num = (_003C_003E1__state = -1);
					goto IL_012b;
				case 2:
					{
						val = _003C_003Eu__1;
						_003C_003Eu__1 = default(TaskAwaiter);
						num = (_003C_003E1__state = -1);
						break;
					}
					IL_012b:
					((TaskAwaiter)(ref val)).GetResult();
					if (_003Cfire_003E5__3 > 0)
					{
						val = PursuitExecutor.Chase(choiceContext, _003Cplayer_003E5__2, 1, decimal.op_Implicit(_003Cfire_003E5__3)).GetAwaiter();
						if (!((TaskAwaiter)(ref val)).IsCompleted)
						{
							num = (_003C_003E1__state = 2);
							_003C_003Eu__1 = val;
							((AsyncTaskMethodBuilder)(ref _003C_003Et__builder)).AwaitUnsafeOnCompleted<TaskAwaiter, _003CLeaveKitsuneForm_003Ed__2>(ref val, ref this);
							return;
						}
						break;
					}
					goto end_IL_0007;
					IL_00ae:
					((TaskAwaiter)(ref val)).GetResult();
					_003Cfire_003E5__3--;
					val = PursuitExecutor.Chase(choiceContext, _003Cplayer_003E5__2, 1, 1m).GetAwaiter();
					if (!((TaskAwaiter)(ref val)).IsCompleted)
					{
						num = (_003C_003E1__state = 1);
						_003C_003Eu__1 = val;
						((AsyncTaskMethodBuilder)(ref _003C_003Et__builder)).AwaitUnsafeOnCompleted<TaskAwaiter, _003CLeaveKitsuneForm_003Ed__2>(ref val, ref this);
						return;
					}
					goto IL_012b;
				}
				((TaskAwaiter)(ref val)).GetResult();
				end_IL_0007:;
			}
			catch (global::System.Exception exception)
			{
				_003C_003E1__state = -2;
				_003Cplayer_003E5__2 = null;
				((AsyncTaskMethodBuilder)(ref _003C_003Et__builder)).SetException(exception);
				return;
			}
			_003C_003E1__state = -2;
			_003Cplayer_003E5__2 = null;
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
	private struct _003CLeaveLadyForm_003Ed__4 : IAsyncStateMachine
	{
		public int _003C_003E1__state;

		public AsyncTaskMethodBuilder _003C_003Et__builder;

		public Creature creature;

		private TaskAwaiter _003C_003Eu__1;

		private void MoveNext()
		{
			//IL_0055: Unknown result type (might be due to invalid IL or missing references)
			//IL_005a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0061: Unknown result type (might be due to invalid IL or missing references)
			//IL_0025: Unknown result type (might be due to invalid IL or missing references)
			//IL_002a: Unknown result type (might be due to invalid IL or missing references)
			//IL_003e: Unknown result type (might be due to invalid IL or missing references)
			//IL_003f: Unknown result type (might be due to invalid IL or missing references)
			int num = _003C_003E1__state;
			try
			{
				TaskAwaiter val;
				if (num == 0)
				{
					val = _003C_003Eu__1;
					_003C_003Eu__1 = default(TaskAwaiter);
					num = (_003C_003E1__state = -1);
					goto IL_0070;
				}
				Player val2 = PlayerFor(creature);
				if (val2 != null)
				{
					val = PlayerCmd.GainEnergy(2m, val2).GetAwaiter();
					if (!((TaskAwaiter)(ref val)).IsCompleted)
					{
						num = (_003C_003E1__state = 0);
						_003C_003Eu__1 = val;
						((AsyncTaskMethodBuilder)(ref _003C_003Et__builder)).AwaitUnsafeOnCompleted<TaskAwaiter, _003CLeaveLadyForm_003Ed__4>(ref val, ref this);
						return;
					}
					goto IL_0070;
				}
				goto end_IL_0007;
				IL_0070:
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

	[StructLayout((LayoutKind)3)]
	[CompilerGenerated]
	private struct _003CLeaveNurseForm_003Ed__3 : IAsyncStateMachine
	{
		public int _003C_003E1__state;

		public AsyncTaskMethodBuilder _003C_003Et__builder;

		public Creature creature;

		private TaskAwaiter _003C_003Eu__1;

		private void MoveNext()
		{
			//IL_0069: Unknown result type (might be due to invalid IL or missing references)
			//IL_006e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0075: Unknown result type (might be due to invalid IL or missing references)
			//IL_0039: Unknown result type (might be due to invalid IL or missing references)
			//IL_003e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0052: Unknown result type (might be due to invalid IL or missing references)
			//IL_0053: Unknown result type (might be due to invalid IL or missing references)
			int num = _003C_003E1__state;
			try
			{
				TaskAwaiter val;
				if (num == 0)
				{
					val = _003C_003Eu__1;
					_003C_003Eu__1 = default(TaskAwaiter);
					num = (_003C_003E1__state = -1);
					goto IL_0084;
				}
				int powerAmount = creature.GetPowerAmount<HealingPower>();
				if (powerAmount > 0 && !creature.IsDead)
				{
					val = CreatureCmd.Heal(creature, decimal.op_Implicit(powerAmount), true).GetAwaiter();
					if (!((TaskAwaiter)(ref val)).IsCompleted)
					{
						num = (_003C_003E1__state = 0);
						_003C_003Eu__1 = val;
						((AsyncTaskMethodBuilder)(ref _003C_003Et__builder)).AwaitUnsafeOnCompleted<TaskAwaiter, _003CLeaveNurseForm_003Ed__3>(ref val, ref this);
						return;
					}
					goto IL_0084;
				}
				goto end_IL_0007;
				IL_0084:
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

	[StructLayout((LayoutKind)3)]
	[CompilerGenerated]
	private struct _003CSetStage_003Ed__6 : IAsyncStateMachine
	{
		public int _003C_003E1__state;

		public AsyncTaskMethodBuilder _003C_003Et__builder;

		public Creature creature;

		public PlayerChoiceContext choiceContext;

		public SectionStage stage;

		private TaskAwaiter<SectionPower?> _003C_003Eu__1;

		private TaskAwaiter<int> _003C_003Eu__2;

		private void MoveNext()
		{
			//IL_0077: Unknown result type (might be due to invalid IL or missing references)
			//IL_007c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0083: Unknown result type (might be due to invalid IL or missing references)
			//IL_0103: Unknown result type (might be due to invalid IL or missing references)
			//IL_0108: Unknown result type (might be due to invalid IL or missing references)
			//IL_010f: Unknown result type (might be due to invalid IL or missing references)
			//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
			//IL_00d3: Expected O, but got Unknown
			//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
			//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
			//IL_0044: Unknown result type (might be due to invalid IL or missing references)
			//IL_0049: Unknown result type (might be due to invalid IL or missing references)
			//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
			//IL_00ed: Unknown result type (might be due to invalid IL or missing references)
			//IL_005d: Unknown result type (might be due to invalid IL or missing references)
			//IL_005e: Unknown result type (might be due to invalid IL or missing references)
			int num = _003C_003E1__state;
			try
			{
				TaskAwaiter<SectionPower> val;
				if (num == 0)
				{
					val = _003C_003Eu__1;
					_003C_003Eu__1 = default(TaskAwaiter<SectionPower>);
					num = (_003C_003E1__state = -1);
					goto IL_0092;
				}
				TaskAwaiter<int> val2;
				if (num != 1)
				{
					SectionPower power = creature.GetPower<SectionPower>();
					if (power == null)
					{
						val = PowerCmd.Apply<SectionPower>(choiceContext, creature, decimal.op_Implicit((int)stage), creature, (CardModel)null, false).GetAwaiter();
						if (!val.IsCompleted)
						{
							num = (_003C_003E1__state = 0);
							_003C_003Eu__1 = val;
							((AsyncTaskMethodBuilder)(ref _003C_003Et__builder)).AwaitUnsafeOnCompleted<TaskAwaiter<SectionPower>, _003CSetStage_003Ed__6>(ref val, ref this);
							return;
						}
						goto IL_0092;
					}
					val2 = PowerCmd.ModifyAmount(choiceContext, (PowerModel)power, decimal.op_Implicit((int)stage) - decimal.op_Implicit(((PowerModel)power).Amount), creature, (CardModel)null, false).GetAwaiter();
					if (!val2.IsCompleted)
					{
						num = (_003C_003E1__state = 1);
						_003C_003Eu__2 = val2;
						((AsyncTaskMethodBuilder)(ref _003C_003Et__builder)).AwaitUnsafeOnCompleted<TaskAwaiter<int>, _003CSetStage_003Ed__6>(ref val2, ref this);
						return;
					}
				}
				else
				{
					val2 = _003C_003Eu__2;
					_003C_003Eu__2 = default(TaskAwaiter<int>);
					num = (_003C_003E1__state = -1);
				}
				val2.GetResult();
				goto end_IL_0007;
				IL_0092:
				val.GetResult();
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

	[StructLayout((LayoutKind)3)]
	[CompilerGenerated]
	private struct _003CSwitchForm_003Ed__1 : IAsyncStateMachine
	{
		public int _003C_003E1__state;

		public AsyncTaskMethodBuilder _003C_003Et__builder;

		public Creature creature;

		public ZhaoForm targetForm;

		public PlayerChoiceContext choiceContext;

		private bool _003CfromIntro_003E5__2;

		private TaskAwaiter _003C_003Eu__1;

		private Player _003Cp3_003E5__3;

		private TaskAwaiter<HealingPower?> _003C_003Eu__2;

		private TaskAwaiter<LightPower?> _003C_003Eu__3;

		private TaskAwaiter<KitsuneFormPower?> _003C_003Eu__4;

		private TaskAwaiter<NurseFormPower?> _003C_003Eu__5;

		private TaskAwaiter<DivaFormPower?> _003C_003Eu__6;

		private TaskAwaiter<LadyFormPower?> _003C_003Eu__7;

		private void MoveNext()
		{
			//IL_0102: Unknown result type (might be due to invalid IL or missing references)
			//IL_0107: Unknown result type (might be due to invalid IL or missing references)
			//IL_010f: Unknown result type (might be due to invalid IL or missing references)
			//IL_016a: Unknown result type (might be due to invalid IL or missing references)
			//IL_016f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0177: Unknown result type (might be due to invalid IL or missing references)
			//IL_01cf: Unknown result type (might be due to invalid IL or missing references)
			//IL_01d4: Unknown result type (might be due to invalid IL or missing references)
			//IL_01dc: Unknown result type (might be due to invalid IL or missing references)
			//IL_0293: Unknown result type (might be due to invalid IL or missing references)
			//IL_0298: Unknown result type (might be due to invalid IL or missing references)
			//IL_02a0: Unknown result type (might be due to invalid IL or missing references)
			//IL_0322: Unknown result type (might be due to invalid IL or missing references)
			//IL_0327: Unknown result type (might be due to invalid IL or missing references)
			//IL_032f: Unknown result type (might be due to invalid IL or missing references)
			//IL_039e: Unknown result type (might be due to invalid IL or missing references)
			//IL_03a3: Unknown result type (might be due to invalid IL or missing references)
			//IL_03ab: Unknown result type (might be due to invalid IL or missing references)
			//IL_0421: Unknown result type (might be due to invalid IL or missing references)
			//IL_0426: Unknown result type (might be due to invalid IL or missing references)
			//IL_042e: Unknown result type (might be due to invalid IL or missing references)
			//IL_049c: Unknown result type (might be due to invalid IL or missing references)
			//IL_04a1: Unknown result type (might be due to invalid IL or missing references)
			//IL_04a9: Unknown result type (might be due to invalid IL or missing references)
			//IL_0536: Unknown result type (might be due to invalid IL or missing references)
			//IL_053b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0543: Unknown result type (might be due to invalid IL or missing references)
			//IL_059a: Unknown result type (might be due to invalid IL or missing references)
			//IL_059f: Unknown result type (might be due to invalid IL or missing references)
			//IL_05a7: Unknown result type (might be due to invalid IL or missing references)
			//IL_05fe: Unknown result type (might be due to invalid IL or missing references)
			//IL_0603: Unknown result type (might be due to invalid IL or missing references)
			//IL_060b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0662: Unknown result type (might be due to invalid IL or missing references)
			//IL_0667: Unknown result type (might be due to invalid IL or missing references)
			//IL_066f: Unknown result type (might be due to invalid IL or missing references)
			//IL_06c6: Unknown result type (might be due to invalid IL or missing references)
			//IL_06cb: Unknown result type (might be due to invalid IL or missing references)
			//IL_06d3: Unknown result type (might be due to invalid IL or missing references)
			//IL_0763: Unknown result type (might be due to invalid IL or missing references)
			//IL_0768: Unknown result type (might be due to invalid IL or missing references)
			//IL_0770: Unknown result type (might be due to invalid IL or missing references)
			//IL_07e0: Unknown result type (might be due to invalid IL or missing references)
			//IL_07e5: Unknown result type (might be due to invalid IL or missing references)
			//IL_07ed: Unknown result type (might be due to invalid IL or missing references)
			//IL_085d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0862: Unknown result type (might be due to invalid IL or missing references)
			//IL_086a: Unknown result type (might be due to invalid IL or missing references)
			//IL_08df: Unknown result type (might be due to invalid IL or missing references)
			//IL_08e4: Unknown result type (might be due to invalid IL or missing references)
			//IL_08ec: Unknown result type (might be due to invalid IL or missing references)
			//IL_02ed: Unknown result type (might be due to invalid IL or missing references)
			//IL_02f2: Unknown result type (might be due to invalid IL or missing references)
			//IL_03ec: Unknown result type (might be due to invalid IL or missing references)
			//IL_03f1: Unknown result type (might be due to invalid IL or missing references)
			//IL_05c8: Unknown result type (might be due to invalid IL or missing references)
			//IL_05cd: Unknown result type (might be due to invalid IL or missing references)
			//IL_062c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0631: Unknown result type (might be due to invalid IL or missing references)
			//IL_0690: Unknown result type (might be due to invalid IL or missing references)
			//IL_0695: Unknown result type (might be due to invalid IL or missing references)
			//IL_0307: Unknown result type (might be due to invalid IL or missing references)
			//IL_0309: Unknown result type (might be due to invalid IL or missing references)
			//IL_0564: Unknown result type (might be due to invalid IL or missing references)
			//IL_0569: Unknown result type (might be due to invalid IL or missing references)
			//IL_0406: Unknown result type (might be due to invalid IL or missing references)
			//IL_0408: Unknown result type (might be due to invalid IL or missing references)
			//IL_05e3: Unknown result type (might be due to invalid IL or missing references)
			//IL_05e5: Unknown result type (might be due to invalid IL or missing references)
			//IL_0647: Unknown result type (might be due to invalid IL or missing references)
			//IL_0649: Unknown result type (might be due to invalid IL or missing references)
			//IL_06ab: Unknown result type (might be due to invalid IL or missing references)
			//IL_06ad: Unknown result type (might be due to invalid IL or missing references)
			//IL_072d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0732: Unknown result type (might be due to invalid IL or missing references)
			//IL_07aa: Unknown result type (might be due to invalid IL or missing references)
			//IL_07af: Unknown result type (might be due to invalid IL or missing references)
			//IL_0827: Unknown result type (might be due to invalid IL or missing references)
			//IL_082c: Unknown result type (might be due to invalid IL or missing references)
			//IL_08ac: Unknown result type (might be due to invalid IL or missing references)
			//IL_08b1: Unknown result type (might be due to invalid IL or missing references)
			//IL_057f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0581: Unknown result type (might be due to invalid IL or missing references)
			//IL_0501: Unknown result type (might be due to invalid IL or missing references)
			//IL_0506: Unknown result type (might be due to invalid IL or missing references)
			//IL_0748: Unknown result type (might be due to invalid IL or missing references)
			//IL_074a: Unknown result type (might be due to invalid IL or missing references)
			//IL_07c5: Unknown result type (might be due to invalid IL or missing references)
			//IL_07c7: Unknown result type (might be due to invalid IL or missing references)
			//IL_0842: Unknown result type (might be due to invalid IL or missing references)
			//IL_0844: Unknown result type (might be due to invalid IL or missing references)
			//IL_08c7: Unknown result type (might be due to invalid IL or missing references)
			//IL_08c9: Unknown result type (might be due to invalid IL or missing references)
			//IL_051b: Unknown result type (might be due to invalid IL or missing references)
			//IL_051d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0369: Unknown result type (might be due to invalid IL or missing references)
			//IL_036e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0467: Unknown result type (might be due to invalid IL or missing references)
			//IL_046c: Unknown result type (might be due to invalid IL or missing references)
			//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
			//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
			//IL_0135: Unknown result type (might be due to invalid IL or missing references)
			//IL_013a: Unknown result type (might be due to invalid IL or missing references)
			//IL_019a: Unknown result type (might be due to invalid IL or missing references)
			//IL_019f: Unknown result type (might be due to invalid IL or missing references)
			//IL_025e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0263: Unknown result type (might be due to invalid IL or missing references)
			//IL_0383: Unknown result type (might be due to invalid IL or missing references)
			//IL_0385: Unknown result type (might be due to invalid IL or missing references)
			//IL_0481: Unknown result type (might be due to invalid IL or missing references)
			//IL_0483: Unknown result type (might be due to invalid IL or missing references)
			//IL_00e7: Unknown result type (might be due to invalid IL or missing references)
			//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
			//IL_014f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0151: Unknown result type (might be due to invalid IL or missing references)
			//IL_01b4: Unknown result type (might be due to invalid IL or missing references)
			//IL_01b6: Unknown result type (might be due to invalid IL or missing references)
			//IL_0278: Unknown result type (might be due to invalid IL or missing references)
			//IL_027a: Unknown result type (might be due to invalid IL or missing references)
			int num = _003C_003E1__state;
			try
			{
				TaskAwaiter val5;
				TaskAwaiter<HealingPower> val7;
				TaskAwaiter<LightPower> val6;
				TaskAwaiter<KitsuneFormPower> val4;
				TaskAwaiter<NurseFormPower> val3;
				TaskAwaiter<DivaFormPower> val2;
				TaskAwaiter<LadyFormPower> val;
				int powerAmount;
				int num3;
				decimal damagePerHit;
				int powerAmount2;
				Player val8;
				switch (num)
				{
				default:
				{
					ZhaoForm currentForm = GetCurrentForm(creature);
					SectionPower section = GetSection(creature);
					int num2 = ((currentForm == ZhaoForm.Diva) ? ((section != null && section.Stage == SectionStage.Intro) ? 1 : 0) : 0);
					_003CfromIntro_003E5__2 = (byte)num2 != 0;
					if (currentForm != ZhaoForm.None && currentForm != targetForm)
					{
						switch (currentForm)
						{
						case ZhaoForm.Kitsune:
							break;
						case ZhaoForm.Nurse:
							goto IL_012a;
						case ZhaoForm.Lady:
							goto IL_018f;
						case ZhaoForm.Diva:
							DivaVideoBackground.HideFromDivaForm();
							goto IL_01f9;
						default:
							goto IL_01f9;
						}
						val5 = LeaveKitsuneForm(choiceContext, creature).GetAwaiter();
						if (!((TaskAwaiter)(ref val5)).IsCompleted)
						{
							num = (_003C_003E1__state = 0);
							_003C_003Eu__1 = val5;
							((AsyncTaskMethodBuilder)(ref _003C_003Et__builder)).AwaitUnsafeOnCompleted<TaskAwaiter, _003CSwitchForm_003Ed__1>(ref val5, ref this);
							return;
						}
						goto IL_011e;
					}
					goto IL_01f9;
				}
				case 0:
					val5 = _003C_003Eu__1;
					_003C_003Eu__1 = default(TaskAwaiter);
					num = (_003C_003E1__state = -1);
					goto IL_011e;
				case 1:
					val5 = _003C_003Eu__1;
					_003C_003Eu__1 = default(TaskAwaiter);
					num = (_003C_003E1__state = -1);
					goto IL_0186;
				case 2:
					val5 = _003C_003Eu__1;
					_003C_003Eu__1 = default(TaskAwaiter);
					num = (_003C_003E1__state = -1);
					goto IL_01eb;
				case 3:
					val5 = _003C_003Eu__1;
					_003C_003Eu__1 = default(TaskAwaiter);
					num = (_003C_003E1__state = -1);
					goto IL_02af;
				case 4:
					val5 = _003C_003Eu__1;
					_003C_003Eu__1 = default(TaskAwaiter);
					num = (_003C_003E1__state = -1);
					goto IL_033e;
				case 5:
					val7 = _003C_003Eu__2;
					_003C_003Eu__2 = default(TaskAwaiter<HealingPower>);
					num = (_003C_003E1__state = -1);
					goto IL_03ba;
				case 6:
					val5 = _003C_003Eu__1;
					_003C_003Eu__1 = default(TaskAwaiter);
					num = (_003C_003E1__state = -1);
					goto IL_043d;
				case 7:
					val6 = _003C_003Eu__3;
					_003C_003Eu__3 = default(TaskAwaiter<LightPower>);
					num = (_003C_003E1__state = -1);
					goto IL_04b8;
				case 8:
					val5 = _003C_003Eu__1;
					_003C_003Eu__1 = default(TaskAwaiter);
					num = (_003C_003E1__state = -1);
					goto IL_0552;
				case 9:
					val5 = _003C_003Eu__1;
					_003C_003Eu__1 = default(TaskAwaiter);
					num = (_003C_003E1__state = -1);
					goto IL_05b6;
				case 10:
					val5 = _003C_003Eu__1;
					_003C_003Eu__1 = default(TaskAwaiter);
					num = (_003C_003E1__state = -1);
					goto IL_061a;
				case 11:
					val5 = _003C_003Eu__1;
					_003C_003Eu__1 = default(TaskAwaiter);
					num = (_003C_003E1__state = -1);
					goto IL_067e;
				case 12:
					val5 = _003C_003Eu__1;
					_003C_003Eu__1 = default(TaskAwaiter);
					num = (_003C_003E1__state = -1);
					goto IL_06e2;
				case 13:
					val4 = _003C_003Eu__4;
					_003C_003Eu__4 = default(TaskAwaiter<KitsuneFormPower>);
					num = (_003C_003E1__state = -1);
					goto IL_077f;
				case 14:
					val3 = _003C_003Eu__5;
					_003C_003Eu__5 = default(TaskAwaiter<NurseFormPower>);
					num = (_003C_003E1__state = -1);
					goto IL_07fc;
				case 15:
					val2 = _003C_003Eu__6;
					_003C_003Eu__6 = default(TaskAwaiter<DivaFormPower>);
					num = (_003C_003E1__state = -1);
					goto IL_0879;
				case 16:
					{
						val = _003C_003Eu__7;
						_003C_003Eu__7 = default(TaskAwaiter<LadyFormPower>);
						num = (_003C_003E1__state = -1);
						break;
					}
					IL_0552:
					((TaskAwaiter)(ref val5)).GetResult();
					goto IL_0559;
					IL_0809:
					val2 = PowerCmd.Apply<DivaFormPower>(choiceContext, creature, 1m, creature, (CardModel)null, false).GetAwaiter();
					if (!val2.IsCompleted)
					{
						num = (_003C_003E1__state = 15);
						_003C_003Eu__6 = val2;
						((AsyncTaskMethodBuilder)(ref _003C_003Et__builder)).AwaitUnsafeOnCompleted<TaskAwaiter<DivaFormPower>, _003CSwitchForm_003Ed__1>(ref val2, ref this);
						return;
					}
					goto IL_0879;
					IL_043d:
					((TaskAwaiter)(ref val5)).GetResult();
					goto IL_0559;
					IL_03ba:
					val7.GetResult();
					powerAmount = creature.GetPowerAmount<HealingPower>();
					val5 = CreatureCmd.Heal(creature, Math.Max(1m, decimal.op_Implicit(powerAmount)), true).GetAwaiter();
					if (!((TaskAwaiter)(ref val5)).IsCompleted)
					{
						num = (_003C_003E1__state = 6);
						_003C_003Eu__1 = val5;
						((AsyncTaskMethodBuilder)(ref _003C_003Et__builder)).AwaitUnsafeOnCompleted<TaskAwaiter, _003CSwitchForm_003Ed__1>(ref val5, ref this);
						return;
					}
					goto IL_043d;
					IL_067e:
					((TaskAwaiter)(ref val5)).GetResult();
					val5 = PowerCmd.Remove<LadyFormPower>(creature).GetAwaiter();
					if (!((TaskAwaiter)(ref val5)).IsCompleted)
					{
						num = (_003C_003E1__state = 12);
						_003C_003Eu__1 = val5;
						((AsyncTaskMethodBuilder)(ref _003C_003Et__builder)).AwaitUnsafeOnCompleted<TaskAwaiter, _003CSwitchForm_003Ed__1>(ref val5, ref this);
						return;
					}
					goto IL_06e2;
					IL_034a:
					val7 = PowerCmd.Apply<HealingPower>(choiceContext, creature, 5m, creature, (CardModel)null, false).GetAwaiter();
					if (!val7.IsCompleted)
					{
						num = (_003C_003E1__state = 5);
						_003C_003Eu__2 = val7;
						((AsyncTaskMethodBuilder)(ref _003C_003Et__builder)).AwaitUnsafeOnCompleted<TaskAwaiter<HealingPower>, _003CSwitchForm_003Ed__1>(ref val7, ref this);
						return;
					}
					goto IL_03ba;
					IL_07fc:
					val3.GetResult();
					goto end_IL_0007;
					IL_05b6:
					((TaskAwaiter)(ref val5)).GetResult();
					val5 = PowerCmd.Remove<NurseFormPower>(creature).GetAwaiter();
					if (!((TaskAwaiter)(ref val5)).IsCompleted)
					{
						num = (_003C_003E1__state = 10);
						_003C_003Eu__1 = val5;
						((AsyncTaskMethodBuilder)(ref _003C_003Et__builder)).AwaitUnsafeOnCompleted<TaskAwaiter, _003CSwitchForm_003Ed__1>(ref val5, ref this);
						return;
					}
					goto IL_061a;
					IL_0879:
					val2.GetResult();
					DivaVideoBackground.ShowForDivaForm(creature);
					goto end_IL_0007;
					IL_018f:
					val5 = LeaveLadyForm(creature).GetAwaiter();
					if (!((TaskAwaiter)(ref val5)).IsCompleted)
					{
						num = (_003C_003E1__state = 2);
						_003C_003Eu__1 = val5;
						((AsyncTaskMethodBuilder)(ref _003C_003Et__builder)).AwaitUnsafeOnCompleted<TaskAwaiter, _003CSwitchForm_003Ed__1>(ref val5, ref this);
						return;
					}
					goto IL_01eb;
					IL_077f:
					val4.GetResult();
					goto end_IL_0007;
					IL_01eb:
					((TaskAwaiter)(ref val5)).GetResult();
					goto IL_01f9;
					IL_012a:
					val5 = LeaveNurseForm(creature).GetAwaiter();
					if (!((TaskAwaiter)(ref val5)).IsCompleted)
					{
						num = (_003C_003E1__state = 1);
						_003C_003Eu__1 = val5;
						((AsyncTaskMethodBuilder)(ref _003C_003Et__builder)).AwaitUnsafeOnCompleted<TaskAwaiter, _003CSwitchForm_003Ed__1>(ref val5, ref this);
						return;
					}
					goto IL_0186;
					IL_033e:
					((TaskAwaiter)(ref val5)).GetResult();
					goto IL_0559;
					IL_0186:
					((TaskAwaiter)(ref val5)).GetResult();
					goto IL_01f9;
					IL_061a:
					((TaskAwaiter)(ref val5)).GetResult();
					val5 = PowerCmd.Remove<DivaFormPower>(creature).GetAwaiter();
					if (!((TaskAwaiter)(ref val5)).IsCompleted)
					{
						num = (_003C_003E1__state = 11);
						_003C_003Eu__1 = val5;
						((AsyncTaskMethodBuilder)(ref _003C_003Et__builder)).AwaitUnsafeOnCompleted<TaskAwaiter, _003CSwitchForm_003Ed__1>(ref val5, ref this);
						return;
					}
					goto IL_067e;
					IL_02af:
					((TaskAwaiter)(ref val5)).GetResult();
					num3 = FoxFireCmd.Get(_003Cp3_003E5__3);
					damagePerHit = Math.Max(1m, decimal.op_Implicit(num3 / 2));
					val5 = PursuitExecutor.Chase(choiceContext, _003Cp3_003E5__3, 1, damagePerHit).GetAwaiter();
					if (!((TaskAwaiter)(ref val5)).IsCompleted)
					{
						num = (_003C_003E1__state = 4);
						_003C_003Eu__1 = val5;
						((AsyncTaskMethodBuilder)(ref _003C_003Et__builder)).AwaitUnsafeOnCompleted<TaskAwaiter, _003CSwitchForm_003Ed__1>(ref val5, ref this);
						return;
					}
					goto IL_033e;
					IL_011e:
					((TaskAwaiter)(ref val5)).GetResult();
					goto IL_01f9;
					IL_01f9:
					if (_003CfromIntro_003E5__2 && targetForm != ZhaoForm.Diva)
					{
						switch (targetForm)
						{
						case ZhaoForm.Kitsune:
							break;
						case ZhaoForm.Nurse:
							goto IL_034a;
						case ZhaoForm.Lady:
							goto IL_0449;
						default:
							goto IL_0559;
						}
						_003Cp3_003E5__3 = PlayerFor(creature);
						if (_003Cp3_003E5__3 != null)
						{
							val5 = FoxFireCmd.Gain(1, _003Cp3_003E5__3).GetAwaiter();
							if (!((TaskAwaiter)(ref val5)).IsCompleted)
							{
								num = (_003C_003E1__state = 3);
								_003C_003Eu__1 = val5;
								((AsyncTaskMethodBuilder)(ref _003C_003Et__builder)).AwaitUnsafeOnCompleted<TaskAwaiter, _003CSwitchForm_003Ed__1>(ref val5, ref this);
								return;
							}
							goto IL_02af;
						}
					}
					goto IL_0559;
					IL_0559:
					val5 = PowerCmd.Remove<KitsuneFormPower>(creature).GetAwaiter();
					if (!((TaskAwaiter)(ref val5)).IsCompleted)
					{
						num = (_003C_003E1__state = 9);
						_003C_003Eu__1 = val5;
						((AsyncTaskMethodBuilder)(ref _003C_003Et__builder)).AwaitUnsafeOnCompleted<TaskAwaiter, _003CSwitchForm_003Ed__1>(ref val5, ref this);
						return;
					}
					goto IL_05b6;
					IL_070f:
					val4 = PowerCmd.Apply<KitsuneFormPower>(choiceContext, creature, 1m, creature, (CardModel)null, false).GetAwaiter();
					if (!val4.IsCompleted)
					{
						num = (_003C_003E1__state = 13);
						_003C_003Eu__4 = val4;
						((AsyncTaskMethodBuilder)(ref _003C_003Et__builder)).AwaitUnsafeOnCompleted<TaskAwaiter<KitsuneFormPower>, _003CSwitchForm_003Ed__1>(ref val4, ref this);
						return;
					}
					goto IL_077f;
					IL_0449:
					val6 = PowerCmd.Apply<LightPower>(choiceContext, creature, 1m, creature, (CardModel)null, false).GetAwaiter();
					if (!val6.IsCompleted)
					{
						num = (_003C_003E1__state = 7);
						_003C_003Eu__3 = val6;
						((AsyncTaskMethodBuilder)(ref _003C_003Et__builder)).AwaitUnsafeOnCompleted<TaskAwaiter<LightPower>, _003CSwitchForm_003Ed__1>(ref val6, ref this);
						return;
					}
					goto IL_04b8;
					IL_078c:
					val3 = PowerCmd.Apply<NurseFormPower>(choiceContext, creature, 1m, creature, (CardModel)null, false).GetAwaiter();
					if (!val3.IsCompleted)
					{
						num = (_003C_003E1__state = 14);
						_003C_003Eu__5 = val3;
						((AsyncTaskMethodBuilder)(ref _003C_003Et__builder)).AwaitUnsafeOnCompleted<TaskAwaiter<NurseFormPower>, _003CSwitchForm_003Ed__1>(ref val3, ref this);
						return;
					}
					goto IL_07fc;
					IL_04b8:
					val6.GetResult();
					powerAmount2 = creature.GetPowerAmount<LightPower>();
					val8 = PlayerFor(creature);
					if (val8 != null && powerAmount2 > 0)
					{
						val5 = PlayerCmd.GainEnergy(decimal.Floor(decimal.op_Implicit(powerAmount2) / 2m), val8).GetAwaiter();
						if (!((TaskAwaiter)(ref val5)).IsCompleted)
						{
							num = (_003C_003E1__state = 8);
							_003C_003Eu__1 = val5;
							((AsyncTaskMethodBuilder)(ref _003C_003Et__builder)).AwaitUnsafeOnCompleted<TaskAwaiter, _003CSwitchForm_003Ed__1>(ref val5, ref this);
							return;
						}
						goto IL_0552;
					}
					goto IL_0559;
					IL_06e2:
					((TaskAwaiter)(ref val5)).GetResult();
					switch (targetForm)
					{
					case ZhaoForm.Kitsune:
						goto IL_070f;
					case ZhaoForm.Nurse:
						goto IL_078c;
					case ZhaoForm.Diva:
						goto IL_0809;
					case ZhaoForm.Lady:
						goto IL_088e;
					}
					goto end_IL_0007;
					IL_088e:
					val = PowerCmd.Apply<LadyFormPower>(choiceContext, creature, 1m, creature, (CardModel)null, false).GetAwaiter();
					if (!val.IsCompleted)
					{
						num = (_003C_003E1__state = 16);
						_003C_003Eu__7 = val;
						((AsyncTaskMethodBuilder)(ref _003C_003Et__builder)).AwaitUnsafeOnCompleted<TaskAwaiter<LadyFormPower>, _003CSwitchForm_003Ed__1>(ref val, ref this);
						return;
					}
					break;
				}
				val.GetResult();
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

	public static ZhaoForm GetCurrentForm(Creature creature)
	{
		if (creature.HasPower<KitsuneFormPower>())
		{
			return ZhaoForm.Kitsune;
		}
		if (creature.HasPower<NurseFormPower>())
		{
			return ZhaoForm.Nurse;
		}
		if (creature.HasPower<DivaFormPower>())
		{
			return ZhaoForm.Diva;
		}
		if (creature.HasPower<LadyFormPower>())
		{
			return ZhaoForm.Lady;
		}
		return ZhaoForm.None;
	}

	[AsyncStateMachine(typeof(_003CSwitchForm_003Ed__1))]
	public static global::System.Threading.Tasks.Task SwitchForm(PlayerChoiceContext choiceContext, Creature creature, ZhaoForm targetForm)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		_003CSwitchForm_003Ed__1 _003CSwitchForm_003Ed__2 = default(_003CSwitchForm_003Ed__1);
		_003CSwitchForm_003Ed__2._003C_003Et__builder = AsyncTaskMethodBuilder.Create();
		_003CSwitchForm_003Ed__2.choiceContext = choiceContext;
		_003CSwitchForm_003Ed__2.creature = creature;
		_003CSwitchForm_003Ed__2.targetForm = targetForm;
		_003CSwitchForm_003Ed__2._003C_003E1__state = -1;
		((AsyncTaskMethodBuilder)(ref _003CSwitchForm_003Ed__2._003C_003Et__builder)).Start<_003CSwitchForm_003Ed__1>(ref _003CSwitchForm_003Ed__2);
		return ((AsyncTaskMethodBuilder)(ref _003CSwitchForm_003Ed__2._003C_003Et__builder)).Task;
	}

	[AsyncStateMachine(typeof(_003CLeaveKitsuneForm_003Ed__2))]
	private static global::System.Threading.Tasks.Task LeaveKitsuneForm(PlayerChoiceContext choiceContext, Creature creature)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		_003CLeaveKitsuneForm_003Ed__2 _003CLeaveKitsuneForm_003Ed__3 = default(_003CLeaveKitsuneForm_003Ed__2);
		_003CLeaveKitsuneForm_003Ed__3._003C_003Et__builder = AsyncTaskMethodBuilder.Create();
		_003CLeaveKitsuneForm_003Ed__3.choiceContext = choiceContext;
		_003CLeaveKitsuneForm_003Ed__3.creature = creature;
		_003CLeaveKitsuneForm_003Ed__3._003C_003E1__state = -1;
		((AsyncTaskMethodBuilder)(ref _003CLeaveKitsuneForm_003Ed__3._003C_003Et__builder)).Start<_003CLeaveKitsuneForm_003Ed__2>(ref _003CLeaveKitsuneForm_003Ed__3);
		return ((AsyncTaskMethodBuilder)(ref _003CLeaveKitsuneForm_003Ed__3._003C_003Et__builder)).Task;
	}

	[AsyncStateMachine(typeof(_003CLeaveNurseForm_003Ed__3))]
	private static global::System.Threading.Tasks.Task LeaveNurseForm(Creature creature)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		_003CLeaveNurseForm_003Ed__3 _003CLeaveNurseForm_003Ed__4 = default(_003CLeaveNurseForm_003Ed__3);
		_003CLeaveNurseForm_003Ed__4._003C_003Et__builder = AsyncTaskMethodBuilder.Create();
		_003CLeaveNurseForm_003Ed__4.creature = creature;
		_003CLeaveNurseForm_003Ed__4._003C_003E1__state = -1;
		((AsyncTaskMethodBuilder)(ref _003CLeaveNurseForm_003Ed__4._003C_003Et__builder)).Start<_003CLeaveNurseForm_003Ed__3>(ref _003CLeaveNurseForm_003Ed__4);
		return ((AsyncTaskMethodBuilder)(ref _003CLeaveNurseForm_003Ed__4._003C_003Et__builder)).Task;
	}

	[AsyncStateMachine(typeof(_003CLeaveLadyForm_003Ed__4))]
	private static global::System.Threading.Tasks.Task LeaveLadyForm(Creature creature)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		_003CLeaveLadyForm_003Ed__4 _003CLeaveLadyForm_003Ed__5 = default(_003CLeaveLadyForm_003Ed__4);
		_003CLeaveLadyForm_003Ed__5._003C_003Et__builder = AsyncTaskMethodBuilder.Create();
		_003CLeaveLadyForm_003Ed__5.creature = creature;
		_003CLeaveLadyForm_003Ed__5._003C_003E1__state = -1;
		((AsyncTaskMethodBuilder)(ref _003CLeaveLadyForm_003Ed__5._003C_003Et__builder)).Start<_003CLeaveLadyForm_003Ed__4>(ref _003CLeaveLadyForm_003Ed__5);
		return ((AsyncTaskMethodBuilder)(ref _003CLeaveLadyForm_003Ed__5._003C_003Et__builder)).Task;
	}

	public static SectionPower? GetSection(Creature creature)
	{
		return creature.GetPower<SectionPower>();
	}

	[AsyncStateMachine(typeof(_003CSetStage_003Ed__6))]
	public static global::System.Threading.Tasks.Task SetStage(PlayerChoiceContext choiceContext, Creature creature, SectionStage stage)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		_003CSetStage_003Ed__6 _003CSetStage_003Ed__7 = default(_003CSetStage_003Ed__6);
		_003CSetStage_003Ed__7._003C_003Et__builder = AsyncTaskMethodBuilder.Create();
		_003CSetStage_003Ed__7.choiceContext = choiceContext;
		_003CSetStage_003Ed__7.creature = creature;
		_003CSetStage_003Ed__7.stage = stage;
		_003CSetStage_003Ed__7._003C_003E1__state = -1;
		((AsyncTaskMethodBuilder)(ref _003CSetStage_003Ed__7._003C_003Et__builder)).Start<_003CSetStage_003Ed__6>(ref _003CSetStage_003Ed__7);
		return ((AsyncTaskMethodBuilder)(ref _003CSetStage_003Ed__7._003C_003Et__builder)).Task;
	}

	[AsyncStateMachine(typeof(_003CEnterInterlude_003Ed__7))]
	public static global::System.Threading.Tasks.Task EnterInterlude(PlayerChoiceContext choiceContext, Creature creature)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		_003CEnterInterlude_003Ed__7 _003CEnterInterlude_003Ed__8 = default(_003CEnterInterlude_003Ed__7);
		_003CEnterInterlude_003Ed__8._003C_003Et__builder = AsyncTaskMethodBuilder.Create();
		_003CEnterInterlude_003Ed__8.choiceContext = choiceContext;
		_003CEnterInterlude_003Ed__8.creature = creature;
		_003CEnterInterlude_003Ed__8._003C_003E1__state = -1;
		((AsyncTaskMethodBuilder)(ref _003CEnterInterlude_003Ed__8._003C_003Et__builder)).Start<_003CEnterInterlude_003Ed__7>(ref _003CEnterInterlude_003Ed__8);
		return ((AsyncTaskMethodBuilder)(ref _003CEnterInterlude_003Ed__8._003C_003Et__builder)).Task;
	}

	public static Player? PlayerFor(Creature creature)
	{
		ICombatState combatState = creature.CombatState;
		if (combatState == null)
		{
			return null;
		}
		return Enumerable.FirstOrDefault<Player>((global::System.Collections.Generic.IEnumerable<Player>)combatState.Players, (Func<Player, bool>)((Player p) => p.Creature == creature));
	}

	[CompilerGenerated]
	internal static bool _003CEnterInterlude_003Eg__IsFormCard_007C7_2(CardModel c)
	{
		if (c is SectionIntro || c is LightCard || c is EmergencyTreatment)
		{
			return true;
		}
		return false;
	}
}
