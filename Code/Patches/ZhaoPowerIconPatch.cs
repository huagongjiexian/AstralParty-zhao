using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
using HarmonyLib;
using MegaCrit.Sts2.Core.Models;

namespace Zhao.Patches;

[HarmonyPatch]
public static class ZhaoPowerIconPatch
{
	[CompilerGenerated]
	private sealed class _003CTargetMethods_003Ed__1 : global::System.Collections.Generic.IEnumerable<MethodBase>, global::System.Collections.IEnumerable, global::System.Collections.Generic.IEnumerator<MethodBase>, global::System.Collections.IEnumerator, global::System.IDisposable
	{
		private int _003C_003E1__state;

		private MethodBase _003C_003E2__current;

		private int _003C_003El__initialThreadId;

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
			_003C_003E1__state = -2;
		}

		private bool MoveNext()
		{
			switch (_003C_003E1__state)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				_003C_003E2__current = (MethodBase)(object)AccessTools.PropertyGetter(typeof(PowerModel), "PackedIconPath");
				_003C_003E1__state = 1;
				return true;
			case 1:
				_003C_003E1__state = -1;
				return false;
			}
		}

		bool global::System.Collections.IEnumerator.MoveNext()
		{
			//ILSpy generated this explicit interface implementation from .override directive in MoveNext
			return this.MoveNext();
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

	private const string PlaceholderPath = "res://zhao/images/powers/zhao_power_placeholder.png";

	[IteratorStateMachine(typeof(_003CTargetMethods_003Ed__1))]
	private static global::System.Collections.Generic.IEnumerable<MethodBase> TargetMethods()
	{
		yield return (MethodBase)(object)AccessTools.PropertyGetter(typeof(PowerModel), "PackedIconPath");
	}

	private static bool Prefix(PowerModel __instance, ref string __result)
	{
		if (((object)__instance).GetType().Namespace == "Zhao.Powers")
		{
			__result = "res://zhao/images/powers/zhao_power_placeholder.png";
			return false;
		}
		return true;
	}
}
