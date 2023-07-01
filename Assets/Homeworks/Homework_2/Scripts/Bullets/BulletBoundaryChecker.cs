using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FrameworkUnity.Architecture.DI;
using FrameworkUnity.Interfaces.Listeners.GameListeners;

// Готово.
namespace ShootEmUp
{
    public class BulletBoundaryChecker : MonoBehaviour, IGameFixedUpdateListener
    {
        private BulletPool _bulletPool;
        private LevelBounds _levelBounds;
        private BulletManager _bulletManager;

        private readonly List<Bullet> _cache = new();


        [Inject]
        public void Construct(BulletPool bulletPool, LevelBounds levelBounds, BulletManager bulletManager)
        {
            _bulletPool = bulletPool;
            _levelBounds = levelBounds;
            _bulletManager = bulletManager;
        }

        public void OnFixedUpdate(float fixedDeltaTime) => CheckOutBounds();

        private void CheckOutBounds()
        {
            _cache.Clear();
            _cache.AddRange(_bulletManager.ActiveBullets);

            for (int i = 0, count = _cache.Count; i < count; i++)
            {
                Bullet bullet = _cache[i];
                if (!_levelBounds.InBounds(bullet.transform.position))
                {
                    _bulletPool.RemoveBullet(bullet);
                }
            }
        }
    }
}
