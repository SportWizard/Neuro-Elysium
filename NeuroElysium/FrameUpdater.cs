using PixelCrushers.DialogueSystem;
using UnityEngine;

namespace NeuroElysium;

internal class FrameUpdater : MonoBehaviour {
    private string prevText = "";

    private void Update() {
        if (!DialogueManager.isConversationActive)
            return;

        ConversationState conversationState = DialogueManager.currentConversationState;

        string text = conversationState.subtitle.dialogueEntry.subtitleText;
        bool hasReponses = conversationState.hasPCResponses;

        if (this.prevText == text)
            return;

        prevText = text;

        Plugin.Log.LogInfo($"Dialogue: {text}");

        if (!hasReponses)
            return;

        Response[] responses = conversationState.pcResponses;
        string[] textResponses = new string[responses.Length];

        Plugin.Log.LogInfo("Responses:");

        // Convert Response to string text
        for (int i = 0; i < responses.Length; i++) {
            textResponses[i] = responses[i].destinationEntry.subtitleText;
            Plugin.Log.LogInfo($"{i + 1}. {textResponses[i]}");
        }
    }
}