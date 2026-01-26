using NeuroSdk.Actions;
using NeuroSdk.Json;
using NeuroSdk.Websocket;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace NeuroElysium.Actions;

internal class ChooseResponseAction : NeuroAction<SunshineResponseButton> {
    private readonly List<SunshineResponseButton> _buttons;
    private readonly string[] _dialogueResponses;

    public ChooseResponseAction(List<SunshineResponseButton> buttons) {
        _buttons = buttons;
        _dialogueResponses = new string[_buttons.Count];

        for (int i = 0; i < _buttons.Count; i++)
            _dialogueResponses[i] = _buttons[i].response.destinationEntry.currentDialogueText;
    }

    public override string Name => "choose_response";

    protected override string Description => "Select a dialogue response";
    
    protected override JsonSchema Schema => new() {
        Type = JsonSchemaType.Object,
        Required = ["choice"],
        Properties = new Dictionary<string, JsonSchema> {
            ["choice"] = QJS.Enum(_dialogueResponses)
        }
    };

    protected override ExecutionResult Validate(ActionJData actionData, out SunshineResponseButton? button) {
        string choice = actionData.Data?["choice"]?.Value<string>();

        if (string.IsNullOrEmpty(choice)) {
            button = null;
            return ExecutionResult.Failure("Action failed. Missing required parameter 'choice'.");
        }

        int index = Array.FindIndex(_dialogueResponses, r => r.Equals(choice));

        if (index == -1) {
            button = null;
            return ExecutionResult.Failure("Action failed. Invalid parameter 'choice'.");
        }

        button = _buttons[index];
        return ExecutionResult.Success();
    }

    protected override void Execute(SunshineResponseButton button) {
        button.RegisterClick();
    }
}