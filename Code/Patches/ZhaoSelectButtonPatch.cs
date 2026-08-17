using System;
using System.Collections;
using System.Collections.Generic;
using Godot;
using HarmonyLib;
using MegaCrit.Sts2.Core.Nodes.GodotExtensions;
using MegaCrit.Sts2.Core.Nodes.Screens.CharacterSelect;
using Zhao.Character;

namespace Zhao.Patches;

[HarmonyPatch(typeof(NCharacterSelectButton), "Init")]
public static class ZhaoSelectButtonPatch
{
	private static void Postfix(NCharacterSelectButton __instance, object[] __args)
	{
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Expected O, but got Unknown
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Expected O, but got Unknown
		if (__args.Length != 0 && __args[0] is ZhaoCharacter)
		{
			ZhaoSelectFrame zhaoSelectFrame = ZhaoSelectFrame.Create();
			((Node)__instance).AddChild((Node)zhaoSelectFrame, false, InternalMode.Disabled);
			((Node)__instance).MoveChild((Node)zhaoSelectFrame, 0);
			ConfigureInput(__instance);
		}
	}

	private static void ConfigureInput(NCharacterSelectButton button)
	{
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Expected O, but got Unknown
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		((Control)button).MouseFilter = (MouseFilterEnum)0;
		IgnoreChildControls((Node)button);
		((GodotObject)button).Connect(BaseButton.SignalName.GuiInput, Callable.From<InputEvent>((Action<InputEvent>)delegate(InputEvent inputEvent)
		{
			SelectOnClick(button, inputEvent);
		}), 0u);
	}

	private static void IgnoreChildControls(Node parent)
	{
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Expected O, but got Unknown
		global::System.Collections.Generic.IEnumerator<Node> enumerator = parent.GetChildren(false).GetEnumerator();
		try
		{
			while (((global::System.Collections.IEnumerator)enumerator).MoveNext())
			{
				Node current = enumerator.Current;
				Control val = (Control)((current is Control) ? current : null);
				if (val != null)
				{
					val.MouseFilter = MouseFilterEnum.Ignore;
				}
				IgnoreChildControls(current);
			}
		}
		finally
		{
			((global::System.IDisposable)enumerator)?.Dispose();
		}
	}

	private static void SelectOnClick(NCharacterSelectButton button, InputEvent inputEvent)
	{
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Expected O, but got Unknown
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Invalid comparison between Unknown and I8
		if (((NClickableControl)button).IsEnabled && !button.IsLocked)
		{
			InputEventMouseButton val = (InputEventMouseButton)((inputEvent is InputEventMouseButton) ? inputEvent : null);
			if (val != null && (long)val.ButtonIndex == 1 && val.Pressed)
			{
				button.Select();
			}
		}
	}
}
