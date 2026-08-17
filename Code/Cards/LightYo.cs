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
using MegaCrit.Sts2.Core.ValueProps;
using Zhao.Forms;

namespace Zhao.Cards;

public sealed class LightYo : ZhaoCardModel
{
	[StructLayout((LayoutKind)3)]
	[CompilerGenerated]
	private struct _003COnPlay_003Ed__3 : IAsyncStateMachine
	{
		public int _003C_003E1__state;

		public AsyncTaskMethodBuilder _003C_003Et__builder;

		public CardPlay cardPlay;

		public LightYo _003C_003E4__this;

		public PlayerChoiceContext choiceContext;

		private Creature _003Ccreature_003E5__2;

		private int _003Clight_003E5__3;

		private TaskAwaiter _003C_003Eu__1;

		private TaskAwaiter<global::System.Collections.Generic.IEnumerable<DamageResult>> _003C_003Eu__2;

		public void MoveNext()
		{
			//IL_0099: Unknown result type (might be due to invalid IL or missing references)
			//IL_009e: Unknown result type (might be due to invalid IL or missing references)
			//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
			//IL_0130: Unknown result type (might be due to invalid IL or missing references)
			//IL_0135: Unknown result type (might be due to invalid IL or missing references)
			//IL_013c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0066: Unknown result type (might be due to invalid IL or missing references)
			//IL_006b: Unknown result type (might be due to invalid IL or missing references)
			//IL_00f6: Unknown result type (might be due to invalid IL or missing references)
			//IL_0100: Expected O, but got Unknown
			//IL_0100: Unknown result type (might be due to invalid IL or missing references)
			//IL_0105: Unknown result type (might be due to invalid IL or missing references)
			//IL_007f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0080: Unknown result type (might be due to invalid IL or missing references)
			//IL_0119: Unknown result type (might be due to invalid IL or missing references)
			//IL_011a: Unknown result type (might be due to invalid IL or missing references)
			int num = _003C_003E1__state;
			LightYo lightYo = _003C_003E4__this;
			try
			{
				TaskAwaiter<global::System.Collections.Generic.IEnumerable<DamageResult>> val;
				TaskAwaiter val2;
				if (num != 0)
				{
					if (num == 1)
					{
						val = _003C_003Eu__2;
						_003C_003Eu__2 = default(TaskAwaiter<global::System.Collections.Generic.IEnumerable<DamageResult>>);
						num = (_003C_003E1__state = -1);
						goto IL_014b;
					}
					ArgumentNullException.ThrowIfNull((object)cardPlay.Target, "cardPlay.Target");
					_003Ccreature_003E5__2 = ((CardModel)lightYo).Owner.Creature;
					_003Clight_003E5__3 = _003Ccreature_003E5__2.GetPowerAmount<LightPower>();
					if (_003Clight_003E5__3 <= 0)
					{
						goto IL_00bb;
					}
					val2 = PowerCmd.Remove<LightPower>(_003Ccreature_003E5__2).GetAwaiter();
					if (!val2.IsCompleted)
					{
						num = (_003C_003E1__state = 0);
						_003C_003Eu__1 = val2;
						_003C_003Et__builder.AwaitUnsafeOnCompleted<TaskAwaiter, _003COnPlay_003Ed__3>(ref val2, ref this);
						return;
					}
				}
				else
				{
					val2 = _003C_003Eu__1;
					_003C_003Eu__1 = default(TaskAwaiter);
					num = (_003C_003E1__state = -1);
				}
				val2.GetResult();
				goto IL_00bb;
				IL_014b:
				val.GetResult();
				goto end_IL_000e;
				IL_00bb:
				if (_003Clight_003E5__3 > 0)
				{
					val = CreatureCmd.Damage(choiceContext, cardPlay.Target, (decimal)(_003Clight_003E5__3) * 2m, ValueProp.Move, _003Ccreature_003E5__2, (CardModel)lightYo).GetAwaiter();
					if (!val.IsCompleted)
					{
						num = (_003C_003E1__state = 1);
						_003C_003Eu__2 = val;
						_003C_003Et__builder.AwaitUnsafeOnCompleted<TaskAwaiter<global::System.Collections.Generic.IEnumerable<DamageResult>>, _003COnPlay_003Ed__3>(ref val, ref this);
						return;
					}
					goto IL_014b;
				}
				end_IL_000e:;
			}
			catch (global::System.Exception exception)
			{
				_003C_003E1__state = -2;
				_003Ccreature_003E5__2 = null;
				_003C_003Et__builder.SetException(exception);
				return;
			}
			_003C_003E1__state = -2;
			_003Ccreature_003E5__2 = null;
			_003C_003Et__builder.SetResult();
		}

		[DebuggerHidden]
		public void SetStateMachine(IAsyncStateMachine stateMachine)
		{
			_003C_003Et__builder.SetStateMachine(stateMachine);
		}
	}

	protected override bool IsPlayable
	{
		get
		{
			if (base.IsPlayable)
			{
				return FormSystem.GetCurrentForm(((CardModel)this).Owner.Creature) == ZhaoForm.Lady;
			}
			return false;
		}
	}

	public LightYo()
		: base(3, CardType.Skill, CardRarity.Rare, TargetType.AnyEnemy)
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
		_003COnPlay_003Ed__4.cardPlay = cardPlay;
		_003COnPlay_003Ed__4._003C_003E1__state = -1;
		_003COnPlay_003Ed__4._003C_003Et__builder.Start<_003COnPlay_003Ed__3>(ref _003COnPlay_003Ed__4);
		return _003COnPlay_003Ed__4._003C_003Et__builder.Task;
	}

	protected override void OnUpgrade()
	{
	}
}
