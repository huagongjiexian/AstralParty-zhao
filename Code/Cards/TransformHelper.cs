using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Nodes.CommonUi;

namespace Zhao.Cards;

public static class TransformHelper
{
	[StructLayout((LayoutKind)3)]
	[CompilerGenerated]
	private struct _003CTransformInto_003Ed__0<T> : IAsyncStateMachine where T : CardModel
	{
		public int _003C_003E1__state;

		public AsyncTaskMethodBuilder _003C_003Et__builder;

		public CardModel original;

		private TaskAwaiter<CardPileAddResult?> _003C_003Eu__1;

		private void MoveNext()
		{
			//IL_004b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0050: Unknown result type (might be due to invalid IL or missing references)
			//IL_0058: Unknown result type (might be due to invalid IL or missing references)
			//IL_0016: Unknown result type (might be due to invalid IL or missing references)
			//IL_001b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0071: Unknown result type (might be due to invalid IL or missing references)
			//IL_0030: Unknown result type (might be due to invalid IL or missing references)
			//IL_0032: Unknown result type (might be due to invalid IL or missing references)
			//IL_0082: Unknown result type (might be due to invalid IL or missing references)
			//IL_0087: Unknown result type (might be due to invalid IL or missing references)
			//IL_0088: Unknown result type (might be due to invalid IL or missing references)
			//IL_0090: Unknown result type (might be due to invalid IL or missing references)
			//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
			int num = _003C_003E1__state;
			try
			{
				TaskAwaiter<CardPileAddResult?> val;
				if (num != 0)
				{
					val = CardCmd.TransformTo<T>(original, (CardPreviewStyle)0).GetAwaiter();
					if (!val.IsCompleted)
					{
						num = (_003C_003E1__state = 0);
						_003C_003Eu__1 = val;
						((AsyncTaskMethodBuilder)(ref _003C_003Et__builder)).AwaitUnsafeOnCompleted<TaskAwaiter<CardPileAddResult?>, _003CTransformInto_003Ed__0<T>>(ref val, ref this);
						return;
					}
				}
				else
				{
					val = _003C_003Eu__1;
					_003C_003Eu__1 = default(TaskAwaiter<CardPileAddResult?>);
					num = (_003C_003E1__state = -1);
				}
				CardPileAddResult? result = val.GetResult();
				CardPileAddResult val2 = default(CardPileAddResult);
				int num2;
				if (result.HasValue)
				{
					val2 = result.GetValueOrDefault();
					if (val2.success)
					{
						num2 = ((val2.cardAdded == null) ? 1 : 0);
						goto IL_009e;
					}
				}
				num2 = 1;
				goto IL_009e;
				IL_009e:
				if (num2 == 0)
				{
					for (int i = 0; i < original.CurrentUpgradeLevel; i++)
					{
						CardCmd.Upgrade(val2.cardAdded, (CardPreviewStyle)0);
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

	[AsyncStateMachine(typeof(_003CTransformInto_003Ed__0<>))]
	public static global::System.Threading.Tasks.Task TransformInto<T>(CardModel original) where T : CardModel
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		_003CTransformInto_003Ed__0<T> _003CTransformInto_003Ed__1 = default(_003CTransformInto_003Ed__0<T>);
		_003CTransformInto_003Ed__1._003C_003Et__builder = AsyncTaskMethodBuilder.Create();
		_003CTransformInto_003Ed__1.original = original;
		_003CTransformInto_003Ed__1._003C_003E1__state = -1;
		((AsyncTaskMethodBuilder)(ref _003CTransformInto_003Ed__1._003C_003Et__builder)).Start<_003CTransformInto_003Ed__0<T>>(ref _003CTransformInto_003Ed__1);
		return ((AsyncTaskMethodBuilder)(ref _003CTransformInto_003Ed__1._003C_003Et__builder)).Task;
	}
}
