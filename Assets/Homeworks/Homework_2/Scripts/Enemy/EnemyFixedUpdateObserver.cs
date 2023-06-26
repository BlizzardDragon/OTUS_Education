using System.Collections.Generic;
using UnityEngine;
using FrameworkUnity.Architecture.DI;
using FrameworkUnity.Interfaces.Installed;

// Готово.
namespace ShootEmUp
{
    public class EnemyFixedUpdateObserver : MonoBehaviour, IInstallableOnStart
    {
        public HashSet<GameObject> ActiveEnemies => _activeEnemies;
        private readonly HashSet<GameObject> _activeEnemies = new();

        private EnemyPool _enemyPool;
        private EnemyPositions _enemyPositions;
        private BulletManager _bulletSystem;
        private EnemyBulletConfigProvider _enemyBulletConfigProvider;
        private ScoreManager _scoreManager;
        private EnemyDestroyObserver _enemyDestroyObserver;


        [Inject]
        public void Construct(EnemyPool enemyPool,
            EnemyPositions enemyPositions,
            EnemyGenerator enemySpawner,
            BulletManager bulletSystem,
            EnemyBulletConfigProvider attackConfig,
            ScoreManager scoreManager,
            EnemySpawner enemyInstaller,
            EnemyDestroyObserver enemyDestroyObserver)
        {
            _enemyPool = enemyPool;
            _enemyPositions = enemyPositions;
            _bulletSystem = bulletSystem;
            _enemyBulletConfigProvider = attackConfig;
            _scoreManager = scoreManager;
            _enemyDestroyObserver = enemyDestroyObserver;
        }

        public void InstallOnStart()
        {
            int positionCount = _enemyPositions.AttackPositionsCount;
            _enemyPool.InstallPool(positionCount);
        }

        public void OnSpawnEnemy(GameObject enemy)
        {
            _activeEnemies.Add(enemy);
            enemy.GetComponent<HitPointsComponent>().OnEmptyHP += _enemyDestroyObserver.OnDestroyEnemy;
            enemy.GetComponent<EnemyAttackAgent>().OnFire += OnEnemyFire;
        }

        public void OnEnemyFire(Vector2 position, Vector2 direction)
        {
            Bullet.Args config = _enemyBulletConfigProvider.GetConfig(position, direction);
            _bulletSystem.FlyBulletByArgs(config);
        }
    }
}