using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.Models;
using Zhao.FoxFire;

namespace Zhao.Cards;

public abstract class ZhaoCardModel : CardModel
{
	[StructLayout((LayoutKind)3)]
	[CompilerGenerated]
	private struct _003CRunTransformAfterPlay_003Ed__13 : IAsyncStateMachine
	{
		public int _003C_003E1__state;

		public AsyncTaskMethodBuilder _003C_003Et__builder;

		public ZhaoCardModel _003C_003E4__this;

		private TaskAwaiter _003C_003Eu__1;

		private void MoveNext()
		{
			//IL_004c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0051: Unknown result type (might be due to invalid IL or missing references)
			//IL_0058: Unknown result type (might be due to invalid IL or missing references)
			//IL_001c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0021: Unknown result type (might be due to invalid IL or missing references)
			//IL_0035: Unknown result type (might be due to invalid IL or missing references)
			//IL_0036: Unknown result type (might be due to invalid IL or missing references)
			int num = _003C_003E1__state;
			ZhaoCardModel zhaoCardModel = _003C_003E4__this;
			try
			{
				TaskAwaiter val;
				if (num == 0)
				{
					val = _003C_003Eu__1;
					_003C_003Eu__1 = default(TaskAwaiter);
					num = (_003C_003E1__state = -1);
					goto IL_0067;
				}
				global::System.Threading.Tasks.Task task = zhaoCardModel.OnTransformAfterPlay();
				if (task != null)
				{
					val = task.GetAwaiter();
					if (!((TaskAwaiter)(ref val)).IsCompleted)
					{
						num = (_003C_003E1__state = 0);
						_003C_003Eu__1 = val;
						((AsyncTaskMethodBuilder)(ref _003C_003Et__builder)).AwaitUnsafeOnCompleted<TaskAwaiter, _003CRunTransformAfterPlay_003Ed__13>(ref val, ref this);
						return;
					}
					goto IL_0067;
				}
				goto end_IL_000e;
				IL_0067:
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

	private const string CardPortraitPlaceholder = "res://zhao/images/cards/zhao_card_placeholder.png";

	public virtual int FoxFireCost => 0;

	protected override bool IsPlayable
	{
		get
		{
			if (((CardModel)this).Owner != null && FoxFireCost > 0 && FoxFireCmd.Get(((CardModel)this).Owner) < FoxFireCost)
			{
				return false;
			}
			return ((CardModel)this).IsPlayable;
		}
	}

	public override string PortraitPath => "res://zhao/images/cards/zhao_card_placeholder.png";

	public override string BetaPortraitPath => "res://zhao/images/cards/zhao_card_placeholder.png";

	protected ZhaoCardModel(int energyCost, CardType type, CardRarity rarity, TargetType target)
		: base(energyCost, type, rarity, target, true)
	{
	}//IL_0002: Unknown result type (might be due to invalid IL or missing references)
	//IL_0003: Unknown result type (might be due to invalid IL or missing references)
	//IL_0004: Unknown result type (might be due to invalid IL or missing references)


	public override void AfterCreated()
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Expected O, but got Unknown
		((CardModel)this).AfterCreated();
		((CardModel)this).Played += new Action(OnPlayedFinalize);
	}

	protected virtual global::System.Threading.Tasks.Task? OnTransformAfterPlay()
	{
		return null;
	}

	private void OnPlayedFinalize()
	{
		if (!CombatManager.Instance.IsOverOrEnding)
		{
			TaskHelper.RunSafely(RunTransformAfterPlay());
		}
	}

	[AsyncStateMachine(typeof(_003CRunTransformAfterPlay_003Ed__13))]
	private global::System.Threading.Tasks.Task RunTransformAfterPlay()
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		_003CRunTransformAfterPlay_003Ed__13 _003CRunTransformAfterPlay_003Ed__14 = default(_003CRunTransformAfterPlay_003Ed__13);
		_003CRunTransformAfterPlay_003Ed__14._003C_003Et__builder = AsyncTaskMethodBuilder.Create();
		_003CRunTransformAfterPlay_003Ed__14._003C_003E4__this = this;
		_003CRunTransformAfterPlay_003Ed__14._003C_003E1__state = -1;
		((AsyncTaskMethodBuilder)(ref _003CRunTransformAfterPlay_003Ed__14._003C_003Et__builder)).Start<_003CRunTransformAfterPlay_003Ed__13>(ref _003CRunTransformAfterPlay_003Ed__14);
		return ((AsyncTaskMethodBuilder)(ref _003CRunTransformAfterPlay_003Ed__14._003C_003Et__builder)).Task;
	}
}
