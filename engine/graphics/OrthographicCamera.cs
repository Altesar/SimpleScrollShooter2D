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
    class OrthographicCamera : Camera
    {
        private Vector2 resolution;
        private Vector2 planes;
        
        public OrthographicCamera(float width, float height, float nearPlane = 0.01f, float farPlane = 1000f)
        {
            transform = new GlTransform();
            resolution = new Vector2(width, height);
            planes = new Vector2(nearPlane, farPlane);
            createProjectionMatrix();
        }

        protected override void createProjectionMatrix()
        {
            projectionMatrix = Matrix4.CreateOrthographicOffCenter
            (
                0,
                resolution.X,
                resolution.Y,
                0,
                planes.X,
                planes.Y
            );
        }
    }
}
