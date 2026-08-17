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
using Zhao.Forms;

namespace Zhao.Cards;

public sealed class SectionIntro : ZhaoCardModel
{
	[StructLayout((LayoutKind)3)]
	[CompilerGenerated]
	private struct _003COnPlay_003Ed__5 : IAsyncStateMachine
	{
		public int _003C_003E1__state;

		public AsyncTaskMethodBuilder _003C_003Et__builder;

		public SectionIntro _003C_003E4__this;

		public PlayerChoiceContext choiceContext;

		private Creature _003Ccreature_003E5__2;

		private TaskAwaiter _003C_003Eu__1;

		private TaskAwaiter<global::System.Collections.Generic.IEnumerable<CardModel>> _003C_003Eu__2;

		public void MoveNext()
		{
			//IL_0076: Unknown result type (might be due to invalid IL or missing references)
			//IL_007b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0082: Unknown result type (might be due to invalid IL or missing references)
			//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
			//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
			//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
			//IL_0146: Unknown result type (might be due to invalid IL or missing references)
			//IL_014b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0152: Unknown result type (might be due to invalid IL or missing references)
			//IL_0043: Unknown result type (might be due to invalid IL or missing references)
			//IL_0048: Unknown result type (might be due to invalid IL or missing references)
			//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
			//IL_00af: Unknown result type (might be due to invalid IL or missing references)
			//IL_0116: Unknown result type (might be due to invalid IL or missing references)
			//IL_011b: Unknown result type (might be due to invalid IL or missing references)
			//IL_005c: Unknown result type (might be due to invalid IL or missing references)
			//IL_005d: Unknown result type (might be due to invalid IL or missing references)
			//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
			//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
			//IL_012f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0130: Unknown result type (might be due to invalid IL or missing references)
			int num = _003C_003E1__state;
			SectionIntro sectionIntro = _003C_003E4__this;
			try
			{
				TaskAwaiter val2;
				TaskAwaiter<global::System.Collections.Generic.IEnumerable<CardModel>> val;
				switch (num)
				{
				default:
					_003Ccreature_003E5__2 = ((CardModel)sectionIntro).Owner.Creature;
					val2 = FormSystem.SwitchForm(choiceContext, _003Ccreature_003E5__2, ZhaoForm.Diva).GetAwaiter();
					if (!val2.IsCompleted)
					{
						num = (_003C_003E1__state = 0);
						_003C_003Eu__1 = val2;
						_003C_003Et__builder.AwaitUnsafeOnCompleted<TaskAwaiter, _003COnPlay_003Ed__5>(ref val2, ref this);
						return;
					}
					goto IL_0091;
				case 0:
					val2 = _003C_003Eu__1;
					_003C_003Eu__1 = default(TaskAwaiter);
					num = (_003C_003E1__state = -1);
					goto IL_0091;
				case 1:
					val2 = _003C_003Eu__1;
					_003C_003Eu__1 = default(TaskAwaiter);
					num = (_003C_003E1__state = -1);
					goto IL_00f8;
				case 2:
					{
						val = _003C_003Eu__2;
						_003C_003Eu__2 = default(TaskAwaiter<global::System.Collections.Generic.IEnumerable<CardModel>>);
						num = (_003C_003E1__state = -1);
						break;
					}
					IL_00f8:
					val2.GetResult();
					val = CardPileCmd.Draw(choiceContext, 1m, ((CardModel)sectionIntro).Owner, false).GetAwaiter();
					if (!val.IsCompleted)
					{
						num = (_003C_003E1__state = 2);
						_003C_003Eu__2 = val;
						_003C_003Et__builder.AwaitUnsafeOnCompleted<TaskAwaiter<global::System.Collections.Generic.IEnumerable<CardModel>>, _003COnPlay_003Ed__5>(ref val, ref this);
						return;
					}
					break;
					IL_0091:
					val2.GetResult();
					val2 = FormSystem.SetStage(choiceContext, _003Ccreature_003E5__2, SectionStage.Intro).GetAwaiter();
					if (!val2.IsCompleted)
					{
						num = (_003C_003E1__state = 1);
						_003C_003Eu__1 = val2;
						_003C_003Et__builder.AwaitUnsafeOnCompleted<TaskAwaiter, _003COnPlay_003Ed__5>(ref val2, ref this);
						return;
					}
					goto IL_00f8;
				}
				val.GetResult();
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

	public override int MaxUpgradeLevel => 2;

	protected override bool IsPlayable
	{
		get
		{
			if (base.IsPlayable)
			{
				return FormSystem.GetCurrentForm(((CardModel)this).Owner.Creature) != ZhaoForm.Diva;
			}
			return false;
		}
	}

	public SectionIntro()
		: base(1, CardType.Skill, CardRarity.Basic, TargetType.Self)
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
		_003COnPlay_003Ed__6._003C_003Et__builder.Start<_003COnPlay_003Ed__5>(ref _003COnPlay_003Ed__6);
		return _003COnPlay_003Ed__6._003C_003Et__builder.Task;
	}

	protected override global::System.Threading.Tasks.Task? OnTransformAfterPlay()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Expected O, but got Unknown
		return TransformHelper.TransformInto<SectionMain>((CardModel)this);
	}

	protected override void OnUpgrade()
	{
		if (((CardModel)this).CurrentUpgradeLevel == 1)
		{
			((CardModel)this).EnergyCost.UpgradeBy(-1);
		}
	}
}
