using OpenTK;
using ScrollShooter2D.managers;

namespace ScrollShooter2D.game_entities
{
    public class Bullet : Projectile
    {
        public Bullet(float width, float height, int damage = 0, float range = 500.0f) : base(width, height, damage, range)
        {
            ColliderBox.Position = new Vector2(position.X, position.Y);
        }

        public override void Update(float deltaTime)
        {
            Vector3 moveDelta = new Vector3(Velocity * deltaTime);
            position += moveDelta;
            ColliderBox.Position = new Vector2(position.X, position.Y);
            UpdateModelMatrix();
            
            traveled += moveDelta.LengthFast;
            
            if(traveled >= range || position.X < 0 || position.Y < 0)
                GameManager.Instance.RemoveEntity(this, StageLayers.Main);
        }

        public override void OnCharacterHit()
        {
            GameManager.Instance.RemoveEntity(this, StageLayers.Main);
        }
    }
}