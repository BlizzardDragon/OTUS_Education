using System.Collections.Generic;
using UnityEngine;
using FrameworkUnity.Architecture.DI;
using FrameworkUnity.Interfaces.Listeners.GameListeners;
using FrameworkUnity.Interfaces.Installed;

// Готово.
namespace ShootEmUp
{
    public class EnemySystemController : MonoBehaviour, IGameStartListener, IGameFinishListener, IInstallableOnStart
    {

        public HashSet<GameObject> ActiveEnemies => _activeEnemies;
        private readonly HashSet<GameObject> _activeEnemies = new();

        private EnemyPool _enemyPool;
        private EnemyPositions _enemyPositions;
        private EnemyGenerator _enemySpawner;
        private BulletManager _bulletSystem;
        private EnemyBulletConfigProvider _enemyBulletConfigProvider;
        private ScoreManager _scoreManager;
        private EnemySpawner _enemyInstaller;
        private FixedUpdater _fixedUpdater;


        [Inject]
        public void Construct(EnemyPool enemyPool,
            EnemyPositions enemyPositions,
            EnemyGenerator enemySpawner,
            BulletManager bulletSystem,
            EnemyBulletConfigProvider attackConfig,
            ScoreManager scoreManager,
            EnemySpawner enemyInstaller,
            FixedUpdater fixedUpdater)
        {
            _enemyPool = enemyPool;
            _enemyPositions = enemyPositions;
            _enemySpawner = enemySpawner;
            _bulletSystem = bulletSystem;
            _enemyBulletConfigProvider = attackConfig;
            _scoreManager = scoreManager;
            _enemyInstaller = enemyInstaller;
            _fixedUpdater = fixedUpdater;
        }

        public void OnStartGame()
        {
            _enemySpawner.OnSpawnTime += _enemyInstaller.SpawnEnemy;
        }

        public void OnFinishGame()
        {
            _enemySpawner.OnSpawnTime -= _enemyInstaller.SpawnEnemy;

            // Прочитал в Майкрософт код конвеншене, что при не явном присваивании нельзя писать var.
            // Но это ведь противоречит OCP? 
            foreach (var enemy in _activeEnemies)
            {
                UnsubscribeEnemy(enemy);
            }
        }

        public void InstallOnStart()
        {
            int positionCount = _enemyPositions.AttackPositionsCount;
            _enemyPool.InstallPool(positionCount);
        }

        public void OnSpawnEnemy(GameObject enemy)
        {
            _activeEnemies.Add(enemy);
            _fixedUpdater.OnFixedUpdateEvent += enemy.GetComponent<EnemyMoveAgent>().TryMove;
            _fixedUpdater.OnFixedUpdateEvent += enemy.GetComponent<EnemyAttackAgent>().TryFire;
            enemy.GetComponent<HitPointsComponent>().OnEmptyHP += OnDestroyEnemy;
            enemy.GetComponent<EnemyAttackAgent>().OnFire += OnEnemyFire;
        }

        public void OnDestroyEnemy(GameObject enemy)
        {
            UnsubscribeEnemy(enemy);
            _enemyPool.UnspawnEnemy(enemy);
            _activeEnemies.Remove(enemy);
            _enemyPositions.RestoreAttackPosition(enemy);
            _scoreManager.AddScore();
        }

        private void UnsubscribeEnemy(GameObject enemy)
        {
            _fixedUpdater.OnFixedUpdateEvent -= enemy.GetComponent<EnemyMoveAgent>().TryMove;
            _fixedUpdater.OnFixedUpdateEvent -= enemy.GetComponent<EnemyAttackAgent>().TryFire;
            enemy.GetComponent<HitPointsComponent>().OnEmptyHP -= OnDestroyEnemy;
            enemy.GetComponent<EnemyAttackAgent>().OnFire -= OnEnemyFire;
        }

        private void OnEnemyFire(Vector2 position, Vector2 direction)
        {
            Bullet.Args config = _enemyBulletConfigProvider.GetConfig(position, direction);
            _bulletSystem.FlyBulletByArgs(config);
        }
    }
}
