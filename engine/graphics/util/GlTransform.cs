using OpenTK;
using OpenTK.Graphics.OpenGL;

using ScrollShooter2D.engine.graphics.util.attributes;

namespace ScrollShooter2D.engine.graphics.util
{
    class GlTransform
    {
        private Vector3 position = Vector3.Zero;
        private Vector3 rotation = Vector3.Zero;
        private Vector3 scale = Vector3.One;

        public Vector3 Position
        {
            get => position;
            set { position = value; updateModelMatrix(); }
        }

        public Vector3 Rotation
        {
            get => rotation;
            set { rotation = value; updateModelMatrix(); }
        }

        public Vector3 Scale
        {
            get => scale;
            set { scale = value; updateModelMatrix(); }
        }

        private Matrix4 modelMatrix = Matrix4.Identity;
        public Matrix4 TransformMatrix => modelMatrix;
        
        private void updateModelMatrix()
        {
            modelMatrix = Matrix4.CreateScale(Scale) *
                          Matrix4.CreateRotationZ(Rotation.Z) *
                          Matrix4.CreateRotationY(Rotation.Y) *
                          Matrix4.CreateRotationX(Rotation.X) *
                          Matrix4.CreateTranslation(Position);
        }

        public void Move(Vector3 delta)
        {
            position += delta;
            updateModelMatrix();
        }

        public void Move(float dx, float dy = 0, float dz = 0)
        {
            position += new Vector3(dx, dy, dz);
            updateModelMatrix();
        }

        public void Rotate(Vector3 delta)
        {
            rotation += delta;
            updateModelMatrix();
        }

        public void Rotate(float dx, float dy = 0, float dz = 0)
        {
            rotation += new Vector3(dx, dy, dz);
            updateModelMatrix();
        }

        public void ChangeScale(Vector3 delta)
        {
            scale += delta;
            updateModelMatrix();
        }

        public void ChangeScale(float dx, float dy = 0, float dz = 0)
        {
            scale += new Vector3(dx, dy, dz);
            updateModelMatrix();
        }
    }
}
