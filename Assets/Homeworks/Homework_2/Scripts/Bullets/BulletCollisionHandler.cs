using UnityEngine;
using FrameworkUnity.Architecture.DI;

// Готово.
namespace ShootEmUp
{
    public class BulletCollisionHandler : MonoBehaviour
    {
        private BulletPool _bulletPool;


        [Inject]
        public void Construct(BulletPool bulletPool) => _bulletPool = bulletPool;

        public void HandleCollision(Bullet bullet, GameObject otherObj)
        {
            if (otherObj.TryGetComponent<Bullet>(out Bullet otherBullet))
            {
                if (bullet.IsPlayer != otherBullet.IsPlayer)
                {
                    RemoveBullet(bullet);
                }
            }
            else if (otherObj.TryGetComponent(out TeamComponent teamComponent))
            {
                if (bullet.IsPlayer != teamComponent.IsPlayer)
                {
                    if (otherObj.TryGetComponent(out HitPointsComponent hitPoints))
                    {
                        hitPoints.TakeDamage(bullet.Damage);
                        RemoveBullet(bullet);
                    }
                }
            }
            else
            {
                RemoveBullet(bullet);
            }
        }

        private void RemoveBullet(Bullet bullet)
        {
            bullet.OnCollisionEntered -= HandleCollision;
            _bulletPool.RemoveBullet(bullet);
        }
    }
}