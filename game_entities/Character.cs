using OpenTK;
using OpenTK.Graphics;
using ScrollShooter2D.render.primitives_2d;
using ScrollShooter2D.util;

namespace ScrollShooter2D.game_entities
{
    public static class CharEventDelegates
    {
        public delegate void OnCollide(/*Collider collider*/);

        public delegate void OnDamageTaken();
        public delegate void OnDestroied();
        
        public delegate void OnActionSuccess();
        public delegate void OnAcitonFail();
    }

    public abstract class Character : Rectangle, ICollidable
    {
        protected int hullPoints;
        protected int shieldPoints;

        public int HullPoints => hullPoints;
        public int ShieldPoints => shieldPoints;

        public ColliderBox ColliderBox { get; set; }

        public event CharEventDelegates.OnDamageTaken OnDamageTaken;
        public event CharEventDelegates.OnDestroied OnDestroied;
        
        public Character(float width, float height) : base(width, height, Color4.White)
        {
            ColliderBox = new ColliderBox(new Vector2(width, height), new Vector2(position.X, position.Y), true);
        }

        public abstract void FireWeapon();

        public abstract void Create();
        public abstract void Update(float deltaTime);

        public virtual void Destroy()
        {
            OnDestroied?.Invoke();
        }

        public abstract void OnProjectileHit(Projectile projectile);

        public override void MoveTo(float x, float y)
        {
            position = new Vector3(x, y, position.Y);
            ColliderBox.Position = new Vector2(position.X, position.Y);
            UpdateModelMatrix();
        }

        public override void Move(float deltaX, float deltaY)
        {
            position += new Vector3(deltaX, deltaY, 0);
            ColliderBox.Position = new Vector2(position.X, position.Y);
            UpdateModelMatrix();
        }
    }
}