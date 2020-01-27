# Arta2D Engine - User Guide 

This User Guide explains how the engine work and how to use it for your games.

### Getting Started

Check the [Readme](../README.md) for Getting Started.

## <a name="classes"></a>Classes
List of classes
* [GameObject - Graphic](#gameobject)
* [Camera2D - Graphic](#camera2d)
* [Button - Graphic.UI](#button)
* [Command - Input](#command)
* [SFX - Audio](#sfx)
* [BGM - Audio](#bgm)
* [Utils - Utils](#utils)
* [Screen Manager - Utiles](screenmanager.md)

### <a name="gameobject"></a>GameObject

This is a simple Game Object class for the Engine. It serves as a container for GameObjects in a similar fashion to Unity 3D engine. 
GameObject extends BaseObject an abstract class that gives some basic functionality. 

A BaseObject has:

* Texture2D Texture

   This holds a Texture2D for each object.

* Vector2 Position

   This holds the position for this object expressed as a Vector2.

* Rectangle BoundingBox

   This holds a Rectangle property that encompass the texture. It is created against its Position and Texture information so each time it is polled will be at the right position. It can be used for general and not very precise collision detection.
   
* Circle BoundingCircle

   This holds a Circle property that encompass the texture. It can be used for collision detection. If an object is less of a square (for instance it's a space ship), checking collisions with a Circle will fire less false collisions than the BoundBox ones. It is a bit more precise than the BoundingBox collision, but it's dependant on the actual texture. This is created against the position and the texture information (from which a center is calculated alongside a radius based on the side of the texture).
   
While the BaseObject cannot be instantiated, it holds a definition for a Draw method that can be overridden if needed:
```c
public virtual void Draw(SpriteBatch spriteBatch)
```

A GameObject has additional stuff:

* Vector2 Velocity

   This holds a Vector2 used for its Velocity.

GameObject implements two different constructors:
```c
GameObject(Texture2D texture, Vector2 position)
```

Omitting a Velocity and one that requires it:
```c
GameObject(Texture2D texture, Vector2 position, Vector2 velocity)
```
   
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

[Go back to Classes](#classes) - Go back to the [Readme](../README.md).