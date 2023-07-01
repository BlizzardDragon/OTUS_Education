using System.Collections.Generic;
using UnityEngine;
using FrameworkUnity.Architecture.DI;
using FrameworkUnity.Interfaces.Listeners.GameListeners;

// Готово.
namespace ShootEmUp
{
    public sealed class BulletSpawner : MonoBehaviour, IGameFinishListener, IGameStartListener
    {
        private BulletPool _bulletPool;
        private BulletCollisionHandler _bulletCollisionHandler;

        private readonly HashSet<Bullet> _activeBullets = new();
        public HashSet<Bullet> ActiveBullets => _activeBullets;


        [Inject]
        public void Construct(BulletPool bulletPool, BulletCollisionHandler bulletCollisionHandler)
        {
            _bulletPool = bulletPool;
            _bulletCollisionHandler = bulletCollisionHandler;
        }

        public void OnStartGame() => _bulletCollisionHandler.OnBulletRemoved += DespawnBullet;
        public void OnFinishGame() => _bulletCollisionHandler.OnBulletRemoved -= DespawnBullet;

        public void SpawnBullet(Bullet.Args args)
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

        public void DespawnBullet(Bullet bullet)
        {
            _activeBullets.Remove(bullet);
            _bulletPool.RemoveBullet(bullet);

            bullet.OnCollisionEntered -= _bulletCollisionHandler.HandleCollision;
        }
    }
}