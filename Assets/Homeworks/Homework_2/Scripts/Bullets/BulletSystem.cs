using System.Collections.Generic;
using UnityEngine;
using FrameworkUnity.Architecture.DI;
using FrameworkUnity.Interfaces.Services;
using FrameworkUnity.Interfaces.Listeners.GameListeners;

// Готово.
namespace ShootEmUp
{
    public sealed class BulletSystem : MonoBehaviour, IService, IGameFixedUpdateListener
    {
        [SerializeField] private int _initialCount = 50;
        [Space(10)]
        [SerializeField] private Transform _container;
        [SerializeField] private Transform _worldTransform;
        [SerializeField] private Bullet _prefab;
        private LevelBounds _levelBounds;

        // Что означает префикс m_?
        private readonly Queue<Bullet> m_bulletPool = new();
        private readonly HashSet<Bullet> m_activeBullets = new();
        private readonly List<Bullet> m_cache = new();


        [Inject]
        public void Construct(LevelBounds levelBounds) => _levelBounds = levelBounds;

        private void Awake() => FillPool();
        public void OnFixedUpdate(float fixedDeltaTime) => CheckOutBounds();

        private void FillPool()
        {
            for (var i = 0; i < _initialCount; i++)
            {
                Bullet newBullet = Instantiate(_prefab, _container);
                m_bulletPool.Enqueue(newBullet);
            }
        }

        private void CheckOutBounds()
        {
            m_cache.Clear();
            m_cache.AddRange(m_activeBullets);

            for (int i = 0, count = m_cache.Count; i < count; i++)
            {
                Bullet bullet = m_cache[i];
                if (!_levelBounds.InBounds(bullet.transform.position))
                {
                    RemoveBullet(bullet);
                }
            }
        }

        public void FlyBulletByArgs(Bullet.Args args)
        {
            if (m_bulletPool.TryDequeue(out Bullet bullet))
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

            if (m_activeBullets.Add(bullet))
            {
                bullet.OnCollisionEntered += OnBulletCollision;
            }
        }

        private void OnBulletCollision(Bullet bullet, Collision2D collision)
        {
            bool isHit = BulletUtils.DealDamage(bullet, collision.gameObject);
            if (isHit)
            {
                RemoveBullet(bullet);
            }
        }

        private void RemoveBullet(Bullet bullet)
        {
            if (m_activeBullets.Remove(bullet))
            {
                bullet.OnCollisionEntered -= OnBulletCollision;
                bullet.transform.SetParent(_container);
                m_bulletPool.Enqueue(bullet);
            }
        }
    }
}