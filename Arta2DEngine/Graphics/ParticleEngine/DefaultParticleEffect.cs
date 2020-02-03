#region File Description
//----------------------------------------------------------------------------------------------
// DefaultParticleEffect.cs
//
// This class will describe a Default Particle Effect, can used as a template for other effects
// Copyright (C) Artanis. All rights reserved.
//----------------------------------------------------------------------------------------------
#endregion

using Arta2DEngine.Graphics.ParticleEngine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Arta2DEngine.Graphics.ParticleEngine
{
    public class DefaultParticleEffect : ParticleEffect
    {
        private Random random;
        private float startingScale;

        /// <summary>
        /// A Default Particle Effect.                
        /// </summary>
        /// <param name="numberOfParticles">The number of particles that will be fired by this effect.</param>   
        public DefaultParticleEffect(int numberOfParticles = 1, float startingScaleOfParticle = 1.0f)
        {
            // Set the random
            random = new Random();

            // Set the total of particles
            this.numberOfParticles = numberOfParticles;

            // Set the starting scale of particles
            this.startingScale = startingScaleOfParticle;
        }

        /// <summary>
        /// This method will generate particles for a Default Particle Effect.                
        /// </summary>
        /// <param name="particleEngine">The Particle Engine that will use this effect.</param> 
        public override Particle GenerateParticle(ParticleEngine particleEngine)
        {
            // Pick a random texture from the texture list available
            Texture2D texture = particleEngine.Textures[random.Next(particleEngine.Textures.Count)];

            // Set the location of the new particle based upon the emitter location
            Vector2 position = particleEngine.EmitterLocation;

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
            float size = (float)random.NextDouble() * startingScale;

            // Set a random ttl for this particle (minimum 5)
            int ttl = 20 + random.Next(40);

            // Set the total number of particles for this effect
            particleEngine.TotalParticles = numberOfParticles;

            // Return a new particle generated according to the above
            return new Particle(texture, position, velocity, angle, angularVelocity, color, size, ttl);
        }
    }
}
