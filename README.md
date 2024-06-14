# Asteroid Breaker
An arcade-style VR game portraying our best hypotheses for common materials composing the Psyche asteroid. Break apart asteroid chunks, identify them, and complete delivery tasks. Aim for a high score!

**Created by Outer Reality for University of California, Irvine capstone course: IN4MATX 191**

Outer Reality Team Members:
* Lee Austin Uy
* Sebin Im
* Hyeon Woo Seo
* Jacob Schreiber
* Nathan Le
* Zaina Azim

## Installation
Download the [apk](build/final.apk) and then load onto your Quest headset. The application we used is [SideQuest](https://sidequestvr.com/setup-howto) using their advanced installer. The app will be partially hidden and can be accessed by clicking on the `Unknown Sources` filter when searching.

## Event Presenter Notes
### Setup
Asteroid Breaker is designed to be a room-scale 3x3 meter experience, where players can walk around to pick up, interact, and break apart rocks using a pickaxe. If such a space cannot be provided, this experience can still be used in a standing play style through the use of joystick movement. Howver, extra caution must be exercised when players are hitting the rock or moving around as normal behavior might shift the player outside of the playing area.

We have provided a paper-size sheet to aid in instructing players how the game works and how to use basic controls.

[Flyer](Asteroid%20Breaker%20Flyer.pdf)

### Controls/Configuration
Per requirements, Asteroid Breaker features in-game controls to restart, end, or otherwise resolve any issues that arise during normal gameplay.

In the main menu, clicking on the settings will provide options to delete any highscore data or disable the timer.

![Main Menu Settings](https://github.com/MissionToPsyche/tungsten_11e_vr_game-uci/assets/40331455/4f205369-ab03-4763-9601-fb6491acdff6)

While in any scene, clicking the menu button on the left hand will bring up the in-game menu. Here, you can restart the game, end the game, or adjust various player settings. These player settings include recentering the player if they find themselves out of bounds or floating and a height selector to raise or lower their height in-game if needed.

![In-Game Menu](https://github.com/MissionToPsyche/tungsten_11e_vr_game-uci/assets/40331455/5115fd08-3880-4baf-b4be-2ab33e60d40a)

### Known Issues
1. Delivery Not Completeing
   - This can be the result of a rock piece actually containing more than one piece with it. To fix, just ensure all pieces on the delivery area have been properly hit. Otherwise, ensure the correct types with the correct quantity are on the delivery area.
2. Floating Player
   - If you are using joysticks to move around, you may find yourself floating or out of bounds. To fix, open the In-Game Menu then navigate to settings. Click on the Recenter button. Please ensure the player is in the center of the play area.

## Technical Documentation
### Tools
Before starting, ensure the following are installed.

- Unity 2022.3.19f1
	- Android Build Support
- Blender

Afterwards, clone this repository and add this project to Unity Hub.

### How To Modify Rock Types
Asteroid Breaker represent rock types in two different ways. Base Rock Types make up the majority of the rock's composition while Target Rock Types are embedded inside of it. Currently, Enstatite is used as a Base Type while Kamacite, Iron Sulfide, and Sulfur are used as Target Types.

1. The first step to making any changes, whether it be adding new types, removing old ones, or changing an existing type, is by visiting the Rock Type scriptable objects. These objects are used in the game to display a rock's name and provide textures for their visual representation. In Unity, navigate to `Assets > Scripts > Rock Types`. You may make any changes to existing types or remove types here. If you wish to create a new Rock Type, right click and in the context menu, navigate to `Create > ScriptableObjects > RockTypeScriptableObject`. This will give you a new Rock Type to work with.

![Rock Type Folder](https://github.com/MissionToPsyche/tungsten_11e_vr_game-uci/assets/40331455/c63abd53-ca44-4b4c-a306-5edb248caf02)

4. The next step to using these Rock Types is modifying the Game Manager. Open up `BasicScene` located under `Assets > Scenes`. Click on the `Game Manager` GameObject under your Hierarchy, you should see the following.

![Game Manager Inspector](https://github.com/MissionToPsyche/tungsten_11e_vr_game-uci/assets/40331455/3138c48d-1356-4cea-8491-6c8885e138a7)

There are two components visible in the Inspector, `Delivery System` and `Rock Factory`.

`Delivery System` is responsible for generating and verifying deliveries for completion. Either drag and drop Rock Types into the type fields or click on the circle to the right to browse assets and select them from there. Chance represent the weight a rock type has of being select. I.E. Two rock types with a chance of 1 each will have a 1/2 chance of being selected between both. If one type was 2, it would be 2/3 for the type. Point Value is value of the the Rock Type which is added to a delivery's overall value.

`Rock Factory` is responsible for generating rocks and cleaning them up after the player requests a new one. Like before, you can drag and drop Rock Types or browse for them in the Type field. Chance also represents a type's weight like before.

For both cases, if you've removed a Rock Type, please ensure there are no erroneous entries in these fields. If you wish to add a Rock Type, click on the plus below the field list.

### How To Modify Timer Length
Currently, experiences are designed to run for 3 minutes before the game ends. If you wish to modify the run time of the experience, open up `BasicScene` located under `Assets > Scenes`. Then, navigate in the Hierarchy, `Complete XR Origin Set Up > XR Origin (XR Rig) > Player Height Offset > Camera Offset > Main Camera > Timer (TMP)`. There should be a `Timer` component in the Inspector. Modifying the Time field will set how long the experience runs for in seconds, so a number of 180 would be 3 minutes.

![Timer Location](https://github.com/MissionToPsyche/tungsten_11e_vr_game-uci/assets/40331455/fb7a85f9-f7c8-42ba-a875-1245dfbf3db1)


### Build Settings
Navigate to `File > Build Settings`. Ensure the selected platform is Android

![Build Settings Image](https://github.com/MissionToPsyche/tungsten_11e_vr_game-uci/assets/40331455/333a6c89-220f-4b61-b6d5-26de4f67ffc6)
