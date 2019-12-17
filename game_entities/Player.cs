using System;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Input;
using ScrollShooter2D.managers;
using ScrollShooter2D.render;
using ScrollShooter2D.util;

namespace ScrollShooter2D.game_entities
{
    public class Player : Character
    {
        private Vector2 velocity = Vector2.Zero;

        public Vector2 Velocity
        {
            get => velocity;
            set => velocity = value;
        }

        private float drag = 100.0f;
        private float maxSpeed = 150.0f;

        private float atkCooldown = 0.15f;
        private float cdMeter = 0;
        
        //TODO: ignore friendly projectiles
        public Player(float width, float height, int hp, int sp) : base(width, height)
        {
            hullPoints = hp;
            shieldPoints = sp;
        }

        public override void FireWeapon()
        {
            if(cdMeter < atkCooldown)
                return;
            
            Bullet bullet = new Bullet(10, 15, 1, 450);
            bullet.Velocity = new Vector2(0, -250);
            bullet.SetColor(Color4.DarkRed);
            bullet.MoveTo(position.X + size.X / 2 - 5.0f / 2.0f, position.Y - 15f);
            
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
            KeyboardState kbState = Keyboard.GetState();

            cdMeter += deltaTime;

            if (kbState.IsKeyDown(Key.Left))
                velocity.X = -maxSpeed;
            else if (kbState.IsKeyDown(Key.Right))
                velocity.X = maxSpeed;
            else
                velocity.X = 0;

            if (kbState.IsKeyDown(Key.Up))
                velocity.Y = -maxSpeed;
            else if (kbState.IsKeyDown(Key.Down))
                velocity.Y = maxSpeed;
            else
                velocity.Y = 0;

            if (kbState.IsKeyDown(Key.Space))
                FireWeapon();

            position += new Vector3(velocity) * deltaTime;
            ColliderBox.Position = new Vector2(position.X, position.Y);

            UpdateModelMatrix();
        }

        public override void Destroy()
        {
            base.Destroy();
            Console.WriteLine($"Player {this} destroied");
            GameManager.Instance.RemoveEntity(this, StageLayers.Main);
        }

        public override void OnProjectileHit(Projectile projectile)
        {
            //TODO: play hit animation

            shieldPoints -= projectile.Damage;
            if (shieldPoints < 0)
            {
                hullPoints += shieldPoints;
                shieldPoints = 0;
            }

            Console.WriteLine("------[Player]---------------");
            Console.WriteLine($"Hull points {hullPoints}\n" +
                              $"Shield points {shieldPoints}");
            
            if(hullPoints <= 0)
                Destroy();
        }
    }
}