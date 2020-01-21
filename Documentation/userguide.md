# Arta2D Engine - User Guide 

This User Guide explains how the engine work and how to use it for your games.

### Getting Started

Check the [Readme](../README.md) for Getting Started.

## Classes
### GameObject

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

### Button

This is a simple implementation for a Button used in an UI.

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

Go back to the [Readme](../README.md).
