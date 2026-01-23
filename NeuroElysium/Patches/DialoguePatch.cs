using HarmonyLib;
using PixelCrushers.DialogueSystem;

namespace NeuroElysium.Patches;

internal class DialoguePatch {
    [HarmonyPatch(typeof(ConversationView), "StartSubtitle")]
    [HarmonyPrefix]
    static void StartSubtitlePrefix(Subtitle subtitle, bool isPCResponseMenuNext, bool isPCAutoResponseNext) {
        Plugin.Log.LogInfo(subtitle.dialogueEntry.currentDialogueText);
    }
}