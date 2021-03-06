# Arta2D Engine - User Guide 

This User Guide explains how the engine work and how to use it for your games.

### Getting Started

Check the [Readme](../README.md) for Getting Started.

## <a name="classes"></a>Classes
List of classes
* [GameObject - Graphic](#gameobject)
* [AnimatedGameObject - Graphic](#animatedgameobject)
* [Camera2D - Graphic](#camera2d)
* [Primitives2D - Graphic](#primitives2D)
* [Button - Graphic.UI](#button)
* [Command - Input](#command)
* [HandleInputs- Input](#handleinputs)
* [SFX - Audio](#sfx)
* [BGM - Audio](#bgm)
* [Utils - Utils](#utils)
* [Screen Manager - Utils](screenmanager.md)
* [Particle Engine - Graphic](#particleengine)

### <a name="gameobject"></a>GameObject

This is a simple Game Object class for the Engine. It serves as a container for GameObjects in a similar fashion to Unity 3D engine. 
GameObject extends BaseObject an abstract class that gives some basic functionality. 

A BaseObject has:

* Texture2D Texture

   This holds a Texture2D for each object.

* Vector2 Position

   This holds the position for this object expressed as a Vector2.
   
* float Scale

   This holds the scale for this object expressed as a float. 1.0f is the default value. It's used in the Draw method. Changing this at any point, changes the scale of the object. For example the following in the Update() method will continuously scale the object: gameObject.Scale += 0.01f; 
   
* float RotationAngle

   This holds the rotation angle for this object expressed as a float. 0.0f is the default value (no rotation). It's used in the Draw method. Changing this at any point, changes the rotation of the object. For example the following in the Update() method will rotate the object: gameObject.RotationAngle += 0.01f; 
   
* Vector2 virtual Origin

   This holds the Origin for this object expressed as a Vector2. 
   
* Vector2 Size

   This holds a Vector2 with the size of the object (width, height). If the texture is not ready (null) it will return 0,0; otherwise it will return the texture.width and texture.height.
   
* Rectangle SourceRectangle

   This holds the source rectangle for this object expressed as a Rectangle. Used for the base drawing method.

* Rectangle BoundingBox

   This holds a Rectangle property that encompass the texture. It is created against its Position, Origin and Texture information so each time it is polled will be at the right position. It can be used for general and not very precise collision detection.
   
* Circle BoundingCircle

   This holds a Circle property that encompass the texture. It can be used for collision detection. If an object is less of a square (for instance it's a space ship), checking collisions with a Circle will fire less false collisions than the BoundBox ones. It is a bit more precise than the BoundingBox collision, but it's dependant on the actual texture. This is created against the position and the texture information (from which a center is calculated alongside a radius based on the side of the texture).
   
While the BaseObject cannot be instantiated, it holds a definition for a Draw method that can be overridden if needed. The default one simply takes a spriteBatch and assumes the origin (as set) and no offset:
```c
public virtual void Draw(SpriteBatch spriteBatch)
```

It also features other Draw() methods. One that takes an offset and draws the object offsetting it by the passed Vector2:
```c
public virtual void Draw(SpriteBatch spriteBatch, Vector2 offset)
```

And one that takes both offset and a defined different origin. Usually one would change the origin in the constructor or updating its property, but this method is also present:
```c
public virtual void Draw(SpriteBatch spriteBatch, Vector2 offset, Vector2 origin)
```

It also features a DrawDebug method used to draw the bounding box, the bouding circle and a radius, using the Primitives2D static class:
```c
public virtual void DrawDebug(SpriteBatch spriteBatch, Color color)
```

A GameObject has additional stuff:

* Vector2 Velocity

   This holds a Vector2 used for its Velocity.

GameObject implements three different constructors:
```c
GameObject(Texture2D texture, Vector2 position)
```

Omitting a Velocity and one that requires it:
```c
GameObject(Texture2D texture, Vector2 position, Vector2 velocity)
```

Also a constructor that takes a string as the path for the texture, enabling a construction and texture load in a single line:
```c
public GameObject(string texturePath, Vector2 position, ContentManager content)
```

All these constructors will assume the origin to be the center of the texture as it's definitely needed for rotation to work. Of course, the origin can be changed at a later time.   
   
To create a GameObject simply do the below:
```c
// GameObject 
GameObject gameObject;

// Initialize it - LoadContent() is a good place for this
gameObject = new GameObject(goTexture, Vector2.Zero);

// Draw it - Draw() is the perfect place. This must be called inside the Begin / End spriteBatch.Draw.
spriteBatch.Begin();
gameObject.Draw(spriteBatch);
spriteBatch.End();
```

GameObject does not overrides the BaseObject.cs Update() method, which stays empty to give an "interface" for its used. 
As such, even if a Velocity is defined, you must personally update it in the Game Update() like below:
```c
gameObject.Position += gameObject.Velocity * speed;
```

Or, you may want to extend GameObject for specific purposes.

While collisions are not specifically implemented in GameObject nor BaseObject, see below a simple implementation.

Using BoundingBoxes:
```c
if (gameObject.BoundingBox.Intersects(gameObject2.BoundingBox))
{	
	Console.Out.WriteLine("gameObject collided with gameObject2");
}
```

Using BoundingCircle:
```c
if (gameObject.BoundingCircle.Intersects(gameObject2.BoundingCircle))
{	
	Console.Out.WriteLine("gameObject collided with gameObject2");
}
```

[Go back to Classes](#classes)

### <a name="animatedgameobject"></a>Animated GameObject

This an extension of the simple Game Object class for the Engine. 
This time, the Game Object is animated; its Texture2D will therefore hold a Texture atlas. Basically an Atlas is an image that can be divided into rows and columns.
Each "cell" contains a frame of the animation. Each frame must be perfectly centered into its cell or the animation will not look right.

An AnimatedBaseObject has, in addition to the Game Object fields:

* public int Rows { get; set; }

   This holds the number of rows from the Texture Atlas.

* public int Columns { get; set; }

   This holds the number of columns from the Texture Atlas.

 
The Animated GameObject implements three different constructors:
```c
public AnimatedGameObject(Texture2D texture, Vector2 position, int rows, int columns)
```

This will ask for the minimum information, a Texture2D (holding an Atlas), a position, the number of rows of the Atlas and the number of Columns.

```c
public AnimatedGameObject(Texture2D texture, Vector2 position, int rows, int columns, Vector2 velocity)
```

The second constructor also asks for a Velocity (but do bear in mind it won't do anything with it either).

```c
public AnimatedGameObject(Texture2D texture, Vector2 position, int rows, int columns, Vector2 velocity, Color color) 
```

The last constructor will also ask for a color tint. Do keep in mind the base GameObject do not have a Draw method and the BaseObject method only draws with a Color.White tint. 

To create an Animated GameObject, given a Texture Atlas with known columns and rows, simply do the below:
```c
// Animated GameObject 
private AnimatedGameObject animatedGO;

// Initialize it - LoadContent() is a good place for this
animatedGO = new AnimatedGameObject(Content.Load<Texture2D>("SmileyWalk"), new Vector2(200, 200), 4, 4, new Vector2(0,0), Color.Purple);

// Updated it in the Update() method
animatedGO.Update(gameTime);

// Draw it - Draw() is the perfect place. This must be called inside the Begin / End spriteBatch.Draw.
spriteBatch.Begin();
animatedGO.Draw(spriteBatch);
spriteBatch.End();
```

[Go back to Classes](#classes) 

### <a name="primitives2D"></a>Primitives 2D

This static class allows to draw primitives for debugging purposes. This class was not written by me and, until replaced, should be credited to [C3 2D XNA Primitives](https://bitbucket.org/C3/2d-xna-primitives/wiki/Home)

To use this static class, you need to include the right using statement:
```c
using Arta2DEngine.Graphics;
```

The static class has several methods:

* FillRectangle 

   This will draws a color filled rectangle. There are several constructors for this. All the parameters are commented.
   * FillRectangle(this SpriteBatch spriteBatch, Rectangle rect, Color color)
   * FillRectangle(this SpriteBatch spriteBatch, Rectangle rect, Color color, float angle)
   * FillRectangle(this SpriteBatch spriteBatch, Vector2 location, Vector2 size, Color color)
   * FillRectangle(this SpriteBatch spriteBatch, Vector2 location, Vector2 size, Color color, float angle)
   * FillRectangle(this SpriteBatch spriteBatch, float x, float y, float w, float h, Color color)
   * FillRectangle(this SpriteBatch spriteBatch, float x, float y, float w, float h, Color color, float angle)
   
* DrawRectangle

   This will draw a rectangle. There are several constructors for this. All the parameters are commented.
   * DrawRectangle(this SpriteBatch spriteBatch, Rectangle rect, Color color)
   * DrawRectangle(this SpriteBatch spriteBatch, Rectangle rect, Color color, float thickness)
   * DrawRectangle(this SpriteBatch spriteBatch, Vector2 location, Vector2 size, Color color)
   * DrawRectangle(this SpriteBatch spriteBatch, Vector2 location, Vector2 size, Color color, float thickness)
   
* DrawLine

   This will draw a rectangle. There are several constructors for this. All the parameters are commented.
   * DrawLine(this SpriteBatch spriteBatch, float x1, float y1, float x2, float y2, Color color)
   * DrawLine(this SpriteBatch spriteBatch, float x1, float y1, float x2, float y2, Color color, float thickness)
   * DrawLine(this SpriteBatch spriteBatch, Vector2 point1, Vector2 point2, Color color)
   * DrawLine(this SpriteBatch spriteBatch, Vector2 point1, Vector2 point2, Color color, float thickness)
   * DrawLine(this SpriteBatch spriteBatch, Vector2 point, float length, float angle, Color color)
   * DrawLine(this SpriteBatch spriteBatch, Vector2 point, float length, float angle, Color color, float thickness)
   
* PutPixel

   This will draw a single pixel.
   * PutPixel(this SpriteBatch spriteBatch, float x, float y, Color color)
   * PutPixel(this SpriteBatch spriteBatch, Vector2 position, Color color)
   
* DrawCircle

   This will draw a circle. There are several constructors for this. All the parameters are commented.
   * DrawCircle(this SpriteBatch spriteBatch, Vector2 center, float radius, int sides, Color color)
   * DrawCircle(this SpriteBatch spriteBatch, Vector2 center, float radius, int sides, Color color, float thickness)
   * DrawCircle(this SpriteBatch spriteBatch, float x, float y, float radius, int sides, Color color)
   * DrawCircle(this SpriteBatch spriteBatch, float x, float y, float radius, int sides, Color color, float thickness)
   
* DrawArc

   This will draw an Arc. There are several constructors for this. All the parameters are commented.
   * DrawArc(this SpriteBatch spriteBatch, Vector2 center, float radius, int sides, float startingAngle, float radians, Color color)   
   * DrawArc(this SpriteBatch spriteBatch, Vector2 center, float radius, int sides, float startingAngle, float radians, Color color, float thickness)

Using this static class is quite easy, you simply have to call the Draw method inside the Draw() between a spriteBatch.Begin and spriteBatch.End. For example:
```c
Primitives2D.DrawCircle(spriteBatch, gameObject.BoundingCircle.Center, gameObject.BoundingCircle.Radius, 10, Color.Red, 1f);
Primitives2D.DrawLine(spriteBatch, gameObject.BoundingCircle.Center, gameObject.BoundingCircle.Radius, 0f, Color.Purple);
Primitives2D.DrawRectangle(spriteBatch, gameObject.BoundingBox, Color.Red);
```

[Go back to Classes](#classes) 

### <a name="button"></a>Button

This is a simple implementation for a Button used in an UI. You need to add the correct using instruction in order to use the Command class:
```c
using Arta2DEngine.Graphics.UI;
```

Button.cs has several constructors, with increasing definitions. The most complete one is below:
```c
public Button(Texture2D texture, SpriteFont font, Color fontColor, Color backColor, Color hovColor);
```

Calling this one will set a texture for the button (for example a black outlined white rectangle), a spriteFont to be used for its text, a fontColor for the color of said button, a backColor tint for the button itself and finally a hovColor tint for when the mouse is hovering on the button.

In order to create a button, you must first create a Game instance variable for it:
```c
Button quitButton;
```

Then, in the LoadContent() method, you need to load the actually call the constructor and load its values. There are two ways of doing so. A more verbose one:
```c
// Load the font for the button
buttonFont = Content.Load<SpriteFont>("ButtonFont");

// Load the texture for the button
buttonTexture = Content.Load<Texture2D>("button");

// Create the button object, assigning it the texture, the font and colors
quitButton = new Button(buttonTexture, buttonFont, Color.Blue, Color.White, Color.Yellow);

// Set the position for the button, in this example at the center of the screen
quitButton.Position = new Vector2(graphics.PreferredBackBufferWidth / 2 - buttonTexture.Width / 2,
								  graphics.PreferredBackBufferHeight / 2 - buttonTexture.Height / 2);

// Set the caption/text of the button
quitButton.Text = "Quit Game";

// Add the event/method that must be processed when the user clicks the button
quitButton.Click += QuitButton_Click;
```

And a more compact one:
```c
var quitButton = new Button(Content.Load<Texture2D>("Controls/Button"), Content.Load<SpriteFont>("Fonts/Font"), Color.Blue, Color.White, Color.Yellow)
{
	Position = new Vector2(graphics.PreferredBackBufferWidth / 2 - buttonTexture.Width / 2,
						   graphics.PreferredBackBufferHeight / 2 - buttonTexture.Height / 2);,
	Text = "Quit Game",
};
// Add the event/method that must be processed when the user clicks the button
quitButton.Click += QuitButton_Click;
```

Then you must implement the QuitButton_Click method which will be called (in our example) when the button is clicked:
```c
private void QuitButton_Click(object sender, EventArgs e)
{
	// Close the game
	Exit();
}
```

Then, in the Update() method, simply update the button:
```c
// UI
quitButton.Update(gameTime);
```

And finally draw it inside the Draw() method - between Begin() and End() calls:
```c
spriteBatch.Begin();

quitButton.Draw(spriteBatch);

spriteBatch.End();
```

Do keep in mind, "Click" is not the only event available. You can also implement the MouseOver event that will fire when the mouse goes over the button. In order to add this event, you just need to do the same as per the Click event:
```c
// Add the event/method that must be processed when the user mouse overs on the button
quitButton.MouseOver += QuitButton_MouseOver;
```

[Go back to Classes](#classes)

### <a name="command"></a>Command

This is a simple implementation for the Input, from the Input module. You need to add the correct using instruction in order to use the Command class:
```c
using Arta2DEngine.Input;
```

Command.cs has three constructors, one for a Keyboard command, one for Mouse command and one for Gamepad command.
The keyboard command expects a key:
```c
public Command(Keys key)
```

The keyboard command expects a mouse button:
```c
public Command(MouseButton mouseButton)
```

The gamepad command expects a gamepad button:
```c
public Command(GamepadButton gamepadButton)
```

Calling this one will set the command type (key or mouse or gamepadbutton) and the keyboard button (or mouse button or gamepadbutton) that is connected to this command.

In order to create a Command, you must first create a Game instance variable for it:
```c
Command fireCommand;
```

Then, in the Initialize() method, you need to load the actually call the constructor. 
For a Keyboard command (in this example the Spacebar) you will do the below:
```c
// Setup the fireCommand
fireCommand = new Command(Keys.Space);
```

All Keys are supported.

For a Mouse command (in this example the Left Mouse Button) you will do the below:
```c
// Setup the clickCommand that will fire once
clickCommand = new Command(Command.MouseButton.LeftButton);
```

For a Mouse command, only Left mouse button (Command.MouseButton.LeftButton), Middle mouse button (Command.MouseButton.MiddleButton) and Right mouse button (Command.MouseButton.RightButton) are supported.

For a Gamepad command (in this example the X Button) you will do the below:
```c
// Setup the GamepadX that will fire once
gamepadX = new Command(Command.GamepadButton.X);
```

For a Gamepad command, all buttons are supported with the exception of Triggers and Thumbstick (L3 and R3 work). Just check the Command.GamepadButton enum to see what's supported.

After this, the command has been created and in this example it will fire when the user presses the Space bar.

Immediately after, we need to set which kind of event we want to invoke:
* SinglePress

   We want this event to fire only once per press. Like jumping, for instance, one click will jump once.
* ContinuedPress

   We want this event to fire multiple times, as long as the button is pressed. Like shooting a laser for a space game.
   
In order to create a SinglePress event you assign to it a new Event method:
```c
fireCommand.SinglePress += FireCommand_SinglePress;
```

While Visual Studio will take care of the method creation if you let it, see below how a FireCommand_SinglePress method (in this example) will be:
```c
private void FireCommand_SinglePress(object sender, EventArgs e)
{
	// Do something
}
```

To create a ContinuedPress event, you do the same thing, but set the ContinuedPress event instead:
```c
fireCommand.ContinuedPress += FireCommand_ContinuedPress;
```

Do note the method doesn't need to be called FireCommand_ContinuedPress, it's just a convention for events. It can be called anything, as long as its definition has an object and an EventArgs.

The last thing to do is to simply call the Update method inside the Update():
```c
fireCommand.Update(gameTime);
```

[Go back to Classes](#classes)

### <a name="handleinputs"></a>HandleInputs

While you can use Command directly and update them manually, you can also take advantage of the HandleInputs static class. 
This will take care of handling a list of all commands and to update them all. It will also significantly reduce the line of codes needed to create a command and of course to update them all. In order to use it you need to apply the usual using statements:
```c
using Arta2DEngine.Input;
```

Afterwards, you can add any command you wish (better to do this inside the Initiate method as usual):
```c
HandleInputs.AddCommand(new Command(Keys.D), FireCommand_SinglePress, FireCommand_ContinuedPress);
```

In the example above we create a new command that will respond to the D key press, and with two events: one for the singlepress (FireCommand_SinglePress) and one for the continuedpress (FireCommand_ContinuedPress). In one single line we have created the command and assigned the events. 
If you don't want to assiign a singlepress event, you can simply pass null. If you  only want to assign a singlepress event, you can simply close the method there since it will default to null. This is the method signature:
```c
public static void AddCommand(Command command, EventHandler singlePressEvent = null, EventHandler continuedPressEvent = null)
```

As you can see, it's not mandatory to define any event, but you can do so in order to reduce the number of lines. If you wish to set the event at a later date, you can use the methods:
```c
public static void SetSinglePressEvent(Command command, EventHandler singlePressEvent)
```
or
```c
public static void SetContinuedPressEvent(Command command, EventHandler continuedPressEvent)
```

To set any event for any command already added in the list. The last thing to do is to call simply
```c
HandleInputs.Update(); 
```

Inside your Update() method and it will take care of updating every single command that has been added.

HandleInputs also has a couple of helpful methods:
```c
public static int GetCommandsCount()
public static void Clear()
DelCommand(Command command)
```

Which will return the total number of stored command, clear a list of commands and delete a command respectively.

[Go back to Classes](#classes)

### <a name="sfx"></a>SFX

This is a simple implementation for sound effects, from the Audio module. You need to add the correct using instruction in order to use the Command class:
```c
using Arta2DEngine.Audio;
```

Since it features default value, the minimum information to pass is the actual SoundEffect. You can also pass a volume, a pitch and pan. All these are between 0.0f and 1.0f.
```c
public SFX(SoundEffect effect, float volume = 1.0f, float pitch = 0.0f, float pan = 0.0f)
```

Of note, the SoundEffect paramenter can be declared directly without a SoundEffect, passing directly the Content.Load instruction:
```c
testSFX = new SFX(Content.Load<SoundEffect>("sound"));
```

The constructor can be placed either in Initialize() or in LoadContent(), though the latter is more advisable. That is it.

There are a few ways to handle a SFX:
* SimplePlay() 

   This will play the sound. It's the easiest way to play a sound effect. You won't have the possibility to stop or pause this, though, so it will need to simply finish on its own. You can use this multiple times (lilke for shooting bullets), launching this multiple times in a row or at the same time.
   
* Play(bool loopPlay = false)

   This will create a soundInstance for this sound effect, enabling control. Passing true will make this a looping sound (for instance, ambience sounds). This sound can be later Paused, Resumed and Stopped. Calling this while the sound is already playing will be ignored. Calling this when the sound is paused, will resume it.
   
* Pause()

   This will pause a sound effect played via the Play() method. It can be resumed with a Resume().
   
* Stop()

   This will stop a currently playing sound effect played via the Play() method.
   
Further, it is possible to Set the volume, pitch and pan values after creation, using:
```c
public void Set(float volume = 1.0f, float pitch = 0.0f, float pan = 0.0f)
```

It is also possible to Set a custom name for this sound (the construction will assign it the one assigned in the Content.mgcb) with this:
```c
public void SetName(string newName)
```

And it is possible to get the name, for other uses (like to find a specific sound from a list of SFX):
```c
public string GetName()
```

[Go back to Classes](#classes)

### <a name="bgm"></a>BGM

This is a simple implementation for Back Ground Music, from the Audio module. You need to add the correct using instruction in order to use the Command class:
```c
using Arta2DEngine.Audio;
```

Since it features default value, the minimum information to pass is the actual SoundEffect. You can also pass a volume.
```c
public BGM(Song song, float volume = 1.0f)
```

Of note, the Song paramenter can be declared directly without a Song, passing directly the Content.Load instruction:
```c
testBGM = new BGM(Content.Load<Song>("sound"));
```

The constructor can be placed either in Initialize() or in LoadContent(), though the latter is more advisable. That is it.

It's very important to note that only 1 BGM can be played at a given time. This is different from the sound effects which can play multiple times and overlap.

There are a few ways to handle a BGM:

* Play(bool loopPlay = false)

   This will play the song. Passing true will make this a looping music. This song can be later Paused, Resumed and Stopped. Calling this while the sound is already playing will stop any music currently playing a restart this one. Calling this when the song is paused, will resume it.
   
* Pause()

   This will pause a song already playing via Play() method. It can be resumed with a Resume(). 
   
* Stop()

   This will stop a currently playing song played via the Play() method.
   
Further, it is possible to Set the volume after creating, using:
```c
public void Set(float volume = 1.0f)
```

It is also possible to Set a custom name for this song (the construction will assign it the one assigned in the Content.mgcb) with this:
```c
public void SetName(string newName)
```

And it is possible to get the name, for other uses (like to find a specific sound from a list of SFX):
```c
public string GetName()
```

[Go back to Classes](#classes).

### <a name="utils"></a>Utils

This is a module that contains some helper methods. The Utils class is a static class so it doesn't need to be instatiated to be used..
You need to add the correct using statement for it:

```c
using Arta2DEngine.Utils;
```

The Utils class contains the below methods:

* public static void SetWindowSize(GraphicsDeviceManager graphicDeviceManager, int width, int height)

   This method will set the current Window Size. It needs the graphicManager from the Game() class, and width and height.

* public static void SetFullScreen(GraphicsDeviceManager graphicDeviceManager, bool fullscreen)

   This method will set the Full Screen. It needs the graphicManager from the Game() class, and a bool for the Fullscreen (true will enable it, false will disable it). Do keep in mind that, if you disable the full screen, the window size will still be the max resolution so you will need to change it back with SetWindowSize.   
   
* public static void SetWindowTitle(GameWindow gameWindow, string windowTitle)

   This method will set the Window's Caption. It needs the gameWindow from the Game() class, and the string.
   
* public static float GetFPS(GameTime gameTime)

   This method will return as a float the current FPS. It needs to be called inside Update() to have this value change and be, well, updated each frame.
   
* public static Vector2 GetCenterPosition(GraphicsDevice graphicsDevice, BaseObject baseObject, int xOffset = 0, int yOffset = 0)

   This method will return a Vector2 calculated to get the related position. Useful for positioning stuff in relation to the window.
   
* public static Vector2 GetTopCenterPosition(GraphicsDevice graphicsDevice, BaseObject baseObject, int xOffset = 0, int yOffset = 0)

   This method will return a Vector2 calculated to get the related position. Useful for positioning stuff in relation to the window.

* public static Vector2 GetBottomCenterPosition(GraphicsDevice graphicsDevice, BaseObject baseObject, int xOffset = 0, int yOffset = 0)

   This method will return a Vector2 calculated to get the related position. Useful for positioning stuff in relation to the window.
  
* public static Vector2 GetBottomLeftPosition(GraphicsDevice graphicsDevice, BaseObject baseObject, int xOffset = 0, int yOffset = 0)

   This method will return a Vector2 calculated to get the related position. Useful for positioning stuff in relation to the window.
  
* public static Vector2 GetBottomRightPosition(GraphicsDevice graphicsDevice, BaseObject baseObject, int xOffset = 0, int yOffset = 0)

   This method will return a Vector2 calculated to get the related position. Useful for positioning stuff in relation to the window.
   
* public static Vector2 GetTopRightPosition(GraphicsDevice graphicsDevice, BaseObject baseObject, int xOffset = 0, int yOffset = 0)

   This method will return a Vector2 calculated to get the related position. Useful for positioning stuff in relation to the window.

* public static float GetDistance(Vector2 pos, Vector2 target)

   This method will return the distance between two vectors as a float.
   
[Go back to Classes](#classes).

### <a name="camera2d"></a>Camera2D

This is a graphic module that implements a simple 2D Camera with zoom, panning and lookat functions.

You need to add the correct using statement for it:
```c
using Arta2DEngine.Graphics;
```

In order to use the Camera, you first need to create one. The ideal place for this is the Initialize() method. 
There are two available constructors. The first one only takes one paramenter: the game viewport. It will set the camera position at Vector2.Zero (0,0):

```c
cam = Camera2D(graphics.GraphicsDevice.Viewport);
```

The second one also takes a Vector2 to set the camera position:

```c
cam = Camera2D(graphics.GraphicsDevice.Viewport, new Vector2 (10.0f, 0.0f));
```

In order to properly use the camera, in the Draw() method, you need to use a specific spriteBatch.Begin instruction:
```c
spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend, null, null, null, null, cam.GetTransformation());
```

Basically, the final parameter of this .Batch constructor takes a Matrix and you must set it as the camera GetTransform method that will return the camera calculated matrix.

The camera has two methods available, plus some editable properties:

* public void Move(Vector2 amount)

   This method will allow you to move the camera position, passing a Vector2 as the amount.

* public Vector2 Position -- public float Zoom -- public float Rotation -- public Vector2 Origin

   These properties are all directly editable. However, the more interesting one is Zoom (beside the obvious position), as it allow you to change the zoom on the fly (read, on the Update() method).
   
* public void LookAt(Vector2 vectorToLookAt)

   This method will allow the camera to follow another object passing its position as Vector2.

See below an example on how to use the above methods inside the Update() function:
```c

// Move the camera and zoom via the keyboard
if (Keyboard.GetState().IsKeyDown(Keys.D))
	cam.Move(new Vector2(5.0f, 0));
if (Keyboard.GetState().IsKeyDown(Keys.A))
	cam.Move(new Vector2(-5.0f, 0));
if (Keyboard.GetState().IsKeyDown(Keys.W))
	cam.Move(new Vector2(0, -5.0f));
if (Keyboard.GetState().IsKeyDown(Keys.S))
	cam.Move(new Vector2(0, 5.0f));
if (Keyboard.GetState().IsKeyDown(Keys.PageUp))
	cam.Zoom += 0.1f;
if (Keyboard.GetState().IsKeyDown(Keys.PageDown))
	cam.Zoom -= 0.1f;
	
// Set the camera to follow an object
cam.LookAt(gameObject.Position);	
```

Do note that if you set the camera to follow an object, the user can't move it no longer manually as the Move() function will be overridden by the LookAt method (changing directly the Zoom property will work, though). If you put the Move() method below the LookAt method, it will produce some movement, but the camera will snap back to the LookAt object.

[Go back to Classes](#classes).

### <a name="particleengine"></a>Particle Engine

This is a graphic module that implements a simple Particle Engine.

You need to add the correct using statement for it:
```c
using Arta2DEngine.Graphics.ParticleEngine;
```

The engine is very basic and only features a single non configurable "emission type". In the future, the Emission Type will be a separate file that will be added in the constructor of a ParticleManager object.

In order to use the Engine, you first need to create a ParticleEngine:
```c
ParticleEngine particleEngine;
```

Then, in the LoadContent() method, you need to load some textures for the particles. It can be any number of them between 1 and... any sensible number. The constructor requires a List so even if you only have one, you should generate a list and then you can call the constructor giving the list of textures and the default position of the Particle Emitter:
```c
 // Load the particles and then initalize the ParticleEngine
            List<Texture2D> textures = new List<Texture2D>();
            textures.Add(Content.Load<Texture2D>("Images/particle")); 
```

Now, you need to create a Particle Engine. The constructor for it takes three parameters:
```c
public ParticleEngine(List<Texture2D> textures, Vector2 location, ParticleEffect particleEffect, bool oneshotEffect = false)
```

The first one is the list of textures it will use (in our example above, we created it and it is called "textures"), the second is the location for its emitter. The third one is the most important parameter of them all: it's a ParticleEffect that tells the engine how to genereate its particles. The fourth parameter (defaulted to false) tells the ParticleEngine if it should fire off the effect continuously (like a starfield) or just once (like an explosion). Coming back to the third paramenter,
Arta2DEngine gives one Default effect called DefaultParticleEffects and it can be passed as parameter; but first you need to create one:
```c
DefaultParticleEffect defaultEffect = new DefaultParticleEffect(2, 1.0f);
```

The constructor for this effect only takes two parameter: The total number of particles that should be fired and the starting scale for the particle (useful if the texture passed for the particle is too big or small). Once you created it, you can call the constructor for the ParticleEngine:

```c
particleEngine = new ParticleEngine(textures, Vector2.Zero, defaultEffect);
```

In the Update() method you only need to call:
```c
particleEngine.Update();
```

Lastly, to draw the particles, you need to call this in the Draw() method, but OUTSIDE of any spriteBatch.Begin/End cycle:
```c
particleEngine.Draw(spriteBatch);
```

Do keep in mind, you have the option to access the EmitterLocation property to change it inside the Update() method if you so desire:

```c
particleEngine.EmitterLocation = new Vector2( random.Next(0, ScreenManager.Game.GraphicsDevice.Viewport.Width) * randomization,
                                              random.Next(0, ScreenManager.Game.GraphicsDevice.Viewport.Height) * randomization);
```

In order to create your own ParticleEffects you need to extend the base ParticleEffect class in the engine, doing so:

```c
public class YourNewParticleEffect : ParticleEffect
```

It is adviseable to set the member field "numberOfParticles" to something you either want the user to customize (so inside a constructor), or directly. Then you only need to implement one single method: 
```c
public override Particle GenerateParticle(ParticleEngine particleEngine)
```

This method will need to return a Particle for the ParticleEngine to work with. In here you can define how you create each particle, and the total number of them. For example, the DefaultParticleEffects method is the below one:

```c
public override Particle GenerateParticle(ParticleEngine particleEngine)
{
	// Pick a random texture from the texture list available
	Texture2D texture = particleEngine.Textures[random.Next(particleEngine.Textures.Count)];

	// Set the location of the new particle based upon the emitter location
	Vector2 position = particleEngine.EmitterLocation;

	// Set the velocity for the new particle using random generated values
	Vector2 velocity = new Vector2(
			1f * (float)(random.NextDouble() * 2 - 1),
			1f * (float)(random.NextDouble() * 2 - 1));

	float angle = 0;

	// Set a random angularvelocity (spinning)
	float angularVelocity = 0.1f * (float)(random.NextDouble() * 2 - 1);

	// Set a random color for this particle
	Color color = new Color(
			(float)random.NextDouble(),
			(float)random.NextDouble(),
			(float)random.NextDouble());

	// Set a random size for this particle
	float size = (float)random.NextDouble();

	// Set a random ttl for this particle (minimum 5)
	int ttl = 20 + random.Next(40);

	// Set the total number of particles for this effect
	particleEngine.TotalParticles = numberOfParticles;

	// Return a new particle generated according to the above
	return new Particle(texture, position, velocity, angle, angularVelocity, color, size, ttl);
}
```

Using this system you can create pretty much any particle effect you want, making sure you create the number of particles you want, the texture you like to use, their time to life, their movement etc.

[Go back to Classes](#classes) - Go back to the [Readme](../README.md).