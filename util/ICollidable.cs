using OpenTK;
using ScrollShooter2D.game_entities;

namespace ScrollShooter2D.util
{
    public interface ICollidable
    {
        void OnProjectileHit(Projectile projectile);
    }
}