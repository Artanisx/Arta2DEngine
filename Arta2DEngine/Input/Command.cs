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

        // To hold the previous Gamepad State
        private GamePadState oldGamepadState;

        // To hold the current Mouse State
        private GamePadState newGamepadState;

        // To hold the Key for this event
        private Keys key;

        // A bool to set this as a Keyboard
        private bool KeyboardCommand;

        // A bool to set this as a Mouse command
        private bool MouseCommand;

        // A bool to set this as a Gamepad command
        private bool GamepadCommand;

        // Enum for Mouse Button States
        public enum MouseButton { LeftButton, MiddleButton, RightButton };
        
        // To hold the mouse button for this event
        private MouseButton mouseButton;

        // Enum for Gamepad Button States
        public enum GamepadButton { A, B, Back, BigButton, LeftShoulder, LeftStick, RightShoulder, RightStick, Start, X, Y, DPadDown, DPadLeft, DPadRight, DPadUp};

        // To hold the Gamepad Button for this event
        private GamepadButton gamepadButton;

        // Event for SinglePress (this event is called only once per action)
        public event EventHandler SinglePress;

        // Event for ContinuedPress (this event is called multiple times per action)
        public event EventHandler ContinuedPress;

        #endregion       

        #region Methods

        // TODO: Consider the possibility to actually create subclasses with Command : Keyboard and Command : Mouse rather than this hybrid solution.
        // Constructor for a Keyboard command
        public Command(Keys key)
        {
            this.key = key;

            KeyboardCommand = true;
            MouseCommand = false;
            GamepadCommand = false;
        }

        // Constructor for a Mouse command
        public Command(MouseButton mouseButton)
        {
            this.mouseButton = mouseButton;

            KeyboardCommand = false;
            MouseCommand = true;
            GamepadCommand = false;
        }

        // Constructor for a Gamepad command
        public Command(GamepadButton gamepadButton)
        {
            this.gamepadButton = gamepadButton;

            KeyboardCommand = false;
            MouseCommand = false;
            GamepadCommand = true;
        }

        public void Update()
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

                // TODO: It might be prudent, optimization wise, to check for SinglePress OR ContinuedPress, knowing if there's a defined event or not.
            }
            
            if (MouseCommand)
            {
                // This is a Mouse command

                // Store the current state of the Mouse
                newMouseState = Mouse.GetState();

                // SINGLEPRESS

                switch (this.mouseButton)
                {
                    case MouseButton.LeftButton:
                        // Check if Left mouse button has been just pressed (to avoid multiple firing)
                        if (newMouseState.LeftButton == ButtonState.Pressed && oldMouseState.LeftButton == ButtonState.Released )
                        {
                            // Invoke the event

                            // If the SinglePress event handler is not null, invoke it -- equivalent would be if(SinglePress != null) > SinglePress(this, new EventArgs())
                            SinglePress?.Invoke(this, new EventArgs());
                        }
                        break;
                    case MouseButton.MiddleButton:
                        // Check if Middle mouse button has been just pressed (to avoid multiple firing)
                        if (newMouseState.MiddleButton == ButtonState.Pressed && oldMouseState.MiddleButton == ButtonState.Released)
                        {
                            // Invoke the event

                            // If the SinglePress event handler is not null, invoke it -- equivalent would be if(SinglePress != null) > SinglePress(this, new EventArgs())
                            SinglePress?.Invoke(this, new EventArgs());
                        }
                        break;
                    case MouseButton.RightButton:
                        // Check if Right mouse button has been just pressed (to avoid multiple firing)
                        if (newMouseState.RightButton == ButtonState.Pressed && oldMouseState.RightButton == ButtonState.Released)
                        {
                            // Invoke the event

                            // If the SinglePress event handler is not null, invoke it -- equivalent would be if(SinglePress != null) > SinglePress(this, new EventArgs())
                            SinglePress?.Invoke(this, new EventArgs());
                        }
                        break;
                }

                // CONTINUED PRESS

                switch (this.mouseButton)
                {
                    case MouseButton.LeftButton:
                        // Check if Left mouse button has been pressed. It allows multiple fire events while it is pressed.
                        if (newMouseState.LeftButton == ButtonState.Pressed)
                        {
                            // Invoke the event

                            // If the ContinuedPress event handler is not null, invoke it -- equivalent would be if(ContinuedPress != null) > ContinuedPress(this, new EventArgs())
                            ContinuedPress?.Invoke(this, new EventArgs());
                        }
                        break;
                    case MouseButton.MiddleButton:
                        // Check if Middle mouse button has been pressed. It allows multiple fire events while it is pressed.
                        if (newMouseState.MiddleButton == ButtonState.Pressed)
                        {
                            // Invoke the event

                            // If the ContinuedPress event handler is not null, invoke it -- equivalent would be if(ContinuedPress != null) > ContinuedPress(this, new EventArgs())
                            ContinuedPress?.Invoke(this, new EventArgs());
                        }
                        break;
                    case MouseButton.RightButton:
                        // Check if Right mouse button has been pressed. It allows multiple fire events while it is pressed.
                        if (newMouseState.RightButton == ButtonState.Pressed)
                        {
                            // Invoke the event

                            // If the ContinuedPress event handler is not null, invoke it -- equivalent would be if(ContinuedPress != null) > ContinuedPress(this, new EventArgs())
                            ContinuedPress?.Invoke(this, new EventArgs());
                        }
                        break;
                }


                oldMouseState = newMouseState; // Set the new state as the old state for next frame  

                // TODO: It might be prudent, optimization wise, to check for SinglePress OR ContinuedPress, knowing if there's a defined event or not.
            }

            if (GamepadCommand)
            {
                // This is a Gamepad command                

                // Store the current state of the gamepad
                newGamepadState = GamePad.GetState(PlayerIndex.One);

                // Check if the Gamepad is connected. If it isn't, we're done.
                if (newGamepadState.IsConnected)
                {
                    // SINGLEPRESS

                    switch (this.gamepadButton)
                    {
                        case GamepadButton.A:
                            // Check if A button has been just pressed (to avoid multiple firing)
                            if (newGamepadState.Buttons.A == ButtonState.Pressed && oldGamepadState.Buttons.A == ButtonState.Released)
                            {
                                // Invoke the event

                                // If the SinglePress event handler is not null, invoke it -- equivalent would be if(SinglePress != null) > SinglePress(this, new EventArgs())
                                SinglePress?.Invoke(this, new EventArgs());
                            }
                            break;
                        case GamepadButton.B:
                            if (newGamepadState.Buttons.B == ButtonState.Pressed && oldGamepadState.Buttons.B == ButtonState.Released)
                            {
                                // Invoke the event

                                // If the SinglePress event handler is not null, invoke it -- equivalent would be if(SinglePress != null) > SinglePress(this, new EventArgs())
                                SinglePress?.Invoke(this, new EventArgs());
                            }
                            break;
                        case GamepadButton.Back:
                            if (newGamepadState.Buttons.Back == ButtonState.Pressed && oldGamepadState.Buttons.Back == ButtonState.Released)
                            {
                                // Invoke the event

                                // If the SinglePress event handler is not null, invoke it -- equivalent would be if(SinglePress != null) > SinglePress(this, new EventArgs())
                                SinglePress?.Invoke(this, new EventArgs());
                            }
                            break;
                        case GamepadButton.BigButton: // XBOX BUTTON
                            if (newGamepadState.Buttons.BigButton == ButtonState.Pressed && oldGamepadState.Buttons.BigButton == ButtonState.Released)
                            {
                                // Invoke the event

                                // If the SinglePress event handler is not null, invoke it -- equivalent would be if(SinglePress != null) > SinglePress(this, new EventArgs())
                                SinglePress?.Invoke(this, new EventArgs());
                            }
                            break;
                        case GamepadButton.LeftShoulder:
                            if (newGamepadState.Buttons.LeftShoulder == ButtonState.Pressed && oldGamepadState.Buttons.LeftShoulder == ButtonState.Released)
                            {
                                // Invoke the event

                                // If the SinglePress event handler is not null, invoke it -- equivalent would be if(SinglePress != null) > SinglePress(this, new EventArgs())
                                SinglePress?.Invoke(this, new EventArgs());
                            }
                            break;
                        case GamepadButton.LeftStick:
                            if (newGamepadState.Buttons.LeftStick == ButtonState.Pressed && oldGamepadState.Buttons.LeftStick == ButtonState.Released)
                            {
                                // Invoke the event

                                // If the SinglePress event handler is not null, invoke it -- equivalent would be if(SinglePress != null) > SinglePress(this, new EventArgs())
                                SinglePress?.Invoke(this, new EventArgs());
                            }
                            break;
                        case GamepadButton.RightShoulder:
                            if (newGamepadState.Buttons.RightShoulder == ButtonState.Pressed && oldGamepadState.Buttons.RightShoulder == ButtonState.Released)
                            {
                                // Invoke the event

                                // If the SinglePress event handler is not null, invoke it -- equivalent would be if(SinglePress != null) > SinglePress(this, new EventArgs())
                                SinglePress?.Invoke(this, new EventArgs());
                            }
                            break;
                        case GamepadButton.RightStick:
                            if (newGamepadState.Buttons.RightStick == ButtonState.Pressed && oldGamepadState.Buttons.RightStick == ButtonState.Released)
                            {
                                // Invoke the event

                                // If the SinglePress event handler is not null, invoke it -- equivalent would be if(SinglePress != null) > SinglePress(this, new EventArgs())
                                SinglePress?.Invoke(this, new EventArgs());
                            }
                            break;
                        case GamepadButton.Start:
                            if (newGamepadState.Buttons.Start == ButtonState.Pressed && oldGamepadState.Buttons.Start == ButtonState.Released)
                            {
                                // Invoke the event

                                // If the SinglePress event handler is not null, invoke it -- equivalent would be if(SinglePress != null) > SinglePress(this, new EventArgs())
                                SinglePress?.Invoke(this, new EventArgs());
                            }
                            break;
                        case GamepadButton.X:
                            if (newGamepadState.Buttons.X == ButtonState.Pressed && oldGamepadState.Buttons.X == ButtonState.Released)
                            {
                                // Invoke the event

                                // If the SinglePress event handler is not null, invoke it -- equivalent would be if(SinglePress != null) > SinglePress(this, new EventArgs())
                                SinglePress?.Invoke(this, new EventArgs());
                            }
                            break;
                        case GamepadButton.Y:
                            if (newGamepadState.Buttons.Y == ButtonState.Pressed && oldGamepadState.Buttons.Y == ButtonState.Released)
                            {
                                // Invoke the event

                                // If the SinglePress event handler is not null, invoke it -- equivalent would be if(SinglePress != null) > SinglePress(this, new EventArgs())
                                SinglePress?.Invoke(this, new EventArgs());
                            }
                            break;
                        case GamepadButton.DPadDown:
                            if (newGamepadState.DPad.Down == ButtonState.Pressed && oldGamepadState.DPad.Down == ButtonState.Released)
                            {
                                // Invoke the event

                                // If the SinglePress event handler is not null, invoke it -- equivalent would be if(SinglePress != null) > SinglePress(this, new EventArgs())
                                SinglePress?.Invoke(this, new EventArgs());
                            }
                            break;
                        case GamepadButton.DPadLeft:
                            if (newGamepadState.DPad.Left == ButtonState.Pressed && oldGamepadState.DPad.Left == ButtonState.Released)
                            {
                                // Invoke the event

                                // If the SinglePress event handler is not null, invoke it -- equivalent would be if(SinglePress != null) > SinglePress(this, new EventArgs())
                                SinglePress?.Invoke(this, new EventArgs());
                            }
                            break;
                        case GamepadButton.DPadRight:
                            if (newGamepadState.DPad.Right == ButtonState.Pressed && oldGamepadState.DPad.Right == ButtonState.Released)
                            {
                                // Invoke the event

                                // If the SinglePress event handler is not null, invoke it -- equivalent would be if(SinglePress != null) > SinglePress(this, new EventArgs())
                                SinglePress?.Invoke(this, new EventArgs());
                            }
                            break;
                        case GamepadButton.DPadUp:
                            if (newGamepadState.DPad.Up == ButtonState.Pressed && oldGamepadState.DPad.Up == ButtonState.Released)
                            {
                                // Invoke the event

                                // If the SinglePress event handler is not null, invoke it -- equivalent would be if(SinglePress != null) > SinglePress(this, new EventArgs())
                                SinglePress?.Invoke(this, new EventArgs());
                            }
                            break;
                    }

                    // CONTINUED PRESS

                    switch (this.gamepadButton)
                    {
                        case GamepadButton.A:
                            // Check if the button has been pressed. Continuos fires are accepted.
                            if (newGamepadState.Buttons.A == ButtonState.Pressed)
                            {
                                // Invoke the event

                                // If the ContinuedPress event handler is not null, invoke it -- equivalent would be if(ContinuedPress != null) > ContinuedPress(this, new EventArgs())
                                ContinuedPress?.Invoke(this, new EventArgs());
                            }
                            break;
                        case GamepadButton.B:
                            if (newGamepadState.Buttons.B == ButtonState.Pressed)
                            {
                                // Invoke the event

                                // If the ContinuedPress event handler is not null, invoke it -- equivalent would be if(ContinuedPress != null) > ContinuedPress(this, new EventArgs())
                                ContinuedPress?.Invoke(this, new EventArgs());
                            }
                            break;
                        case GamepadButton.Back:
                            if (newGamepadState.Buttons.Back == ButtonState.Pressed)
                            {
                                // Invoke the event

                                // If the ContinuedPress event handler is not null, invoke it -- equivalent would be if(ContinuedPress != null) > ContinuedPress(this, new EventArgs())
                                ContinuedPress?.Invoke(this, new EventArgs());
                            }
                            break;
                        case GamepadButton.BigButton: // XBOX BUTTON
                            if (newGamepadState.Buttons.BigButton == ButtonState.Pressed)
                            {
                                // Invoke the event

                                // If the ContinuedPress event handler is not null, invoke it -- equivalent would be if(ContinuedPress != null) > ContinuedPress(this, new EventArgs())
                                ContinuedPress?.Invoke(this, new EventArgs());
                            }
                            break;
                        case GamepadButton.LeftShoulder:
                            if (newGamepadState.Buttons.LeftShoulder == ButtonState.Pressed)
                            {
                                // Invoke the event

                                // If the ContinuedPress event handler is not null, invoke it -- equivalent would be if(ContinuedPress != null) > ContinuedPress(this, new EventArgs())
                                ContinuedPress?.Invoke(this, new EventArgs());
                            }
                            break;
                        case GamepadButton.LeftStick:
                            if (newGamepadState.Buttons.LeftStick == ButtonState.Pressed)
                            {
                                // Invoke the event

                                // If the ContinuedPress event handler is not null, invoke it -- equivalent would be if(ContinuedPress != null) > ContinuedPress(this, new EventArgs())
                                ContinuedPress?.Invoke(this, new EventArgs());
                            }
                            break;
                        case GamepadButton.RightShoulder:
                            if (newGamepadState.Buttons.RightShoulder == ButtonState.Pressed)
                            {
                                // Invoke the event

                                // If the ContinuedPress event handler is not null, invoke it -- equivalent would be if(ContinuedPress != null) > ContinuedPress(this, new EventArgs())
                                ContinuedPress?.Invoke(this, new EventArgs());
                            }
                            break;
                        case GamepadButton.RightStick:
                            if (newGamepadState.Buttons.RightStick == ButtonState.Pressed)
                            {
                                // Invoke the event

                                // If the ContinuedPress event handler is not null, invoke it -- equivalent would be if(ContinuedPress != null) > ContinuedPress(this, new EventArgs())
                                ContinuedPress?.Invoke(this, new EventArgs());
                            }
                            break;
                        case GamepadButton.Start:
                            if (newGamepadState.Buttons.Start == ButtonState.Pressed)
                            {
                                // Invoke the event

                                // If the ContinuedPress event handler is not null, invoke it -- equivalent would be if(ContinuedPress != null) > ContinuedPress(this, new EventArgs())
                                ContinuedPress?.Invoke(this, new EventArgs());
                            }
                            break;
                        case GamepadButton.X:
                            if (newGamepadState.Buttons.X == ButtonState.Pressed)
                            {
                                // Invoke the event

                                // If the ContinuedPress event handler is not null, invoke it -- equivalent would be if(ContinuedPress != null) > ContinuedPress(this, new EventArgs())
                                ContinuedPress?.Invoke(this, new EventArgs());
                            }
                            break;
                        case GamepadButton.Y:
                            if (newGamepadState.Buttons.Y == ButtonState.Pressed)
                            {
                                // Invoke the event

                                // If the ContinuedPress event handler is not null, invoke it -- equivalent would be if(ContinuedPress != null) > ContinuedPress(this, new EventArgs())
                                ContinuedPress?.Invoke(this, new EventArgs());
                            }
                            break;
                        case GamepadButton.DPadDown:
                            if (newGamepadState.DPad.Down == ButtonState.Pressed)
                            {
                                // Invoke the event

                                // If the ContinuedPress event handler is not null, invoke it -- equivalent would be if(ContinuedPress != null) > ContinuedPress(this, new EventArgs())
                                ContinuedPress?.Invoke(this, new EventArgs());
                            }
                            break;
                        case GamepadButton.DPadLeft:
                            if (newGamepadState.DPad.Left == ButtonState.Pressed)
                            {
                                // Invoke the event

                                // If the ContinuedPress event handler is not null, invoke it -- equivalent would be if(ContinuedPress != null) > ContinuedPress(this, new EventArgs())
                                ContinuedPress?.Invoke(this, new EventArgs());
                            }
                            break;
                        case GamepadButton.DPadRight:
                            if (newGamepadState.DPad.Right == ButtonState.Pressed)
                            {
                                // Invoke the event

                                // If the ContinuedPress event handler is not null, invoke it -- equivalent would be if(ContinuedPress != null) > ContinuedPress(this, new EventArgs())
                                ContinuedPress?.Invoke(this, new EventArgs());
                            }
                            break;
                        case GamepadButton.DPadUp:
                            if (newGamepadState.DPad.Up == ButtonState.Pressed)
                            {
                                // Invoke the event

                                // If the ContinuedPress event handler is not null, invoke it -- equivalent would be if(ContinuedPress != null) > ContinuedPress(this, new EventArgs())
                                ContinuedPress?.Invoke(this, new EventArgs());
                            }
                            break;
                    }
                }
                else
                    Console.WriteLine("Gamepad not connected");

                
                oldGamepadState = newGamepadState; // Set the new state as the old state for next frame
            }
        }

        #endregion
    }
}
