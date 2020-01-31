using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Arta2DEngine.Graphics.ParticleEngine
{
    /// <summary>
    /// This is the base class for a Particle. To be used with the Particle Engine.     
    /// </summary>
    public class Particle
    {
        public Texture2D Texture { get; set; }        // The texture that will be drawn to represent the particle
        public Vector2 Position { get; set; }        // The current position of the particle        
        public Vector2 Velocity { get; set; }        // The speed of the particle at the current instance
        public float Angle { get; set; }            // The current angle of rotation of the particle (spinning)
        public float AngularVelocity { get; set; }    // The speed that the angle is changing
        public Color Color { get; set; }            // The color of the particle
        public float Size { get; set; }                // The size of the particle
        public int TTL { get; set; }                // The 'time to live' of the particle. Essentially, this variable will count down to zero, at which point, the particle is dead.

        /// <summary>
        /// This is the Particle class constructor. It takes all the arguments to create a particle.
        /// </summary>
        /// <param name="texture">The texture that will be drawn to represent the particle.</param>
        /// <param name="position">The current position of the particle.</param>
        /// <param name="velocity">The speed of the particle at the current instance.</param>
        /// <param name="angle">The current angle of rotation of the particle (spinning).</param>
        /// <param name="angularVelocity">The speed that the angle is changing.</param>
        /// <param name="color">The color of the particle.</param>
        /// <param name="size">The size of the particle.</param>
        /// <param name="ttl">The 'time to live' of the particle. Essentially, this variable will count down to zero, at which point, the particle is dead.</param>
        public Particle(Texture2D texture, Vector2 position, Vector2 velocity, float angle, float angularVelocity, Color color, float size, int ttl)
        {
            Texture = texture;
            Position = position;
            Velocity = velocity;
            Angle = angle;
            AngularVelocity = angularVelocity;
            Color = color;
            Size = size;
            TTL = ttl;
        }

        /// <summary>
        /// First, it updates both the position and angle by the velocity and angular velocities respectively. 
        /// Additionally, it decrements the TTL (time to live) variable to move it one step closer to expiring.
        /// </summary>        
        public void Update()
        {
            TTL--;
            Position += Velocity;
            Angle += AngularVelocity;
        }

        /// <summary>
        /// This method will draw the particle. 
        /// </summary>   
        /// <param name="spriteBatch">This is the SpriteBatch to use for the drawing. Usually it's the Game() one.</param>        
        public void Draw(SpriteBatch spriteBatch)
        {
            // Create a rectangle from the texture of the particle (which we want to cover the entire texture) 
            Rectangle sourceRectangle = new Rectangle(0, 0, Texture.Width, Texture.Height);

            // Define its origin point for the rotation (center of the texture)
            Vector2 origin = new Vector2(Texture.Width / 2, Texture.Height / 2);

            // Draw the particle at position, color, angle, origin and size
            // Notice that we don't call spriteBatch.Begin() or spriteBatch.End() here, because we are going to be drawing a lot of these all at one time.
            spriteBatch.Draw(Texture, Position, sourceRectangle, Color, Angle, origin, Size, SpriteEffects.None, 0f);
        }
    }
}
