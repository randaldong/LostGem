# Pixel Sidescroller

## 2D Pixel Specific Settings

- Main Camera --> Rendering --> Anti-aliasing --> No Anti-aliasing
- Animation Transition --> Transition Duration = 0, want immediate transition for pixel style



## Inspector

- `[Header("Section Name")]`
- `[SerializedField] private`
- `[System.NonSerialized] public` (instead of `[HideInInspector] public`)



## Tilemap

### Traditional Tiles

- import & change settings of 2D textures/sprites

  - Texture Type: Sprite
  - Sprite Mode: Single/Multiple

  - Pixels Per Unit: 16/32/64, according to actual texture

  - Filter Mode: Point (using no mipmap, usually for pixel style)

  - Compression: None

- if the mode is multiple, slice the sprite in Sprite Editor: 

  - for character, grid by cell count, fill in C & R, pivot: bottom (so that computations will always be relative to the feet)
  - for environment assets, grid by cell size, fill in pixel size, pivot: center

- create a Tilemap in the scene (a tilemap is like a layer)

- use Tile Palette (Window --> 2D --> Tile Palette) to draw tilemaps
	- create a palette (a collection of tiles that you can choose from, stored as an object in your project, put them inside `Assets/Tilemaps/Palettes`)
	- drag the parent texture into the Tile Palette window (this will trigger Unity to ask you to create Tile asset for each sprite, store them in `Assets/Tilemaps/Tiles/XXX`). Note, if you drag the children, they will be shuffled and messy in the window.
	- paint the base tilemap using those handy tools (`shift` + click to erase)
	- create a new tilemap (new layer) for overlay levels/objects, set the `Order in Layer` to a greater value, in Tile Palette switch active tilemap

- change the viewport size in game mode to preview; change the camera size to change the scene size



### Rule Tiles

- create rule tiles
- add rules, pay attention to layer order and corner rules



### Animated Tiles

- use several sprites for one animated tile to create a moving effect



### Layer

- to put an object in front of everything, a good practice is to create different sorting layers for different hierarchies, and put the object in a "bigger" layer
- to prioritize a collider (e.g., collider to take damage), set a larger "Layer Override Priority"



## Physics 2D

Can change the settings in Project Settings --> Physics 2D, including Gravity (-9.81 by default), Simulation Mode (Fixed Update by default, 0.02s per update)

- Rigidbody 2D for character
  - freeze its rotation in Constraints so that it won't fall
  - switch Collision Detection to Continuous for higher accuracy when the character could have lots of moves & collisions
- Collider 2D
  - for the character, Capsule Collider 2D, edit collider to let it fit 
  - for the object , Tilemap Collider 2D (select Used By Composite to optimize, otherwise Unity will check collision with each tile; also switch Geometry Type from Outlines to Polygons, otherwise collision will not be detected once object goes through it), Composite Collider 2D (will automatically add a Rigidbody, set its Body Type as Static so it won't be affected by gravity/won't fall)

### Collider

Check where the collision is (relative to the object of interest)

- add (collision) layer to the object (that will collide with our subject)
- use `OverlapCircle` to detect collision with objects of interest (can specify position & size of the detection area)
- can visualize the gizmo when object is selected using `OnDrawGizmosSelected`

> :warning:
>
> if you want to exclude collisions between specific objects, create & assign layers to them and  select their layers in Collider --> Layer Overrides --> Exclude Layers



### Trigger

No physics, can be used to trigger certain events like damage.

- Edit Collider, select Is Trigger, Exclude Layers as needed
- use `OnTriggerXXX` to trigger the behavior after collision



## Input Control

Use the new Input System (the old one is not friendly to cross-platform input):

Project Settings --> Player --> Other Settings --> Configuration

- API Compatibility Level = .NET Framework, can use more C# features
- Active Input Handling = Input System Package (New), apply & restart, install it in Package Manager (Unity Registry)



Manually Add Configuration File:

- create a new folder: `Assets/Settings/InputSystem`
- create Input Actions in this folder
- edit Action Maps (can create different maps for different input systems, e.g., gameplay, UI, different levels...)
- add control schemes for different bindings



Default Input Action Automatically Created by Unity:

- Player --> add Player Input component
- follow the notification :warning: and `Create Actions...`, save the auto-generated input action in `Assets/Settings/InputSystem` (contains Action Maps for Player & UI)
- edit the map if you need
- go back to Player, delete the Player Input component (or you can keep it and set the Behavior as Invoke Unity Events, then use the event listener to implement controls, which is quite limited) because here we're going to use code for controls.
- go to the input action generated by Unity, check Generate C# Class, apply
- create a script that instantiate an object of the above class & use its methods
- Happy Coding :)



Player Controls:

- move: change velocity (change gravity scale for best falling speed), flip face direction
- jump: add jump force (refer to Unity Manual [Rigidbody2D.AddForce](https://docs.unity3d.com/ScriptReference/Rigidbody2D.AddForce.html))
  - limit continuous jumping/limit to double jump/triple jump using custom collision detection
  - player will stick to wall if keep pressing `A`/`D` (because of friction); fix: apply a Physics 2D Material with 0 friction to the collider
  - player can move in the air; fix: disable movement input when in the air



Change move direction, two methods:

- `scale.x = -scale.x` :heavy_check_mark:
- use the Flip property in Sprite Renderer (will flip according to pivot); however, components (like Rigidbody) will not flip



## Animator & Animation

Basic animation

- add `Animator` component to the player, create animator controller and drag it into Controller property of the component
- create & edit animation clips in Animation Window, change sample rate (how many animation frames per second)
- configure state in Animator Window 
  - add transition
  - uncheck `Has Exit Time` & `Fixed Duration`
  - Transition Duration = 0
  - add parameter & condition
  - (set Exit Time = 1 if want to play the full animation than transit)
- using script to update the value of this parameter



### Blend Tree

Animations that have multiple stages, using Blend Tree:

- create separate animations for each stage
- in Animator, delete individual states, Create State --> From New Blend Tree, rename, double click to edit it
- choose Blend Type, Parameter, add Motions
- uncheck Automate Thresholds and manually set them
- using script to update the value of the parameter

> :warning:
>
> If transit from Any State, remember to uncheck "Can Transition To Self", otherwise will cause infinite loop.
>
> If want to interrupt an animation in the middle (e.g., no landing if immediate walk/run), transit to exit



### Layer

A good practice is to use layers for different animations:

- Weight
- Blending: Additive
- create new state & animation that modifies Sprite Renderer --> Material Color
- edit values for keyframes, modify Curves



### Behavior

Execute functions during this animation. Inherit from [`StateMachineBehaviour`](https://docs.unity3d.com/ScriptReference/StateMachineBehaviour.html)



## Combat System

### Combo Attack

3-hit combo attack

- click once to perform state-1
  - before state-1 ends (say, 90%), if not click again, exit
  - before state-1 ends (say, 90%), if click again, interrupt state-1, enter into state-2
    - before state-2 ends, if not click again, exit
    - before state-2 ends, if click again, interrupt state-2, enter into state-3
- can be implemented with trigger
- when to apply attack? 
  - choose a specific frame, use Polygon Collider 2D (trigger) to draw the attack area, only activate the trigger on this frame (in animation)
  - when target falls into this area (trigger), apply damage



## Event



## Coroutine

A coroutine is a method that you declare with an [IEnumerator](https://docs.microsoft.com/en-us/dotnet/api/system.collections.ienumerator) return type and with a [yield](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/yield) return statement included somewhere in the body. It will suspend its execution (yield) until the given [YieldInstruction](https://docs.unity3d.com/ScriptReference/YieldInstruction.html) finishes.

- `yield return null`, pause execution and resumes in the next frame
- `yield return new WaitForSeconds(float_time)`, pause for a period of time then continue following executions

To set a coroutine running, you need to use the [StartCoroutine](https://docs.unity3d.com/ScriptReference/MonoBehaviour.StartCoroutine.html) function (`StartCoroutine(SomeMethod());`).

To stop a coroutine (cancel/stop early), use [StopCoroutine](https://docs.unity3d.com/ScriptReference/MonoBehaviour.StopCoroutine.html) and [StopAllCoroutines](https://docs.unity3d.com/ScriptReference/MonoBehaviour.StopAllCoroutines.html).



## Async/Await

Coroutine is still linear, using the main thread; while async creates a new thread.

[Unity async / await: Coroutine's Hot Sister [C# & Unity]](https://www.youtube.com/watch?v=WY-mk-ZGAq8&t=0s)

[Unity async / await: Awaitable](https://www.youtube.com/watch?v=X9Dtb_4os1o)

[Unity Async Await - Make Your Game Run Smoother!](https://www.youtube.com/watch?v=gxaJyuf-2dI)





## OOP

























## Script Lifecycle

See official Manual of [Execution Order](https://docs.unity3d.com/Manual/ExecutionOrder.html) for more details.

Common functions:

- `Awake`: First lifecycle function called when a new instance of an object is created. Always called before any `Start` functions. If a GameObject is inactive during start up, `Awake` is not called until it is made active.
- `OnEnable`: Called when the object becomes enabled and active, always after `Awake` (on the same object) and before any `Start`.
- `Start`: called before the first frame update only if the script instance is enabled.
- `FixedUpdate` happens at fixed intervals ([Time.fixedDeltaTime](https://docs.unity3d.com/ScriptReference/Time-fixedDeltaTime.html), can be changed in the Editor's [Time settings](https://docs.unity3d.com/Manual/class-TimeManager.html) ) of in-game time rather than per frame. All physics calculations and updates occur immediately after it.
- `Update` is called once per frame and is the main function for frame updates.
- `LateUpdate` is called once per frame, after `Update` has finished. ( A common use would be a following third-person camera, ensuring that the character has moved completely before the camera tracks its position.)



![](https://docs.unity3d.com/uploads/Main/monobehaviour_flowchart.svg)

## Tips & Tricks

### "Grad N Drop" vs. "Read Component in Awake"

- "Grad N Drop": fast, get the component before code runs, used for components that need to be read immediately when the game starts
- "Read Component in Awake": not so urgent, don't need to get the component before code runs 

### Velocity Correction

- if update position in `Update`, need to `* Time.deltaTime` so that the velocity is independent of frame rate
- if update `rb.velocity` in `FixedUpdate`, don't need this correction

### Computation Optimization

- put any vector after float will optimize the performance for this reduces multiplication with vectors

### Inspector Related

- Use `HideInInspector` with caution! This can lead to lots of bugs because the variable is still serialized, its just not shown in the inspector. Consider using `[System.NonSerialized]` for public variables that you do not wish to be serialized.



## Bugs

### `OnTriggerStay2D()` works only when player is moving

In the RigidBody2D component --> Sleeping Mode, select Never Sleep

[Reference](https://stackoverflow.com/questions/66597912/method-ontriggerstay2d-works-only-when-player-is-moving)





## Awesome Unity Packages

[UniTask: Provides an efficient allocation free async/await integration for Unity.](https://github.com/Cysharp/UniTask)

[ML-Agents: Machine Learning Agents Toolkit](https://github.com/Unity-Technologies/ml-agents)

