using System.Collections.Generic;
using UnityEngine;
using FrameworkUnity.Architecture.DI;
using System;

// Готово.
namespace ShootEmUp
{
    public sealed class BulletSpawner : MonoBehaviour
    {
        private BulletPool _bulletPool;
        private BulletCollisionHandler _bulletCollisionHandler;

        private readonly HashSet<Bullet> _activeBullets = new();
        public HashSet<Bullet> ActiveBullets => _activeBullets;

        public event Action<Bullet.Args> OnBulletSpawned;
        public event Action<Bullet> OnBulletDespawned;


        [Inject]
        public void Construct(BulletPool bulletPool, BulletCollisionHandler bulletCollisionHandler)
        {
            _bulletPool = bulletPool;
            _bulletCollisionHandler = bulletCollisionHandler;
        }

        public void InvokeSpawnBullet(Bullet.Args args)
        {
            OnBulletSpawned?.Invoke(args);
        }

        public void SpawnBullet(Bullet.Args args, Bullet bullet)
        {
            bullet.SetPosition(args.Position);
            bullet.SetColor(args.Color);
            bullet.SetPhysicsLayer(args.PhysicsLayer);
            bullet.Damage = args.Damage;
            bullet.IsPlayer = args.IsPlayer;
            bullet.SetVelocity(args.Velocity);

            _activeBullets.Add(bullet);
        }

        public void DespawnBullet(Bullet bullet)
        {
            _activeBullets.Remove(bullet);
            _bulletPool.RemoveBullet(bullet);
            OnBulletDespawned?.Invoke(bullet);
        }
    }
}