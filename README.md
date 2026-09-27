# my-fps-game

Unity FPS prototype prepared as a runnable project.

## Requirements
- Unity 2022.3 LTS
- Windows/macOS/Linux editor

## Open and run
1. Clone/download this repository.
2. Open the repository root in Unity Hub.
3. Let Unity import the project.
4. Create a scene with a Player GameObject containing `CharacterController`, `PlayerController`, `Health`, a Camera, and a `HitscanWeapon` child.
5. Add a floor with a Collider and target objects with Collider + `Health`.
6. Press Play.

Controls: WASD move, mouse look, Space jump, Shift sprint, Left Mouse fire, R reload, Esc unlock/lock cursor.

The repository originally contained only three weapon/HUD scripts; the missing runtime components and Unity project metadata have been added so the code has a complete executable gameplay foundation.
