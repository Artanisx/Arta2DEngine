using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Arta2DEngine.Graphics
{
    /// <summary>
    /// This is a simple 2D Camera.     
    /// </summary>
    public class Camera2D
    {
        #region Fields
        protected float zoom;       // Camera Zoom
        public Matrix transform;    // Matrix Transform for the Camera movements
        public Vector2 position;    // Camera POsition
        protected float rotation;   // Camera rotation
        protected Vector2 origin;   // Camera origin
        protected Viewport viewport;    // Game viewport

        #endregion

        #region Properties

        public float Zoom
        {
            get { return zoom; }
            set
            {
                // Make sure there can be no negative zoom as  it would flip the image
                zoom = value;

                if (zoom < 0.1f)
                    zoom = 0.1f;
            }
        }

        public float Rotation
        {
            get { return rotation; }
            set { rotation = value; }
        }

        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

        public Vector2 Origin
        {
            get { return origin; }
            set { origin = value; }
        }

        #endregion

        #region Methods

        // <summary>
        /// This constructor creates a camera. It needs a viewport to calculate the origin.        
        /// </summary>
        /// <param name="viewport">The Viewport of the game (graphics.GraphicsDevice.Viewport).</param>
        public Camera2D(Viewport viewport)
        {
            // Default values
            zoom = 1.0f;
            rotation = 0.0f;
            position = Vector2.Zero;
            this.viewport = viewport;   // Save the viewport
            origin = new Vector2(viewport.Width / 2.0f, viewport.Height / 2.0f);
        }

        // <summary>
        /// This constructor creates a camera and sets a position as well. I needs a viewport to calculate the origin.        
        /// </summary>
        /// <param name="viewport">The Viewport of the game (graphics.GraphicsDevice.Viewport).</param>
        /// <param name="position">The position for the new camera.</param>
        public Camera2D(Viewport viewport, Vector2 position)
        {
            // Default values
            zoom = 1.0f;
            rotation = 0.0f;
            this.position = position;
            this.viewport = viewport;   // Save the viewport
            origin = new Vector2(viewport.Width / 2.0f, viewport.Height / 2.0f);
        }

        // <summary>
        /// This method moves the camera.        
        /// </summary>
        /// <param name="amount">The Vector2 describing the amount to move the camera.</param>
        public void Move(Vector2 amount)
        {
            position += amount;
        }

        // <summary>
        /// This method moves the camera, following (looking at) another Vector2.        
        /// </summary>
        /// <param name="itemToLookAt">The Vector2 describing the object we need the camera to follow (look at).</param>
        public void LookAt(Vector2 vectorToLookAt)
        {
            position = vectorToLookAt - new Vector2(viewport.Width / 2.0f, viewport.Height / 2.0f);
        }

        // <summary>
        /// This method needs to be called inside the spriteBatch.Begin call in order for it to be "attached" to the spritebatch drawing.        
        /// spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend, null, null, null, null, cam.GetTransformation(graphics));
        /// As this metod returns the transform used by the Begin() constructor.
        /// </summary>
        public Matrix GetTransformation()
        {
            transform = Matrix.CreateTranslation(new Vector3(-position.X, -position.Y, 0)) *
                                                    Matrix.CreateTranslation(new Vector3(-Origin.X, -Origin.Y, 0.0f)) *
                                                    Matrix.CreateRotationZ(Rotation) *
                                                    Matrix.CreateScale(new Vector3(Zoom, Zoom, 1)) *
                                                    Matrix.CreateTranslation(new Vector3(Origin.X, Origin.Y, 0.0f));

            return transform;
        }

        #endregion

    }
}
