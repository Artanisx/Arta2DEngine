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

## Creating the Skeleton
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

MainGame.cs is practlycally completed and we don't need to change this anymore. We can now concentrate our efforts directly on the GameScreens, MenuScreen (for which we have created the skeleton already) and later the actual PlayScreen.

## Creating the MenuScreen
Coming back to the MenuScreen, let's fill up the Fields we're going to need. 
```c
// Fundamental Objects
ContentManager Content;
SpriteBatch spriteBatch;
Viewport viewPort;

// Menu Buttons
private Button playButton;
private Button quitButton;

// Text Labels        
private string playText;
private string quitText;

// Menu Positions
private Vector2 titlePos;
private Vector2 playButtonPos;
private Vector2 quitButtonPos;

// Fonts
private SpriteFont buttonFont;

// Textures 2D
private Texture2D buttonTexture;
private Texture2D titleTexture;
private Texture2D backgroundTexture;

// Audio
private BGM mainMenuMusic;
private SFX mouseOverSFX;

// Colors
private Color buttonFontColor, buttonBackColor, buttonHoverColor;

// Particle Engine
ParticleEngine particleEngine;
```

Most of them are pretty much clear, but let's go over them:
* **Menu Buttons**: These will hold the Buttons (Button class from the Arta2DEngine.Graphics.UI module) for our main menu.
* **Text Labels**: These will hold the strings we're going to use for the button text.
* **Menu Positions**: These will hold the Vector2 to store the position of various UI elements.
* **Fonts**: This holds the spritefont used for the button text.
* **Textures 2D**: The texture for our button, title and background.
* **Audio**: The sound effect (SFX) and song (BGM) for the menu screen.
* **Colors**: Colors used for the button backcolor, forecolor and hover color.
* **ParticleEngine**: The particle engine used for the menuscreen effect.

As I said, pretty much self explanatory. Before going forward, let's double click our Content.mgcb file inside Visual Studio to open the MonoGame Pipeline Tool. We'll need to import all our game resource. Rather than go one by one, let's import everything in. Follow the folder structure and naming structure as the below screenshot:
![MonoGame Pipeline Tool](../Documentation/images/pong_content.png)
We basically have three folders:
* **Audio**: This will hold our sound effects and music.
* **Fonts**: This will hold two sprite fonts for UI purposes.
* **Images**: This will hold the images and sprites.

You can basically put whatever you want in here as all the drawing code will take account of the size of the items (albeit having paddles too big will definitely make the game less interesting), but feel free to take the resources from [this archive](../Documentation/resources/monopong_resources.7z). All graphic beside the particle.png (I found it on my PC but I cannot recall where I got it...) has been expertly drawn by me with Paint3D specifically for this game. All sound effects have been done by me with [BFXR](https://www.bfxr.net/). The musics are done by [EricSkiff](http://ericskiff.com/music/).

Let's now go back to our MenuScreen constructor:
```c
public MenuScreen()
{
    // Labels & Text Setup            
    playText = "Play";
    quitText = "Quit";            

    // Color Setup
    buttonFontColor = Color.Yellow;
    buttonBackColor = Color.Blue;
    buttonHoverColor = Color.CornflowerBlue;            
}
```

Here we setup the strings for the buttons Play and Quit and we change the colors for font, background and hover of the buttons. You can put whatever you like here!

Logically, now let's take care of the "Funtamental" functions which are the LoadContent, UnloadContent, Update and finally Draw. Note that these fundamental functions are the base of any Monogame application!

Let's start with **LoadContent()**:
```c
public override void LoadContent()
{
    // Setup the ContentManager and the related root folder.
    if (Content == null)
        Content = new ContentManager(ScreenManager.Game.Services, "Content");

    // Set the viewport for ease of access
    this.viewPort = ScreenManager.Game.GraphicsDevice.Viewport;

    // Background Texture
    backgroundTexture = Content.Load<Texture2D>("Images/MenuBackground");

    // Initialize Button Texture
    buttonTexture = Content.Load<Texture2D>("Images/MenuButton");

    // Initialize Font
    buttonFont = Content.Load<SpriteFont>("Fonts/ButtonFont");

    // Ttile Logo
    titleTexture = Content.Load<Texture2D>("Images/MonoPongLogo");
    titlePos = new Vector2(viewPort.Width / 2 - titleTexture.Width / 2, 50 + titleTexture.Height / 2);          

    // Play Button position            
    playButtonPos = new Vector2(viewPort.Width / 2 - buttonTexture.Width / 2, viewPort.Height / 2 - buttonTexture.Height / 2 + 100);

    // Quit Button position            
    quitButtonPos = new Vector2(viewPort.Width / 2 - buttonTexture.Width / 2, viewPort.Height / 2 - buttonTexture.Height / 2 + 200);

    // Button Initialization
    playButton = new Button(buttonTexture, buttonFont, buttonFontColor, buttonBackColor, buttonHoverColor);
    playButton.Text = playText;
    playButton.Position = playButtonPos;
    playButton.Click += PlayButton_Click;
    playButton.MouseOver += Button_MouseOver;

    quitButton = new Button(buttonTexture, buttonFont, buttonFontColor, buttonBackColor, buttonHoverColor);
    quitButton.Text = quitText;
    quitButton.Position = quitButtonPos;
    quitButton.Click += QuitButton_Click;
    quitButton.MouseOver += Button_MouseOver;

    // Load the particles and then initalize the ParticleEngine
    List<Texture2D> textures = new List<Texture2D>();
    textures.Add(Content.Load<Texture2D>("Images/particle"));
    DefaultParticleEffect defaultEffect = new DefaultParticleEffect(5,0.15f);
    particleEngine = new ParticleEngine(textures, Vector2.Zero, defaultEffect, false);

    // LOAD AUDIO
    mainMenuMusic = new BGM(Content.Load<Song>("Audio/menumusic"), 0.7f);
    mainMenuMusic.Play(true);

    mouseOverSFX = new SFX(Content.Load<SoundEffect>("Audio/Blip"), 1.0f, 0.0f, 0.0f);

    // Show mouse cursor            
    ScreenManager.Game.IsMouseVisible = true;

    // Set Window Title
    Utils.SetWindowTitle(ScreenManager.Game.Window, "Mono Pong");
}
```

It's quite long, as we're loading several resources and creating a lot of items, but it's quite self explanatory. We load a Background texture, we initialize the button texture to be used for buttons, the font for them, we load the "MONO PONG" logo texture and position them using the Viewport to obtain the window width and height. Then we initalize thetwo buttons, and add the events to which they willrespond (both for Click event and MouseOver event). Then we create a list that will hold textures for our particles (we'll add only one though) and then create a DefaultParticleEffect composed of 5 particles scaled by 0,15 since our particle image is quite big. Then we initialize our ParticleEngine passing this particle effect. The effect itself is pretty basic, it simply spawns 5 (in our case) randomly rotating and moving particles! We then load the audio for BGM (and start the music) and SFXs. Everything here should be hopefully easy enough to grasp.

Next the **UnloadContent()**:
```c
public override void UnloadContent()
{
    // Stop and unload the music
    mainMenuMusic.Stop();
    mainMenuMusic = null;

    Content.Unload();
}
```
This is very easy, it simply stops the music, unload it and then call Content.Unload() to unload everything else we loaded in the LoadContent()! Pretty easy.

The **Update()** method is just as short:
```c
public override void Update(GameTime gameTime)
{
    base.Update(gameTime);

    // Update the Particles
    particleEngine.EmitterLocation = RandomizeParticleEmitterPosition(particleEngine);
    particleEngine.Update();

    // Update Buttons
    UpdateButtons(gameTime);
}
```
Firstly, we randomize the position of the ParticleEmitter in order to have the particles spawn everywhere. The actual method will be implemented below so don't worry about it for now. Then we Update() the particle Engine so that it can work its magic. Lastly we call a method that will update all the buttons. We'll create it later too.

Lastly, the elephant in the room: the **Draw()** method:
```c
public override void Draw(GameTime gameTime)
{
    // Set the spriteBatch if needed
    if (spriteBatch == null)
        spriteBatch = ScreenManager.SpriteBatch;

    // Set the background
    ScreenManager.GraphicsDevice.Clear(ClearOptions.Target, Color.CornflowerBlue, 0, 0);

    // DRAW BEGIN
    spriteBatch.Begin();

    // Draw Background
    spriteBatch.Draw(backgroundTexture, new Vector2(0, 0), Color.White);

    // Draw Title
    spriteBatch.Draw(titleTexture, titlePos, Color.White);

    // DRAW END
    spriteBatch.End();

    // Draw Particles
    particleEngine.Draw(spriteBatch);

    // Draw Buttons            
    DrawButtons();
}
```

It's fairly simple too. We load the spriteBatch we're going to use for drawing. Then we clear the screen with a standard color (this is not entirely needed as our background will cover the whole screen). Then we start a Draw cycle and draw the background and the logo. We start (and close) a draw cycle calling particle.Engine.Draw that will take care of drawing all the particles and in the end we call the DrawButtons() method that will take care of drawing each single button. We'll create this method shortly.

Before creating the several methods we discussed, let's go complete the Events that will be used for our buttons.
```c
private void PlayButton_Click(object sender, EventArgs e)
{
   LoadingScreen.Load(ScreenManager, false, new PlayScreen()); // Load the gameplay screen (PlayScreeN)
}

private void QuitButton_Click(object sender, EventArgs e)
{
    // Close the game.
    ScreenManager.Game.Exit();
}

private void Button_MouseOver(object sender, EventArgs e)
{
    // Play a sound effect
    mouseOverSFX.SimplePlay();
}
```

Extremely simple events that will fire when a player clicks on the Play button, the Quit button and finally when he mouse overs. The PlayButton_Click event loads the actual gameplay screen. For now, since we don't have a PlayScreen.cs yet, this won't work so feel free to keep that line commented if you don't want to see compiler errors. The QuitButton_Click events calls the Exit() method from the base Game() stored inside the ScreenManager, while the Button_MouseOver event will play a sound effect.

Finally, the private methods we called during the Update() and Draw():
```c
 private void UpdateButtons(GameTime gameTime)
{
    playButton.Update(gameTime);
    quitButton.Update(gameTime);
}

private void DrawButtons()
{
    spriteBatch.Begin();
    playButton.Draw(spriteBatch);
    quitButton.Draw(spriteBatch);
    spriteBatch.End();
}

private Vector2 RandomizeParticleEmitterPosition(ParticleEngine particleEngine)
{
    // Generate a random
    Random random = new Random();

    // Apply some random jitter to make the enemy move around.
    const float randomization = 1;

    return new Vector2( random.Next(0, viewPort.Width) * randomization,
                        random.Next(0, viewPort.Height) * randomization);            
}
```

Those are quite self explanatory as well. UpdateButtons simply calls Update() for each button. It's there for having a clean Update() function. DrawButtons() does exactly what it says, while the RandomizeParticleEmitterPosition is a little bit more involved. It basically moves the Emitter in a randomized position. The X axis is between 0 and the screen width plus some randomization, and the Y axis again between 0 and the screen height plus randomization. This makes the particles appear all over the screen.

The MainScreen.cs is done!

## Creating the Game Objects
Now, before we takle the PlayScreen which is our gameplay screen, let's think about what this game needs.
We will need a Ball that will bounce around. We will also need two Paddles, one of which will be player controlled. That's about it as far as the game object go.

Let's start with the **Ball**! Create a new class called:
```c
Ball.cs
```

The ball will be an extension of the GameObject from Arta2DEngine, so make sure to include the right using statements:
```c
using Arta2DEngine.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
```

Then the actual class declaration:
```c
public class Ball : GameObject
```

Let's begin with the fields. They are not many as most of the ground work has been taken care of by the GameObject (and BaseObject) classes of the engine:
```c
// Main fields
private Viewport viewPort;
private Random random;
private Vector2 direction;
private Vector2 origin;

// Events
public event EventHandler WallBounce;       

// We need to override the base Origin method as we need the actual origin, not the precalculated and centered one given by the GameObject class
public override Vector2 Origin
{
    get
    {
        return this.origin;
    }

    set { this.origin = value; }
}
```

The main fields are quite easy, we do add a direction (since our ball will need to to move around) and an origin. The origin property from the GameObject implementation is overridden. Basically the GameObject property automatically returns the origin as its center point. This makes a lot of sense if you want to rotate objects around, but since this is a ball we don't need to rotate its sprite, as such we actually prefer to consider its origin point to be at 0,0. We also add an event for when the ball will bounce on walls and create the EventHandler WallBounce.

Let's go with the constructor now!
```c
public Ball(Texture2D texture, Vector2 position, Vector2 velocity, Viewport viewPort, EventHandler wallBounceEvent) : base(texture, position, velocity)
{
    random = new Random();

    this.viewPort = viewPort;

    // Set a random rotation angle for the beginning of the game
    RandomDirection();

    // Set the origin to the 0,0
    this.Origin = new Vector2(0, 0);

    // The event handler for a wall bounce
    this.WallBounce = wallBounceEvent;
}      
```

The constructor basically calls the base GameObject constructor, but does a couple of things of its own. Creates a new random object, it imprts the viewport that is passed as an argument, sets a RandomDirection calling the method we will write later, sets the origin point to 0,0 as we said earlier and sets the event handler WallBounce to what has been set in the constructor. Easy!

As per our MenuScreen, let's go with the fundamental functions first: Update() and Draw():
```c
public override void Update(GameTime gameTime)
{
    float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

    // Move the ball by its velocity
    MoveBall(Velocity, dt);

    // Checks to avoid it going out of bounds
    CheckBounds();
}
```
The update method is quite simple, it creates a delta time, calls the method MoveBall (that will.. move the ball!) and CheckBunds that will make sure the ball won't go out of bounds. Both methods will be written later.

Now, the Draw() method:
```c
public override void Draw(SpriteBatch spriteBatch)
{
    // We need to use a basic draw here, we don't need to setup the origin nor the rotationangle despite there being one.
    spriteBatch.Draw(Texture, Position, Color.White);
}
```

This is super easy. We simply draw the ball! We actually override the Draw() from the GameObject because we don't want to involve any rotation despite the object having a rotation itself. 

Let's now go to the regular method we called earlier.

```c
public void RandomDirection()
{
    random = new Random();

    float angle = random.Next(0, 10);

    if (angle > 5)
        SetRotationAngle(45);
    else
        SetRotationAngle(220);
}
```    
This method will simply generate a random direction for our ball, between angle 45 (right) and angle 220 (left) so that the ball can go either direction when the game start/resets.

```c
private void MoveBall(Vector2 speed, float dt)
{
    direction = new Vector2((float)Math.Cos(this.RotationAngle),
                            (float)Math.Sin(this.RotationAngle));

    direction.Normalize();
    this.Position += direction * speed * dt;
}
```         

MoveBall takes a speed and a deltatime to move the ball. The direction is calculated with some math magic against the rotation angle we have set (remember the SetRotationAngle method called in the RandomDirection?), then we normalize the vector (other math witchcraft) and calculate the position along this direction multiplied by the speed and the delta time. This is all regular "game" math stuff, that should make sense.

```c
private void SetRotationAngle(float degrees)
{
    this.RotationAngle = MathHelper.ToRadians(degrees);
}
```        

SetRotationAngle is easy, it uses the MathHelper from MonoGame to calculate radians from degrees. Math devilry.
```c
private void CheckBounds()
{
    int maxX = this.viewPort.Width - this.Texture.Width;
    int maxY = this.viewPort.Height - this.Texture.Height;

    if (this.Position.X > maxX || this.Position.X < 0)
    {
        this.Velocity = new Vector2(this.Velocity.X * -1, this.Velocity.Y);
    }


    if (this.Position.Y > maxY || this.Position.Y < 0)
    {
        this.Velocity = new Vector2(this.Velocity.X, this.Velocity.Y * -1);

        // Invoke the WallBounce event
        WallBounce?.Invoke(this, new EventArgs());
    }

}
```   
The CheckBound method will make sure the ball will stay "in" the window. Firstly we calculate what is the maximum X position against the window width and the texture width of the ball. We do the same with the Y position. Then we calculate the actual position against these maximum positions (our borders) and if the ball is over them, it inverts the velocity (basically it goes the opposite way). If it touches the ceiling or the floor (basically if it goes over or under the Y axis) it will also invoke our WallBounce event so that we can do something like playing a sound effect.

The ball is done!

Let's proceed with the **Paddle**! Create a new class called:
```c
Paddle.cs
```

The paddle is  an extension of the GameObject from Arta2DEngine as well, so make sure to include the right using statements:
```c
using Arta2DEngine.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
```

Then the actual class declaration:
```c
public class Paddle : GameObject
```

As far as fields go, it only needs a Viewport:
```c
private Viewport viewPort;
```

As such, the constructor is incredibly easy:
```c
public Paddle(Texture2D texture, Vector2 position, Vector2 velocity, Viewport viewPort) : base(texture, position, velocity)
{
    this.viewPort = viewPort;
}
```

But rest assured, the rest of the Paddle class is just as easy. We can go straight to the Update() method:
```c
 public override void Update(GameTime gameTime)
{            
    // Check bounds so it doesn't go outside
    CheckBounds();
}
```
This simply calls a CheckBounds() method that will make sure the paddles don't exit the screen.

Now, let's write the methods the Paddle will need, starting with the Move():
```c
public void Move(float speed, GameTime gameTime)
{
    // The paddle can only move in the Y axis

    float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

    this.Position += new Vector2(0.0f, speed * dt);
}
```

This will be used to move the paddle gived a velocity. We basically move it on the Y axis only since the paddle can only go UP or DOWN.

Finally the CheckBounds() method:
```c
 private void CheckBounds()
{            
    if (this.Position.Y <= 0 )
    {
        // Stop this to Y = 0
        this.Position = new Vector2(this.Position.X, 0);
    }

    if (this.Position.Y >= this.viewPort.Height - this.Texture.Height)
    {
        // Stop this to Y = max height - texture height
        this.Position = new Vector2(this.Position.X, this.viewPort.Height - this.Texture.Height);
    }
}
```
This works similarly to the Ball CheckBounds method, but this zeroes the Y position when the paddle tries to go too much "up" and sets it to screen.height - paddle.height when it tries to go too much "down".

Believe it or not, the Paddle is done! 

It's time for our last game object, the most interesting one: the **PlayerPaddle**. This is going to be an extension not of GameObject, but of the Paddle class we just wrote.

Create a new class called:
```c
PlayerPaddle.cs
```

Since this time player input is involved, let's make sure to include the right using statements including the Arta2DEngine.Input module:
```c
using System;
using Arta2DEngine.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
```


Then the actual class declaration:
```c
public class PlayerPaddle : Paddle
```

Now, the fields are not a lot:
```c
// Commands
Command MoveUp_1, MoveDown_1, MoveUp_2, MoveDown_2;

// Gametime Variable
GameTime gameTime;
```

We're using Command from Arta2DEngine.Input module to control our player input. We have two MoveUp and two MoveDown because we want to allow the player to move the paddle both with W/S and with the up and down arrows.

The constructor is a little bit more involved than the others, but this is just because we're implementing the commands. It's actually fairly easy:
```c
public PlayerPaddle(Texture2D texture, Vector2 position, Vector2 velocity, Viewport viewPort) : base(texture, position, velocity, viewPort)
{
    MoveUp_1 = new Command(Keys.W);
    MoveUp_2 = new Command(Keys.Up);
    MoveUp_1.ContinuedPress += MoveUp_ContinuedPress;            
    MoveUp_2.ContinuedPress += MoveUp_ContinuedPress;

    MoveDown_1 = new Command(Keys.S);            
    MoveDown_2 = new Command(Keys.Down);
    MoveDown_1.ContinuedPress += MoveDown_ContinuedPress;
    MoveDown_2.ContinuedPress += MoveDown_ContinuedPress;

    // Make sure the velocity is positive
    this.Velocity = new Vector2(0f, Math.Abs(velocity.Y));
}
```

We create new Command objects, we pass the Keys we need as mentioned earlier, and we attach the EventHandlers for the ContinuedPress events (as we need to "keep moving" until the button is pressed). We also use Math.Abs to get the absolute value of our velocity, basically making sure it's positive.

Let's now create the Events we're going to respond to:
```c
private void MoveDown_ContinuedPress(object sender, EventArgs e)
{            
    Move(this.Velocity.Y, this.gameTime);
}

private void MoveUp_ContinuedPress(object sender, EventArgs e)
{
    Move(-this.Velocity.Y, this.gameTime);
}
```

This will use the Paddle.Move() method to move the paddle up or down. As far as the Fundamental functions, we only have one easy Update():
```c
 public override void Update(GameTime gameTime)
{
    this.gameTime = gameTime;

    HandleInput(gameTime);

    base.Update(gameTime);
}
```

We call the HandleInput() method which we'll write soon, and the base.Update() method to call the Paddle.Update() which will do what needs to be done since the player paddle is also a paddle!

The HandleInput() is super easy:
```c
private void HandleInput(GameTime gameTime)
{
    MoveUp_1.Update(gameTime);
    MoveUp_2.Update(gameTime);
    MoveDown_1.Update(gameTime);
    MoveDown_2.Update(gameTime);
}
```

It basically updates all Commands!

Yes, you guessed it, the PlayerPaddle class is done and we can go to the big and scary PlayScreen class, where the magic begins!

## Creating the PlayScreen
The PlayScreen class is where the actual gameplay is. You might recall we called it inside the MenuScreen, when the player clicked on the playButton. Now it's time to deal with this. In this class all the gameplay for MonoPong actually happens. It's going to be big, but fret not: it's actually only marginally longer than the MenuScreen class.

Go ahead and create a new class and call it:
```c
PlayScreen.cs
```

There's a lot of stuff going on, so the using statement section is a bit on the long side:
```c
using Arta2DEngine.Audio;
using Arta2DEngine.Input;
using Arta2DEngine.Utils.SceneManager;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
```

As you know, PlayScreen is an extension of the GameScreen class provided by the Arta2DEngine.Utils.ScreenManager module:
```c
class PlayScreen : GameScreen
```

Let's go with all the **Fields** first:
```c
 // Fundamental Objects
ContentManager Content;
SpriteBatch spriteBatch;
Viewport viewPort;

// Game Objects
PlayerPaddle leftPaddle;
Paddle rightPaddle;
Ball ball;

// Commands
private Command commandStartGame;
private Command quitToMenu;

// Rectangles
Rectangle leftGoal, rightGoal;

// Textures2D
private Texture2D backgroundTexture;

// SpriteFonts
private SpriteFont HUDFont;

// Audio
private SFX bounceBall;
private SFX paddleBounceBall;
private SFX goalBounceBall;
private BGM playMusic;

// Game Variables
private float paddleSpeed = 250f;
private float ballSpeed = 500f;
private float acceleration = 1.05f;        
private int playerScore = 0;
private int enemyScore = 0;
private string startText = "Press SPACE to start!\n" +
                           ">>>>ESC to quit<<<<";
private bool startTextVisible = true;
private bool ballAtTheCenter = true;
```

Wow. That's a lot of variables! Let's go over them:
* **Game Objects**: We know we need two paddles and a ball and we created the classes for them. So now we create the fields.
* **Commands**: We need a couple of commands for this screen, a way to start/restart the game and a way to quit back to the main menu.
* **Rectangles**: There are two goal in the screen, the left and the right "walls". If the ball goes to the left side, the enemy will gain a point. If the ball goes to the right side, the player will gain a point. But "left" and "right" side are vague. We will need two rectangles to define these places for our collisions to work.
* **Textures2D**: This holds the background texture.
* **SpriteFonts**: The font for our HUD messages like the score and the message to star the game.
* **Audio**: The sound effect (SFX) and song (BGM) for the play screen.
* **Game Variables**: We have quite a few values here. These can be changed and define the speed of the ball and paddle, the acceleration for each bounce (we want the ball to go a little bit faster each time it bounces on a paddle), the variables for the scores, the text we want to show for the start/restart the game and a couple of bool that will set if we must show the "restart" text and if we are actually waiting to start/restart (ballAtCenter) or not.

Not too complex after all! We will need a constructor, but this is going to be the easiest so far:
```c
public PlayScreen()
{
    
}
```

Why is it empty? Because we don't need to initialize anything that can be initialized outside of the LoadContent() module. But, since this is a class, we need a constructor! Speaking of LoadContent, we need to take care of the Fundamental Functiosn now, starting with **LoadContent()** indeed:
```c
 // Setup the ContentManager and the related root folder.
if (Content == null)
    Content = new ContentManager(ScreenManager.Game.Services, "Content");

// Set the viewport for ease of access
this.viewPort = ScreenManager.Game.GraphicsDevice.Viewport;

// Background Texture
backgroundTexture = Content.Load<Texture2D>("Images/playbg");

// Paddles
leftPaddle = new PlayerPaddle(Content.Load<Texture2D>("Images/paddle"), Vector2.Zero, new Vector2(0.0f, paddleSpeed), viewPort);
leftPaddle.Position = new Vector2(50, viewPort.Height / 2 - leftPaddle.Texture.Height / 2);

rightPaddle = new Paddle(Content.Load<Texture2D>("Images/paddle"), Vector2.Zero, new Vector2(0.0f, paddleSpeed), viewPort);
rightPaddle.Position = new Vector2(viewPort.Width - 50 - rightPaddle.Texture.Width, viewPort.Height / 2 - rightPaddle.Texture.Height / 2);

// Ball - Also passing the Method for the WallBounce event
ball = new Ball(Content.Load<Texture2D>("Images/Ball"), Vector2.Zero, Vector2.Zero, viewPort, WallBounce);
ball.Position = new Vector2(viewPort.Width / 2 - ball.Texture.Width / 2, viewPort.Height / 2 - ball.Texture.Height / 2);           

// Audio
bounceBall = new SFX(Content.Load<SoundEffect>("Audio/bounce"));
paddleBounceBall = new SFX(Content.Load<SoundEffect>("Audio/paddleBounce"));
goalBounceBall = new SFX(Content.Load<SoundEffect>("Audio/goalBounce"));
playMusic = new BGM(Content.Load<Song>("Audio/playMusic"));
playMusic.Set(0.8f);
playMusic.Play(true);            

// Fonts
HUDFont = Content.Load<SpriteFont>("Fonts/HUDFont");

// Goals
leftGoal = new Rectangle(0, 0, 25, viewPort.Height);
rightGoal = new Rectangle(viewPort.Width - 25, 0, 25, viewPort.Height);

// Commands
commandStartGame = new Command(Keys.Space);
commandStartGame.SinglePress += CommandStartGame_SinglePress;
quitToMenu = new Command(Keys.Escape);
quitToMenu.SinglePress += QuitToMenu_SinglePress;  
```

There's a lot going on here. The first few lines are not new, but when we reach the paddles we have something new indeed. We create a leftPaddle using the PlayerPaddle constructor. We pass:
```c
leftPaddle = new PlayerPaddle(Content.Load<Texture2D>("Images/paddle"), Vector2.Zero, new Vector2(0.0f, paddleSpeed), viewPort);
```
The first argument might be a bit confusing as the first paramenter for the constructor expects a Texture2D. Instead of creating a Texture2D and to fill it with the result of the Content.Load we skip a step and pass the method as a parameter. The very same could be achieved if we went fully verbose:
```c
Texture2D paddle = Content.Load<Texture2D>("Images/paddle");
leftPaddle = new PlayerPaddle(paddle, Vector2.Zero, new Vector2(0.0f, paddleSpeed), viewPort);
```

In any event, the second paramenter is the position. We set it at Vector2.Zero because we actually want to use the texture (and viewport) to position it well, but until the leftPaddle object has been created we don't actually know the texture width for it! Then we pass a Vector2 for the velocity (no movement on X axis: it's a paddle) and then the viewport.

At this point we do set up the position of the paddle:
```c
leftPaddle.Position = new Vector2(50, viewPort.Height / 2 - leftPaddle.Texture.Height / 2);
```

Thanks to the viewport and the now existent texture, we position it at the center left portion of the screne.

The rightPaddle is basically the same, and so it is the ball with one exception: we also pass the EventHandler for the WallBounce event:
```c
ball = new Ball(Content.Load<Texture2D>("Images/Ball"), Vector2.Zero, Vector2.Zero, viewPort, WallBounce);
```

Then we load some audio and music and the font. We then create two rectangles for the "goals":
```c
leftGoal = new Rectangle(0, 0, 25, viewPort.Height);
rightGoal = new Rectangle(viewPort.Width - 25, 0, 25, viewPort.Height);
```
These will be used for collisions and to detect when the player (or the enemy) score. We lastly create the commands setting the events and the keys (Space for Start/Restart, Esc for quit to main menu).

It's time for the **UnloadContent()** which is way shorter.
```c
public override void UnloadContent()
{
    // Stop and unload the music
    playMusic.Stop();
    playMusic = null;

    Content.Unload();
}
```

Not much to say here, it's identical to the MenuScreen one. Off to the **Update()** method:
```c
public override void Update(GameTime gameTime)
{
    base.Update(gameTime);

    // Update all the game objects
    leftPaddle.Update(gameTime);
    rightPaddle.Update(gameTime);
    ball.Update(gameTime);

    // Update the Commands
    commandStartGame.Update(gameTime);
    quitToMenu.Update(gameTime);

    // Make the rightPaddle chase the ball
    ChaseBall(gameTime);

    // Check all collisions between ball and the paddles
    CheckCollisions();
}
```

Firstly we update each GameObject calling their related Update(). Then we update the Commands() so we can be sure their event will be listened to. Then we call two methods which we'll write later: one to make sure the EnemyPaddle will try to chase the ball (basically the AI). And one to check for collisions! But before we can take a look at them, we need to **Draw()**:
```c
public override void Draw(GameTime gameTime)
{
    // Set the spriteBatch if needed
    if (spriteBatch == null)
        spriteBatch = ScreenManager.SpriteBatch;

    // Set the background
    ScreenManager.GraphicsDevice.Clear(ClearOptions.Target, Color.CornflowerBlue, 0, 0);

    // DRAW BEGIN
    spriteBatch.Begin();

    // Draw Background
    spriteBatch.Draw(backgroundTexture, new Vector2(0, 0), Color.White);

    // Draw Paddles
    leftPaddle.Draw(spriteBatch);
    rightPaddle.Draw(spriteBatch);

    // Draw Ball
    ball.Draw(spriteBatch);

    // Draw HUD
    spriteBatch.DrawString(HUDFont, playerScore.ToString(), new Vector2(20, 20), Color.Red);
    spriteBatch.DrawString(HUDFont, enemyScore.ToString(), new Vector2(viewPort.Width - 40, 20), Color.Red);

    // Only draw the StartMessage if it's needed
    if (startTextVisible)
        spriteBatch.DrawString(HUDFont, startText, new Vector2(viewPort.Width/2 - HUDFont.MeasureString(startText).X / 2, viewPort.Height/ 2 - HUDFont.MeasureString(startText).Y / 2), Color.Yellow);

    // DRAW END
    spriteBatch.End();
}
```
This Draw() is not much different than the MenuScreen one, but we do have a couple of new entry. Sure, we draw the paddles and ball calling their Draw methods, inside a Draw cycle. But we also call the DrawString() method twice to draw our HUD:
```c
spriteBatch.DrawString(HUDFont, playerScore.ToString(), new Vector2(20, 20), Color.Red);
spriteBatch.DrawString(HUDFont, enemyScore.ToString(), new Vector2(viewPort.Width - 40, 20), Color.Red);
```

We use these to draw the number that shows the player score in the upper left corner (20,20) and the enemy score in the upper right corner (width -40, 20), in Red! Last but not least, we draw the text that tells the player they need to press Space to start/restart the game. But only if "startTextVisible" is true. We don't need this to be shown while the match is ongoing. 

It is now time to write the **Events**, used for our Commands but also for our Ball when it "WallBounces":
```c
private void QuitToMenu_SinglePress(object sender, EventArgs e)
{
    LoadingScreen.Load(ScreenManager, false, new MenuScreen()); // Load the menu screen
}

private void CommandStartGame_SinglePress(object sender, EventArgs e)
{
    Start();
}

private void WallBounce(object sender, EventArgs e)
{
    bounceBall.SimplePlay();
}
```

There's nothing particular here. The QuitToMenu calls the .Load method to load the MenuScreen. The CommandStartGame calls a Start() method which will start/restart the game. The WallBounce method simply plays a sound effect.

Alright, we're on the home stretch now! Let's start with... **Start()**:
```c
public void Start()
{
    // This method can only be launched if the ball is at the center (meaning the game hasn't yet started or a goal was just made).
    if (ballAtTheCenter)
    {
        ball.Position = new Vector2(viewPort.Width / 2 - ball.Texture.Width / 2, viewPort.Height / 2 - ball.Texture.Height / 2);
        acceleration = 1.1f;
        ball.Velocity = new Vector2(ballSpeed, ballSpeed);
        ball.RandomDirection();
        ballAtTheCenter = false;
        startTextVisible = false;
    }
}
```

This method firstly checks if the ballAtCenter is true. Meaning, the ball is sitting at the center of the screen and the game is waiting us to give the "greenlight" to start a match. If so, we position the ball at the center (this is to be sure we reset it there), we reset the acceleration, we reset the velocity, give a new random direction to the ball and set to false both the ballAtCenter (this is because with the new velocity and direction the ball will start moving right away and the match will re/start!) and also hide away the starText telling the player to press space to start... since they just did that!

Now on to **CheckCollision()** which is probably the longest non LoadContent function:

```c
public void CheckCollisions()
{
    // Check if the ball collides with the leftpaddle or rightpaddle
    if (ball.BoundingBox.Intersects(leftPaddle.BoundingBox) || ball.BoundingBox.Intersects(rightPaddle.BoundingBox))
    {
        // Let's avoid the accelleration to become too much
        acceleration = MathHelper.Clamp(acceleration, 1, 2);

        Random rnd = new Random();

        float offset = rnd.Next(1, 10);

        ball.Velocity = new Vector2((ball.Velocity.X * acceleration) * -1, ball.Velocity.Y + offset);

        // Play the paddlebounce sound
        paddleBounceBall.SimplePlay();
    }

    // Check if ball collides with the right goal (player scores)
    if (ball.BoundingBox.Intersects(rightGoal))
    {
        // Ball hit a goal, reset it
        ball.Position = new Vector2(viewPort.Width / 2 - ball.Texture.Width / 2, viewPort.Height / 2 - ball.Texture.Height / 2);
        acceleration = 1.1f;
        ball.Velocity = Vector2.Zero;
        ballAtTheCenter = true;
        startTextVisible = true;

        // Play the goalBounce sound
        goalBounceBall.SimplePlay();

        playerScore++;
    }

    // Check if ball collides with the right goal (enemy scores)
    if (ball.BoundingBox.Intersects(leftGoal))
    {
        // Ball hit a goal, reset it
        ball.Position = new Vector2(viewPort.Width / 2 - ball.Texture.Width / 2, viewPort.Height / 2 - ball.Texture.Height / 2);
        acceleration = 1.1f;
        ball.Velocity = Vector2.Zero;
        ballAtTheCenter = true;
        startTextVisible = true;

        // Play the goalBounce sound
        goalBounceBall.SimplePlay();

        enemyScore++;
    }
}
```

Here we do some collision checks as the method's name tells us it will. Thanks to the BoundingBoxes afforded us to each GameObject (thanks Arta2DEngine.Graphics!) we can call the Intersects method to see if the ball insersects (collides!) with any of the two paddles. If it does, the acceleration will be incremented, we will invert the ball's velocity (making it go the opposite way) and we play a sound effect.
Afterwards we want to check if the ball intersects (collides!) with one of the two rectangles we defined for the goals. If it's the rightGoal then the player scored! We need to reset the ball for a new match (positioning it at the center, with starting acceleration, zero velocity so it doesn't move and setting the ballAtCenter and startTextVisible variables accordingly), play a sound effect and of course, to increment the player score!
We then check if the ball collided with the leftGoal... if that happenend, basically the same stuff will happen minus the player score being incremented... this time the Enemy score will go up. Sad!
And that's it. It's time for the enemy egregious AI. **ChaseBall()** I choose you:
```c
private void ChaseBall(GameTime gameTime)
{
    // Only if the ball is moving.
    if (!ballAtTheCenter)
    {
        // Ball is lower than the right paddle
        if (ball.Position.Y >= rightPaddle.Position.Y)
            rightPaddle.Move(paddleSpeed, gameTime); // Move paddle up                     

        // Ball is higher than the right pabble
        if (ball.Position.Y < rightPaddle.Position.Y)
            rightPaddle.Move(-paddleSpeed, gameTime); // Move paddle down                
    }
}
```

This basically checks if the ball is NOT at the center (so the match is ongoing) and then simply checks: Is the ball higher than the right paddle position? If so, move the right paddle up!
Is the ball lower than the right paddle position? If so, move the right paddle down! Simple as that, but believe me, the enemy paddle will be quite hard to beat!

It might seem hard to believe, but this is the end of the Mono Pong tutorial! The game is complete. I hope you had fun following this tutorial!

Go back to the [Readme](../README.md) for Getting Started.
