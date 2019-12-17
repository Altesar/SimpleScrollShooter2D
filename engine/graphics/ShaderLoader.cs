using System;
using System.Collections.Generic;
using System.IO;
using OpenTK.Graphics.OpenGL;

namespace ScrollShooter2D.engine.graphics
{
    public struct ShaderSrc
    {
        public readonly ShaderType Type;
        public readonly string SrcPath;

        public ShaderSrc(ShaderType type, string srcPath)
        {
            SrcPath = srcPath;
            Type = type;
        }
    }

    public static class ShaderLoader
    {
        /// <summary>
        /// Compiles shader
        /// </summary>
        /// <param name="type">Type of Shader</param>
        /// <param name="sourcePath">Path to source file</param>
        /// <returns>Id of created shader</returns>
        public static int CompileShader(ShaderType type, string sourcePath)
        {
            try
            {
                string source = File.ReadAllText(sourcePath);

                int shaderId = GL.CreateShader(type);
                GL.ShaderSource(shaderId, source);
                GL.CompileShader(shaderId);

                string shaderLog = GL.GetShaderInfoLog(shaderId);
                bool err = !string.IsNullOrEmpty(shaderLog);

                if (err)
                {
                    Console.WriteLine($"{sourcePath}:\n{shaderLog}");

                    GL.DeleteShader(shaderId);
                    shaderId = -1;
                }

                return shaderId;
            }
            catch (IOException e)
            {
                Console.WriteLine(e.Message);
                return -1;
            }
        }


        /// <summary>
        /// Compiles shader
        /// </summary>
        /// <param name="shaderSrc">Shader source</param>
        /// <returns></returns>
        public static int CompileShader(ShaderSrc shaderSrc)
        {
            return CompileShader(shaderSrc.Type, shaderSrc.SrcPath);
        }

        /// <summary>
        /// Compiles several shaders
        /// </summary>
        /// <param name="shaderSrcs">List of sources</param>
        /// <returns></returns>
        public static List<int> CompileShaders(List<ShaderSrc> shaderSrcs)
        {
            List<int> shaders = new List<int>();
            int shader;
            bool hadErrors = false;
            foreach (ShaderSrc shaderSrc in shaderSrcs)
            {
                shader = CompileShader(shaderSrc.Type, shaderSrc.SrcPath);
                if (shader != -1)
                    shaders.Add(shader);
                else
                    hadErrors = true;
            }

            if (hadErrors)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Could not compile some shaders");
                Console.ResetColor();
            }

            return shaders;
        }

        /// <summary>
        /// Links shader program
        /// </summary>
        /// <param name="shaders">List of shader Ids</param>
        /// <returns></returns>
        public static int MakeProgram(List<int> shaders)
        {
            int program = GL.CreateProgram();
            foreach (int shader in shaders)
                GL.AttachShader(program, shader);
            
            GL.LinkProgram(program);

            foreach (int shader in shaders)
                GL.DeleteShader(shader);
            
            return program;
        }

        /// <summary>
        /// Compiles shaders and links program
        /// </summary>
        /// <param name="shaderSrcs">List of sources</param>
        /// <returns></returns>
        public static int LoadProgram(List<ShaderSrc> shaderSrcs)
        {
            List<int> shaders = CompileShaders(shaderSrcs);
            int program = MakeProgram(shaders);
            return program;
        }
    }
}