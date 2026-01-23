using HarmonyLib;
using Il2CppInterop.Runtime.InteropTypes.Arrays;
using NeuroSdk.Actions;
using PixelCrushers.DialogueSystem;

namespace NeuroElysium.Patches;

[HarmonyPatch]
internal class ResponsesPatch {
    [HarmonyPatch(typeof(ConversationView), "StartResponses")]
    [HarmonyPrefix]
    static void StartResponsesPrefix(Subtitle subtitle, Il2CppReferenceArray<Response> responses) {
        if (responses == null)
            return;

        ActionWindow.Create(GO.PluginObject)
            .SetForce(0, "Choose a response", "", false)
            .AddAction(new ChooseResponseAction(responses))
            .Register();
    }
}