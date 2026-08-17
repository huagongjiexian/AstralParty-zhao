using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.Models;
using Zhao.FoxFire;

namespace Zhao.Relics;

public sealed class FoxFireRelic : RelicModel
{
	[StructLayout((LayoutKind)3)]
	[CompilerGenerated]
	private struct _003CBeforeCombatStart_003Ed__8 : IAsyncStateMachine
	{
		public int _003C_003E1__state;

		public AsyncTaskMethodBuilder _003C_003Et__builder;

		public FoxFireRelic _003C_003E4__this;

		private TaskAwaiter _003C_003Eu__1;

		public void MoveNext()
		{
			//IL_0053: Unknown result type (might be due to invalid IL or missing references)
			//IL_0058: Unknown result type (might be due to invalid IL or missing references)
			//IL_005f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0023: Unknown result type (might be due to invalid IL or missing references)
			//IL_0028: Unknown result type (might be due to invalid IL or missing references)
			//IL_003c: Unknown result type (might be due to invalid IL or missing references)
			//IL_003d: Unknown result type (might be due to invalid IL or missing references)
			int num = _003C_003E1__state;
			FoxFireRelic foxFireRelic = _003C_003E4__this;
			try
			{
				TaskAwaiter val;
				if (num != 0)
				{
					((RelicModel)foxFireRelic).Flash();
					val = FoxFireCmd.Gain(2, ((RelicModel)foxFireRelic).Owner).GetAwaiter();
					if (!val.IsCompleted)
					{
						num = (_003C_003E1__state = 0);
						_003C_003Eu__1 = val;
						_003C_003Et__builder.AwaitUnsafeOnCompleted<TaskAwaiter, _003CBeforeCombatStart_003Ed__8>(ref val, ref this);
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

	public override RelicRarity Rarity => (RelicRarity)1;

	public override string PackedIconPath => "res://zhao/images/packed/relics/fox_fire_relic.png";

	protected override string PackedIconOutlinePath => "res://zhao/images/packed/relics/fox_fire_relic_outline.png";

	protected override string BigIconPath => "res://zhao/images/relics/fox_fire_relic.png";

	[AsyncStateMachine(typeof(_003CBeforeCombatStart_003Ed__8))]
	public override global::System.Threading.Tasks.Task BeforeCombatStart()
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		_003CBeforeCombatStart_003Ed__8 _003CBeforeCombatStart_003Ed__9 = default(_003CBeforeCombatStart_003Ed__8);
		_003CBeforeCombatStart_003Ed__9._003C_003Et__builder = AsyncTaskMethodBuilder.Create();
		_003CBeforeCombatStart_003Ed__9._003C_003E4__this = this;
		_003CBeforeCombatStart_003Ed__9._003C_003E1__state = -1;
		_003CBeforeCombatStart_003Ed__9._003C_003Et__builder.Start<_003CBeforeCombatStart_003Ed__8>(ref _003CBeforeCombatStart_003Ed__9);
		return _003CBeforeCombatStart_003Ed__9._003C_003Et__builder.Task;
	}
}
