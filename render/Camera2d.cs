using System;
using OpenTK;

namespace ScrollShooter2D.render
{
    public class Camera2D
    {
        private Vector3 position = Vector3.Zero;
        private Vector2 angle = Vector2.Zero;

        private Matrix4 translation = Matrix4.Identity;
        private Matrix4 rotation = Matrix4.Identity;

        private Matrix4 viewMatrix => translation * rotation;
        private Matrix4 projectionMatrix;

        private Vector2 resolution;
        private float nearPlane;
        private float farPlane;
        
        public Vector3 Position => position;
        public Vector2 Angle => angle;

        public Vector2 Resolution { get; set; }

        public float NearPlane
        {
            get => nearPlane;
            set { nearPlane = value; createProjection(); }
        }

        public float FarPlane
        {
            get => farPlane;
            set { farPlane = value; createProjection(); }
        }
        
        public Matrix4 ViewProjection => viewMatrix * projectionMatrix;

        public Matrix4 ProjectionMatrix => projectionMatrix;
        public Matrix4 ViewMatrix => viewMatrix;     

        //TODO: resolution independant scaling
        public Camera2D(int width = 1024, int height = 768, float nearPlane = -1.0f, float farPlane = 100f)
        {
            resolution.X = width;
            resolution.Y = height;
            
            this.nearPlane = nearPlane;
            this.farPlane = farPlane;
            
            createProjection();
        }

        private void createProjection()
        {
            projectionMatrix = Matrix4.CreateOrthographicOffCenter
            (
                0,
                resolution.X,
                resolution.Y,
                0,
                nearPlane,
                farPlane
            );
        }

        public void MoveTo(float x, float y, float z)
        {
            position.X = x;
            position.Y = y;
            position.Z = z;

            translation = Matrix4.CreateTranslation(position);
        }

        public void MoveTo(Vector3 position)
        {
            translation = Matrix4.CreateTranslation(position);
            this.position = position;
        }

        public void Move(float deltaX, float deltaY)
        {
            position.X += deltaX;
            position.Y += deltaY;
            
            translation = Matrix4.CreateTranslation(position);
        }

        public void Move(Vector2 deltaMove)
        {
            position += new Vector3(deltaMove);
            translation = Matrix4.CreateTranslation(position);
        }

        public void SetRotaion(float x, float y)
        {
            rotation = Matrix4.CreateRotationX(MathHelper.DegreesToRadians(-y)) *
                       Matrix4.CreateRotationY(MathHelper.DegreesToRadians(x));

            angle.X = x;
            angle.Y = y;
        }
        
        public void SetRotaion(Vector2 angle)
        {
            rotation = Matrix4.CreateRotationX(MathHelper.DegreesToRadians(-angle.Y)) *
                       Matrix4.CreateRotationY(MathHelper.DegreesToRadians(angle.X));

            this.angle = angle;
        }

        public void Rotate(float x, float y)
        {
            angle.X += x;
            angle.Y += y;
            
            rotation = Matrix4.CreateRotationY(MathHelper.DegreesToRadians(angle.X)) *
                       Matrix4.CreateRotationX(MathHelper.DegreesToRadians(-angle.Y));
        }

        public void Rotate(Vector2 angle)
        {
            rotation *= Matrix4.CreateRotationX(MathHelper.DegreesToRadians(-angle.Y)) *
                        Matrix4.CreateRotationY(MathHelper.DegreesToRadians(angle.X));

            this.angle += angle;
        }
    }
}