Forklift VR 
Project Documentation
________________________________________
1. Project Overview
Forklift VR is a simplified, yet realistic VR-based training module designed to guide users through the safe operation of a forklift inside a warehouse environment.
The simulation delivers a scenario-driven learning experience with step-by-step instructions, interactive controls, and voice guidance to replicate real-world forklift operations in a safe, controlled, virtual space.
Training session includes:
1.	Entering the forklift
2.	Starting the ignition
3.	Performing a driving test
4.	Lifting a crate safely
5.	Transporting the crate to a target zone
6.	Lowering and depositing the crate
7.	Safely shutting down and exiting the forklift
________________________________________
2. Initial Design Considerations
Target Audience
•	Beginner forklift operators or trainees in an industrial setting
Learning Objectives
•	Understand forklift startup and control panel usage
•	Practice steering and manoeuvring in a safe environment
•	Safely lift, transport, and deposit loads
•	Follow a structured, guided training sequence
Training Methodology
•	Guided Learning: Voice prompts + subtitles at each step
•	Hands-On Interaction: Steering wheel, ignition, levers, and fork controls
•	Error Tracking: Collision detection, crate flipping monitoring
•	Scenario Completion: End-of-training UI with restart/quit options
________________________________________
3. Technical Approach
VR SDK Choice
•	Unity XR Interaction Toolkit (OpenXR backend for cross-compatibility)
•	Supports Meta Quest, PC VR (SteamVR), and future XR devices
Input Methods
•	Grab based interaction for forklift control and select based interaction for button presses
•	Headset tracking for realistic seated driver’s perspective
Simulation of Forklift Physics
•	Rigidbody-based forklift movement with wheel colliders
•	All primary forklift controls use XR Knob input:
o	Steering wheel rotation
o	Fork lift/lower operation
o	Drive/brake control
•	Inputs are converted into physics-based forklift behaviour for realistic handling
Visual Feedback
•	Floating UI tooltips
•	Action button prompts
•	Forklift control highlights
Audio Feedback
•	Voice-over instructions from XML-based audio/subtitle system
•	Engine sounds during movement which changes pitch according to the speed of the forklift________________________________________
4. Implementation Details
4.1 State Machine Architecture
The training session is controlled by a Finite State Machine (FSM) with the following states:
1.	Main Menu State - Displays the training introduction and menu options, allowing the player to start the session or quit the application.
2.	Enter Forklift State – Player teleports into forklift seat
3.	Start Ignition State – Player starts forklift using ignition button
4.	Driving Test State - Player performs a basic driving test to familiarize with controls
5.	Steering Test State – Player turns steering wheel fully left/right
6.	Forklift Test State – Lifts crate to correct height
7.	Move To Crate State – Drives to target area
8.	Deposit Crate State – Player lowers and places the crate in the designated spot
9.	Exit Forklift State – Player shuts down the forklift, exits the seat, and views the final results with restart/quit options.________________________________________
4.2 Audio + Subtitles System
•	XML-based configuration stored in StreamingAssets
•	All audio files are stored in StreamingAssets/VoiceLines
•	Tool tip Manager loads XML and plays corresponding voice line + subtitle
•	Keeps audio/subtitle management independent from gameplay states for easy updates
________________________________________
4.3 Data Logging System
•	Training Data Manager creates a log file in StreamingAssets/Logs for every session
•	Records:
o	Actions performed (with timestamps)
o	Errors (collisions, crate flipping)
o	Session completion status
Example log:
[0.00s] ACTION: Entered Forklift
[3.45s] ACTION: Ignition Started
[15.20s] ERROR: Crate collided with warehouse shelf
[120.50s] ACTION: Forklift Safely Shut Off
________________________________________
4.4 Error Monitoring
•	Crate Error Logger detects:
o	Collisions with non-warehouse/non-forklift objects
o	Crate flipping (tilt > 45° for > 2 seconds)
•	Automatically resets flipped crate to starting position
•	All incidents logged for review
________________________________________
5. Challenges & Solutions
Challenge	Solution
Simulating realistic forklift steering in VR	Mapped XR Knob input to steering wheel rotation and converted values into physics-based steering control
Preventing unstable crate physics	Tuned mass, applied constraints, and implemented auto-reset on flipping
Making UI readable from seated VR position	Used world-space canvases positioned and oriented relative to forklift controls
Providing accessible voice/subtitle prompts	Built an XML-based voice + subtitle system for easy content updates without code changes
Ensuring VR performance	Used URP, baked lighting, and optimized physics interactions
________________________________________
6. Future Improvements
•	Scoring system based on speed, accuracy, and safety
•	Haptic feedback for collisions and engine vibration
•	Support for dynamic loads of varying weights and sizes
•	Advanced training modules with obstacle courses and precision challenges
•	Integration with LMS or REST API for progress tracking
________________________________________
7. Build & Run Instructions
1.	Unzip the Unity project repository
2.	Open in Unity  6000.0.43f1
3.	Connect VR headset & controllers
4.	Open Forklift VR scene
5.	Press Play in Unity Editor or build and run for desired VR platform
6.	Follow on-screen & audio instructions to complete training

