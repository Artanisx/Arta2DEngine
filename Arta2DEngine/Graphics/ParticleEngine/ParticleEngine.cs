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
        private int totalParticles = 2;
        private bool oneshot;
        private bool particlesGenerated = false;

        // The effect to be used
        private ParticleEffect particleEffect;

        // To access the number of total particles fired by this Engine. Accessed by the ParticleEffect.
        public int TotalParticles { get => totalParticles; set => totalParticles = value; }

        // To access the Textures. Accessed by the ParticleEffect.
        public List<Texture2D> Textures { get => textures; set => textures = value; }

        /// <summary>
        /// We want the user to tell us what textures we can use, as well as the default location of the emitter,
        /// but the list of particles is for the particle engine to manage, and the user should not have to worry about them. 
        /// Also, random is given a default new random number generator.
        /// </summary>
        /// <param name="textures">The textures we can use.</param>
        /// <param name="location">The default location of the emitter.</param>
        /// <param name="particleEffect">The Particle Effect to be used by this Engine.</param>
        /// <param name="oneshotEffect">Sets if this effect should be fired continuously (false) or only once (true). Default is false</param>
        public ParticleEngine(List<Texture2D> textures, Vector2 location, ParticleEffect particleEffect, bool oneshotEffect = false)
        {
            EmitterLocation = location;
            this.textures = textures;

            // Create a new list of particles
            this.particles = new List<Particle>();

            // Create a new RNG
            random = new Random();

            // Set the particle effect to be used
            this.particleEffect = particleEffect;

            // Set this as oneshot effect or not
            this.oneshot = oneshotEffect;
        }

        /// <summary>
        /// One of the important things this class will need to be able to do is to create particles with parameters
        /// that make the engine behave like you want it. This method will call the ParticleEffect that was passed with the constructor.
        /// </summary>
        private Particle GenerateNewParticle()
        {
            return particleEffect.GenerateParticle(this);            
        }

        /// <summary>
        /// This adds new particles as needed, allow each of the particles to update themselves, and remove dead particles.        
        /// </summary>        
        public void Update()
        {   
            if ( (oneshot == true) && (particlesGenerated == false) )
            {
                // Generate each particle up until the total number above
                for (int i = 0; i < totalParticles; i++)
                {
                    particles.Add(GenerateNewParticle());
                }

                // Fired once, so we're done
                particlesGenerated = true;
            }
            else if ( (oneshot == false) )
            {
                // Generate each particle up until the total number above
                for (int i = 0; i < totalParticles; i++)
                {
                    particles.Add(GenerateNewParticle());
                }
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
