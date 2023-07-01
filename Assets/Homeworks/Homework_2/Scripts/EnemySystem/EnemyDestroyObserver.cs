using System;
using FrameworkUnity.Architecture.DI;
using UnityEngine;

// Готово.
namespace ShootEmUp
{
    public class EnemyDestroyObserver : MonoBehaviour
    {
        private EnemySpawnPool _enemyPool;
        private EnemyPositions _enemyPositions;
        private ScoreManager _scoreManager;
        private EnemyDeactivator _enemyDeactivator;
        private EnemiesContainer _enemiesController;

        public event Action OnEnemyDestroyed;


        [Inject]
        public void Construct(EnemySpawnPool enemyPool,
            EnemyPositions enemyPositions,
            ScoreManager scoreManager,
            EnemyDeactivator enemyDeactivator,
            EnemiesContainer enemySystemController)
        {
            _enemyPool = enemyPool;
            _enemyPositions = enemyPositions;
            _scoreManager = scoreManager;
            _enemyDeactivator = enemyDeactivator;
            _enemiesController = enemySystemController;
        }

        public void OnDestroyEnemy(GameObject enemy)
        {
            _enemyPool.Unspawn(enemy);
            _enemyPositions.RestoreAttackPosition(enemy);
            _scoreManager.AddScore();
            _enemyDeactivator.DeactivateEnemy(enemy);
            _enemiesController.ActiveEnemies.Remove(enemy);
        }
    }
}
