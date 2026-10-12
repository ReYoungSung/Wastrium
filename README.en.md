[한국어](README.md) | **English**

# Wastrium

An XR waste-sorting game using motion capture and a large screen. It delivers feedback about marine pollution through gameplay. The name combines “Waste” and “Aquarium.”

## Background

A research project associated with KCGS 2023, the Korea Computer Graphics Society conference. Youngsung Ree led the main game development and project management and worked with the supervising professor on C# gesture-recognition scripts using OptiTrack–Motive tracking data.

## Team

CGLab (Computer Graphics Laboratory) — 이영성 (Youngsung Ree), 김윤섭, 김현진, 유승은, 채지훈

## Development environment

| Item | Value |
| --- | --- |
| Unity Editor | **2018.2.1f1** |
| Unity project folder | `CASA/` |
| Default branch | `main` |

The Unity version is taken from [ProjectVersion.txt](CASA/ProjectSettings/ProjectVersion.txt).

- OptiTrack equipment and Motive
- Immersion Pro
- A space suitable for a large screen and motion capture

## Getting started

```bash
git clone --branch main https://github.com/ReYoungSung/Wastrium.git
```

1. Install the Editor version above through Unity Hub.
2. Add the repository's `CASA` folder as a project. The repository root and Unity project folder are different.
3. Complete the initial import and Package Manager dependency restoration.
4. Open `Assets/Scenes/InGame.unity` and enter Play mode.

The first scene in the current build configuration is `InGame.unity`; the second is `RealFinalStartScene.unity`. Check initialization and transitions before changing the order based on names alone. OptiTrack reception and exhibition display settings must be configured to reproduce the on-site interaction.

See [EditorBuildSettings.asset](CASA/ProjectSettings/EditorBuildSettings.asset) for build configuration and [manifest.json](CASA/Packages/manifest.json) for dependencies. The `Assets/` paths below are relative to the Unity project folder.

## Main folders

| Path | Contents |
| --- | --- |
| [Assets/Scenes/](CASA/Assets/Scenes/) | Gameplay, start, and mission scenes |
| [Assets/Scripts/](CASA/Assets/Scripts/) | Waste spawning, scoring, and gesture recognition |
| [Assets/OptiTrack/](CASA/Assets/OptiTrack/) | Motion-capture integration |

## Development notes

- `HandTracker.cs` recognizes horizontal/vertical movements and circular gestures.
- Distinguish `CASA.exe` at the repository root from the Unity source folder `CASA/`. Open `CASA/` in Unity for development.
- The project targets Unity 2018.2.1f1. Check it in that version before considering an engine upgrade.

## Usage

### Experience flow

1. The operator checks the OptiTrack–Motive connection to Unity, projection alignment, and waste-sorting detection areas.
2. Set the participant count and feedback mode in the `InGame` scene.
3. On screens prompting Enter, press Enter to advance and wait for the starting countdown.
4. Manipulate objects linked to the tracking equipment to move on-screen waste to the correct sorting area. Once the item is placed steadily inside the matching area, points and sound effects indicate the result.
5. During missions, perform the prompted hand gestures and check the gauges, time, and result screens.

### Operator settings

The following keys are defined in the gameplay scene at build index 0.

| Key | Function |
| --- | --- |
| `1` | Single-player mode |
| `3` | Three-player cooperative mode |
| `P` | Positive-feedback / ending mode |
| `N` | Negative-feedback / ending mode |
| `Enter` | Advance from a UI waiting for Enter |

Sorting detection uses tags such as plastic, paper, and cans, together with area coordinates. If the physical layout changes, align the detection regions with on-site coordinates. The current code awards 300 points for a correct placement in single-player mode and 100 in cooperative mode.

### Diagnostic inputs and ending

`Q/W/E` directly trigger sorting scores, and the up/down arrow keys artificially increase gesture counts. Use these only for operator diagnostics so they do not affect normal session scores. Exit Play mode to end the experience in the Editor.

Behavior references: [GenerateFallingTrash.cs](CASA/Assets/Scripts/GenerateFallingTrash.cs) · [RightPositionDiscriminator.cs](CASA/Assets/Scripts/RightPositionDiscriminator.cs) · [UI_pressEnter.cs](CASA/Assets/Scripts/UI_pressEnter.cs)
