# Simple Interactables

Interactable is a Unity script for creating interactable objects in a game. It uses the [Unity_HideIf](https://github.com/Baste-RainGames/Unity_HideIf) package by Baste-RainGames (MIT license) to allow hiding certain fields in the Inspector based on the values of other fields. 

## Interactable Types and Actions

`SpriteType` is an enum that determines whether an interactable object should have a static sprite or an animated sprite.

`InteractableType` is an enum that determines whether an interactable object is of type `Toggle` or `Radius`. 
- An object of type `Toggle` can be activated by the player by pressing a button, and it remains activated until the player presses the button again. 
- An object of type `Radius` can be activated by the player if they are within a certain radius of the object, and it remains activated as long as the player is within that radius. If the player leaves the radius, the object is deactivated.

`InteractableAction` is an enum that determines whether an interactable object has a narrative action or a kinetic action. (Unfinished - doesn't do anything at the moment)
- A narrative action is something that advances the story or dialogue in the game, while a kinetic action is something that affects the game world, such as moving an object or opening a door.

## Public Fields

The script has several public fields, including:
- `type`: the `InteractableType` of the object
- `player`: a reference to the player object
- `renderer`: a reference to the sprite renderer component of the object
- `radius`: the radius within which the player can activate a `Radius` type object
- `activated`: a bool indicating whether the object is currently activated
- `unselectedSprite`, `selectedSprite`, and `activatedSprite`: `SpriteType` enums indicating the type of sprite to use for each state of the object
- `defaultSprite`, `selectSprite`, and `activateSprite`: static sprites to use for each state of the object
- `defaultAnimatedSprite`, `selectedAnimatedSprite`, and `activatedAnimatedSprite`: animator components to use for each state of the object

Some of these fields are hidden in the Inspector using the `Unity_HideIf` package based on the values of other fields. For example, the `defaultSprite` field is only shown if the `unselectedSprite` field is set to `Static`, and the `defaultAnimatedSprite` field is only shown if the `unselectedSprite` field is set to `Animated`.

## Functions

The script has an `OnValidate` function that sets the animator components to the object's `Animator` component if they are null.
The script has an `Update` function that calculates the distance between the player and the object, and updates the object's sprite and activation state based on that distance and the object's type. 
- If the object is of type `Toggle`, the player can activate it by pressing the interact button. 
- If the object is of type `Radius`, it is activated if the player is within the radius and presses the interact button, and deactivated if the player leaves the radius.

## Installation

To use the `Interactable` script in your project, simply download the code and place it in your Unity project's `Assets` folder.

## Example Usage

1. Attach the `Interactable` script to a game object in your scene.
2. In the Inspector, set the values for the public fields as desired. For example:
   - Set `type` to `Radius`.
   - Drag the player object into the `player` field.
   - Set the radius value to 2.
   - Set `unselectedSprite` to `Static`, `selectedSprite` to `Animated`, and `activatedSprite` to `Static`.
   - Drag the object's `SpriteRenderer` component into the `renderer` field.
   - Drag a static sprite into the `defaultSprite` field and an animator component into the `selectedAnimatedSprite` field.
   - Drag a static sprite into the `activateSprite` field.
3. In the game, the object will now change its sprite and activation state based on the player's distance from it and whether the player is pressing the interact button. When the player is within the radius and presses the interact button, the object will be activated and its `activateSprite` will be displayed. When the player leaves the radius, the object will be deactivated and its `defaultSprite` will be displayed. When the player is within the radius and not pressing the interact button, the object's `selectedAnimatedSprite` will be displayed.
