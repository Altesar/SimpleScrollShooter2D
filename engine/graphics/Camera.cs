using OpenTK;
using ScrollShooter2D.engine.graphics.util;
using ScrollShooter2D.engine.graphics.util.attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScrollShooter2D.engine.graphics
{
    abstract class Camera
    {
        protected GlTransform transform;
        public GlTransform Transform => transform;
        [GlUniformProperty(UniformType.Matrix4, "camera_transform")]
        protected Matrix4 viewMatrix => Transform.TransformMatrix;

        [GlUniformProperty(UniformType.Matrix4, "camera_projection")]
        protected Matrix4 projectionMatrix { get; set; }

        protected abstract void createProjectionMatrix();
    }
}
