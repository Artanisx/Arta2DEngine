using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Arta2DEngine.Utils
{
    /// <summary>
    /// This is the static class holds some handy helper methods.     
    /// </summary>
    public static class Utils
    {
        /// <summary>
        /// This method will take care of changing the Window Size.
        /// </summary>
        /// <param name="graphicDeviceManager">The Game's Graphic Device Manager.</param>
        /// <param name="width">The prefered Window's Width.</param>
        /// <param name="height">The prefered Window's Height.</param>
        public static void SetWindowSize(GraphicsDeviceManager graphicDeviceManager, int width, int height)
        {
            graphicDeviceManager.PreferredBackBufferWidth = width;  // set this value to the desired width of your window
            graphicDeviceManager.PreferredBackBufferHeight = height;   // set this value to the desired height of your window
            graphicDeviceManager.ApplyChanges();
        }

        /// <summary>
        /// This method will make the game go Full Screen.
        /// </summary>
        /// <param name="graphicDeviceManager">The Game's Graphic Device Manager.</param>        
        /// <param name="fullscreen">Set this to true if you want the game to switch to Full screen mode.</param>  
        public static void SetFullScreen(GraphicsDeviceManager graphicDeviceManager, bool fullscreen)
        {
            graphicDeviceManager.PreferredBackBufferWidth = graphicDeviceManager.GraphicsDevice.DisplayMode.Width;   // Set the Window's width to the monitor's actual screen resolution width
            graphicDeviceManager.PreferredBackBufferHeight = graphicDeviceManager.GraphicsDevice.DisplayMode.Height; // Set the Window's height to the monitor's actual screen resolution height
            graphicDeviceManager.IsFullScreen = fullscreen;
            graphicDeviceManager.ApplyChanges();
        }

        /// <summary>
        /// This method will set the Window's title.
        /// </summary>
        /// <param name="gameWindow">The Game's Window.</param>        
        /// <param name="windowTitle">Set this to the new Window's Title.</param>  
        public static void SetWindowTitle(GameWindow gameWindow, string windowTitle)
        {
            gameWindow.Title = windowTitle;
        }

        /// <summary>
        /// This method will return the current Frame Rate.
        /// </summary>
        /// <param name="gameTime">The Game's GameTime.</param>                
        public static float GetFPS(GameTime gameTime)
        {
            float frameRate;

            frameRate = 1 / (float)gameTime.ElapsedGameTime.TotalSeconds;

            return frameRate;
        }
    }
}
