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
        private EnemyPool _enemyPool;
        private EnemyPositions _enemyPositions;
        private EnemySpawner _enemySpawner;
        private BulletSystem _bulletSystem;
        private EnemyAttackConfigurator _attackConfig;
        private FixedUpdater _fixedUpdater;


        [Inject]
        public void Construct(EnemyPool enemyPool,
            EnemyPositions enemyPositions,
            EnemySpawner enemySpawner,
            BulletSystem bulletSystem,
            EnemyAttackConfigurator attackConfig,
            FixedUpdater fixedUpdater)
        {
            _enemyPool = enemyPool;
            _enemyPositions = enemyPositions;
            _enemySpawner = enemySpawner;
            _bulletSystem = bulletSystem;
            _attackConfig = attackConfig;
            _fixedUpdater = fixedUpdater;
        }

        public void OnStartGame()
        {
            _enemySpawner.OnSpawnTime += TryGetEnemy;
            _enemySpawner.OnEnemySpawned += OnSpawnEnemy;
            _enemySpawner.OnEnemyDestroyed += OnUnspawnEnemy;
            _attackConfig.OnFired += _bulletSystem.FlyBulletByArgs;
        }

        public void OnFinishGame()
        {
            _enemySpawner.OnSpawnTime -= TryGetEnemy;
            _enemySpawner.OnEnemyDestroyed -= OnUnspawnEnemy;
            _attackConfig.OnFired -= _bulletSystem.FlyBulletByArgs;
            _bulletSystem.DisableActiveBullets();

            HashSet<GameObject> enemies = _enemySpawner.ActiveEnemies;
            foreach (var enemy in enemies)
            {
                UnsubscribeEnemy(enemy);
            }
        }

        public void InstallOnStart()
        {
            int positionCount = _enemyPositions.AttackPositionsCount;
            _enemyPool.InstallPool(positionCount);
        }

        public void TryGetEnemy()
        {
            var enemy = _enemyPool.TryDequeueEnemy();
            if (enemy != null)
            {
                var spawnPosition = _enemyPositions.RandomSpawnPosition();
                var attackPosition = _enemyPositions.GetRandomAttackPosition(enemy);
                enemy = _enemyPool.SpawnEnemy(enemy, spawnPosition.position, attackPosition.position);

                _enemySpawner.SpawnEnemy(enemy);
            }
        }

        private void OnSpawnEnemy(GameObject enemy)
        {
            _fixedUpdater.OnFixedUpdateEvent += enemy.GetComponent<EnemyMoveAgent>().TryMove;
            _fixedUpdater.OnFixedUpdateEvent += enemy.GetComponent<EnemyAttackAgent>().TryFire;
            enemy.GetComponent<EnemyAttackAgent>().OnFire += _attackConfig.OnFire;
        }

        public void OnUnspawnEnemy(GameObject enemy)
        {
            UnsubscribeEnemy(enemy);
            _enemyPool.UnspawnEnemy(enemy);
            _enemyPositions.RestoreAttackPosition(enemy);
        }

        private void UnsubscribeEnemy(GameObject enemy)
        {
            _fixedUpdater.OnFixedUpdateEvent -= enemy.GetComponent<EnemyMoveAgent>().TryMove;
            _fixedUpdater.OnFixedUpdateEvent -= enemy.GetComponent<EnemyAttackAgent>().TryFire;
            enemy.GetComponent<EnemyAttackAgent>().OnFire -= _attackConfig.OnFire;
        }
    }
}
