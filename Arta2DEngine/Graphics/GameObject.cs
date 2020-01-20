using Arta2DEngine.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Arta2DEngine.Graphics
{
    /// <summary>
    /// This is the main component of each single object inside the game. Implements the BaseObject class.    
    /// </summary>
    public class GameObject : BaseObject
    {
        // A GameObject also has a velocity
        public Vector2 Velocity { get; set; }

        /// <summary>
        /// This is going to be the box around the object to be used for collisions.
        /// </summary>        
        public Rectangle BoundingBox
        {
            get
            {
                // Creates a rectangle around the object and return it
                return new Rectangle((int) Position.X, (int) Position.Y, Texture.Width, Texture.Height);

            }
        }

        /// <summary>
        /// First constructor. We pass a Texture and a Position to create a GameObject
        /// </summary>
        /// <param name="textures">The textures for the game object.</param>
        /// <param name="position">The starting position of the game object.</param>
        public GameObject(Texture2D texture, Vector2 position)
        {
            this.Texture = texture;
            this.Position = position;            
        }

        /// <summary>
        /// Second constructor. We pass a Texture, a Position and a Velocity to create a GameObject
        /// </summary>
        /// <param name="textures">The textures for the game object.</param>
        /// <param name="position">The starting position of the game object.</param>
        /// <param name="velocity">The starting velocity of the game object.</param>
        public GameObject(Texture2D texture, Vector2 position, Vector2 velocity)
        {
            this.Texture = texture;
            this.Position = position;
            this.Velocity = velocity;
        }       
    }
}
