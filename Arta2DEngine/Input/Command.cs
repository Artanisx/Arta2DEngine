using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace Arta2DEngine.Input
{
    public class Command
    {
        #region Fields

        // To hold the previous Keyboard State
        private KeyboardState oldKeyboardState;

        // To hold the current Keyboard State
        private KeyboardState newKeyboardState;

        // To hold the previous Mouse State
        private MouseState oldMouseState;

        // To hold the current Mouse State
        private MouseState newMouseState;

        // To hold the Key for this event
        private Keys key;

        // To hold the mouse ButtonState for this event
        private ButtonState mouseButton;

        // A bool to set this as a Keyboard or Mouse command
        private bool KeyboardCommand;

        // Event for SinglePress (this event is called only once per action)
        public event EventHandler SinglePress;

        // Event for ContinuedPress (this event is called multiple times per action)
        public event EventHandler ContinuedPress;

        #endregion

        #region Methods

        // Constructor for a Keyboard command
        public Command(Keys key)
        {
            this.key = key;

            KeyboardCommand = true;
        }

        // Constructor for a Mouse command
        public Command(ButtonState mouseButton)
        {
            this.mouseButton = mouseButton;

            KeyboardCommand = false;
        }

        public void Update(GameTime gameTime)
        {
            if (KeyboardCommand)
            {
                // This is a Keyboard command

                // Store the current state of the Keyboard
                newKeyboardState = Keyboard.GetState();

                // SINGLEPRESS

                // Check if the key for this command has been just pressed (to avoid multiple firing)
                if (oldKeyboardState.IsKeyUp(this.key) && newKeyboardState.IsKeyDown(this.key))
                {
                    // Invoke the event

                    // If the SinglePress event handler is not null, invoke it -- equivalent would be if(SinglePress != null) > SinglePress(this, new EventArgs())
                    SinglePress?.Invoke(this, new EventArgs());
                }

                // CONTINUED PRESS

                // Simply check if the key for this command has been pressed (it will fire each frame)
                if (newKeyboardState.IsKeyDown(this.key))
                {
                    // Invoke the event

                    // If the ContinuedPress event handler is not null, invoke it -- equivalent would be if(ContinuedPress != null) > ContinuedPress(this, new EventArgs())
                    ContinuedPress?.Invoke(this, new EventArgs());
                }

                oldKeyboardState = newKeyboardState;  // Set the new state as the old state for next frame  
            }
            else if (!KeyboardCommand)
            {
                // This is a Mouse command
            }
        }

        #endregion
    }
}
