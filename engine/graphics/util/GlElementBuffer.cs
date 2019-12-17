using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL;

namespace ScrollShooter2D.engine.graphics.util
{
    class GlElementBuffer : GlBufferObject
    {
        private ushort[] indexArray;
        private List<ushort> indices;
        public List<ushort> Indices
        {
            get => indices;
            set
            {
                indices = value;
                indexArray = indices.ToArray();
                updateBufferData();
            }
        }
        
        public GlElementBuffer(List<ushort> indices, BeginMode drawMode = BeginMode.Triangles, BufferUsageHint bufferUsage = BufferUsageHint.StaticDraw)
            : base(BufferTarget.ElementArrayBuffer, drawMode, bufferUsage)
        {
            this.indices = indices;
            this.indexArray = indices.ToArray();
            updateBufferData();
        }

        protected override void updateBufferData()
        {
            bind();
            GL.BufferData(
                bufferType,
                indexArray.Length * sizeof(ushort),
                indexArray,
                bufferUsage
            );
        }

        public void Draw()
        {
            bind();
            GL.DrawElements(drawMode, indexArray.Length, DrawElementsType.UnsignedShort, 0);
        }
    }
}
