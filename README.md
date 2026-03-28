# Neuro-Elysium
This project provides an integration that allows Neuro-sama to interact with [Disco Elysium](https://store.steampowered.com/app/632470/Disco_Elysium__The_Final_Cut) using the [Neuro SDK](https://github.com/VedalAI/neuro-sdk).

## What can Neuro control?
Currently, Neuro can only:
- Choose dialogue responses (excluding "Continue" and "End" responses)

## Prerequisites
- [.NET 6 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/6.0)
- [Disco Elysium](https://store.steampowered.com/app/632470/Disco_Elysium__The_Final_Cut)
- Neuro Agent running locally (e.g. [neuro-api-tony](https://github.com/Pasu4/neuro-api-tony))
- Environment variable `NEURO_SDK_WS_URL` (e.g. ws://localhost:8000)

## Installation
1. Installed [BepInEx 6-be Unity (IL2CPP)](https://builds.bepinex.dev/projects/bepinex_be)
2. Go to `PathOfGame\BepInEx\interop` and copy the following to `.\NeuroElysium\lib`
   - `Assembly-CSharp.dll`
   - `DialogueSystem.dll`
   - `Il2Cppmscorlib.dll`
   - `Il2CppSystem.dll`
   - `UnityEngine.CoreModule.dll`
   - `UnityEngine.UnityWebRequestModule.dll`
3. Run `dotnet build` in `.\NeuroElysium`
4. Copy `.\NeuroElysium\bin\Debug\net6.0\NeuroElysium.dll` to `PathOfGame\BepInEx\plugins`

> [!NOTE]
> You might require to download [Newtonsoft.Json](https://github.com/JamesNK/Newtonsoft.Json) v13.0.3 and extract `.\Bin\net6.0\Newtonsoft.Json.dll` to `PathOfGame\BepInEx\plugins` for the mod to work with the Neuro SDK. Since Neuro SDK v2.0.0 uses Newtonsoft.Json v13.0.3.

## Disclaimer
This mod does **not include Disco Elysium or any of its game files**. You must own the game and have it installed yourself. Do **not redistribute any game files or DLLs**; only copy them locally for the mod to work.

All game content is accessed at runtime from the user's local installation and is not stored or distributed by this project.

This project is not affiliated with or endorsed by ZA/UM.
Disco Elysium is a trademark and property of its respective owners.
