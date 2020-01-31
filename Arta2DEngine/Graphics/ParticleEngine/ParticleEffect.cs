#region File Description
//----------------------------------------------------------------------------------
// ParticleEffect.cs
//
// This class will describe a Particle Effect, used by the Particle Engine to hande
// Particle effect.
// Copyright (C) Artanis. All rights reserved.
//----------------------------------------------------------------------------------
#endregion

using Microsoft.Xna.Framework;

namespace Arta2DEngine.Graphics.ParticleEngine
{       
    public abstract class ParticleEffect
    {
        // Number of Particles for this Effect
        protected int numberOfParticles;

        /// <summary>
        /// A Particle Effect needs to implement a GenerateParticle() method.                
        /// </summary>
        /// <param name="particleEngine">The Particle Engine that will use this. Needed for EmitterLocation and access to totalParticles field.</param>      
        public abstract Particle GenerateParticle(ParticleEngine particleEngine);
    }
}
