# Zhao (照)

> A girl with four forms. Weaves her battle song with the Miko (Foxfire), Nurse (Healing), Diva (Sections) and Lady (Light).

Zhao is an original character mod for **Slay the Spire 2**. She switches freely between four forms, each with its own distinct battle rhythm:

| Form | Core Mechanic | Playstyle |
|---|---|---|
| Miko | Foxfire | The base form, stacking Foxfire into a growing snowball |
| Nurse | Healing | Sustained healing that ticks each turn and decays at turn end |
| Diva | Sections | Intro → Main → Chorus → Interlude → Outro, a rhythm-driven progression |
| Lady | Light | Bank Light, then unleash it all in a single burst |

## Download & Install

Grab the latest package from [Releases](https://github.com/huagongjiexian/AstralParty-zhao/releases) (`zhao-0.0.15.zip`), or take the three files directly from the `发布/` directory.

1. Unzip to get `zhao.dll`, `zhao.json`, and `zhao.pck`
2. Place the three files into the game's `mods\zhao\` folder (create it if it does not exist)
3. Launch the game and select **Zhao** on the character select screen

## Overview

| Field | Value |
|---|---|
| Mod id | `zhao` |
| Version | `0.0.15` |
| Author | 喵小照 (Miao Xiao Zhao) |
| Target game | Slay the Spire 2 `0.107.1` |

## Cards

Foxfire Strike · Miss Zhao is Our Light! · Section - Intro · Emergency Treatment · Section - Main · Section - Chorus · Outro · O Light! · Chase Chase · Fast Forward

## Directory Structure

| Path | Contents |
|---|---|
| `Code/` | C# mod source (`[ModInitializer]` entry point + Harmony patches) |
| `zhao/` | Godot assets: art `art/`, icons `images/`, localization `localization/`, video `video/` |
| `scenes/` | Godot scenes (creature visuals, UI, character select screen) |
| `src/` | Helper scripts |
| `发布/` | Deliverables `zhao.dll` / `zhao.json` / `zhao.pck` |
| `project.godot` / `zhao.csproj` / `zhao.sln` | Godot + .NET project files |
| `build_checks.ps1` | Pre-packaging static checks |

## Building

```powershell
dotnet build zhao.csproj
```

Re-export `zhao.pck`:

```powershell
megadot --headless --path . --export-pack "BasicExport" "发布\zhao.pck"
```

> `megadot` is the MegaDot (Godot 4.5) console executable matching the game.

Pre-packaging static check:

```powershell
powershell -File build_checks.ps1
```

## Dependencies

- Godot 4.5 (MegaDot, matching the game)
- .NET SDK 9
