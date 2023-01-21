This project is various fx bits from Game Jam Starter Kit by the lovely Andrew Seward

For the full repo, check out his work here:
https://gitlab.com/ASeward/gamejamstarterkit/-/tree/master

To check out the project in action, view the demo:
![demo_screenshot.png](demo_screenshot.png)

# FX System Package
FX System is a way to bridge animations and scripts, allowing easy spawning of effects, and other game objects right from animators.

`GameJamStarterKit.FXSystem` is a system designed to allow artists and programmers to work together on animations and events without the hassle of writing single purpose scripts or weird timing coroutines.

# Features
* Various useful functions to be called from Animators.
* FX Units - Define a GameObject as an FX allowing it to be spawned and controlled by an FX Spawner.
* FX Spawner - A GameObject responsible for creating FX Units. Usually tied to an animation or other script.
* Signalling - A Method to block code execution until a signal is raised either by an animation or other code.

### Example Signalling
```c#
Signaller s = myActor.GetComponent<Signaller>();
myActor.TransitionIn();
yield return s.WaitForSignal("Unblock");
// animation plays, and raises the signal Unblock
myActor.TransitionOut();
```