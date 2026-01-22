using HarmonyLib;
using PixelCrushers.DialogueSystem;
using System;

namespace NeuroElysium.Patches;

[HarmonyPatch(typeof(Conversation), "GetDialogueEntry", new Type[] { typeof(int) })]
internal class DialoguePatch {
    [HarmonyPostfix]
    static void CurrentDialogue(DialogueEntry __result) {
        Plugin.Log.LogInfo(__result.currentDialogueText);
    }
}