using System.Collections.Generic;
using UnityEngine;
using FrameworkUnity.Architecture.DI;
using FrameworkUnity.Interfaces.Listeners.GameListeners;

// Готово.
namespace ShootEmUp
{
    public sealed class BulletManager : MonoBehaviour, IGameFinishListener, IGameStartListener
    {
        private BulletPool _bulletPool;
        private BulletCollisionHandler _bulletCollisionHandler;

        public HashSet<Bullet> ActiveBullets => _activeBullets;
        private readonly HashSet<Bullet> _activeBullets = new();


        [Inject]
        public void Construct(BulletPool bulletPool, BulletCollisionHandler bulletCollisionHandler)
        {
            _bulletPool = bulletPool;
            _bulletCollisionHandler = bulletCollisionHandler;
        }

        public void OnStartGame() => _bulletCollisionHandler.OnBulletRemoved += RemoveBullet;

        public void OnFinishGame()
        {
            _bulletCollisionHandler.OnBulletRemoved -= RemoveBullet;
            DisableActiveBullets();
        }

        public void FlyBulletByArgs(Bullet.Args args)
        {
            Bullet bullet = _bulletPool.GetBullet();

            bullet.SetPosition(args.Position);
            bullet.SetColor(args.Color);
            bullet.SetPhysicsLayer(args.PhysicsLayer);
            bullet.Damage = args.Damage;
            bullet.IsPlayer = args.IsPlayer;
            bullet.SetVelocity(args.Velocity);

            _activeBullets.Add(bullet);
            bullet.OnCollisionEntered += _bulletCollisionHandler.HandleCollision;
        }

        private void RemoveBullet(Bullet bullet)
        {
            bullet.OnCollisionEntered -= _bulletCollisionHandler.HandleCollision;
            _activeBullets.Remove(bullet);
            _bulletPool.RemoveBullet(bullet);
        }

        public void DisableActiveBullets()
        {
            foreach (var bullet in _activeBullets)
            {
                bullet.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            }
        }
    }
}