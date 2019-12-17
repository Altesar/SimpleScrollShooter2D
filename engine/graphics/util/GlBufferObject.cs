using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScrollShooter2D.engine.graphics.util
{
    abstract class GlBufferObject
    {
        protected int bufferId;
        protected BufferTarget bufferType;
        protected BeginMode drawMode;
        protected BufferUsageHint bufferUsage;

        public GlBufferObject(BufferTarget bufferType, BeginMode drawMode, BufferUsageHint bufferUsage)
        {
            bufferId = GL.GenBuffer();

            this.bufferType = bufferType;
            this.drawMode = drawMode;
            this.bufferUsage = bufferUsage;
        }

        public void Destroy()
        {
            GL.DeleteBuffer(bufferId);
        }

        protected void bind()
        {
            GL.BindBuffer(bufferType, bufferId);
        }

        protected abstract void updateBufferData();
        //TODO: public Element_t getBufferData
    }
}
