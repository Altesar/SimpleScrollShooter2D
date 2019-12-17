using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

using ScrollShooter2D.engine.graphics.util;
using ScrollShooter2D.engine.graphics.util.attributes;

namespace ScrollShooter2D.engine.graphics
{
    class Mesh
    {
        protected GlVertexBuffer vertexBufferObject;
        protected GlElementBuffer elementBufferObject;
        protected GlTransform transform;

        public bool Visible { get; set; }

        public GlTransform Transform => transform;

        [GlUniformProperty(UniformType.Matrix4, "model_transform")]
        protected Matrix4 transformMatrix => transform.TransformMatrix;

        public Mesh(List<Vertex> vertices, List<ushort> indices)
        {
            vertexBufferObject = new GlVertexBuffer(vertices);
            elementBufferObject = new GlElementBuffer(indices);
            transform = new GlTransform();
        }

        //TODO: MeshBufferResolver helper class
        public virtual void Draw()
        {
            vertexBufferObject.EnableVertexAttribs();
            elementBufferObject.Draw();
        }

        //TODO: move to more appropriate place
        public void SetColor(Color4 color)
        {
            List<Vertex> vertices = vertexBufferObject.Vertices;
            vertices.ForEach(delegate (Vertex vertex) { vertex.Color = color; });
            vertexBufferObject.Vertices = vertices;
        }

        public void Destroy()
        {
            vertexBufferObject.Destroy();
            elementBufferObject.Destroy();
        }
    }
}
