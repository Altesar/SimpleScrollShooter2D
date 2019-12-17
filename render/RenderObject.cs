using System;
using System.Collections.Generic;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace ScrollShooter2D.render
{

    public struct Vertex
    {
        public Vector4 Position;
        public Color4 Color;

        public static readonly int Size = 4 * 2 * sizeof(float);

        public Vertex(Vector4 position, Color4 color)
        {
            Position = position;
            Color = color;
        }
    }

    internal enum LineType { Vertex, Face, Comment, Unknown }

    public class RenderObject : IDisposable
    {        
        #region Buffers

        public int VertexBufferId { get; }
        public int ElementBufferId { get; }
        public BufferUsageHint UsageHint { get; set; }
        public BeginMode DrawMode { get; }
        
        private List<Vertex> vertices;
        public List<Vertex> Vertices
        {
                     get => vertices;
                     set { vertices = value; UpdateBuffers(); }
                 }

        private List<ushort> indices;
        public List<ushort> Indices
        {
            get => indices;
            set { indices = value; UpdateBuffers(); }
        }

        private Vertex[] vertexBuffer;
        public Vertex[] VertexBuffer => vertexBuffer;

        private ushort[] indexBuffer;
        public ushort[] IndexBuffer => indexBuffer;
        
        #endregion
        
        #region  Model

        protected Vector3 position = Vector3.Zero;
        protected Vector3 rotation = Vector3.Zero;
        protected Vector3 scale = Vector3.One;
        
        public Vector3 Position
        {
            get => position;
            set { position = value; UpdateModelMatrix(); }
        }

        public Vector3 Rotation
        {
            get => rotation;
            set { rotation = value; UpdateModelMatrix(); }
        }

        public Vector3 Scale
        {
            get => scale;
            set { scale = value; UpdateModelMatrix();  }
        }
        
        private Matrix4 modelMatrix = Matrix4.Identity;
        public Matrix4 ModelMatrix => modelMatrix;
        
        #endregion

        public bool Visible { get; set; }

        public RenderObject()
        {
            vertices = new List<Vertex>();
            indices = new List<ushort>();
            
            VertexBufferId = GL.GenBuffer();
            ElementBufferId = GL.GenBuffer();
            DrawMode = BeginMode.Triangles;
            UsageHint = BufferUsageHint.DynamicDraw;

            Visible = true;
            
            UpdateBuffers();
        }
        
        /// <param name="drawMode">How vertices should be handled</param>
        /// <param name="usageHint">How buffers should be handled</param>
        public RenderObject(BeginMode drawMode, BufferUsageHint usageHint)
        {
            vertices = new List<Vertex>();
            indices = new List<ushort>();
            
            VertexBufferId = GL.GenBuffer();
            ElementBufferId = GL.GenBuffer();
            DrawMode = drawMode;
            UsageHint = usageHint;
            
            UpdateBuffers();
        }
        
        /// <summary>
        /// Renders object
        /// </summary>
        /// <param name="renderInfo">Render context information</param>
        protected virtual void draw(RenderInfo renderInfo)
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, VertexBufferId);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, ElementBufferId);

            GL.VertexAttribPointer(renderInfo.PositionAttrib, 4, VertexAttribPointerType.Float, false, Vertex.Size, 0);
            GL.VertexAttribPointer(renderInfo.ColorAttrib, 4, VertexAttribPointerType.Float, false, Vertex.Size, Vertex.Size / 2);

            GL.EnableVertexAttribArray(renderInfo.PositionAttrib);
            GL.EnableVertexAttribArray(renderInfo.ColorAttrib);

            Matrix4 mvp = ModelMatrix * renderInfo.Camera.ViewProjection;
            GL.UniformMatrix4(renderInfo.ModelViewUniform, false, ref mvp);

            GL.DrawElements(DrawMode, IndexBuffer.Length, DrawElementsType.UnsignedShort, 0);

            GL.DisableVertexAttribArray(renderInfo.PositionAttrib);
            GL.DisableVertexAttribArray(renderInfo.ColorAttrib);
        }

        public void Dispose()
        {
            GL.DeleteBuffer(VertexBufferId);
            GL.DeleteBuffer(ElementBufferId);
        }

        public void UpdateBuffers()
        {
            vertexBuffer = Vertices.ToArray();
            indexBuffer = Indices.ToArray();

            GL.BindBuffer(BufferTarget.ArrayBuffer, VertexBufferId);
            GL.BufferData(
                BufferTarget.ArrayBuffer,
                vertexBuffer.Length * Vertex.Size,
                vertexBuffer,
                UsageHint
            );

            GL.BindBuffer(BufferTarget.ElementArrayBuffer, ElementBufferId);
            GL.BufferData(
                BufferTarget.ElementArrayBuffer,
                indexBuffer.Length * sizeof(ushort),
                indexBuffer,
                UsageHint
            );
            
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0);
        }

        public void UpdateModelMatrix()
        {
            modelMatrix = Matrix4.CreateScale(Scale) *
                          Matrix4.CreateRotationZ(Rotation.Z) *
                          Matrix4.CreateRotationY(Rotation.Y) *
                          Matrix4.CreateRotationX(Rotation.X) *
                          Matrix4.CreateTranslation(Position);
        }

        public void SetColor(Color4 color)
        {
            Vertex temp;
            for(int i = 0; i < vertices.Count; i++)
            {
                temp = vertices[i];
                temp.Color = color;
                vertices[i] = temp;
            }
            
            UpdateBuffers();
        }

        public virtual void MoveTo(float x, float y)
        {
            position.X = x;
            position.Y = y;
            
            UpdateModelMatrix();
        }

        public virtual void Move(float deltaX, float deltaY)
        {
            position.X += deltaX;
            position.Y += deltaY;
            
            UpdateModelMatrix();
        }
    }
}
















