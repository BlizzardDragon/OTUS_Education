using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FrameworkUnity.Interfaces.Installed;

// Готово.
namespace ShootEmUp
{
    public class BulletPool : MonoBehaviour, IInstallableOnStart
    {
        [SerializeField, Space(10)] private int _initialCount = 50;
        [SerializeField] private Bullet _prefab;
        [SerializeField] private Transform _container;
        [SerializeField] private Transform _worldTransform;

        private readonly Queue<Bullet> _bulletPool = new();


        public void InstallOnStart() => FillPool();

        private void FillPool()
        {
            for (var i = 0; i < _initialCount; i++)
            {
                Bullet newBullet = Instantiate(_prefab, _container);
                _bulletPool.Enqueue(newBullet);
            }
        }

        public Bullet GetBullet()
        {
            if (_bulletPool.TryDequeue(out Bullet bullet))
            {
                bullet.transform.SetParent(_worldTransform);
            }
            else
            {
                bullet = Instantiate(_prefab, _worldTransform);
            }

            return bullet;
        }

        public void RemoveBullet(Bullet bullet)
        {
            bullet.transform.SetParent(_container);
            _bulletPool.Enqueue(bullet);
        }
    }
}
