using BepInEx;
using BepInEx.Logging;
using BepInEx.Unity.IL2CPP;
using NeuroSdk;
using HarmonyLib;
using NeuroElysium.Patches;

namespace NeuroElysium;

[BepInPlugin("com.sportwizard.neuroelysium", "Neuro Elysium", "1.0.0")]
[BepInProcess("Disco Elysium.exe")]
internal class Plugin : BasePlugin {
    internal static new ManualLogSource Log;

    public override void Load() {
        Log = base.Log;

        Harmony harmony = new("com.sportwizard.neuroelysium");
        harmony.PatchAll(typeof(DialoguePatch));
        harmony.PatchAll(typeof(ResponsesPatch));

        NeuroSdkSetup.Initialize(Global.Game);
    }
}