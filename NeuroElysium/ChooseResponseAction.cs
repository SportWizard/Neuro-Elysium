using Il2CppInterop.Runtime.InteropTypes.Arrays;
using NeuroSdk.Actions;
using NeuroSdk.Json;
using NeuroSdk.Websocket;
using Newtonsoft.Json.Linq;
using PixelCrushers.DialogueSystem;
using System;
using System.Collections.Generic;

namespace NeuroElysium;

internal class ChooseResponseAction : NeuroAction<Response> {
    private readonly Il2CppReferenceArray<Response> _responses;
    private readonly string[] _responsesString;

    public ChooseResponseAction(Il2CppReferenceArray<Response> responses) {
        _responses = responses;
        _responsesString = new string[responses.Length];

        for (int i = 0; i < responses.Length; i++)
            _responsesString[i] = responses[i].destinationEntry.DialogueText;
    }

    public override string Name => "choose_response";

    protected override string Description => "Select a dialogue response";
    
    protected override JsonSchema Schema => new() {
        Type = JsonSchemaType.Object,
        Required = new List<string> { "choice" },
        Properties = new Dictionary<string, JsonSchema> {
            ["choice"] = QJS.Enum(_responsesString)
        }
    };

    protected override ExecutionResult Validate(ActionJData actionData, out Response? response) {
        string choice = actionData.Data?["choice"]?.Value<string>();

        if (string.IsNullOrEmpty(choice)) {
            response = null;
            return ExecutionResult.Failure("Action failed. Missing required parameter 'choice'.");
        }

        int index = Array.FindIndex(_responsesString, r => r.Equals(choice));

        if (index == -1) {
            response = null;
            return ExecutionResult.Failure("Action failed. Invalid parameter 'choice'.");
        }

        response = _responses[index];
        return ExecutionResult.Success();
    }

    protected override void Execute(Response response) {
        DialogueManager.ConversationView.SelectResponse(new SelectedResponseEventArgs(response));
    }
}