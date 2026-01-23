using HarmonyLib;
using Il2CppInterop.Runtime.InteropTypes.Arrays;
using PixelCrushers.DialogueSystem;

namespace NeuroElysium.Patches;

internal class ResponsesPatch {
    [HarmonyPatch(typeof(ConversationView), "StartResponses")]
    [HarmonyPrefix]
    static void StartResponsesPrefix(Subtitle subtitle, Il2CppReferenceArray<Response> responses) {
        foreach (Response response in responses)
            Plugin.Log.LogInfo(response.destinationEntry.currentDialogueText);
    }
}