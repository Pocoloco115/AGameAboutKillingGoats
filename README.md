# A Game About Killing Goats or Something

![Game Preview](Media/gameplay.gif)

Hi,
**A Game About Killing Goats or Something** is a third-person/first-person arcade shooter built in Unity. The project focuses on short gameplay loops, lightweight progression, and a set of supporting systems around the core action.

The current build is a playable version and includes gameplay, menus, persistent configuration, achievements, and save data handling.

## Overview

The game is centered on surviving against explosive goats while managing movement, combat, reload timing, and resource pressure. Around that loop, the project also implements several supporting systems that make the build playable as a complete package rather than only a prototype.

## Implemented Systems

### Gameplay

- Player movement, camera control, jumping, crouching, and sprinting
- Weapon firing, reload flow, ammo UI, and combat feedback
- Enemy spawning and goat behavior
- Damage handling, death flow, and player recovery systems
- Scene transitions and loading flow

### Progression and Achievements

- Achievement catalog loaded from data
- Unlock tracking for gameplay milestones
- Achievement persistence across sessions
- Achievement UI and popup feedback

### Settings and Configuration

- Graphics configuration with quality and VSync options
- Control configuration with remappable bindings
- Audio configuration with separate volume controls
- In-game menus for graphics, audio, keyboard, and general settings

### Serialization and Persistence

- JSON-based save files stored in the user persistence folder
- Saved statistics data
- Saved achievement state
- Saved control bindings
- Saved graphics configuration

### Audio

- Centralized audio manager
- Background music handling for menu and gameplay contexts
- SFX routing for weapon, enemy, UI, hit, reload, and explosion events
- Volume application based on saved configuration

### UI

- Main menu, settings menus, gameplay HUD, pause menu, and achievement screen
- Ammo, timer, stats, and feedback widgets
- Buttons and sliders connected to the configuration systems

### Notes on Presentation

- Some menus are still visually simple and not fully decorated
- All implemented menu flows and interactions work as expected

## Project Structure

```text
Assets/
├─ Animation/
├─ Material/
├─ Models/
├─ Physic Material/
├─ Prefabs/
├─ Resources/
│  ├─ achievements_catalog.json
│  └─ AchievementIcons/
├─ Scenes/
├─ Scripts/
│  ├─ AI/
│  ├─ Game/
│  │  ├─ Shared/
│  │  └─ UI/
│  ├─ Gameplay/
│  │  └─ Manager/
│  └─ ...
├─ Settings/
├─ SFX/
├─ Sprites/
├─ TextMesh Pro/
├─ TutorialInfo/
└─ YughuesFreeSandMaterials/

Attributions/
Media/
Packages/
ProjectSettings/
```

## Build and Status

- Current state: playable version
- Target platforms: Windows and Linux
- Repository includes release-oriented assets, configuration, persistence systems, and gameplay content

## Download the Build

The latest builds are published in the [Releases section](https://github.com/Pocoloco115/AGameAboutKillingGoats/releases).

### Windows

1. Open the latest release.
2. Download the Windows build artifact.
3. Extract the archive if needed and run the executable.

### Linux

1. Open the latest release.
2. Download the Linux build artifact.
3. Extract the archive if needed and run the executable with the appropriate permissions.

## Credits

Some art, sound, and third-party assets were used in the project. Full attribution details are available in [Attributions/Attributions.txt](Attributions/Attributions.txt).
