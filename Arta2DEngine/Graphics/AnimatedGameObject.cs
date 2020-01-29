using Arta2DEngine.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Arta2DEngine.Graphics
{
    /// <summary>
    /// This is an extended Game Object that also implements animation via a texture atlas.    
    /// </summary>
    public class AnimatedGameObject : GameObject
    {
        // The Texture2D Texture from the BaseObject will hold the Texture Atlas

        // This is the number of Rows inside the atlas
        public int Rows { get; set; }

        // This is the number of Columns inside the atlas
        public int Columns { get; set; }

        // Current frame of the animation
        private int currentFrame;

        // Total frames of the animation
        private int totalFrames;

        // Rectangles for the source and destination, used to calculate the correct frame for drawing
        private Rectangle sourceRectangle;
        private Rectangle destinationRectangle;

        // Color tint
        private Color colorTint;

        /// <summary>
        /// This is the AnimationSprite class constructor. It takes a Texture2D (the atlas) and its rows and columns.
        /// </summary>
        /// <param name="texture">The Texture2D containing the Atlas.</param>
        /// <param name="position">The starting position of the game object.</param>
        /// <param name="rows">The number of rows in the passed Atlas.</param>
        /// <param name="columns">The number of columns in the passed Atlas.</param>
        public AnimatedGameObject(Texture2D texture, Vector2 position, int rows, int columns) : base(texture, position)
        {
            Rows = rows;
            Columns = columns;
            colorTint = Color.White;

            // Set the current frame to 0 (beginning)
            currentFrame = 0;

            // Calculate the total number of available frames in this atlas
            totalFrames = Rows * Columns;
        }

        /// <summary>
        /// This is the AnimationSprite class constructor. It takes a Texture2D (the atlas) and its rows and columns.
        /// </summary>
        /// <param name="texture">The Texture2D containing the Atlas.</param>
        /// <param name="position">The starting position of the game object.</param>
        /// <param name="rows">The number of rows in the passed Atlas.</param>
        /// <param name="columns">The number of columns in the passed Atlas.</param>
        /// <param name="velocity">The starting velocity of the game object.</param>
        public AnimatedGameObject(Texture2D texture, Vector2 position, int rows, int columns, Vector2 velocity) : base(texture, position)
        {
            Rows = rows;
            Columns = columns;
            colorTint = Color.White;

            // Set the current frame to 0 (beginning)
            currentFrame = 0;

            // Calculate the total number of available frames in this atlas
            totalFrames = Rows * Columns;
        }

        /// <summary>
        /// This is the AnimationSprite class constructor. It takes a Texture2D (the atlas) and its rows and columns.
        /// </summary>
        /// <param name="texture">The Texture2D containing the Atlas.</param>
        /// <param name="position">The starting position of the game object.</param>
        /// <param name="rows">The number of rows in the passed Atlas.</param>
        /// <param name="columns">The number of columns in the passed Atlas.</param>
        /// <param name="velocity">The starting velocity of the game object.</param>
        /// <param name="color">The color tint for the texture.</param>
        public AnimatedGameObject(Texture2D texture, Vector2 position, int rows, int columns, Vector2 velocity, Color color) : base(texture, position)
        {
            Rows = rows;
            Columns = columns;
            colorTint = color;

            // Set the current frame to 0 (beginning)
            currentFrame = 0;

            // Calculate the total number of available frames in this atlas
            totalFrames = Rows * Columns;
        }


        /// <summary>
        /// This method will simply change the current frame to the next frame. This won't do anything else, so it will be animated, but not moved.
        /// </summary> 
        public override void Update(GameTime gameTime)
        {
            // Increment the current frame
            currentFrame++;

            // Check if we've reached the end of the animation
            if (currentFrame == totalFrames)
                currentFrame = 0;   // We've reached the end, restart from the beginning
        }

        /// <summary>
        /// This method will draw the animation. 
        /// </summary>   
        /// <param name="spriteBatch">This is the SpriteBatch to use for the drawing. Usually it's the Game() one.</param>        
        public override void Draw(SpriteBatch spriteBatch)
        {
            CalculateFrame();

            // Finally, we draw the correct frame in the wanted location            
            spriteBatch.Draw(Texture, destinationRectangle, sourceRectangle, colorTint);                        
        }

        private void CalculateFrame()
        {
            // First, we need to get the size of each individual frame
            int width = Texture.Width / Columns;
            int height = Texture.Height / Rows;

            // Then we need to calculate where is the current frame located inside the Atlas, depending on currentFrame
            int row = (int)((float)currentFrame / (float)Columns);
            int column = currentFrame % Columns;

            // We then create a rectangle to "cut" the current frame using the above calculated values
            sourceRectangle = new Rectangle(width * column, height * row, width, height);

            // We then create a rectangle that will be at the position (location) of where we need to draw it on the screen
            destinationRectangle = new Rectangle((int)Position.X, (int)Position.Y, width, height);
        }
    }
}
