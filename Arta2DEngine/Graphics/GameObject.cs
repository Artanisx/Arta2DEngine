using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Arta2DEngine
{
    /// <summary>
    /// This is the main component of each single object inside the game. Currently it's very basic and only contains a Texture2D, a Vector2D for its position and velocity.
    /// </summary>
    public class GameObject
    {
        // Each Gameobject has a texture
        public Texture2D Texture { get; set; }        

        // A position
        public Vector2 Position { get; set; }

        // And a velocity
        public Vector2 Velocity { get; set; }

        /// <summary>
        /// This is going to be the box around the objec tot be used for collisions.
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
        /// This is going to be a circle around the objec tot be used for collisions.
        /// </summary>        
        //public Circle BoundingCircle
        //{
        //    get
        //    {
        //        // Creates a circle around the object and return it
        //        Vector2 center;
        //        center = new Vector2(Position.X + Texture.Width / 2, Position.Y + Texture.Height / 2);

        //        float radius;
        //        radius = Texture.Width / 2;

        //        return new Circle(center, radius);
        //    }
        //}

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

        // <summary>
        /// This simply draws the gameobject. It must be called between a spriteBatch.Begin and spriteBatch.End
        /// </summary>
        /// <param name="spriteBatch">The spriteBatch from Game().</param
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, Color.White);
        }
    }
}
