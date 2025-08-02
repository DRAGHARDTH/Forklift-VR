🏗 Forklift VR
📖 Overview
Forklift VR is a VR-based training simulation for learning safe forklift operation in a warehouse environment.
The training covers:

Entering the forklift

Starting the ignition

Driving test

Lifting and transporting a crate

Depositing the crate

Shutting down and exiting safely

🏛 Architecture
State Machine
The simulation runs on a Finite State Machine (FSM) controlling the game flow:

Main Menu – Show introduction and options

Enter Forklift – Seat the player in the forklift

Ignition Start – Start forklift engine

Driving Test – Basic driving practice

Steering Test – Steering wheel check

Lift Test – Raise/lower crate

Move to Crate – Drive to target zone

Deposit Crate – Place crate at target location

Exit Forklift – End training, show results

Core Systems
Forklift Physics – Rigidbody + wheel colliders for realistic driving

Interaction – Unity XR Interaction Toolkit for VR controls (grab levers, press buttons)

Audio + Subtitles – XML-driven voice lines in StreamingAssets/VoiceLines

Error Tracking – Detect collisions & crate flips (>45° tilt)

Data Logging – Saves logs to StreamingAssets/Logs

🛠 Build & Run
Clone / Unzip Project

bash
Copy
Edit
git clone <repo-url>
Open in Unity

Version: 6000.0.43f1

Connect VR Hardware

Meta Quest (via Link) or PC VR headset (SteamVR)

Open Scene

Assets/Scenes/ForkliftVR.unity

Play or Build

Press Play in Unity Editor

Or Build & Run for target VR platform
