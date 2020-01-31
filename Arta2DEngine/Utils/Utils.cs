using Arta2DEngine.Graphics;
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

        /// <summary>
        /// This method will return a Vector2 calculated as the Center position of the window of a given object. Used to position an object in the center of the window.
        /// This assumes a game object Origin of 0,0.
        /// </summary>
        /// <param name="graphicsDevice">The Game's GraphicsDevice (ScreenManager.Game.GraphicsDevice) if used with Screen Manager.</param>     
        /// <param name="object">The Game object we need to place. Needed to calculate its width.</param>     
        /// <param name="xOffset">The number of pixel of offset from the Top Middle position in the X axis.</param>     
        /// <param name="yOffset">The number of pixel of offset from the Top Middle position in the Y axis.</param>    
        public static Vector2 GetCenterPosition(GraphicsDevice graphicsDevice, BaseObject baseObject, int xOffset = 0, int yOffset = 0)
        {
            return new Vector2(graphicsDevice.Viewport.Width / 2 - baseObject.Texture.Width / 2 + xOffset, graphicsDevice.Viewport.Height / 2 - baseObject.Texture.Height / 2 + yOffset);
        }

        /// <summary>
        /// This method will return a Vector2 calculated as the Top Center position of the window of a given object. Used to position an object in the top center of the window.
        /// This assumes a game object Origin of 0,0.
        /// </summary>
        /// <param name="graphicsDevice">The Game's GraphicsDevice (ScreenManager.Game.GraphicsDevice) if used with Screen Manager.</param>     
        /// <param name="object">The Game object we need to place. Needed to calculate its width.</param>     
        /// <param name="xOffset">The number of pixel of offset from the Top Middle position in the X axis.</param>     
        /// <param name="yOffset">The number of pixel of offset from the Top Middle position in the Y axis.</param>    
        public static Vector2 GetTopCenterPosition(GraphicsDevice graphicsDevice, BaseObject baseObject, int xOffset = 0, int yOffset = 0)
        {
            return new Vector2(graphicsDevice.Viewport.Width / 2 - baseObject.Texture.Width / 2 + xOffset, baseObject.Texture.Height / 2 + yOffset);
        }

        /// <summary>
        /// This method will return a Vector2 calculated as the Bottom Center position of the window of a given object. Used to position an object in the bottom center of the window.
        /// This assumes a game object Origin of 0,0.
        /// </summary>
        /// <param name="graphicsDevice">The Game's GraphicsDevice (ScreenManager.Game.GraphicsDevice) if used with Screen Manager.</param>     
        /// <param name="object">The Game object we need to place. Needed to calculate its width.</param>     
        /// <param name="xOffset">The number of pixel of offset from the Top Middle position in the X axis.</param>     
        /// <param name="yOffset">The number of pixel of offset from the Top Middle position in the Y axis.</param>    
        public static Vector2 GetBottomCenterPosition(GraphicsDevice graphicsDevice, BaseObject baseObject, int xOffset = 0, int yOffset = 0)
        {
            return new Vector2(graphicsDevice.Viewport.Width / 2 - baseObject.Texture.Width / 2 + xOffset, graphicsDevice.Viewport.Height - baseObject.Texture.Height / 2 + yOffset);
        }

        /// <summary>
        /// This method will return a Vector2 calculated as the Bottom Left position of the window of a given object. Used to position an object in the bottom Left of the window.
        /// This assumes a game object Origin of 0,0.
        /// </summary>
        /// <param name="graphicsDevice">The Game's GraphicsDevice (ScreenManager.Game.GraphicsDevice) if used with Screen Manager.</param>     
        /// <param name="object">The Game object we need to place. Needed to calculate its width.</param>     
        /// <param name="xOffset">The number of pixel of offset from the Top Middle position in the X axis.</param>     
        /// <param name="yOffset">The number of pixel of offset from the Top Middle position in the Y axis.</param>    
        public static Vector2 GetBottomLeftPosition(GraphicsDevice graphicsDevice, BaseObject baseObject, int xOffset = 0, int yOffset = 0)
        {
            return new Vector2(0 + xOffset, graphicsDevice.Viewport.Height - baseObject.Texture.Height + yOffset);
        }

        /// <summary>
        /// This method will return a Vector2 calculated as the Bottom Right position of the window of a given object. Used to position an object in the bottom Right of the window.
        /// This assumes a game object Origin of 0,0.
        /// </summary>
        /// <param name="graphicsDevice">The Game's GraphicsDevice (ScreenManager.Game.GraphicsDevice) if used with Screen Manager.</param>     
        /// <param name="object">The Game object we need to place. Needed to calculate its width.</param>     
        /// <param name="xOffset">The number of pixel of offset from the Top Middle position in the X axis.</param>     
        /// <param name="yOffset">The number of pixel of offset from the Top Middle position in the Y axis.</param>    
        public static Vector2 GetBottomRightPosition(GraphicsDevice graphicsDevice, BaseObject baseObject, int xOffset = 0, int yOffset = 0)
        {
            return new Vector2(graphicsDevice.Viewport.Width - baseObject.Texture.Width + xOffset, graphicsDevice.Viewport.Height - baseObject.Texture.Height + yOffset);
        }       

        /// <summary>
        /// This method will return a Vector2 calculated as the Top Right position of the window of a given object. Used to position an object in the top right of the window.
        /// This assumes a game object Origin of 0,0.
        /// </summary>
        /// <param name="graphicsDevice">The Game's GraphicsDevice (ScreenManager.Game.GraphicsDevice) if used with Screen Manager.</param>     
        /// <param name="object">The Game object we need to place. Needed to calculate its width.</param>     
        /// <param name="xOffset">The number of pixel of offset from the Top Middle position in the X axis.</param>     
        /// <param name="yOffset">The number of pixel of offset from the Top Middle position in the Y axis.</param>    
        public static Vector2 GetTopRightPosition(GraphicsDevice graphicsDevice, BaseObject baseObject, int xOffset = 0, int yOffset = 0)
        {
            return new Vector2(graphicsDevice.Viewport.Width - baseObject.Texture.Width + xOffset, 0 + yOffset);
        }
    }
}
