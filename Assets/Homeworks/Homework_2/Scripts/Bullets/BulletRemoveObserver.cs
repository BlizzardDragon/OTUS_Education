using FrameworkUnity.Architecture.DI;
using FrameworkUnity.Interfaces.Listeners.GameListeners;
using UnityEngine;

// Готово.
namespace ShootEmUp
{
    public class BulletRemoveObserver : MonoBehaviour, IGameFinishListener, IGameStartListener
    {
        private BulletSpawner _bulletSpawner;
        private BulletCollisionHandler _bulletCollisionHandler;


        [Inject]
        public void Construct(BulletSpawner bulletSpawner, BulletCollisionHandler bulletCollisionHandler)
        {
            _bulletSpawner = bulletSpawner;
            _bulletCollisionHandler = bulletCollisionHandler;
        }

        public void OnStartGame() => _bulletCollisionHandler.OnBulletRemoved += _bulletSpawner.DespawnBullet;
        public void OnFinishGame() => _bulletCollisionHandler.OnBulletRemoved -= _bulletSpawner.DespawnBullet;
    }
}
