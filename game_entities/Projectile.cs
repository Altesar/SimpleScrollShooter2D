using System;
using OpenTK;
using OpenTK.Graphics;
using ScrollShooter2D.render.primitives_2d;
using ScrollShooter2D.util;

namespace ScrollShooter2D.game_entities
{
    //TODO: implements ICollidable
    public abstract class Projectile : Rectangle
    {
        protected int damage;
        protected float range;
        protected float traveled = 0;

        public bool TypeChecksEnabled { get; set; }
        protected Type parentType;
        public Type ParentType => parentType;

        public Vector2 Direction { get; set; }
        public Vector2 Velocity { get; set; }

        public int Damage => damage;
        
        public ColliderBox ColliderBox { get; set; }
        
        public Projectile(float width, float height, int damage = 0, float range = 500.0f, Type parentType = null) : base(width, height, Color4.White)
        {
            this.damage = damage;
            this.range = range;
            Velocity = Vector2.Zero;
            ColliderBox = new ColliderBox(new Vector2(width, height), new Vector2(position), true);

            if (!(parentType == null))
            {
                TypeChecksEnabled = true;
                this.parentType = parentType;
            }
            else
                TypeChecksEnabled = false;

        }

        public abstract void Update(float deltaTime);
        public abstract void OnCharacterHit();
    }
}