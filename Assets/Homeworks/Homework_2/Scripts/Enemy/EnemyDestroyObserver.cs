using System;
using System.Collections;
using System.Collections.Generic;
using FrameworkUnity.Architecture.DI;
using UnityEngine;


namespace ShootEmUp
{
    public class EnemyDestroyObserver : MonoBehaviour
    {
        private EnemyPool _enemyPool;
        private EnemyPositions _enemyPositions;
        private ScoreManager _scoreManager;
        private EnemyDeactivator _enemyDeactivator;
        private EnemiesContainer _enemySystemController;

        public event Action OnEnemyDestroyed;


        [Inject]
        public void Construct(EnemyPool enemyPool,
            EnemyPositions enemyPositions,
            ScoreManager scoreManager,
            EnemyDeactivator enemyDeactivator,
            EnemiesContainer enemySystemController)
        {
            _enemyPool = enemyPool;
            _enemyPositions = enemyPositions;
            _scoreManager = scoreManager;
            _enemyDeactivator = enemyDeactivator;
            _enemySystemController = enemySystemController;
        }

        public void OnDestroyEnemy(GameObject enemy)
        {
            _enemyPool.UnspawnEnemy(enemy);
            _enemyPositions.RestoreAttackPosition(enemy);
            _scoreManager.AddScore();
            _enemyDeactivator.DeactivateEnemy(enemy);
            _enemySystemController.ActiveEnemies.Remove(enemy);
        }
    }
}
