using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arta2DEngine.Audio
{
    public class BGM
    {
        #region Fields

        private Song song;

        private float volume;

        private string songName;

        #endregion

        // <summary>
        /// This method create a BGM object.
        /// </summary>
        /// <param name="song">The Song to create. You can pass directly a Content.Load<Song> call.</param>   
        /// <param name="volume">The volume for this song  effect. Default value is 1.0f (max volume)</param>
        public BGM(Song song, float volume = 1.0f)
        {
            this.song = song;
            this.volume = volume;
            this.songName = song.Name;            
        }

        // <summary>
        /// This method plays the song.
        /// </summary>
        /// <param name="loopPlay">If set to true, this song will play and loop indefinitely.</param>
        public void Play(bool loopPlay = false)
        {
            // Play the song unless it's already playing or paused
            if (MediaPlayer.State != MediaState.Playing && MediaPlayer.State != MediaState.Paused)
            {
                MediaPlayer.Volume = this.volume;
                MediaPlayer.IsRepeating = loopPlay;
                MediaPlayer.Play(this.song);
            }
            else if (MediaPlayer.State == MediaState.Paused)    // If the song is paused, then resume it
            {
                MediaPlayer.Resume();
            }       
            else if (MediaPlayer.State == MediaState.Playing)   // A song is already playing. Asking to play again means stopping this song and play another.
            {
                MediaPlayer.Stop();

                MediaPlayer.Volume = this.volume;
                MediaPlayer.IsRepeating = loopPlay;
                MediaPlayer.Play(this.song);
            }
        }

        // <summary>
        /// This method stops the song.
        /// </summary>        
        public void Stop()
        {
            // Stop the song only if it's playing or paused
            if (MediaPlayer.State == MediaState.Playing || MediaPlayer.State == MediaState.Paused)
            {
                MediaPlayer.Stop();
            }
        }

        // <summary>
        /// This method pauses the song.
        /// </summary>        
        public void Pause()
        {
            // Stop the song only if it's playing
            if (MediaPlayer.State == MediaState.Playing)
            {
                MediaPlayer.Pause();
            }            
        }

        // <summary>
        /// This returns the song name.
        /// </summary>         
        public string GetName()
        {
            return this.songName;
        }

        // <summary>
        /// This sets the song name. It overrides the names set on creation (but doesn't change anything of the loaded info from content.mgcb).
        /// </summary>         
        public void SetName(string newName)
        {
            this.songName = newName;
        }
    }
}
