using OpenTK;
using OpenTK.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ScrollShooter2D.engine.graphics.util.attributes;

namespace ScrollShooter2D.engine.graphics.util
{
    //TODO: test performance of buffer construction, based on reflection
    [GlVertexAttrib("vertex_position", shaderAttribLocation: 0, offset: 0, size: 4)]
    [GlVertexAttrib("vertex_color", shaderAttribLocation: 1, offset: 4 * sizeof(float), size: 4)]
    public struct Vertex
    {
        public Vector4 Position;
        public Color4 Color;
        //public Vector2 TextureUv;
        //TODO: normalVector

        public static readonly int Size = 4 * 2 * sizeof(float) /* + 2 * sizeof(float)*/;

        public Vertex(Vector4 position, Color4 color /*, Vector2 textureUv*/)
        {
            Position = position;
            Color = color;
            //TextureUv = textureUv;
        }
    }
}
