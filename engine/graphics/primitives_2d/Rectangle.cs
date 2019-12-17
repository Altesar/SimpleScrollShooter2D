using System.Collections.Generic;
using OpenTK;
using OpenTK.Graphics;

using ScrollShooter2D.engine.graphics.util;

namespace ScrollShooter2D.engine.graphics.primitives_2d
{
    class Rectangle : Mesh
    {
        protected Vector2 size;

        public Rectangle(float width, float height, Color4 color) 
            : base(
                new List<Vertex>
                {
                    new Vertex(new Vector4(0, 0, 0.0f, 1), color), // Top left corner
                    new Vertex(new Vector4(width, 0, 0.0f, 1), color), // Top right corner
                    new Vertex(new Vector4(0, height, 0.0f, 1), color), // Bottom left corner
                    new Vertex(new Vector4(width, height, 0.0f, 1), color) // Bottom right corner      
                },
                new List<ushort>
                {
                    0, 1, 2,
                    2, 1, 3
                }
              )
        {
            size = new Vector2(width, height);
        }
    }
}












