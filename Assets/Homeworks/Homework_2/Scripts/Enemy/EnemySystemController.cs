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
            _enemyManager.OnEnemySpawned += TrySpawnEnemy;
            _enemyManager.OnEnemyDestroyed += UnspawnEnemy;
            _enemyManager.OnFired += _bulletSystem.FlyBulletByArgs;
        }

        public void OnFinishGame()
        {
            _enemyPool.OnUnspawnEnemy -= _enemyPositions.RestoreAttackPosition;
            _enemyManager.OnEnemySpawned -= TrySpawnEnemy;
            _enemyManager.OnEnemyDestroyed -= UnspawnEnemy;
            _enemyManager.OnFired -= _bulletSystem.FlyBulletByArgs;
        }

        public void InstallOnStart()
        {
            int positionCount = _enemyPositions.AttackPositionsCount;
            _enemyPool.InstallPool(positionCount);
        }

        private void TrySpawnEnemy()
        {
            var enemy = GetEnemy();
            _enemyManager.TrySpawnEnemy(enemy);
        }

        public GameObject GetEnemy()
        {
            var spawnPosition = _enemyPositions.RandomSpawnPosition();
            var attackPosition = _enemyPositions.GetRandomAttackPosition();
            var enemy = _enemyPool.SpawnEnemy(spawnPosition.position, attackPosition.position);
            return enemy;
        }

        public void UnspawnEnemy(GameObject enemy) => _enemyPool.UnspawnEnemy(enemy);
    }
}
