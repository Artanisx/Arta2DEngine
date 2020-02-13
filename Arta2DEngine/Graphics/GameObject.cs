using Arta2DEngine.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
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
        /// First constructor. We pass a Texture and a Position to create a GameObject.
        /// Origin is defaulted to center of the texture.
        /// </summary>
        /// <param name="texture">The texture for the game object.</param>
        /// <param name="position">The starting position of the game object.</param>
        public GameObject(Texture2D texture, Vector2 position)
        {
            this.Texture = texture;
            this.Position = position;
            this.Origin = new Vector2(Texture.Bounds.Width / 2, Texture.Bounds.Height / 2);
        }

        /// <summary>
        /// This constructor will create a GameObject passing directly the path to its texture.
        /// It will save time as the constructor can take care of both initialization and load.
        /// Origin is defaulted to center of the texture.
        /// </summary>
        /// <param name="texture">The texture for the game object.</param>
        /// <param name="position">The starting position of the game object.</param>
        /// <param name="content">The game's content manager. Used to load the texture from path.</param>
        public GameObject(string texturePath, Vector2 position, ContentManager content)
        {
            // Load the texture directly from the passed path
            if (texturePath != null & texturePath.Length > 0)
                this.Texture = content.Load<Texture2D>(texturePath);
            
            this.Position = position;
            this.Origin = new Vector2(Texture.Bounds.Width / 2, Texture.Bounds.Height / 2);
        }

        /// <summary>
        /// Second constructor. We pass a Texture, a Position and a Velocity to create a GameObject
        /// Origin is defaulted to center of the texture.
        /// </summary>
        /// <param name="texture">The texture for the game object.</param>
        /// <param name="position">The starting position of the game object.</param>
        /// <param name="velocity">The starting velocity of the game object.</param>
        public GameObject(Texture2D texture, Vector2 position, Vector2 velocity)
        {
            this.Texture = texture;
            this.Position = position;
            this.Velocity = velocity;
            this.Origin = new Vector2(Texture.Bounds.Width / 2, Texture.Bounds.Height / 2);
        }       
    }
}
