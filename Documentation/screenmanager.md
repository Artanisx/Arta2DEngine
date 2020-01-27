# Arta2D Engine - Screen Manager

This document explains how the Screen Manager module works and how to use it for your games.

### Getting Started

In order to use the Screen Manager system you have to do some preparations, first.

Firstly, you need to add the correct using statement in the Game1.cs class:
```c
using Arta2DEngine.Utils;
```

Secondly, you need to create a ScreenManager inside your Game1.cs class, in the class-level variables, before the constructor:

```c
ScreenManager screenManager;
```

In the game class constructor, you need to properly create it, add the screen manager in the Components list and finally Add your first game screen, the one that will contain the point of entry of your game.

```c
// Create the screen manager component.
screenManager = new ScreenManager(this);
```

This creates the new scenemanager. It needs the Game1 object as a paramenter, as it needs to be able to have access to the basic devices and content.

```c
 // Add the screen manager to the Components.
Components.Add(screenManager);
```

Afterwards, it is needed to add the screen manager to the Components list, as it's a drawable component in itself.

```c
// Activate the first screen (the enter point of the game).
screenManager.AddScreen(new GameTest1());
```

Finally, we need to add the first screen of your game, which will be the entry point of the game (it could be a splash screen, the main manu or the game itself). The GameTest1() in the above example is the constructor of the class that will contain your game screen.

The last thing your Game1.cs class will need is a Draw() that will simply call the base.Draw() method. All the drawing will happen in the scene manager component and related scenes:

```c
protected override void Draw(GameTime gameTime)
{
	GraphicsDevice.Clear(Color.CornflowerBlue);

	// The real drawing happens inside the screen manager component.

	base.Draw(gameTime);
}
```

### Creating a Game Screen

In order to create a Game Screen, you first need to create a separate cs file. That file will contain a class that will extend GameScreen.
GameScreen is the abstract class that defines a game screen and it has the below methods that can/should be overriden:

* public override void LoadContent()
   In this method you'll load all your sprites, audio and content from the Content pipeline as you'll do usually. For example:
   ```c
    if (content == null)
        content = new ContentManager(ScreenManager.Game.Services, "Content");

    sprite = content.Load<Texture2D>("game1");
   ```
   
* public override void UnLoadContent()
   This will unload all content loaded by the game screen.
   ```c
   public override void UnloadContent()
        {
            content.Unload();
        }
   ```
   
* public override void Update(GameTime gameTime)
   This is the heart of the scene. It will contain the all the update code, moving objects, AI, input handling etc.
   ```c
    public override void Update(GameTime gameTime)
        {
            base.Update(gameTime); 
			
			// Handle the input
            HandleInput(gameTime);
		}
   ```   
   
* public override void Draw(GameTime gameTime)
   All the draw from this game screen happens in here.
   
* public override void HandleInput(GameTime gameTime)
   This method will handle all the input for the screen, the game controls, the mouse buttons...
   For example:
    ```c
    public override void HandleInput(GameTime gameTime)
        {
            // If the user clicks ENTER it will load GameTest2
            if (Keyboard.GetState().IsKeyDown(Keys.Enter))
                LoadingScreen.Load(ScreenManager, true, new GameTest2()); // Load the next level

            // If the user clicks ESC it will quit
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))                
                ScreenManager.Game.Exit();
        }        
   ```   
   
### Loading a game screen

As mentioned earlier, the first game screen to be loadead (the entry point of the game) is the one passed in the Game1() constructor with the ScreenManager.AddScreen method:
```c
    // Activate the first screen (the enter point of the game).
	screenManager.AddScreen(new GameTest1());
```

Afterwards, you can load another screen using the LoadingScreen.Load method:
```c
LoadingScreen.Load(ScreenManager, true, new GameTest2()); // Load the next level
```

This method needs the screen manager, a bool detailing if the loading of this screen might need some time (passing true) and the constructor call for the new scene.

The "loadingSlow" bool, is set to true, will trigger a loading screen (at the moment it will simply print the message "Loading..." in the consolle. If set to false, like for a game screen that isn't supposed to take too much time, this will be skipped.


Go back to the [User Manual](userguide.md).