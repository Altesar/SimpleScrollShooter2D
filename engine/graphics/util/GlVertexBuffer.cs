using System;
using System.Collections.Generic;
using System.Linq;
using OpenTK.Graphics.OpenGL;
using ScrollShooter2D.engine.graphics.util.attributes;

namespace ScrollShooter2D.engine.graphics.util
{
    class GlVertexBuffer : GlBufferObject
    {
        private GlVertexAttrib[] vertexAttribArray;
        private Vertex[] vertexArray;
        private List<Vertex> vertices;
        public List<Vertex> Vertices
        {
            get => vertices;
            set
            {
                vertices = value;
                vertexArray = vertices.ToArray();
                updateBufferData();
            }
        }

        public GlVertexBuffer(List<Vertex> vertices, BeginMode drawMode = BeginMode.Triangles, BufferUsageHint bufferUsage = BufferUsageHint.StaticDraw)
            : base(BufferTarget.ArrayBuffer, drawMode, bufferUsage)
        {
            this.vertices = vertices;
            this.vertexArray = vertices.ToArray();
            updateBufferData();
            getVertexAttribArray();
        }

        protected override void updateBufferData()
        {
            bind();
            GL.BufferData(
                bufferType, 
                Vertex.Size * vertices.Count,
                vertexArray,
                bufferUsage
            );
        }

        private void getVertexAttribArray()
        {
            Type vertexType = typeof(Vertex);
            GlVertexAttrib[] attribs = Attribute.GetCustomAttributes(vertexType, typeof(GlVertexAttrib)) as GlVertexAttrib[];
            if (attribs == null)
                throw new Exception("Could not find vertex attributes");

            vertexAttribArray = attribs;
        }

        public void EnableVertexAttribs()
        {
            bind();
            foreach(GlVertexAttrib attrib in vertexAttribArray)
            {
                GL.VertexAttribPointer(
                    attrib.ShaderAttribLocation,
                    attrib.Size,
                    VertexAttribPointerType.Float,
                    false,
                    Vertex.Size,
                    attrib.Offset
                );

                GL.EnableVertexAttribArray(attrib.ShaderAttribLocation);
            }
        }

        //TODO: check what happends if not used
        public void DisableVertexAttribs()
        {
            foreach (GlVertexAttrib attrib in vertexAttribArray)
            {
                 GL.DisableVertexAttribArray(attrib.ShaderAttribLocation);
            }
        }
    }
}
