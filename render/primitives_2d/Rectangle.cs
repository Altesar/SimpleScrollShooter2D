using System.Collections.Generic;
using OpenTK;
using OpenTK.Graphics;

namespace ScrollShooter2D.render.primitives_2d
{
    public class Rectangle : RenderObject
    {
        protected Vector2 size;

        public Rectangle(float width, float height, Color4 color)
        {
            this.size = new Vector2(width, height);
            Vertices = new List<Vertex>
            {
                new Vertex(new Vector4(0, 0, 0.0f, 1), color), // Top left corner
                new Vertex(new Vector4(width, 0, 0.0f, 1), color), // Top right corner
                new Vertex(new Vector4(0, height, 0.0f, 1), color), // Bottom left corner
                new Vertex(new Vector4(width, height, 0.0f, 1), color) // Bottom right corner      
            };

            Indices = new List<ushort>
            {
                0, 1, 2,
                2, 1, 3
            };
            
            position = new Vector3(0, 0, -1);
        }
    }
}












