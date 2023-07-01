using UnityEngine;
using System;

// Готово.
namespace ShootEmUp
{
    public class BulletCollisionHandler : MonoBehaviour
    {
        public event Action<Bullet> OnBulletRemoved;


        public void HandleCollision(Bullet bullet, GameObject otherObj)
        {
            if (otherObj.TryGetComponent<Bullet>(out Bullet otherBullet))
            {
                if (bullet.IsPlayer != otherBullet.IsPlayer)
                {
                    OnBulletRemoved?.Invoke(bullet);
                }
            }
            else if (otherObj.TryGetComponent(out TeamComponent teamComponent))
            {
                if (bullet.IsPlayer != teamComponent.IsPlayer)
                {
                    if (otherObj.TryGetComponent(out HitPointsComponent hitPoints))
                    {
                        hitPoints.TakeDamage(bullet.Damage);
                        OnBulletRemoved?.Invoke(bullet);
                    }
                }
            }
            else
            {
                OnBulletRemoved?.Invoke(bullet);
            }
        }
    }
}