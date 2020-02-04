# MonoPong

This tutorial will guide you into making a Pong clone with Arta2D Engine! Before starting, have a look at how the full game will look like:
![Main Menu](../Documentation/images/pong_mainmenu.png)
![Game screen](../Documentation/images/pong_gamescreen.png)
![Game screen 2](../Documentation/images/pong_gamescreen_2.png)

## Create the Project
The first thing to do is to have Monogame properly installed. Arta2D Engine uses the Monogame framework and thus it needs it to be correctly installed. Also, you will need the Templates for your Visual Studio version (or what you will be working with). I'm assuming the above is already set, so you can choose the "Monogame Windows Project" template, which is a Monogame for the Windows Desktop platform using DirectX.

After you created the project, you need to add the reference to the Arta2DEngine. To do so, right click on the "References" item under your project and click "Add Reference". 
If you have downloaded the release DLL (or compiled one yourself), you will need to click the Browse button and select the DLL (which you also will need to distribute in your compiled game).
If you want to have the source of the engine available to you, then you need to git clone the Arta2DEngine and add this project to your solution. At that point, you will find the "Arta2DEngine" reference in the Projects tab. Select it and click OK.

## Creating the Screens
We're going to use Arta2DEngine.Utils.SceneManager to handle the two screens of our game. The MenuScreen, which will contain the main menu shown earlier:
![Main Menu](../Documentation/images/pong_mainmenu.png)
And the actual play screen:
![Game screen 2](../Documentation/images/pong_gamescreen_2.png)

Add a new class and call it:
```c
MenuScreen.cs
```

In the using statements, put the below:
```c
using System;
using System.Collections.Generic;
using Arta2DEngine.Utils.SceneManager;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Arta2DEngine.Graphics.UI;
using Arta2DEngine.Utils;
using Arta2DEngine.Graphics.ParticleEngine;
using Arta2DEngine.Audio;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;
```

This MenuScreen is going to be an extension of the GameScreen class, so change the class declaration as:
```c
 class MenuScreen : GameScreen
```

We will need several fields, but let's begin with the most important ones:
```c
// Fundamental Objects
ContentManager Content;
SpriteBatch spriteBatch;
Viewport viewPort;
```

The first two are needed to load resources from our Content pipeline and to draw them, while the third one is used to have a viewport to get the window's width and height for positioning reasons.

Let's now create our constructor:
```c
public MenuScreen()
{
       
}
```

It's empty for now, but it will have some variables initialization later. In order to have a functioning GameScreen, we need to implement some Fundamental functions. Let's do so now:
```c
public override void LoadContent()
{
    // Setup the ContentManager and the related root folder.
    if (Content == null)
        Content = new ContentManager(ScreenManager.Game.Services, "Content");

    // Set the viewport for ease of access
    this.viewPort = ScreenManager.Game.GraphicsDevice.Viewport;     
    
    // Show mouse cursor            
    ScreenManager.Game.IsMouseVisible = true;

    // Set Window Title
    Utils.SetWindowTitle(ScreenManager.Game.Window, "Mono Pong");
}

public override void UnloadContent()
{
    Content.Unload();
}

public override void Update(GameTime gameTime)
{
    base.Update(gameTime);
}

public override void Draw(GameTime gameTime)
{
    // Set the spriteBatch if needed
    if (spriteBatch == null)
        spriteBatch = ScreenManager.SpriteBatch;
}
```

Now, the skeleton for the MainScreen.cs file is done. It's time to go back to the Game1.cs (I will call it "MainGame.cs" as I have renamed it, but feel free to keep it Game1.cs if you want). You should basically remove most of the preliminary code for it. Make sure to put the correct using statements:
```c
using Microsoft.Xna.Framework;
using Arta2DEngine.Utils.SceneManager;
```
Basically we will be using tghe Arta2DEngine SceneManager and of course the XNA framework (monogame).

Then, we need to create a couple of fields:
```c
 // Fundamental Objects
GraphicsDeviceManager graphics;
ScreenManager screenManager;

// Screen Resolution
const int BufferWidth = 800;
const int BufferHeight = 600;
```
We're basically setting the resolution to 800x600 for our game. Then, in the MainGame() (or Game1) constructor, we set up a couple of things:
```c
public MainGame()
{
    // Setup the GraphichDeviceManager and Content
    graphics = new GraphicsDeviceManager(this);
    Content.RootDirectory = "Content";

    // Setup resolution
    graphics.PreferredBackBufferWidth = BufferWidth;
    graphics.PreferredBackBufferHeight = BufferHeight;

    // Create the screen manager component.
    screenManager = new ScreenManager(this);

    // Add the screen manager to the Components.
    Components.Add(screenManager);

    // Activate the first screen (the enter point of the game).
    screenManager.AddScreen(new MenuScreen());
}   
```
The most important thing here is the SceneManager initialization. We create a ScreenManager (passing "this" as argument, so the MainGame variable holding everything togheter) and add it to the Components, a list that includes everything that needs to be drawn. Lastly and most importantly, we add the first screen we want to show: the MenuScreen, passing as an argument its constructor.


Go back to the [Readme](../README.md) for Getting Started.
