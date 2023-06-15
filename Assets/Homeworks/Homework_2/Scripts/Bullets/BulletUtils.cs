using UnityEngine;

// Готово.
namespace ShootEmUp
{
    internal static class BulletUtils
    {
        internal static bool DealDamage(Bullet bullet, GameObject other)
        {
            if (other.TryGetComponent<Bullet>(out Bullet otherBullet))
            {
                if (bullet.IsPlayer == otherBullet.IsPlayer)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                if (!other.TryGetComponent(out TeamComponent team))
                {
                    return true;
                }

                if (bullet.IsPlayer == team.IsPlayer)
                {
                    return false;
                }

                if (other.TryGetComponent(out HitPointsComponent hitPoints))
                {
                    hitPoints.TakeDamage(bullet.Damage);
                    return true;
                }

                return false;
            }
        }
    }
}