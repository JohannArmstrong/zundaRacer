# Multiplayer Race Game (Unity + Mirror)

## Overview
This project is a multiplayer 2D race game prototype developed in Unity using Mirror Networking.
It was created as a technical portfolio project to demonstrate gameplay architecture, networking authority, and clean code practices in a real-time multiplayer environment.

The core game mode is a race: the first player to reach the finish line wins. Once a player wins, the match is frozen and the result is displayed to all connected clients.

## Key Technical Features
### Architecture & Design
- Modular, component-based architecture
- Clear separation of responsibilities:
 - Game Flow / Match State
 - Player Input, Movement, Health, Visuals
 - Enemy AI (State Machine–based)
- Finite State Machines used for:
 - Enemy behavior
 - Match lifecycle (waiting → countdown → playing → finished)

### Networking (Mirror)
- Server-authoritative movement and combat
- Clients send input; the server validates and applies state
- Use of:
 - NetworkBehaviour
 - Command
 - ClientRpc
 - SyncVar with hooks
- Deterministic gameplay logic handled exclusively on the server

### Gameplay Systems
- Multiplayer-ready player controller
- Enemy AI with patrol, stunned, and dead states
- Player damage system with:
 - Server-side validation
 - Temporary invulnerability
 - Client-side visual feedback
- Server-side finish line detection

### Extensibility
- The architecture allows for easy extension, including:
 - Additional game modes (time attack, elimination, team race)
 - New enemy behaviors via state extension
 - UI expansion without modifying core gameplay logic

### Technologies Used
- Unity (2D)
- C#
- Mirror Networking
- Rigidbody2D physics
- Finite State Machines
- Event-driven game flow

### Project Purpose
This repository is intended as a professional portfolio sample.

It demonstrates:
- Gameplay system design
- Multiplayer authority handling
- Clean, maintainable, and scalable code structure

This project is not intended for commercial release.

### Notes for Reviewers

- The focus of this project is engineering and architecture, not art or polish
- All visual and audio assets used in this project are placeholders and are not representative of final production quality.
- Gameplay systems are implemented with multiplayer determinism in mind

### License
This project is licensed under the MIT License.
See the LICENSE file for details.