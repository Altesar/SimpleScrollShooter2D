using System;
using System.Collections.Generic;
using System.Reflection;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace ScrollShooter2D.render
{
    public struct RenderInfo
    {
        public int PositionAttrib, ColorAttrib, ModelViewUniform;
        public Camera2D Camera;

        public RenderInfo(int positionAttrib, int colorAttrib, int modelViewUniform, Camera2D camera)
        {
            PositionAttrib = positionAttrib;
            ColorAttrib = colorAttrib;
            ModelViewUniform = modelViewUniform;
            Camera = camera;
        }
    }

    public class Renderer
    {
        private List<RenderObject> drawQueue;
        private MethodInfo drawMethod;

        private int positionAttrib, colorAttrib;
        private int modelViewUniform;

        private Camera2D cam;
        
        /// <param name="glProgram">Id of shader program</param>
        /// <param name="camera2D">Camera object</param>
        public Renderer(int glProgram, Camera2D camera2D)
        {
            drawQueue = new List<RenderObject>();
            cam = camera2D;
            
            positionAttrib   = GL.GetAttribLocation(glProgram, "vertex_position");
            colorAttrib      = GL.GetAttribLocation(glProgram, "vertex_color");
            modelViewUniform = GL.GetUniformLocation(glProgram, "model_view_projection");

            if (!validateAttribLocations())
                throw new Exception("Could not get attrib location");

            drawMethod = typeof(render.RenderObject).GetMethod(
                "draw",
                BindingFlags.Instance | BindingFlags.NonPublic,
                Type.DefaultBinder,
                new [] { typeof(render.RenderInfo) },
                null
            );
        }

        /// <summary>
        /// Checks if all shader attribute locations were loaded correctly
        /// </summary>
        /// <returns>true if correct</returns>
        private bool validateAttribLocations()
        {
            return positionAttrib >= 0 && positionAttrib >= 0 && modelViewUniform >= 0;
        }

        /// <summary>
        /// Displays staged objects
        /// </summary>
        public void Draw()
        {
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            
            Console.WriteLine($"Rendering {drawQueue.Count} objects");

            RenderInfo renderInfo = new RenderInfo(positionAttrib, colorAttrib, modelViewUniform, cam);

            foreach (RenderObject renderObject in drawQueue)
            {
                if(!renderObject.Visible)
                    continue;

                drawMethod.Invoke(renderObject as Object, new Object[] { renderInfo });
            }

            drawQueue.Clear();
            
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0);
            
            GL.Flush();
        }

        /// <summary>
        /// Adds RenderObject to draw que
        /// </summary>
        /// <param name="renderObject">Object to be displaied</param>
        public void StageDraw(RenderObject renderObject)
        {
            if (!drawQueue.Contains(renderObject))
                drawQueue.Add(renderObject);
        }

        /// <summary>
        /// Adds list of RenderObjects to draw que
        /// </summary>
        /// <param name="renderObjects">List of objects to be displaied</param>
        public void StageDraw(List<RenderObject> renderObjects)
        {
            drawQueue.AddRange(renderObjects);
        }
    }
}