#region File Description
//-----------------------------------------------------------------------------
// LoadingScreen.cs
//
// This class will describe a LoadingScreen, used to load other screens and 
// if needed, to show a "Loading..." message
// Copyright (C) Artanis. All rights reserved.
//-----------------------------------------------------------------------------
#endregion

using System;
using Microsoft.Xna.Framework;

namespace Arta2DEngine.Utils.SceneManager
{
    public class LoadingScreen : GameScreen
    {
        // Message that will be shown in the consolle during loading
        const string message = "Loading...";

        bool loadingIsSlow;

        GameScreen[] screensToLoad;

        #region Initialization

        /// <summary>
        /// The constructor is private: loading screens should
        /// be activated via the static Load method instead.
        /// </summary>
        private LoadingScreen(ScreenManager screenManager, bool loadingIsSlow, GameScreen[] screensToLoad)
        {
            this.loadingIsSlow = loadingIsSlow;
            this.screensToLoad = screensToLoad;
        }

        /// <summary>
        /// Activates the loading screen.
        /// </summary>
        public static void Load(ScreenManager screenManager, bool loadingIsSlow, params GameScreen[] screensToLoad)
        {
            // Tell all the current screens to go off.
            foreach (GameScreen screen in screenManager.GetScreens())
                screen.ExitScreen();

            // Create and activate the loading screen.
            LoadingScreen loadingScreen = new LoadingScreen(screenManager, loadingIsSlow, screensToLoad);

            screenManager.AddScreen(loadingScreen);
        }

        #endregion

        #region Update and Draw

        /// <summary>
        /// Updates the loading screen.
        /// </summary>
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            // First, remove this screen from the manager, as it's going to compelte it's task soon
            ScreenManager.RemoveScreen(this);

            foreach (GameScreen screen in screensToLoad)
            {
                if (screen != null)
                {
                    ScreenManager.AddScreen(screen);
                }
            }

            // Once the load has finished, we use ResetElapsedTime to tell
            // the  game timing mechanism that we have just finished a very
            // long frame, and that it should not try to catch up.
            ScreenManager.Game.ResetElapsedTime();
        }

        /// <summary>
        /// Draws the loading screen.
        /// </summary>
        public override void Draw(GameTime gameTime)
        {
            // The gameplay screen takes a while to load, so we should display a loading
            // message while that is going on, but the menus load very quickly, and
            // it would look silly if we flashed this up for just a fraction of a
            // second while returning from the game to the menus. This parameter
            // tells us how long the loading is going to take, so we know whether
            // to bother drawing the message.
            // We will simply put out a console message instead.
            if (loadingIsSlow)
            {
                Console.Out.WriteLine(message);
            }
        }

        #endregion


    }
}
