using System;
using System.Collections.Generic;
using System.Reflection;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using ScrollShooter2D.engine.graphics.util;

namespace ScrollShooter2D.engine.graphics
{
    //TODO: struct ViewPort = camera + drawQueue
    //TODO: batch rendering
    class Renderer
    {
        private List<Mesh> drawQueue;
        private MethodInfo drawMethod;

        private GlProgram glProgram;

        private Camera camera;
        
        /// <param name="glProgram">Id of shader program</param>
        /// <param name="camera">Camera object</param>
        public Renderer(int glProgram, Camera camera)
        {
            drawQueue = new List<Mesh>();
            this.camera = camera;
            this.glProgram = new GlProgram(glProgram);
        }

        /// <summary>
        /// Displays staged objects
        /// </summary>
        public void Draw()
        {
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            foreach (Mesh renderObject in drawQueue)
            {
                if (renderObject.Visible)
                {
                    glProgram.ApplyUniforms(renderObject);
                    glProgram.ApplyUniforms(camera);
                    renderObject.Draw();
                }
            }

            drawQueue.Clear();
            
            GL.Flush();
        }

        /// <summary>
        /// Adds RenderObject to draw que
        /// </summary>
        /// <param name="renderObject">Object to be displaied</param>
        public void StageDraw(Mesh renderObject)
        {
            drawQueue.Add(renderObject);
        }

        /// <summary>
        /// Adds list of RenderObjects to draw que
        /// </summary>
        /// <param name="renderObjects">List of objects to be displaied</param>
        public void StageDraw(List<Mesh> renderObjects)
        {
            drawQueue.AddRange(renderObjects);
        }
    }
}