using System.Collections.Generic;
using UnityEngine;
using FrameworkUnity.Architecture.DI;

// Готово.
namespace ShootEmUp
{
    public sealed class BulletController : MonoBehaviour
    {
        private BulletPool _bulletPool;


        [Inject]
        public void Construct(BulletPool bulletPool) => _bulletPool = bulletPool;

        public void FlyBulletByArgs(Bullet.Args args)
        {
            Bullet bullet = _bulletPool.GetBullet();

            bullet.SetPosition(args.Position);
            bullet.SetColor(args.Color);
            bullet.SetPhysicsLayer(args.PhysicsLayer);
            bullet.Damage = args.Damage;
            bullet.IsPlayer = args.IsPlayer;
            bullet.SetVelocity(args.Velocity);
            
            bullet.OnCollisionEntered += OnBulletCollision;
        }

        private void OnBulletCollision(Bullet bullet, GameObject gameObj)
        {
            bool isHit = BulletUtils.DealDamage(bullet, gameObj);
            if (isHit)
            {
                bullet.OnCollisionEntered -= OnBulletCollision;
                _bulletPool.RemoveBullet(bullet);
            }
        }

        public void DisableActiveBullets()
        {
            foreach (var bullet in _bulletPool.ActiveBullets)
            {
                bullet.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            }
        }
    }
}