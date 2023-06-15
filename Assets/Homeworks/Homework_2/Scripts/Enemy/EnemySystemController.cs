using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FrameworkUnity.Architecture.DI;
using FrameworkUnity.Interfaces.Listeners.GameListeners;
using FrameworkUnity.Interfaces.Installed;


namespace ShootEmUp
{
    public class EnemySystemController : MonoBehaviour, IGameStartListener, IGameFinishListener, IInstallableOnStart
    {
        private EnemyPool _enemyPool;
        private EnemyPositions _enemyPositions;
        private EnemyManager _enemyManager;
        private BulletSystem _bulletSystem;


        [Inject]
        public void Construct(EnemyPool enemyPool,
            EnemyPositions enemyPositions,
            EnemyManager enemyManager,
            BulletSystem bulletSystem)
        {
            _enemyPool = enemyPool;
            _enemyPositions = enemyPositions;
            _enemyManager = enemyManager;
            _bulletSystem = bulletSystem;
        }

        public void OnStartGame()
        {
            _enemyPool.OnUnspawnEnemy += _enemyPositions.RestoreAttackPosition;
            _enemyManager.OnEnemySpawned += TryGetEnemy;
            _enemyManager.OnEnemyDestroyed += UnspawnEnemy;
            _enemyManager.OnFired += _bulletSystem.FlyBulletByArgs;
        }

        public void OnFinishGame()
        {
            _enemyPool.OnUnspawnEnemy -= _enemyPositions.RestoreAttackPosition;
            _enemyManager.OnEnemySpawned -= TryGetEnemy;
            _enemyManager.OnEnemyDestroyed -= UnspawnEnemy;
            _enemyManager.OnFired -= _bulletSystem.FlyBulletByArgs;
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
                var attackPosition = _enemyPositions.GetRandomAttackPosition();
                enemy = _enemyPool.SpawnEnemy(enemy, spawnPosition.position, attackPosition.position);

                _enemyManager.SpawnEnemy(enemy);
            }
        }

        public void UnspawnEnemy(GameObject enemy) => _enemyPool.UnspawnEnemy(enemy);
    }
}
