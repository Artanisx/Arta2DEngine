using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arta2DEngine.Utils
{
    /// <summary>
    /// This is the simple struct for a Circle. 
    /// This is used by the BaseObject property BoundingCircle, used for collisions.
    /// </summary>
    public struct Circle
    {
        public Vector2 Center { get; set; }
        public float Radius { get; set; }

        public Circle(Vector2 center, float radius)
        {
            Center = center;
            Radius = radius;
        }

        /// <summary>
        /// This checks if this circle contains a point (could be the mouse cursor). 
        /// This can be used by the BaseObject property BoundingCircle, for collisions (against a point).
        /// </summary>
        public bool Contains(Vector2 point)
        {
            return ((point - Center).Length() <= Radius);
        }

        /// <summary>
        /// This checks if this circle intesects (touches) another circle. 
        /// This can be used for collisions.
        /// </summary>
        public bool Intersects(Circle other)
        {
            Vector2 relativePosition = other.Center - this.Center;
            float distanceBetweenCenters = relativePosition.Length();
            if (distanceBetweenCenters <= this.Radius + other.Radius) { return true; }
            else { return false; }
        }
    }
}
