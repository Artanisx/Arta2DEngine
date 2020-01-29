#region File Description
//-----------------------------------------------------------------------------
// GameScreen.cs
//
// This class will describe a GameScreen, used by the Screen Manager to hande
// Different game screens like Menu, Gameplay, Game Over...
// Copyright (C) Artanis. All rights reserved.
//-----------------------------------------------------------------------------
#endregion

using Microsoft.Xna.Framework;

namespace Arta2DEngine.Utils.SceneManager
{
    public abstract class GameScreen
    {
        #region Fields    

        // The screenmanager that handle this screen
        ScreenManager screenManager;

        #endregion

        #region Properties

        /// <summary>
        /// Gets the manager that this screen belongs to.
        /// </summary>
        public ScreenManager ScreenManager
        {
            get { return screenManager; }
            internal set { screenManager = value; }
        }

        #endregion

        #region Initialization Methods

        /// <summary>
        /// Load graphics content for the screen.
        /// </summary>
        public virtual void LoadContent() { }


        /// <summary>
        /// Unload content for the screen.
        /// </summary>
        public virtual void UnloadContent() { }

        #endregion

        #region Updated and Draw Methods

        /// <summary>
        /// Allows the screen to run logic, such as updating the transition position.
        /// </summary>
        public virtual void Update(GameTime gameTime) { }

        /// <summary>
        /// This is called when the screen should draw itself.
        /// </summary>
        public virtual void Draw(GameTime gameTime) { }

        /// <summary>
        /// This is called to handle all inputs.
        /// </summary>
        public virtual void HandleInput(GameTime gameTime) { }

        #endregion

        #region Public Methods

        /// <summary>
        /// Tells the screen to go away. 
        /// </summary>
        public void ExitScreen()
        {
            // If the screen has a zero transition time, remove it immediately.
            ScreenManager.RemoveScreen(this);
        }

        #endregion
    }
}
