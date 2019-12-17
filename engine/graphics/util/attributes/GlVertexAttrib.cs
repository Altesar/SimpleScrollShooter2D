using System;

namespace ScrollShooter2D.engine.graphics.util.attributes
{
    //TODO: bind to property
    [AttributeUsage(AttributeTargets.Struct, AllowMultiple = true)]
    class GlVertexAttrib : Attribute
    {
        public string ShaderAttribName { get; }
        public int Size { get; }
        public int Offset { get; }
        public int ShaderAttribLocation { get; }

        public GlVertexAttrib(string shaderAttribName, int shaderAttribLocation, int offset, int size)
        {
            Size = size;
            Offset = offset;
            ShaderAttribName = shaderAttribName;
            ShaderAttribLocation = shaderAttribLocation;
        }
    }
}
