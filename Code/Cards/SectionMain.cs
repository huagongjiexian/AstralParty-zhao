using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using Zhao.Forms;
using Zhao.FoxFire;
using Zhao.Powers;
using Zhao.Pursuit;

namespace Zhao.Cards;

public sealed class SectionMain : ZhaoCardModel
{
	[StructLayout((LayoutKind)3)]
	[CompilerGenerated]
	private struct _003COnPlay_003Ed__5 : IAsyncStateMachine
	{
		public int _003C_003E1__state;

		public AsyncTaskMethodBuilder _003C_003Et__builder;

		public SectionMain _003C_003E4__this;

		public PlayerChoiceContext choiceContext;

		private Creature _003Ccreature_003E5__2;

		private Player _003Cplayer_003E5__3;

		private TaskAwaiter _003C_003Eu__1;

		private TaskAwaiter<StrengthPower?> _003C_003Eu__2;

		private TaskAwaiter<int> _003C_003Eu__3;

		private TaskAwaiter<NurseMainHealingPower?> _003C_003Eu__4;

		private TaskAwaiter<MainMelodyPower?> _003C_003Eu__5;

		public void MoveNext()
		{
			//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
			//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
			//IL_0106: Unknown result type (might be due to invalid IL or missing references)
			//IL_016a: Unknown result type (might be due to invalid IL or missing references)
			//IL_016f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0177: Unknown result type (might be due to invalid IL or missing references)
			//IL_01d4: Unknown result type (might be due to invalid IL or missing references)
			//IL_01d9: Unknown result type (might be due to invalid IL or missing references)
			//IL_01e1: Unknown result type (might be due to invalid IL or missing references)
			//IL_023c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0241: Unknown result type (might be due to invalid IL or missing references)
			//IL_0249: Unknown result type (might be due to invalid IL or missing references)
			//IL_02a6: Unknown result type (might be due to invalid IL or missing references)
			//IL_02ab: Unknown result type (might be due to invalid IL or missing references)
			//IL_02b3: Unknown result type (might be due to invalid IL or missing references)
			//IL_033d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0342: Unknown result type (might be due to invalid IL or missing references)
			//IL_034a: Unknown result type (might be due to invalid IL or missing references)
			//IL_03ae: Unknown result type (might be due to invalid IL or missing references)
			//IL_03b3: Unknown result type (might be due to invalid IL or missing references)
			//IL_03bb: Unknown result type (might be due to invalid IL or missing references)
			//IL_042a: Unknown result type (might be due to invalid IL or missing references)
			//IL_042f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0437: Unknown result type (might be due to invalid IL or missing references)
			//IL_0495: Unknown result type (might be due to invalid IL or missing references)
			//IL_049a: Unknown result type (might be due to invalid IL or missing references)
			//IL_04a2: Unknown result type (might be due to invalid IL or missing references)
			//IL_0500: Unknown result type (might be due to invalid IL or missing references)
			//IL_0505: Unknown result type (might be due to invalid IL or missing references)
			//IL_050d: Unknown result type (might be due to invalid IL or missing references)
			//IL_05ad: Unknown result type (might be due to invalid IL or missing references)
			//IL_05b2: Unknown result type (might be due to invalid IL or missing references)
			//IL_05ba: Unknown result type (might be due to invalid IL or missing references)
			//IL_0613: Unknown result type (might be due to invalid IL or missing references)
			//IL_0618: Unknown result type (might be due to invalid IL or missing references)
			//IL_0620: Unknown result type (might be due to invalid IL or missing references)
			//IL_067c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0681: Unknown result type (might be due to invalid IL or missing references)
			//IL_0689: Unknown result type (might be due to invalid IL or missing references)
			//IL_06e7: Unknown result type (might be due to invalid IL or missing references)
			//IL_06ec: Unknown result type (might be due to invalid IL or missing references)
			//IL_06f4: Unknown result type (might be due to invalid IL or missing references)
			//IL_0752: Unknown result type (might be due to invalid IL or missing references)
			//IL_0757: Unknown result type (might be due to invalid IL or missing references)
			//IL_075f: Unknown result type (might be due to invalid IL or missing references)
			//IL_07c2: Unknown result type (might be due to invalid IL or missing references)
			//IL_07c7: Unknown result type (might be due to invalid IL or missing references)
			//IL_07cf: Unknown result type (might be due to invalid IL or missing references)
			//IL_082d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0832: Unknown result type (might be due to invalid IL or missing references)
			//IL_083a: Unknown result type (might be due to invalid IL or missing references)
			//IL_08a9: Unknown result type (might be due to invalid IL or missing references)
			//IL_08ae: Unknown result type (might be due to invalid IL or missing references)
			//IL_08b6: Unknown result type (might be due to invalid IL or missing references)
			//IL_092c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0931: Unknown result type (might be due to invalid IL or missing references)
			//IL_0939: Unknown result type (might be due to invalid IL or missing references)
			//IL_0135: Unknown result type (might be due to invalid IL or missing references)
			//IL_013a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0207: Unknown result type (might be due to invalid IL or missing references)
			//IL_020c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0271: Unknown result type (might be due to invalid IL or missing references)
			//IL_0276: Unknown result type (might be due to invalid IL or missing references)
			//IL_0379: Unknown result type (might be due to invalid IL or missing references)
			//IL_037e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0460: Unknown result type (might be due to invalid IL or missing references)
			//IL_0465: Unknown result type (might be due to invalid IL or missing references)
			//IL_04ca: Unknown result type (might be due to invalid IL or missing references)
			//IL_04cf: Unknown result type (might be due to invalid IL or missing references)
			//IL_0646: Unknown result type (might be due to invalid IL or missing references)
			//IL_064b: Unknown result type (might be due to invalid IL or missing references)
			//IL_06b1: Unknown result type (might be due to invalid IL or missing references)
			//IL_06b6: Unknown result type (might be due to invalid IL or missing references)
			//IL_071c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0721: Unknown result type (might be due to invalid IL or missing references)
			//IL_07f7: Unknown result type (might be due to invalid IL or missing references)
			//IL_07fc: Unknown result type (might be due to invalid IL or missing references)
			//IL_0868: Unknown result type (might be due to invalid IL or missing references)
			//IL_0873: Expected O, but got Unknown
			//IL_0873: Unknown result type (might be due to invalid IL or missing references)
			//IL_0878: Unknown result type (might be due to invalid IL or missing references)
			//IL_014f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0151: Unknown result type (might be due to invalid IL or missing references)
			//IL_019f: Unknown result type (might be due to invalid IL or missing references)
			//IL_01a4: Unknown result type (might be due to invalid IL or missing references)
			//IL_0221: Unknown result type (might be due to invalid IL or missing references)
			//IL_0223: Unknown result type (might be due to invalid IL or missing references)
			//IL_028b: Unknown result type (might be due to invalid IL or missing references)
			//IL_028d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0393: Unknown result type (might be due to invalid IL or missing references)
			//IL_0395: Unknown result type (might be due to invalid IL or missing references)
			//IL_03ea: Unknown result type (might be due to invalid IL or missing references)
			//IL_03f5: Expected O, but got Unknown
			//IL_03f5: Unknown result type (might be due to invalid IL or missing references)
			//IL_03fa: Unknown result type (might be due to invalid IL or missing references)
			//IL_047a: Unknown result type (might be due to invalid IL or missing references)
			//IL_047c: Unknown result type (might be due to invalid IL or missing references)
			//IL_04e5: Unknown result type (might be due to invalid IL or missing references)
			//IL_04e7: Unknown result type (might be due to invalid IL or missing references)
			//IL_05dd: Unknown result type (might be due to invalid IL or missing references)
			//IL_05e2: Unknown result type (might be due to invalid IL or missing references)
			//IL_0661: Unknown result type (might be due to invalid IL or missing references)
			//IL_0663: Unknown result type (might be due to invalid IL or missing references)
			//IL_06cc: Unknown result type (might be due to invalid IL or missing references)
			//IL_06ce: Unknown result type (might be due to invalid IL or missing references)
			//IL_0737: Unknown result type (might be due to invalid IL or missing references)
			//IL_0739: Unknown result type (might be due to invalid IL or missing references)
			//IL_0812: Unknown result type (might be due to invalid IL or missing references)
			//IL_0814: Unknown result type (might be due to invalid IL or missing references)
			//IL_088e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0890: Unknown result type (might be due to invalid IL or missing references)
			//IL_078c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0791: Unknown result type (might be due to invalid IL or missing references)
			//IL_01b9: Unknown result type (might be due to invalid IL or missing references)
			//IL_01bb: Unknown result type (might be due to invalid IL or missing references)
			//IL_08ee: Unknown result type (might be due to invalid IL or missing references)
			//IL_08f9: Expected O, but got Unknown
			//IL_08f9: Unknown result type (might be due to invalid IL or missing references)
			//IL_08fe: Unknown result type (might be due to invalid IL or missing references)
			//IL_040f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0411: Unknown result type (might be due to invalid IL or missing references)
			//IL_05f8: Unknown result type (might be due to invalid IL or missing references)
			//IL_05fa: Unknown result type (might be due to invalid IL or missing references)
			//IL_0308: Unknown result type (might be due to invalid IL or missing references)
			//IL_030d: Unknown result type (might be due to invalid IL or missing references)
			//IL_07a7: Unknown result type (might be due to invalid IL or missing references)
			//IL_07a9: Unknown result type (might be due to invalid IL or missing references)
			//IL_0558: Unknown result type (might be due to invalid IL or missing references)
			//IL_056c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0577: Expected O, but got Unknown
			//IL_0577: Expected O, but got Unknown
			//IL_0577: Unknown result type (might be due to invalid IL or missing references)
			//IL_057c: Unknown result type (might be due to invalid IL or missing references)
			//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
			//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
			//IL_0914: Unknown result type (might be due to invalid IL or missing references)
			//IL_0916: Unknown result type (might be due to invalid IL or missing references)
			//IL_0322: Unknown result type (might be due to invalid IL or missing references)
			//IL_0324: Unknown result type (might be due to invalid IL or missing references)
			//IL_0592: Unknown result type (might be due to invalid IL or missing references)
			//IL_0594: Unknown result type (might be due to invalid IL or missing references)
			//IL_00de: Unknown result type (might be due to invalid IL or missing references)
			//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
			int num = _003C_003E1__state;
			SectionMain sectionMain = _003C_003E4__this;
			try
			{
				TaskAwaiter val3;
				TaskAwaiter<StrengthPower> val5;
				TaskAwaiter<int> val4;
				TaskAwaiter<NurseMainHealingPower> val2;
				TaskAwaiter<MainMelodyPower> val;
				int powerAmount;
				int num2;
				switch (num)
				{
				default:
				{
					_003Ccreature_003E5__2 = ((CardModel)sectionMain).Owner.Creature;
					_003Cplayer_003E5__3 = ((CardModel)sectionMain).Owner;
					SectionPower section = FormSystem.GetSection(_003Ccreature_003E5__2);
					ZhaoForm currentForm = FormSystem.GetCurrentForm(_003Ccreature_003E5__2);
					if (section != null && section.Stage == SectionStage.Intro)
					{
						if (FoxFireCmd.Get(_003Cplayer_003E5__3) > 0)
						{
							val3 = FoxFireCmd.Lose(1, _003Cplayer_003E5__3).GetAwaiter();
							if (!val3.IsCompleted)
							{
								num = (_003C_003E1__state = 0);
								_003C_003Eu__1 = val3;
								_003C_003Et__builder.AwaitUnsafeOnCompleted<TaskAwaiter, _003COnPlay_003Ed__5>(ref val3, ref this);
								return;
							}
							goto IL_0115;
						}
						goto IL_018d;
					}
					switch (currentForm)
					{
					case ZhaoForm.Kitsune:
						break;
					case ZhaoForm.Lady:
						goto IL_0528;
					case ZhaoForm.Nurse:
						goto IL_077a;
					default:
						goto IL_08cd;
					}
					if (FoxFireCmd.Get(_003Cplayer_003E5__3) > 0)
					{
						val3 = FoxFireCmd.Lose(1, _003Cplayer_003E5__3).GetAwaiter();
						if (!val3.IsCompleted)
						{
							num = (_003C_003E1__state = 5);
							_003C_003Eu__1 = val3;
							_003C_003Et__builder.AwaitUnsafeOnCompleted<TaskAwaiter, _003COnPlay_003Ed__5>(ref val3, ref this);
							return;
						}
						goto IL_0359;
					}
					goto IL_03d1;
				}
				case 0:
					val3 = _003C_003Eu__1;
					_003C_003Eu__1 = default(TaskAwaiter);
					num = (_003C_003E1__state = -1);
					goto IL_0115;
				case 1:
					val3 = _003C_003Eu__1;
					_003C_003Eu__1 = default(TaskAwaiter);
					num = (_003C_003E1__state = -1);
					goto IL_0186;
				case 2:
					val3 = _003C_003Eu__1;
					_003C_003Eu__1 = default(TaskAwaiter);
					num = (_003C_003E1__state = -1);
					goto IL_01f0;
				case 3:
					val3 = _003C_003Eu__1;
					_003C_003Eu__1 = default(TaskAwaiter);
					num = (_003C_003E1__state = -1);
					goto IL_0258;
				case 4:
					val3 = _003C_003Eu__1;
					_003C_003Eu__1 = default(TaskAwaiter);
					num = (_003C_003E1__state = -1);
					goto IL_02c2;
				case 5:
					val3 = _003C_003Eu__1;
					_003C_003Eu__1 = default(TaskAwaiter);
					num = (_003C_003E1__state = -1);
					goto IL_0359;
				case 6:
					val3 = _003C_003Eu__1;
					_003C_003Eu__1 = default(TaskAwaiter);
					num = (_003C_003E1__state = -1);
					goto IL_03ca;
				case 7:
					val5 = _003C_003Eu__2;
					_003C_003Eu__2 = default(TaskAwaiter<StrengthPower>);
					num = (_003C_003E1__state = -1);
					goto IL_0446;
				case 8:
					val3 = _003C_003Eu__1;
					_003C_003Eu__1 = default(TaskAwaiter);
					num = (_003C_003E1__state = -1);
					goto IL_04b1;
				case 9:
					val3 = _003C_003Eu__1;
					_003C_003Eu__1 = default(TaskAwaiter);
					num = (_003C_003E1__state = -1);
					goto IL_051c;
				case 10:
					val4 = _003C_003Eu__3;
					_003C_003Eu__3 = default(TaskAwaiter<int>);
					num = (_003C_003E1__state = -1);
					goto IL_05c9;
				case 11:
					val3 = _003C_003Eu__1;
					_003C_003Eu__1 = default(TaskAwaiter);
					num = (_003C_003E1__state = -1);
					goto IL_062f;
				case 12:
					val3 = _003C_003Eu__1;
					_003C_003Eu__1 = default(TaskAwaiter);
					num = (_003C_003E1__state = -1);
					goto IL_0698;
				case 13:
					val3 = _003C_003Eu__1;
					_003C_003Eu__1 = default(TaskAwaiter);
					num = (_003C_003E1__state = -1);
					goto IL_0703;
				case 14:
					val3 = _003C_003Eu__1;
					_003C_003Eu__1 = default(TaskAwaiter);
					num = (_003C_003E1__state = -1);
					goto IL_076e;
				case 15:
					val3 = _003C_003Eu__1;
					_003C_003Eu__1 = default(TaskAwaiter);
					num = (_003C_003E1__state = -1);
					goto IL_07de;
				case 16:
					val3 = _003C_003Eu__1;
					_003C_003Eu__1 = default(TaskAwaiter);
					num = (_003C_003E1__state = -1);
					goto IL_0849;
				case 17:
					val2 = _003C_003Eu__4;
					_003C_003Eu__4 = default(TaskAwaiter<NurseMainHealingPower>);
					num = (_003C_003E1__state = -1);
					goto IL_08c5;
				case 18:
					{
						val = _003C_003Eu__5;
						_003C_003Eu__5 = default(TaskAwaiter<MainMelodyPower>);
						num = (_003C_003E1__state = -1);
						break;
					}
					IL_05c9:
					val4.GetResult();
					goto IL_05d1;
					IL_05d1:
					val3 = FoxFireCmd.Gain(1, _003Cplayer_003E5__3).GetAwaiter();
					if (!val3.IsCompleted)
					{
						num = (_003C_003E1__state = 11);
						_003C_003Eu__1 = val3;
						_003C_003Et__builder.AwaitUnsafeOnCompleted<TaskAwaiter, _003COnPlay_003Ed__5>(ref val3, ref this);
						return;
					}
					goto IL_062f;
					IL_04b1:
					val3.GetResult();
					val3 = FormSystem.SetStage(choiceContext, _003Ccreature_003E5__2, SectionStage.Main).GetAwaiter();
					if (!val3.IsCompleted)
					{
						num = (_003C_003E1__state = 9);
						_003C_003Eu__1 = val3;
						_003C_003Et__builder.AwaitUnsafeOnCompleted<TaskAwaiter, _003COnPlay_003Ed__5>(ref val3, ref this);
						return;
					}
					goto IL_051c;
					IL_062f:
					val3.GetResult();
					val3 = PlayerCmd.GainEnergy(1m, _003Cplayer_003E5__3).GetAwaiter();
					if (!val3.IsCompleted)
					{
						num = (_003C_003E1__state = 12);
						_003C_003Eu__1 = val3;
						_003C_003Et__builder.AwaitUnsafeOnCompleted<TaskAwaiter, _003COnPlay_003Ed__5>(ref val3, ref this);
						return;
					}
					goto IL_0698;
					IL_0115:
					val3.GetResult();
					val3 = PursuitExecutor.Chase(choiceContext, _003Cplayer_003E5__3, 1, 6m).GetAwaiter();
					if (!val3.IsCompleted)
					{
						num = (_003C_003E1__state = 1);
						_003C_003Eu__1 = val3;
						_003C_003Et__builder.AwaitUnsafeOnCompleted<TaskAwaiter, _003COnPlay_003Ed__5>(ref val3, ref this);
						return;
					}
					goto IL_0186;
					IL_03ca:
					val3.GetResult();
					goto IL_03d1;
					IL_0186:
					val3.GetResult();
					goto IL_018d;
					IL_018d:
					val3 = CreatureCmd.Heal(_003Ccreature_003E5__2, 6m, true).GetAwaiter();
					if (!val3.IsCompleted)
					{
						num = (_003C_003E1__state = 2);
						_003C_003Eu__1 = val3;
						_003C_003Et__builder.AwaitUnsafeOnCompleted<TaskAwaiter, _003COnPlay_003Ed__5>(ref val3, ref this);
						return;
					}
					goto IL_01f0;
					IL_0698:
					val3.GetResult();
					val3 = FormSystem.SwitchForm(choiceContext, _003Ccreature_003E5__2, ZhaoForm.Diva).GetAwaiter();
					if (!val3.IsCompleted)
					{
						num = (_003C_003E1__state = 13);
						_003C_003Eu__1 = val3;
						_003C_003Et__builder.AwaitUnsafeOnCompleted<TaskAwaiter, _003COnPlay_003Ed__5>(ref val3, ref this);
						return;
					}
					goto IL_0703;
					IL_01f0:
					val3.GetResult();
					val3 = PlayerCmd.GainEnergy(1m, _003Cplayer_003E5__3).GetAwaiter();
					if (!val3.IsCompleted)
					{
						num = (_003C_003E1__state = 3);
						_003C_003Eu__1 = val3;
						_003C_003Et__builder.AwaitUnsafeOnCompleted<TaskAwaiter, _003COnPlay_003Ed__5>(ref val3, ref this);
						return;
					}
					goto IL_0258;
					IL_051c:
					val3.GetResult();
					goto IL_08cd;
					IL_0258:
					val3.GetResult();
					val3 = FormSystem.SetStage(choiceContext, _003Ccreature_003E5__2, SectionStage.Main).GetAwaiter();
					if (!val3.IsCompleted)
					{
						num = (_003C_003E1__state = 4);
						_003C_003Eu__1 = val3;
						_003C_003Et__builder.AwaitUnsafeOnCompleted<TaskAwaiter, _003COnPlay_003Ed__5>(ref val3, ref this);
						return;
					}
					goto IL_02c2;
					IL_0359:
					val3.GetResult();
					val3 = PursuitExecutor.Chase(choiceContext, _003Cplayer_003E5__3, 1, 6m).GetAwaiter();
					if (!val3.IsCompleted)
					{
						num = (_003C_003E1__state = 6);
						_003C_003Eu__1 = val3;
						_003C_003Et__builder.AwaitUnsafeOnCompleted<TaskAwaiter, _003COnPlay_003Ed__5>(ref val3, ref this);
						return;
					}
					goto IL_03ca;
					IL_02c2:
					val3.GetResult();
					goto IL_08cd;
					IL_076e:
					val3.GetResult();
					goto IL_08cd;
					IL_077a:
					val3 = FormSystem.SwitchForm(choiceContext, _003Ccreature_003E5__2, ZhaoForm.Diva).GetAwaiter();
					if (!val3.IsCompleted)
					{
						num = (_003C_003E1__state = 15);
						_003C_003Eu__1 = val3;
						_003C_003Et__builder.AwaitUnsafeOnCompleted<TaskAwaiter, _003COnPlay_003Ed__5>(ref val3, ref this);
						return;
					}
					goto IL_07de;
					IL_0703:
					val3.GetResult();
					val3 = FormSystem.SetStage(choiceContext, _003Ccreature_003E5__2, SectionStage.Main).GetAwaiter();
					if (!val3.IsCompleted)
					{
						num = (_003C_003E1__state = 14);
						_003C_003Eu__1 = val3;
						_003C_003Et__builder.AwaitUnsafeOnCompleted<TaskAwaiter, _003COnPlay_003Ed__5>(ref val3, ref this);
						return;
					}
					goto IL_076e;
					IL_07de:
					val3.GetResult();
					val3 = FormSystem.SetStage(choiceContext, _003Ccreature_003E5__2, SectionStage.Main).GetAwaiter();
					if (!val3.IsCompleted)
					{
						num = (_003C_003E1__state = 16);
						_003C_003Eu__1 = val3;
						_003C_003Et__builder.AwaitUnsafeOnCompleted<TaskAwaiter, _003COnPlay_003Ed__5>(ref val3, ref this);
						return;
					}
					goto IL_0849;
					IL_0446:
					val5.GetResult();
					val3 = FormSystem.SwitchForm(choiceContext, _003Ccreature_003E5__2, ZhaoForm.Diva).GetAwaiter();
					if (!val3.IsCompleted)
					{
						num = (_003C_003E1__state = 8);
						_003C_003Eu__1 = val3;
						_003C_003Et__builder.AwaitUnsafeOnCompleted<TaskAwaiter, _003COnPlay_003Ed__5>(ref val3, ref this);
						return;
					}
					goto IL_04b1;
					IL_0849:
					val3.GetResult();
					val2 = PowerCmd.Apply<NurseMainHealingPower>(choiceContext, _003Ccreature_003E5__2, 1m, _003Ccreature_003E5__2, (CardModel)sectionMain, false).GetAwaiter();
					if (!val2.IsCompleted)
					{
						num = (_003C_003E1__state = 17);
						_003C_003Eu__4 = val2;
						_003C_003Et__builder.AwaitUnsafeOnCompleted<TaskAwaiter<NurseMainHealingPower>, _003COnPlay_003Ed__5>(ref val2, ref this);
						return;
					}
					goto IL_08c5;
					IL_03d1:
					val5 = PowerCmd.Apply<StrengthPower>(choiceContext, _003Ccreature_003E5__2, 3m, _003Ccreature_003E5__2, (CardModel)sectionMain, false).GetAwaiter();
					if (!val5.IsCompleted)
					{
						num = (_003C_003E1__state = 7);
						_003C_003Eu__2 = val5;
						_003C_003Et__builder.AwaitUnsafeOnCompleted<TaskAwaiter<StrengthPower>, _003COnPlay_003Ed__5>(ref val5, ref this);
						return;
					}
					goto IL_0446;
					IL_08c5:
					val2.GetResult();
					goto IL_08cd;
					IL_0528:
					powerAmount = _003Ccreature_003E5__2.GetPowerAmount<LightPower>();
					num2 = Math.Min(2, powerAmount);
					if (num2 > 0)
					{
						val4 = PowerCmd.ModifyAmount(choiceContext, (PowerModel)_003Ccreature_003E5__2.GetPower<LightPower>(), (decimal)(-num2), _003Ccreature_003E5__2, (CardModel)sectionMain, false).GetAwaiter();
						if (!val4.IsCompleted)
						{
							num = (_003C_003E1__state = 10);
							_003C_003Eu__3 = val4;
							_003C_003Et__builder.AwaitUnsafeOnCompleted<TaskAwaiter<int>, _003COnPlay_003Ed__5>(ref val4, ref this);
							return;
						}
						goto IL_05c9;
					}
					goto IL_05d1;
					IL_08cd:
					if (((CardModel)sectionMain).CurrentUpgradeLevel >= 1)
					{
						val = PowerCmd.Apply<MainMelodyPower>(choiceContext, _003Ccreature_003E5__2, 1m, _003Ccreature_003E5__2, (CardModel)sectionMain, false).GetAwaiter();
						if (!val.IsCompleted)
						{
							num = (_003C_003E1__state = 18);
							_003C_003Eu__5 = val;
							_003C_003Et__builder.AwaitUnsafeOnCompleted<TaskAwaiter<MainMelodyPower>, _003COnPlay_003Ed__5>(ref val, ref this);
							return;
						}
						break;
					}
					goto end_IL_000e;
				}
				val.GetResult();
				end_IL_000e:;
			}
			catch (global::System.Exception exception)
			{
				_003C_003E1__state = -2;
				_003Ccreature_003E5__2 = null;
				_003Cplayer_003E5__3 = null;
				_003C_003Et__builder.SetException(exception);
				return;
			}
			_003C_003E1__state = -2;
			_003Ccreature_003E5__2 = null;
			_003Cplayer_003E5__3 = null;
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
			if (!base.IsPlayable)
			{
				return false;
			}
			Creature creature = ((CardModel)this).Owner.Creature;
			ZhaoForm currentForm = FormSystem.GetCurrentForm(creature);
			SectionPower section = FormSystem.GetSection(creature);
			if (section != null && section.Stage == SectionStage.Intro)
			{
				return true;
			}
			if ((uint)(currentForm - 1) > 1u && currentForm != ZhaoForm.Lady)
			{
				return false;
			}
			return true;
		}
	}

	public SectionMain()
		: base(1, CardType.Skill, CardRarity.Token, TargetType.Self)
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
		return TransformHelper.TransformInto<SectionChorus>((CardModel)this);
	}

	protected override void OnUpgrade()
	{
		if (((CardModel)this).CurrentUpgradeLevel == 1)
		{
			((CardModel)this).EnergyCost.UpgradeBy(-1);
		}
	}
}
