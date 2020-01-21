# Arta2D Engine - User Guide 

This User Guide explains how the engine work and how to use it for your games.

### Getting Started

Check the [Readme](../README.md) for Getting Started.

## <a name="classes"></a>Classes
List of classes
* [GameObject](#gameobject)
* [Button](#button)
* [Command](#command)

### <a name="gameobject"></a>GameObject

This is the first simple class for the Engine. It serves as a container for GameObjects in a similar fashion to Unity 3D engine.
TODO: Complete the guide

To create a GameObject simply do the below:
```c
// Create a GameObjectg
GameObject gameObject;

// Initialize it - LoadContent() is a good place for this
gameObject = new GameObject(goTexture, Vector2.Zero);

// Draw it - Draw() is the perfect place. This must be called inside the Begin / End spriteBatch.Draw.
spriteBatch.Begin();
gameObject.Draw(spriteBatch);
spriteBatch.End();
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
gamepadX.SinglePress += GamepadX_SinglePress;
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

[Go back to Classes](#classes) - Go back to the [Readme](../README.md).
