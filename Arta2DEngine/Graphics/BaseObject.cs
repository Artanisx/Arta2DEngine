using Arta2DEngine.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Arta2DEngine.Graphics
{
    /// <summary>
    /// This is the base class for each Game Object. 
    /// This cannot be instanced, but has to be inherited and its functions needs to be implemented or overriden.
    /// </summary>
    public abstract class BaseObject
    {
        // Each BaseObject has a texture
        public Texture2D Texture { get; set; }

        // Each BaseObject has a position
        public Vector2 Position { get; set; }

        /// <summary>
        /// This is going to be the box around the object to be used for collisions.
        /// </summary>        
        public Rectangle BoundingBox
        {
            get
            {
                // Creates a rectangle around the object and return it
                return new Rectangle((int)Position.X, (int)Position.Y, Texture.Width, Texture.Height);
            }
        }

        /// <summary>
        /// This is going to be the circle around the object to be used for collisions.
        /// </summary>        
        public Circle BoundingCircle
        {
            get
            {
                // Creates a circle around the object and return it

                // Define its center
                Vector2 centerOfObject = new Vector2((int)Position.X + Texture.Width / 2, (int)Position.Y + Texture.Height / 2);

                // Define its radius (half its side)
                float radiusOfSprite = Texture.Width / 2;

                Circle circle = new Circle(centerOfObject, radiusOfSprite);

                return circle;
            }
        }

        // <summary>
        /// This method draws the object. It must be called between a spriteBatch.Begin and spriteBatch.End
        /// It can be overridden if needed.
        /// </summary>
        /// <param name="spriteBatch">The spriteBatch from Game().</param>
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, Color.White);
        }
    }
}
