using Microsoft.Xna.Framework.Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arta2DEngine.Audio
{
    public class SFX
    {
        #region Fields

        private SoundEffect soundEffect;

        private SoundEffectInstance soundEffectInstance;

        private string soundName;

        private float volume;

        private float pitch;

        private float pan;

        #endregion

        #region Methods
               
        // <summary>
        /// This method create a SFX object, but sets some additional parameters
        /// </summary>
        /// <param name="effect">The SoundEffect to create. You can pass directly a Content.Load<SoundEffect> call.</param>
        /// <param name="volume">The volume for this sound effect. Default value is 1.0f (max volume)</param>
        /// <param name="pitch">The pitch for this sound effect. Default value is 0.0f (no pitch)</param>
        /// <param name="pan">The pan for this sound effect. Default value is 0.0f (balanced)</param>
        public SFX(SoundEffect effect, float volume = 1.0f, float pitch = 0.0f, float pan = 0.0f)
        {
            this.soundEffect = effect;

            // Set its name
            this.soundName = effect.Name;

            // Set the volume, pitch and pan
            this.volume = volume;
            this.pitch = pitch;
            this.pan = pan;

            // Create the related SoundEffectInstance
            this.soundEffectInstance = soundEffect.CreateInstance();

            // Set its values
            this.soundEffectInstance.Volume = this.volume;
            this.soundEffectInstance.Pitch = this.pitch;
            this.soundEffectInstance.Pan = this.pan;
        }

        // <summary>
        /// This method plays the sound directly, without any further access. Good for repeated sound effects like bullet that can overlap.
        /// </summary>        
        public void SimplePlay()
        {
            // Plays the sound.
            soundEffect.Play(this.volume, this.pitch, this.pan);
        }

        // <summary>
        /// This method plays the sound, but it can be later paused, stopped and resumed.
        /// </summary>
        /// <param name="loopPlay">If set to true, this sound will play and loop indefinitely.</param>
        public void Play(bool loopPlay = false)
        {
            // Checks if the sound paused, in which case simply resume it
            if (soundEffectInstance.State == SoundState.Paused)
            {
                soundEffectInstance.Resume();
                return;
            }

            if (soundEffectInstance.State == SoundState.Playing)
                return; // If the sound is already playing, ignore this.

            // This sound was not paused, so we must set it and play for the first time.

            // Set looping
            soundEffectInstance.IsLooped = loopPlay;            

            // Plays the sound.
            soundEffectInstance.Play();
        }

        // <summary>
        /// This method pauses the sound, it can later resumed calling Play() again.
        /// </summary>        
        public void Pause()
        {
            if (soundEffectInstance.State == SoundState.Playing)                
                soundEffectInstance.Pause(); // Pauses the sound.
        }

        // <summary>
        /// This method stops the sound for good..
        /// </summary>        
        public void Stop()
        {
            if (soundEffectInstance.State == SoundState.Playing)
                soundEffectInstance.Stop(); // Stops the sound.
        }

        // <summary>
        /// This method changes the volume, pitch and pan of the already created sound.
        /// </summary> 
        /// <param name="volume">The volume for this sound effect. Default value is 1.0f (max volume)</param>
        /// <param name="pitch">The pitch for this sound effect. Default value is 0.0f (no pitch)</param>
        /// <param name="pan">The pan for this sound effect. Default value is 0.0f (balanced)</param>
        public void Set(float volume = 1.0f, float pitch = 0.0f, float pan = 0.0f)
        {
            // Set the values
            this.volume = volume;
            this.pitch = pitch;
            this.pan = pan;

            // Apply the values to the soundeffectinstance
            this.soundEffectInstance.Volume = this.volume;
            this.soundEffectInstance.Pitch = this.pitch;
            this.soundEffectInstance.Pan = this.pan;
        }

        // <summary>
        /// This returns the sound name.
        /// </summary>         
        public string GetName()
        {
            return this.soundName;
        }

        // <summary>
        /// This sets the sound name. It overrides the names set on creation (but doesn't change anything of the loaded info from content.mgcb).
        /// </summary>         
        public void SetName(string newName)
        {
            this.soundName = newName;
        }

        #endregion

    }
}
