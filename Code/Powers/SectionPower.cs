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
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using Zhao.Forms;
using Zhao.FoxFire;
using Zhao.Pursuit;

namespace Zhao.Powers;

public class SectionPower : PowerModel
{
	[StructLayout((LayoutKind)3)]
	[CompilerGenerated]
	private struct _003CAfterPowerAmountChanged_003Ed__14 : IAsyncStateMachine
	{
		public int _003C_003E1__state;

		public AsyncTaskMethodBuilder _003C_003Et__builder;

		public PowerModel power;

		public SectionPower _003C_003E4__this;

		public PlayerChoiceContext choiceContext;

		private TaskAwaiter _003C_003Eu__1;

		private void MoveNext()
		{
			//IL_005f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0064: Unknown result type (might be due to invalid IL or missing references)
			//IL_006b: Unknown result type (might be due to invalid IL or missing references)
			//IL_002f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0034: Unknown result type (might be due to invalid IL or missing references)
			//IL_0048: Unknown result type (might be due to invalid IL or missing references)
			//IL_0049: Unknown result type (might be due to invalid IL or missing references)
			int num = _003C_003E1__state;
			SectionPower sectionPower = _003C_003E4__this;
			try
			{
				TaskAwaiter val;
				if (num == 0)
				{
					val = _003C_003Eu__1;
					_003C_003Eu__1 = default(TaskAwaiter);
					num = (_003C_003E1__state = -1);
					goto IL_007a;
				}
				if ((object)power == sectionPower && sectionPower.Stage == SectionStage.Outro)
				{
					val = sectionPower.OutroSettlement(choiceContext).GetAwaiter();
					if (!((TaskAwaiter)(ref val)).IsCompleted)
					{
						num = (_003C_003E1__state = 0);
						_003C_003Eu__1 = val;
						((AsyncTaskMethodBuilder)(ref _003C_003Et__builder)).AwaitUnsafeOnCompleted<TaskAwaiter, _003CAfterPowerAmountChanged_003Ed__14>(ref val, ref this);
						return;
					}
					goto IL_007a;
				}
				goto end_IL_000e;
				IL_007a:
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

	[StructLayout((LayoutKind)3)]
	[CompilerGenerated]
	private struct _003CAfterSideTurnEnd_003Ed__13 : IAsyncStateMachine
	{
		public int _003C_003E1__state;

		public AsyncTaskMethodBuilder _003C_003Et__builder;

		public CombatSide side;

		public global::System.Collections.Generic.IEnumerable<Creature> participants;

		public SectionPower _003C_003E4__this;

		public PlayerChoiceContext choiceContext;

		private TaskAwaiter<int> _003C_003Eu__1;

		private TaskAwaiter _003C_003Eu__2;

		private void MoveNext()
		{
			//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
			//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
			//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
			//IL_0111: Unknown result type (might be due to invalid IL or missing references)
			//IL_0116: Unknown result type (might be due to invalid IL or missing references)
			//IL_011d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0174: Unknown result type (might be due to invalid IL or missing references)
			//IL_0179: Unknown result type (might be due to invalid IL or missing references)
			//IL_0180: Unknown result type (might be due to invalid IL or missing references)
			//IL_0021: Unknown result type (might be due to invalid IL or missing references)
			//IL_0027: Invalid comparison between Unknown and I4
			//IL_00de: Unknown result type (might be due to invalid IL or missing references)
			//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
			//IL_0144: Unknown result type (might be due to invalid IL or missing references)
			//IL_0149: Unknown result type (might be due to invalid IL or missing references)
			//IL_00f7: Unknown result type (might be due to invalid IL or missing references)
			//IL_00f8: Unknown result type (might be due to invalid IL or missing references)
			//IL_015d: Unknown result type (might be due to invalid IL or missing references)
			//IL_015e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0055: Unknown result type (might be due to invalid IL or missing references)
			//IL_007d: Expected O, but got Unknown
			//IL_007d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0082: Unknown result type (might be due to invalid IL or missing references)
			//IL_0096: Unknown result type (might be due to invalid IL or missing references)
			//IL_0097: Unknown result type (might be due to invalid IL or missing references)
			int num = _003C_003E1__state;
			SectionPower sectionPower = _003C_003E4__this;
			try
			{
				TaskAwaiter<int> val2;
				TaskAwaiter val;
				switch (num)
				{
				default:
					if ((int)side == 1 && Enumerable.Contains<Creature>(participants, ((PowerModel)sectionPower).Owner) && sectionPower.Stage == SectionStage.Chorus)
					{
						val2 = PowerCmd.ModifyAmount(choiceContext, (PowerModel)sectionPower, 4m - decimal.op_Implicit(((PowerModel)sectionPower).Amount), ((PowerModel)sectionPower).Owner, (CardModel)null, false).GetAwaiter();
						if (!val2.IsCompleted)
						{
							num = (_003C_003E1__state = 0);
							_003C_003Eu__1 = val2;
							((AsyncTaskMethodBuilder)(ref _003C_003Et__builder)).AwaitUnsafeOnCompleted<TaskAwaiter<int>, _003CAfterSideTurnEnd_003Ed__13>(ref val2, ref this);
							return;
						}
						goto IL_00cb;
					}
					goto end_IL_000e;
				case 0:
					val2 = _003C_003Eu__1;
					_003C_003Eu__1 = default(TaskAwaiter<int>);
					num = (_003C_003E1__state = -1);
					goto IL_00cb;
				case 1:
					val = _003C_003Eu__2;
					_003C_003Eu__2 = default(TaskAwaiter);
					num = (_003C_003E1__state = -1);
					goto IL_012c;
				case 2:
					{
						val = _003C_003Eu__2;
						_003C_003Eu__2 = default(TaskAwaiter);
						num = (_003C_003E1__state = -1);
						break;
					}
					IL_00cb:
					val2.GetResult();
					val = PowerCmd.Remove<LiberationPower>(((PowerModel)sectionPower).Owner).GetAwaiter();
					if (!((TaskAwaiter)(ref val)).IsCompleted)
					{
						num = (_003C_003E1__state = 1);
						_003C_003Eu__2 = val;
						((AsyncTaskMethodBuilder)(ref _003C_003Et__builder)).AwaitUnsafeOnCompleted<TaskAwaiter, _003CAfterSideTurnEnd_003Ed__13>(ref val, ref this);
						return;
					}
					goto IL_012c;
					IL_012c:
					((TaskAwaiter)(ref val)).GetResult();
					val = FormSystem.EnterInterlude(choiceContext, ((PowerModel)sectionPower).Owner).GetAwaiter();
					if (!((TaskAwaiter)(ref val)).IsCompleted)
					{
						num = (_003C_003E1__state = 2);
						_003C_003Eu__2 = val;
						((AsyncTaskMethodBuilder)(ref _003C_003Et__builder)).AwaitUnsafeOnCompleted<TaskAwaiter, _003CAfterSideTurnEnd_003Ed__13>(ref val, ref this);
						return;
					}
					break;
				}
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

	[StructLayout((LayoutKind)3)]
	[CompilerGenerated]
	private struct _003COutroSettlement_003Ed__15 : IAsyncStateMachine
	{
		public int _003C_003E1__state;

		public AsyncTaskMethodBuilder _003C_003Et__builder;

		public SectionPower _003C_003E4__this;

		public PlayerChoiceContext choiceContext;

		private Player _003Cplayer_003E5__2;

		private bool _003Cupgraded_003E5__3;

		private int _003Cfoxfire_003E5__4;

		private TaskAwaiter _003C_003Eu__1;

		private void MoveNext()
		{
			//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
			//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
			//IL_0107: Unknown result type (might be due to invalid IL or missing references)
			//IL_0163: Unknown result type (might be due to invalid IL or missing references)
			//IL_0168: Unknown result type (might be due to invalid IL or missing references)
			//IL_0170: Unknown result type (might be due to invalid IL or missing references)
			//IL_01ee: Unknown result type (might be due to invalid IL or missing references)
			//IL_01f3: Unknown result type (might be due to invalid IL or missing references)
			//IL_01fb: Unknown result type (might be due to invalid IL or missing references)
			//IL_0251: Unknown result type (might be due to invalid IL or missing references)
			//IL_0256: Unknown result type (might be due to invalid IL or missing references)
			//IL_025e: Unknown result type (might be due to invalid IL or missing references)
			//IL_02e1: Unknown result type (might be due to invalid IL or missing references)
			//IL_02e6: Unknown result type (might be due to invalid IL or missing references)
			//IL_02ee: Unknown result type (might be due to invalid IL or missing references)
			//IL_0344: Unknown result type (might be due to invalid IL or missing references)
			//IL_0349: Unknown result type (might be due to invalid IL or missing references)
			//IL_0351: Unknown result type (might be due to invalid IL or missing references)
			//IL_03a4: Unknown result type (might be due to invalid IL or missing references)
			//IL_03a9: Unknown result type (might be due to invalid IL or missing references)
			//IL_03b1: Unknown result type (might be due to invalid IL or missing references)
			//IL_012e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0133: Unknown result type (might be due to invalid IL or missing references)
			//IL_021c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0221: Unknown result type (might be due to invalid IL or missing references)
			//IL_030f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0314: Unknown result type (might be due to invalid IL or missing references)
			//IL_0148: Unknown result type (might be due to invalid IL or missing references)
			//IL_014a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0236: Unknown result type (might be due to invalid IL or missing references)
			//IL_0238: Unknown result type (might be due to invalid IL or missing references)
			//IL_0329: Unknown result type (might be due to invalid IL or missing references)
			//IL_032b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0368: Unknown result type (might be due to invalid IL or missing references)
			//IL_0372: Expected O, but got Unknown
			//IL_0372: Unknown result type (might be due to invalid IL or missing references)
			//IL_0377: Unknown result type (might be due to invalid IL or missing references)
			//IL_038c: Unknown result type (might be due to invalid IL or missing references)
			//IL_038e: Unknown result type (might be due to invalid IL or missing references)
			//IL_01b9: Unknown result type (might be due to invalid IL or missing references)
			//IL_01be: Unknown result type (might be due to invalid IL or missing references)
			//IL_02ac: Unknown result type (might be due to invalid IL or missing references)
			//IL_02b1: Unknown result type (might be due to invalid IL or missing references)
			//IL_01d3: Unknown result type (might be due to invalid IL or missing references)
			//IL_01d5: Unknown result type (might be due to invalid IL or missing references)
			//IL_02c6: Unknown result type (might be due to invalid IL or missing references)
			//IL_02c8: Unknown result type (might be due to invalid IL or missing references)
			//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
			//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
			//IL_00df: Unknown result type (might be due to invalid IL or missing references)
			//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
			int num = _003C_003E1__state;
			SectionPower sectionPower = _003C_003E4__this;
			try
			{
				TaskAwaiter val;
				int powerAmount;
				int powerAmount2;
				switch (num)
				{
				default:
					_003Cplayer_003E5__2 = FormSystem.PlayerFor(((PowerModel)sectionPower).Owner);
					if (_003Cplayer_003E5__2 != null)
					{
						_003Cupgraded_003E5__3 = sectionPower.OutroLevel >= 1;
						_003Cfoxfire_003E5__4 = FoxFireCmd.Get(_003Cplayer_003E5__2);
						if (_003Cfoxfire_003E5__4 > 0)
						{
							int hitCount = _003Cfoxfire_003E5__4 + (_003Cupgraded_003E5__3 ? (_003Cfoxfire_003E5__4 / 4) : 0);
							decimal damagePerHit = (_003Cupgraded_003E5__3 ? 7m : 6m);
							val = PursuitExecutor.Chase(choiceContext, _003Cplayer_003E5__2, hitCount, damagePerHit).GetAwaiter();
							if (!((TaskAwaiter)(ref val)).IsCompleted)
							{
								num = (_003C_003E1__state = 0);
								_003C_003Eu__1 = val;
								((AsyncTaskMethodBuilder)(ref _003C_003Et__builder)).AwaitUnsafeOnCompleted<TaskAwaiter, _003COutroSettlement_003Ed__15>(ref val, ref this);
								return;
							}
							goto IL_0116;
						}
						goto IL_0186;
					}
					goto end_IL_000e;
				case 0:
					val = _003C_003Eu__1;
					_003C_003Eu__1 = default(TaskAwaiter);
					num = (_003C_003E1__state = -1);
					goto IL_0116;
				case 1:
					val = _003C_003Eu__1;
					_003C_003Eu__1 = default(TaskAwaiter);
					num = (_003C_003E1__state = -1);
					goto IL_017f;
				case 2:
					val = _003C_003Eu__1;
					_003C_003Eu__1 = default(TaskAwaiter);
					num = (_003C_003E1__state = -1);
					goto IL_020a;
				case 3:
					val = _003C_003Eu__1;
					_003C_003Eu__1 = default(TaskAwaiter);
					num = (_003C_003E1__state = -1);
					goto IL_026d;
				case 4:
					val = _003C_003Eu__1;
					_003C_003Eu__1 = default(TaskAwaiter);
					num = (_003C_003E1__state = -1);
					goto IL_02fd;
				case 5:
					val = _003C_003Eu__1;
					_003C_003Eu__1 = default(TaskAwaiter);
					num = (_003C_003E1__state = -1);
					goto IL_0360;
				case 6:
					{
						val = _003C_003Eu__1;
						_003C_003Eu__1 = default(TaskAwaiter);
						num = (_003C_003E1__state = -1);
						break;
					}
					IL_0116:
					((TaskAwaiter)(ref val)).GetResult();
					val = FoxFireCmd.Lose(_003Cfoxfire_003E5__4, _003Cplayer_003E5__2).GetAwaiter();
					if (!((TaskAwaiter)(ref val)).IsCompleted)
					{
						num = (_003C_003E1__state = 1);
						_003C_003Eu__1 = val;
						((AsyncTaskMethodBuilder)(ref _003C_003Et__builder)).AwaitUnsafeOnCompleted<TaskAwaiter, _003COutroSettlement_003Ed__15>(ref val, ref this);
						return;
					}
					goto IL_017f;
					IL_020a:
					((TaskAwaiter)(ref val)).GetResult();
					val = PowerCmd.Remove<LightPower>(((PowerModel)sectionPower).Owner).GetAwaiter();
					if (!((TaskAwaiter)(ref val)).IsCompleted)
					{
						num = (_003C_003E1__state = 3);
						_003C_003Eu__1 = val;
						((AsyncTaskMethodBuilder)(ref _003C_003Et__builder)).AwaitUnsafeOnCompleted<TaskAwaiter, _003COutroSettlement_003Ed__15>(ref val, ref this);
						return;
					}
					goto IL_026d;
					IL_0186:
					powerAmount = ((PowerModel)sectionPower).Owner.GetPowerAmount<LightPower>();
					if (powerAmount > 0)
					{
						val = PlayerCmd.GainEnergy(decimal.op_Implicit(powerAmount + (_003Cupgraded_003E5__3 ? (powerAmount / 4) : 0)), _003Cplayer_003E5__2).GetAwaiter();
						if (!((TaskAwaiter)(ref val)).IsCompleted)
						{
							num = (_003C_003E1__state = 2);
							_003C_003Eu__1 = val;
							((AsyncTaskMethodBuilder)(ref _003C_003Et__builder)).AwaitUnsafeOnCompleted<TaskAwaiter, _003COutroSettlement_003Ed__15>(ref val, ref this);
							return;
						}
						goto IL_020a;
					}
					goto IL_0274;
					IL_0367:
					val = PowerCmd.Remove((PowerModel)sectionPower).GetAwaiter();
					if (!((TaskAwaiter)(ref val)).IsCompleted)
					{
						num = (_003C_003E1__state = 6);
						_003C_003Eu__1 = val;
						((AsyncTaskMethodBuilder)(ref _003C_003Et__builder)).AwaitUnsafeOnCompleted<TaskAwaiter, _003COutroSettlement_003Ed__15>(ref val, ref this);
						return;
					}
					break;
					IL_017f:
					((TaskAwaiter)(ref val)).GetResult();
					goto IL_0186;
					IL_0360:
					((TaskAwaiter)(ref val)).GetResult();
					goto IL_0367;
					IL_026d:
					((TaskAwaiter)(ref val)).GetResult();
					goto IL_0274;
					IL_0274:
					powerAmount2 = ((PowerModel)sectionPower).Owner.GetPowerAmount<HealingPower>();
					if (powerAmount2 > 0)
					{
						int num2 = powerAmount2 + (_003Cupgraded_003E5__3 ? (powerAmount2 / 4) : 0);
						val = CreatureCmd.Heal(((PowerModel)sectionPower).Owner, decimal.op_Implicit(num2), true).GetAwaiter();
						if (!((TaskAwaiter)(ref val)).IsCompleted)
						{
							num = (_003C_003E1__state = 4);
							_003C_003Eu__1 = val;
							((AsyncTaskMethodBuilder)(ref _003C_003Et__builder)).AwaitUnsafeOnCompleted<TaskAwaiter, _003COutroSettlement_003Ed__15>(ref val, ref this);
							return;
						}
						goto IL_02fd;
					}
					goto IL_0367;
					IL_02fd:
					((TaskAwaiter)(ref val)).GetResult();
					val = PowerCmd.Remove<HealingPower>(((PowerModel)sectionPower).Owner).GetAwaiter();
					if (!((TaskAwaiter)(ref val)).IsCompleted)
					{
						num = (_003C_003E1__state = 5);
						_003C_003Eu__1 = val;
						((AsyncTaskMethodBuilder)(ref _003C_003Et__builder)).AwaitUnsafeOnCompleted<TaskAwaiter, _003COutroSettlement_003Ed__15>(ref val, ref this);
						return;
					}
					goto IL_0360;
				}
				((TaskAwaiter)(ref val)).GetResult();
				end_IL_000e:;
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

	public override PowerType Type => (PowerType)1;

	public override PowerStackType StackType => (PowerStackType)2;

	public override int DisplayAmount => 0;

	public SectionStage Stage => (SectionStage)((PowerModel)this).Amount;

	[field: CompilerGenerated]
	public int OutroLevel
	{
		[CompilerGenerated]
		get;
		[CompilerGenerated]
		set;
	}

	public override bool TryModifyEnergyCostInCombat(CardModel card, decimal originalCost, out decimal modifiedCost)
	{
		modifiedCost = originalCost;
		if (Stage != SectionStage.Intro)
		{
			return false;
		}
		Player owner = card.Owner;
		if (((owner != null) ? owner.Creature : null) != ((PowerModel)this).Owner)
		{
			return false;
		}
		if (originalCost > 2m)
		{
			modifiedCost = originalCost - 1m;
			return true;
		}
		return false;
	}

	[AsyncStateMachine(typeof(_003CAfterSideTurnEnd_003Ed__13))]
	public override global::System.Threading.Tasks.Task AfterSideTurnEnd(PlayerChoiceContext choiceContext, CombatSide side, global::System.Collections.Generic.IEnumerable<Creature> participants)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		_003CAfterSideTurnEnd_003Ed__13 _003CAfterSideTurnEnd_003Ed__14 = default(_003CAfterSideTurnEnd_003Ed__13);
		_003CAfterSideTurnEnd_003Ed__14._003C_003Et__builder = AsyncTaskMethodBuilder.Create();
		_003CAfterSideTurnEnd_003Ed__14._003C_003E4__this = this;
		_003CAfterSideTurnEnd_003Ed__14.choiceContext = choiceContext;
		_003CAfterSideTurnEnd_003Ed__14.side = side;
		_003CAfterSideTurnEnd_003Ed__14.participants = participants;
		_003CAfterSideTurnEnd_003Ed__14._003C_003E1__state = -1;
		((AsyncTaskMethodBuilder)(ref _003CAfterSideTurnEnd_003Ed__14._003C_003Et__builder)).Start<_003CAfterSideTurnEnd_003Ed__13>(ref _003CAfterSideTurnEnd_003Ed__14);
		return ((AsyncTaskMethodBuilder)(ref _003CAfterSideTurnEnd_003Ed__14._003C_003Et__builder)).Task;
	}

	[AsyncStateMachine(typeof(_003CAfterPowerAmountChanged_003Ed__14))]
	public override global::System.Threading.Tasks.Task AfterPowerAmountChanged(PlayerChoiceContext choiceContext, PowerModel power, decimal amount, Creature? applier, CardModel? cardSource)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		_003CAfterPowerAmountChanged_003Ed__14 _003CAfterPowerAmountChanged_003Ed__15 = default(_003CAfterPowerAmountChanged_003Ed__14);
		_003CAfterPowerAmountChanged_003Ed__15._003C_003Et__builder = AsyncTaskMethodBuilder.Create();
		_003CAfterPowerAmountChanged_003Ed__15._003C_003E4__this = this;
		_003CAfterPowerAmountChanged_003Ed__15.choiceContext = choiceContext;
		_003CAfterPowerAmountChanged_003Ed__15.power = power;
		_003CAfterPowerAmountChanged_003Ed__15._003C_003E1__state = -1;
		((AsyncTaskMethodBuilder)(ref _003CAfterPowerAmountChanged_003Ed__15._003C_003Et__builder)).Start<_003CAfterPowerAmountChanged_003Ed__14>(ref _003CAfterPowerAmountChanged_003Ed__15);
		return ((AsyncTaskMethodBuilder)(ref _003CAfterPowerAmountChanged_003Ed__15._003C_003Et__builder)).Task;
	}

	[AsyncStateMachine(typeof(_003COutroSettlement_003Ed__15))]
	private global::System.Threading.Tasks.Task OutroSettlement(PlayerChoiceContext choiceContext)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		_003COutroSettlement_003Ed__15 _003COutroSettlement_003Ed__16 = default(_003COutroSettlement_003Ed__15);
		_003COutroSettlement_003Ed__16._003C_003Et__builder = AsyncTaskMethodBuilder.Create();
		_003COutroSettlement_003Ed__16._003C_003E4__this = this;
		_003COutroSettlement_003Ed__16.choiceContext = choiceContext;
		_003COutroSettlement_003Ed__16._003C_003E1__state = -1;
		((AsyncTaskMethodBuilder)(ref _003COutroSettlement_003Ed__16._003C_003Et__builder)).Start<_003COutroSettlement_003Ed__15>(ref _003COutroSettlement_003Ed__16);
		return ((AsyncTaskMethodBuilder)(ref _003COutroSettlement_003Ed__16._003C_003Et__builder)).Task;
	}
}
