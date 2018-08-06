using System;
using OpenTK;
using OpenTK.Graphics;
using ScrollShooter2D.managers;
using ScrollShooter2D.util;

namespace ScrollShooter2D.game_entities
{
    public class Enemy : Character
    {
        private Vector2 velocity = Vector2.Zero;

        public Vector2 Velocity
        {
            get => velocity;
            set => velocity = value;
        }

        private float drag = 100.0f;
        private float maxSpeed = 150.0f;

        private float atkCooldown = 0.65f;
        private float cdMeter = 0;

        public Enemy(float width, float height, int hp, int sp) : base(width, height)
        {
            hullPoints = hp;
            shieldPoints = sp;
        }

        public override void FireWeapon()
        {
            if(cdMeter < atkCooldown)
                return;
                
            Bullet bullet = new Bullet(10, 15, 1, 200);
            bullet.Velocity = new Vector2(0, 250);
            bullet.SetColor(Color4.DarkRed);
            bullet.MoveTo(position.X + size.X / 2 - 5.0f / 2.0f, position.Y + size.Y + 15f);
            
            GameManager.Instance.AddEntity(bullet, StageLayers.Main);

            cdMeter = 0;
        }
        
        public override void Create()
        {
            //TODO: play spawn animation
            throw new NotImplementedException();
        }

        public override void Update(float deltaTime)
        {
            cdMeter += deltaTime;
            FireWeapon();
        }

        public override void Destroy()
        {
            base.Destroy();
            GameManager.Instance.RemoveEntity(this, StageLayers.Main);
        }

        public override void OnProjectileHit(Projectile projectile)
        {
            shieldPoints -= projectile.Damage;
            if (shieldPoints < 0)
            {
                hullPoints += shieldPoints;
                shieldPoints = 0;
            }
            
            if(hullPoints <= 0)
                Destroy();

            Console.WriteLine("------[Enemy]---------------");
            Console.WriteLine($"Hull points {hullPoints}\n" +
                              $"Shield points {shieldPoints}");
        }
    }
}