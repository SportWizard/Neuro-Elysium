using HarmonyLib;
using PixelCrushers.DialogueSystem;
using NeuroSdk.Messages.Outgoing;

namespace NeuroElysium.Patches;

[HarmonyPatch]
internal class DialoguePatch {
    [HarmonyPatch(typeof(ConversationView), "StartSubtitle")]
    [HarmonyPrefix]
    static void StartSubtitlePrefix(Subtitle subtitle, bool isPCResponseMenuNext, bool isPCAutoResponseNext) {
        string text = subtitle.dialogueEntry.currentDialogueText;

        if (string.IsNullOrEmpty(text))
            return;

        Plugin.Log.LogInfo(text);

        Context.Send(text);
    }
}