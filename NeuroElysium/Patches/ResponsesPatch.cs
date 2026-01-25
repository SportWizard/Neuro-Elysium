using HarmonyLib;
using NeuroSdk.Actions;
using PixelCrushers.DialogueSystem;
using System.Collections.Generic;

namespace NeuroElysium.Patches;

[HarmonyPatch]
internal class ResponsesPatch {
    private static List<SunshineResponseButton> buttons = [];

    [HarmonyPatch(typeof(SunshineResponseButton), "OnEnable")]
    [HarmonyPostfix]
    static void SunshineResponseButtonPostfix(ref SunshineResponseButton __instance) {
        if (__instance == null)
            return;

        buttons.Add(__instance);

        // Since each instance is only one response. Need to collect all the response before sending to Neuro
        if (DialogueManager.currentConversationState.pcResponses.Length == buttons.Count) {
            ActionWindow.Create(GO.PluginObject)
                .SetForce(0, "Choose a response", "", false)
                .AddAction(new ChooseResponseAction(ref buttons))
                .Register();

            buttons = [];
        }
    }
}