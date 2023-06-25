using System.Collections.Generic;
using UnityEngine;
using FrameworkUnity.Architecture.DI;
using FrameworkUnity.Interfaces.Listeners.GameListeners;
using FrameworkUnity.Interfaces.Installed;

// Готово.
namespace ShootEmUp
{
    public sealed class BulletSystem : MonoBehaviour, IGameFixedUpdateListener, IInstallableOnStart
    {
        [SerializeField] private int _initialCount = 50;
        [Space(10)]
        [SerializeField] private Transform _container;
        [SerializeField] private Transform _worldTransform;
        [SerializeField] private Bullet _prefab;
        private LevelBounds _levelBounds;

        private readonly Queue<Bullet> _bulletPool = new();
        private readonly HashSet<Bullet> _activeBullets = new();
        private readonly List<Bullet> _cache = new();


        [Inject]
        public void Construct(LevelBounds levelBounds) => _levelBounds = levelBounds;

        public void InstallOnStart() => FillPool();

        public void OnFixedUpdate(float fixedDeltaTime) => CheckOutBounds();

        private void FillPool()
        {
            for (var i = 0; i < _initialCount; i++)
            {
                Bullet newBullet = Instantiate(_prefab, _container);
                _bulletPool.Enqueue(newBullet);
            }
        }

        private void CheckOutBounds()
        {
            _cache.Clear();
            _cache.AddRange(_activeBullets);

            for (int i = 0, count = _cache.Count; i < count; i++)
            {
                Bullet bullet = _cache[i];
                if (!_levelBounds.InBounds(bullet.transform.position))
                {
                    RemoveBullet(bullet);
                }
            }
        }

        public void FlyBulletByArgs(Bullet.Args args)
        {
            if (_bulletPool.TryDequeue(out Bullet bullet))
            {
                bullet.transform.SetParent(_worldTransform);
            }
            else
            {
                bullet = Instantiate(_prefab, _worldTransform);
            }

            bullet.SetPosition(args.Position);
            bullet.SetColor(args.Color);
            bullet.SetPhysicsLayer(args.PhysicsLayer);
            bullet.Damage = args.Damage;
            bullet.IsPlayer = args.IsPlayer;
            bullet.SetVelocity(args.Velocity);

            if (_activeBullets.Add(bullet))
            {
                bullet.OnCollisionEntered += OnBulletCollision;
            }
        }

        private void OnBulletCollision(Bullet bullet, GameObject gameObj)
        {
            bool isHit = BulletUtils.DealDamage(bullet, gameObj);
            if (isHit)
            {
                RemoveBullet(bullet);
            }
        }

        private void RemoveBullet(Bullet bullet)
        {
            if (_activeBullets.Remove(bullet))
            {
                bullet.OnCollisionEntered -= OnBulletCollision;
                bullet.transform.SetParent(_container);
                _bulletPool.Enqueue(bullet);
            }
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