using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Arta2DEngine.Graphics.UI
{
    public class Button : BaseObject
    {
        #region Fields

        private MouseState _currentMouse;

        private MouseState _previousMouse;

        private bool _isHovering;

        private SpriteFont _font;

        private Color backgroundColor = Color.White;
        private Color hoverColor = Color.Gray;

        #endregion

        #region Properties

        // Event for each Click action
        public event EventHandler Click;

        // State of the button
        public bool Clicked { get; private set; }

        // Color of the font
        public Color FontColor { get; set; }

        // Button caption text
        public string Text { get; set; }

        #endregion

        #region Methods

        // Consturctor with all info (texture, font and color)
        public Button(Texture2D texture, SpriteFont font, Color fontColor)
        {
            this.Texture = texture;

            _font = font;

            FontColor = fontColor;
        }

        // Constructor with minimum info (texture, font). Color will be defaulted to Black
        public Button(Texture2D texture, SpriteFont font)
        {
            this.Texture = texture;

            _font = font;

            FontColor = Color.Black;
        }

        // Constructor with maximum info (texture, font, font color, background color and hovercolor).
        public Button(Texture2D texture, SpriteFont font, Color fontColor, Color backColor, Color hovColor)
        {
            this.Texture = texture;

            _font = font;

            FontColor = fontColor;

            backgroundColor = backColor;

            hoverColor = hovColor;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            var colour = backgroundColor;

            // When the mouse is over the button, change the color to highlight it
            if (_isHovering)
                colour = hoverColor;

            // Draw the button
            spriteBatch.Draw(this.Texture, BoundingBox, colour);

            // If text is not empty (i.e. there's some text)
            if (!string.IsNullOrEmpty(Text))
            {
                // Calculate the position X and Y for the text to appear (centered align)
                var x = (BoundingBox.X + (BoundingBox.Width / 2)) - (_font.MeasureString(Text).X / 2);
                var y = (BoundingBox.Y + (BoundingBox.Height / 2)) - (_font.MeasureString(Text).Y / 2);

                spriteBatch.DrawString(_font, Text, new Vector2(x, y), FontColor);
            }
        }

        public void Update(GameTime gameTime)
        {
            // Save the previous mouse state
            _previousMouse = _currentMouse;

            // Get the current mouse state
            _currentMouse = Mouse.GetState();

            // Create a rectangle (for "collision") on the current mouse position (1x1 pixel)
            var mouseRectangle = new Rectangle(_currentMouse.X, _currentMouse.Y, 1, 1);

            // By default, set isHovering to false as we want to set it true if the mouse is actually hovering the button
            _isHovering = false;

            // Check if the mouse is indeed inside the button (Rectangle of it)
            if (mouseRectangle.Intersects(BoundingBox))
            {
                _isHovering = true;

                // If the mouse has been released and it was previously pressed (one "firing" event when one release the buttom)
                if (_currentMouse.LeftButton == ButtonState.Released && _previousMouse.LeftButton == ButtonState.Pressed)
                {
                    // If the CLICK event handler is not null, invoke it -- equivalent would be if(Click != null) > Click(this, new EventArgs())
                    Click?.Invoke(this, new EventArgs());
                }
            }
        }

        #endregion
    }
}
