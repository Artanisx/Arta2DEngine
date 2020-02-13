using Arta2DEngine.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Arta2DEngine.Graphics
{
    /// <summary>
    /// This is the base class for each Game Object. 
    /// This cannot be instanced, but has to be inherited and its functions needs to be implemented or overriden.
    /// </summary>
    public abstract class BaseObject
    {
        // Each BaseObject has a texture
        public Texture2D Texture { get; set; }

        // Each BaseObject has a position
        public Vector2 Position { get; set; }

        // Each BaseObject has an origin
        private Vector2 origin;

        // Each BaseObject has a source rectangle
        private Rectangle sourceRectangle;

        // Each BaseObject has a rotation angle
        private float rotationAngle = 0;

        // Each BaseObject has a scale
        private float scale = 1.0f;

        /// <summary>
        /// This is going to be the scale of the Base Object.
        /// </summary>
        public float Scale
        {
            get { return scale; }
            set { scale = value; }
        }


        /// <summary>
        /// This is going to be the rotation angle of the Base Object (used for rotations).
        /// </summary>
        public float RotationAngle
        {
            get { return rotationAngle; }
            set { rotationAngle = value; }
        }

        /// <summary>
        /// This is going to be the SourceRectangle of the Base Object (to cover the whole image, used for rotations).
        /// </summary>
        public Rectangle SourceRectangle
        {
            get
            {
                return new Rectangle(0, 0, Texture.Width, Texture.Height);
            }

            set { sourceRectangle = value; }
        }

        /// <summary>
        /// This is going to be the Origin of the Base Object (centered to the texture). It is overridable since the base implementation is suited only for basic rotations.
        /// </summary>  
        public virtual Vector2 Origin
        {
            get
            {
                return origin;
            }

            set { origin = value; }
        }

        /// <summary>
        /// This is going to return a Vector2 with the size of the object (width, height). If the texture is not ready (null) it will return 0,0.
        /// </summary>  
        public Vector2 Size
        {
            get
            {
                return Texture == null ? Vector2.Zero : new Vector2(Texture.Width, Texture.Height);
            }
        }

        /// <summary>
        /// This is going to be the box around the object to be used for collisions.
        /// </summary>        
        public Rectangle BoundingBox
        {
            get
            {
                // Creates a rectangle around the object and return it - It takes the origin to be sure it's taken into account
                return new Rectangle((int)Position.X - (int)Origin.X, (int)Position.Y - (int)Origin.Y, Texture.Bounds.Width, Texture.Bounds.Height);
            }
        }

        /// <summary>
        /// This is going to be the circle around the object to be used for collisions.
        /// </summary>        
        public Circle BoundingCircle
        {
            get
            {
                // Creates a circle around the object and return it

                // Define its center
                //Vector2 centerOfObject = new Vector2((int)Position.X + Texture.Width / 2, (int)Position.Y + Texture.Height / 2);
                //Vector2 centerOfObject = new Vector2((int)Position.X - (int)Origin.X - Texture.Width / 2, (int)Position.Y - (int)Origin.Y - Texture.Height / 2);
                Vector2 centerOfObject = new Vector2((int)Position.X, (int)Position.Y);

                // Define its radius (half its side)
                float radiusOfSprite = Texture.Width / 2;

                Circle circle = new Circle(centerOfObject, radiusOfSprite);

                return circle;
            }
        }

        // <summary>
        /// This method draws the object. It must be called between a spriteBatch.Begin and spriteBatch.End
        /// It can be overridden if needed. This will assume a center origin and no offset.
        /// </summary>
        /// <param name="spriteBatch">The spriteBatch from Game().</param>
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (Texture != null)
                spriteBatch.Draw(Texture, Position, SourceRectangle, Color.White, RotationAngle, Origin, Scale, SpriteEffects.None, 0);            
        }

        /// <summary>
        /// This is another overload for Draw. It will take an offset and use it to draw the object.
        /// This will take care of the rotation as well. The origin point will be the center.
        /// </summary>
        /// <param name="spriteBatch">The spritebatch</param>
        /// <param name="offset">The offset from its position</param>
        public virtual void Draw(SpriteBatch spriteBatch, Vector2 offset)
        {
            if (Texture != null)
                spriteBatch.Draw(Texture, new Vector2((int)(Position.X + offset.X), (int)(Position.Y + offset.Y)), SourceRectangle, Color.White, RotationAngle, Origin, Scale, SpriteEffects.None, 0);
        }

        /// <summary>
        /// This overload will take an offset and a defined origin to draw the object.
        /// </summary>
        /// <param name="spriteBatch">The spritebatch</param>
        /// <param name="offset">The offset from its position</param>
        /// <param name="origin">The origin</param>
        public virtual void Draw(SpriteBatch spriteBatch, Vector2 offset, Vector2 origin)
        {
            if (Texture != null)
                spriteBatch.Draw(Texture, new Vector2((int)(Position.X + offset.X), (int)(Position.Y + offset.Y)), SourceRectangle, Color.White, RotationAngle, new Vector2(origin.X, origin.Y), Scale, SpriteEffects.None, 0);
            
        }

        // <summary>
        /// This method draws the bounding boxes, circles and radii for debug purposes. It must be called between a spriteBatch.Begin and spriteBatch.End        
        /// </summary>
        /// <param name="spriteBatch">The spriteBatch from Game().</param>
        public virtual void DrawDebug(SpriteBatch spriteBatch, Color color)
        {           
            // Draw the Bounding Circle
            Primitives2D.DrawCircle(spriteBatch, this.BoundingCircle.Center, this.BoundingCircle.Radius, 10, color, 1f);

            // Draw the Radius
            Primitives2D.DrawLine(spriteBatch, this.BoundingCircle.Center, this.BoundingCircle.Radius, 0f, color);

            // Draw the Bounding Box
            Primitives2D.DrawRectangle(spriteBatch, this.BoundingBox, color);           
        }

        // <summary>
        /// This method updates the object. The base implementation is empty, so it must be overridden if needed.
        /// </summary>
        public virtual void Update(GameTime gameTime) { }
    }
}
