using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Arta2DEngine.Graphics.ParticleEngine
{
    /// <summary>
    /// This is the ParticleEngine. IT will take care of generating particles at an emitter location, update them and drawing them.     
    /// </summary>
    public class ParticleEngine
    {
        private Random random;
        public Vector2 EmitterLocation { get; set; }
        private List<Particle> particles;
        private List<Texture2D> textures;

        /// <summary>
        /// We want the user to tell us what textures we can use, as well as the default location of the emitter,
        /// but the list of particles is for the particle engine to manage, and the user should not have to worry about them. 
        /// Also, random is given a default new random number generator.
        /// </summary>
        /// <param name="textures">The textures we can use.</param>
        /// <param name="location">The default location of the emitter.</param>
        public ParticleEngine(List<Texture2D> textures, Vector2 location)
        {
            EmitterLocation = location;
            this.textures = textures;

            // Create a new list of particles
            this.particles = new List<Particle>();

            // Create a new RNG
            random = new Random();
        }

        /// <summary>
        /// One of the important things this class will need to be able to do is to create particles with parameters
        /// that make the engine behave like you want it. We are going to create a default method for generating particles below.
        /// </summary>
        private Particle GenerateNewParticle()
        {
            // Pick a random texture from the texture list available
            Texture2D texture = textures[random.Next(textures.Count)];

            // Set the location of the new particle based upon the emitter location
            Vector2 position = EmitterLocation;

            // Set the velocity for the new particle using random generated values
            Vector2 velocity = new Vector2(
                    1f * (float)(random.NextDouble() * 2 - 1),
                    1f * (float)(random.NextDouble() * 2 - 1));

            float angle = 0;

            // Set a random angularvelocity (spinning)
            float angularVelocity = 0.1f * (float)(random.NextDouble() * 2 - 1);

            // Set a random color for this particle
            Color color = new Color(
                    (float)random.NextDouble(),
                    (float)random.NextDouble(),
                    (float)random.NextDouble());

            // Set a random size for this particle
            float size = (float)random.NextDouble();

            // Set a random ttl for this particle (minimum 5)
            int ttl = 20 + random.Next(40);

            // Return a new particle generated according to the above
            return new Particle(texture, position, velocity, angle, angularVelocity, color, size, ttl);
        }

        /// <summary>
        /// This adds new particles as needed, allow each of the particles to update themselves, and remove dead particles.        
        /// </summary>        
        public void Update()
        {
            // Number of maximum total particles to have at a time
            int total = 2;

            // Generate each particle up until the total number above
            for (int i = 0; i < total; i++)
            {
                particles.Add(GenerateNewParticle());
            }

            // Loop over each of the created particle
            for (int particle = 0; particle < particles.Count; particle++)
            {
                // Now let's update each single one of them
                particles[particle].Update();

                // Check if it's time to remove a dead particle 
                if (particles[particle].TTL <= 0)
                {
                    particles.RemoveAt(particle);
                    particle--;
                }
            }
        }

        /// <summary>
        /// This method will ask each particle to draw themselves. It must not be called inside another SpriteBatch.Begin/End cycle.
        /// </summary>   
        /// <param name="spriteBatch">This is the SpriteBatch to use for the drawing. Usually it's the Game() one.</param>     
        public void Draw(SpriteBatch spriteBatch)
        {
            //spriteBatch.Begin();
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Additive);
            for (int index = 0; index < particles.Count; index++)
            {
                particles[index].Draw(spriteBatch);
            }
            spriteBatch.End();
        }
    }
}
