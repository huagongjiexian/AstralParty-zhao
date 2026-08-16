using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
using HarmonyLib;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.Models;
using Zhao.Character;

namespace Zhao.Patches;

[HarmonyPatch]
public static class ZhaoVisualPatch
{
	[CompilerGenerated]
	private sealed class _003CTargetMethods_003Ed__1 : global::System.Collections.Generic.IEnumerable<MethodBase>, global::System.Collections.IEnumerable, global::System.Collections.Generic.IEnumerator<MethodBase>, global::System.Collections.IEnumerator, global::System.IDisposable
	{
		private int _003C_003E1__state;

		private MethodBase _003C_003E2__current;

		private int _003C_003El__initialThreadId;

		private Enumerator<string, string> _003C_003E7__wrap1;

		MethodBase global::System.Collections.Generic.IEnumerator<MethodBase>.Current
		{
			[DebuggerHidden]
			get
			{
				return _003C_003E2__current;
			}
		}

		object global::System.Collections.IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return _003C_003E2__current;
			}
		}

		[DebuggerHidden]
		public _003CTargetMethods_003Ed__1(int _003C_003E1__state)
		{
			this._003C_003E1__state = _003C_003E1__state;
			_003C_003El__initialThreadId = Environment.CurrentManagedThreadId;
		}

		[DebuggerHidden]
		void global::System.IDisposable.Dispose()
		{
			//IL_0020: Unknown result type (might be due to invalid IL or missing references)
			int num = _003C_003E1__state;
			if (num == -3 || num == 1)
			{
				try
				{
				}
				finally
				{
					_003C_003Em__Finally1();
				}
			}
			_003C_003E7__wrap1 = default(Enumerator<string, string>);
			_003C_003E1__state = -2;
		}

		private bool MoveNext()
		{
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			//IL_002c: Unknown result type (might be due to invalid IL or missing references)
			//IL_009a: Unknown result type (might be due to invalid IL or missing references)
			try
			{
				switch (_003C_003E1__state)
				{
				default:
					return false;
				case 0:
					_003C_003E1__state = -1;
					_003C_003E7__wrap1 = PathOverrides.Keys.GetEnumerator();
					_003C_003E1__state = -3;
					break;
				case 1:
					_003C_003E1__state = -3;
					break;
				}
				while (_003C_003E7__wrap1.MoveNext())
				{
					string current = _003C_003E7__wrap1.Current;
					MethodInfo val = AccessTools.PropertyGetter(typeof(CharacterModel), current.Substring(4));
					if (val != (MethodInfo)null)
					{
						_003C_003E2__current = (MethodBase)(object)val;
						_003C_003E1__state = 1;
						return true;
					}
				}
				_003C_003Em__Finally1();
				_003C_003E7__wrap1 = default(Enumerator<string, string>);
				return false;
			}
			catch
			{
				//try-fault
				((global::System.IDisposable)this).Dispose();
				throw;
			}
		}

		bool global::System.Collections.IEnumerator.MoveNext()
		{
			//ILSpy generated this explicit interface implementation from .override directive in MoveNext
			return this.MoveNext();
		}

		private void _003C_003Em__Finally1()
		{
			_003C_003E1__state = -1;
			((global::System.IDisposable)_003C_003E7__wrap1/*cast due to .constrained prefix*/).Dispose();
		}

		[DebuggerHidden]
		void global::System.Collections.IEnumerator.Reset()
		{
			//IL_0000: Unknown result type (might be due to invalid IL or missing references)
			throw new NotSupportedException();
		}

		[DebuggerHidden]
		global::System.Collections.Generic.IEnumerator<MethodBase> global::System.Collections.Generic.IEnumerable<MethodBase>.GetEnumerator()
		{
			if (_003C_003E1__state == -2 && _003C_003El__initialThreadId == Environment.CurrentManagedThreadId)
			{
				_003C_003E1__state = 0;
				return this;
			}
			return new _003CTargetMethods_003Ed__1(0);
		}

		[DebuggerHidden]
		global::System.Collections.IEnumerator global::System.Collections.IEnumerable.GetEnumerator()
		{
			return (global::System.Collections.IEnumerator)((global::System.Collections.Generic.IEnumerable<MethodBase>)this).GetEnumerator();
		}
	}

	private static readonly Dictionary<string, string> PathOverrides = new Dictionary<string, string>
	{
		["get_VisualsPath"] = SceneHelper.GetScenePath("creature_visuals/zhao"),
		["get_TrailPath"] = SceneHelper.GetScenePath("vfx/card_trail_ironclad"),
		["get_IconTexturePath"] = "res://zhao/images/ui/zhao_character_placeholder.png",
		["get_IconOutlineTexturePath"] = "res://zhao/images/ui/zhao_character_placeholder.png",
		["get_EnergyCounterPath"] = SceneHelper.GetScenePath("combat/energy_counters/ironclad_energy_counter"),
		["get_RestSiteAnimPath"] = SceneHelper.GetScenePath("rest_site/characters/ironclad_rest_site"),
		["get_MerchantAnimPath"] = SceneHelper.GetScenePath("merchant/characters/ironclad_merchant"),
		["get_CharacterSelectBg"] = SceneHelper.GetScenePath("screens/char_select/char_select_bg_zhao"),
		["get_CharacterSelectTransitionPath"] = "res://materials/transitions/ironclad_transition_mat.tres",
		["get_AttackSfx"] = "event:/sfx/characters/ironclad/ironclad_attack",
		["get_CastSfx"] = "event:/sfx/characters/ironclad/ironclad_cast",
		["get_DeathSfx"] = "event:/sfx/characters/ironclad/ironclad_die",
		["get_ArmPointingTexturePath"] = "res://zhao/images/ui/zhao_character_placeholder.png",
		["get_ArmRockTexturePath"] = "res://zhao/images/ui/zhao_character_placeholder.png",
		["get_ArmPaperTexturePath"] = "res://zhao/images/ui/zhao_character_placeholder.png",
		["get_ArmScissorsTexturePath"] = "res://zhao/images/ui/zhao_character_placeholder.png"
	};

	[IteratorStateMachine(typeof(_003CTargetMethods_003Ed__1))]
	private static global::System.Collections.Generic.IEnumerable<MethodBase> TargetMethods()
	{
		Enumerator<string, string> enumerator = PathOverrides.Keys.GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				string current = enumerator.Current;
				MethodInfo val = AccessTools.PropertyGetter(typeof(CharacterModel), current.Substring(4));
				if (val != (MethodInfo)null)
				{
					yield return (MethodBase)(object)val;
				}
			}
		}
		finally
		{
			((global::System.IDisposable)enumerator/*cast due to .constrained prefix*/).Dispose();
		}
	}

	private static bool Prefix(CharacterModel __instance, MethodInfo __originalMethod, ref string? __result)
	{
		if (!(__instance is ZhaoCharacter))
		{
			return true;
		}
		string text = default(string);
		if (PathOverrides.TryGetValue(((MemberInfo)__originalMethod).Name, ref text))
		{
			__result = text;
			return false;
		}
		return true;
	}
}
