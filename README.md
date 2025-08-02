# Forklift VR

## Project Overview

Forklift VR is a VR-based training simulation for learning safe forklift operation in a warehouse environment.

##  Architecture
### 1) State Machine: <br/>
The simulation runs on a Finite State Machine (FSM) controlling the game flow.

- Main Menu – Show introduction and options
- Enter Forklift – Seat the player in the forklift
- Ignition Start – Start forklift engine
- Driving Test – Basic driving practice
- Steering Test – Steering wheel check
- Lift Test – Raise/lower crate
- Move to Crate – Drive to target zone
- Deposit Crate – Place crate at target location
- Exit Forklift – End training, show results

### 2) Core Systems: <br/>
- Forklift Physics – Rigidbody + wheel colliders for realistic driving
- Interaction – Unity XR Interaction Toolkit for VR controls (grab levers, press buttons)
- Audio + Subtitles – XML-driven voice lines in StreamingAssets/VoiceLines
- Error Tracking – Detect collisions & crate flips (>45° tilt)
- Data Logging – Saves logs to StreamingAssets/Logs

## Build & Run

### 1) Clone / Unzip Project

### 2) Open in Unity
- Version: 6000.0.43f1
### 3) Connect VR Hardware
- Meta Quest 3(via Link)
### 4) Open Scene
- Assets/Scenes/ForkliftVR.unity
### 5) Play or Build
- Press Play in Unity Editor
- Or Build & Run for target VR platform

