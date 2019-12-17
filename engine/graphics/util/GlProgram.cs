using System;
using System.Collections.Generic;
using System.Reflection;
using OpenTK;

using OpenTK.Graphics.OpenGL;
using ScrollShooter2D.engine.graphics.util.attributes;

namespace ScrollShooter2D.engine.graphics.util
{
    enum UniformType { Matrix4 }
    
    //TODO: move program specific stuff here
    //TODO: cache reflection data 
    class GlProgram
    {
        private int programId;
        private Dictionary<string, int> uniforms;

        public GlProgram(int glProgram)
        {
            programId = glProgram;
            uniforms = new Dictionary<string, int>();
        }

        public void ApplyUniforms(Object obj)
        {
            Type type = obj.GetType();
            PropertyInfo[] properties = type.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            GlUniformProperty attribute;
            foreach(PropertyInfo property in properties)
            {
                attribute = property.GetCustomAttribute(typeof(GlUniformProperty)) as GlUniformProperty;
                if(attribute != null)
                {
                    applyUniform(obj, property, attribute);
                }
            }
        }

        private int lookupUniformLocation(string name)
        {
            int location = GL.GetUniformLocation(programId, name);

            Console.WriteLine($"Resolved {name} uniform to {location}");

            if (location < 0)
                throw new Exception($"Uniform {name} does not exist");

            return location;
        }

        private void applyUniform(Object instance, PropertyInfo property, GlUniformProperty uniform)
        {
            if (!uniforms.ContainsKey(uniform.ShaderUniformName))
                uniforms[uniform.ShaderUniformName] = lookupUniformLocation(uniform.ShaderUniformName);

            switch (uniform.UniformType)
            {
                case UniformType.Matrix4:
                    Matrix4 value = (Matrix4)property.GetValue(instance);
                    GL.UniformMatrix4(uniforms[uniform.ShaderUniformName], false, ref value);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
