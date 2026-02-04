using HarmonyLib;
using NeuroElysium.Actions;
using NeuroSdk.Actions;
using PixelCrushers.DialogueSystem;
using System.Collections.Generic;

namespace NeuroElysium.Patches;

[HarmonyPatch]
internal class ResponsesPatch {
    private static List<SunshineResponseButton> _buttons = [];
    private static int _lockedButton = 0;

    [HarmonyPatch(typeof(SunshineResponseButton), "OnEnable")]
    [HarmonyPostfix]
    private static void SunshineResponseButtonPostfix(SunshineResponseButton __instance) {
        if (__instance == null || DialogueManager.CurrentConversationState == null || DialogueManager.CurrentConversationState.pcResponses == null)
            return;

        if (__instance.response.enabled)
            _buttons.Add(__instance);
        else
            _lockedButton++;

        // Since each instance is only one response. Need to collect all the response before sending to Neuro
        if (DialogueManager.CurrentConversationState.pcResponses.Length - _lockedButton == _buttons.Count) {
            ActionWindow.Create(GO.PluginObject)
                .SetForce(0, "Choose a response", "", false)
                .AddAction(new ChooseResponseAction(_buttons))
                .Register();

            _buttons = [];
            _lockedButton = 0;
        }
    }
}