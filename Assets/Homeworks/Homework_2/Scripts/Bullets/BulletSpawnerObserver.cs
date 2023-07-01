using UnityEngine;
using FrameworkUnity.Interfaces.Listeners.GameListeners;
using FrameworkUnity.Architecture.DI;

// Готово.
namespace ShootEmUp
{
    public class BulletSpawnerObserver : MonoBehaviour, IGameFinishListener, IGameStartListener
    {
        private BulletSpawner _bulletSpawner;
        private BulletPool _bulletPool;
        private BulletCollisionHandler _bulletCollisionHandler;


        [Inject]
        public void Construct(BulletSpawner bulletSpawner, BulletPool bulletPool, BulletCollisionHandler bulletCollisionHandler)
        {
            _bulletSpawner = bulletSpawner;
            _bulletPool = bulletPool;
            _bulletCollisionHandler = bulletCollisionHandler;
        }

        public void OnStartGame()
        {
            _bulletSpawner.OnBulletSpawned += InvokeSpawnBullt;
            _bulletSpawner.OnBulletDespawned += OnDespawnBullet;
        }

        public void OnFinishGame()
        {
            _bulletSpawner.OnBulletSpawned -= InvokeSpawnBullt;
            _bulletSpawner.OnBulletDespawned -= OnDespawnBullet;
        }

        private void InvokeSpawnBullt(Bullet.Args args)
        {
            Bullet bullet = _bulletPool.GetBullet();
            _bulletSpawner.SpawnBullet(args, bullet);

            bullet.OnCollisionEntered += _bulletCollisionHandler.HandleCollision;
        }

        private void OnDespawnBullet(Bullet bullet)
        {
            bullet.OnCollisionEntered -= _bulletCollisionHandler.HandleCollision;
        }
    }
}
