using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScrollShooter2D.engine.graphics.util.attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    class GlUniformProperty : Attribute
    {
        public string ShaderUniformName { get; }
        public UniformType UniformType { get; }

        public GlUniformProperty(UniformType uniformType, string shaderUniformName)
        {
            UniformType = uniformType;
            ShaderUniformName = shaderUniformName;
        }
    }
}
