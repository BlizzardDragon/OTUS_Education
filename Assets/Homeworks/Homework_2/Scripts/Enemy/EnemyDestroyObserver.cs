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
        private FixedUpdater _fixedUpdater;
        private EnemySystemController _enemySystemController;


        [Inject]
        public void Construct(EnemyPool enemyPool,
            EnemyPositions enemyPositions,
            ScoreManager scoreManager,
            FixedUpdater fixedUpdater,
            EnemySystemController enemySystemController)
        {
            _enemyPool = enemyPool;
            _enemyPositions = enemyPositions;
            _scoreManager = scoreManager;
            _fixedUpdater = fixedUpdater;
            _enemySystemController = enemySystemController;
        }

        public void OnFinishGame()
        {
            foreach (var enemy in _enemySystemController.ActiveEnemies)
            {
                UnsubscribeEnemy(enemy);
            }
        }

        public void OnDestroyEnemy(GameObject enemy)
        {
            _enemySystemController.ActiveEnemies.Remove(enemy);
            UnsubscribeEnemy(enemy);
            _enemyPool.UnspawnEnemy(enemy);
            _enemyPositions.RestoreAttackPosition(enemy);
            _scoreManager.AddScore();
        }

        private void UnsubscribeEnemy(GameObject enemy)
        {
            _fixedUpdater.OnFixedUpdateEvent -= enemy.GetComponent<EnemyMoveAgent>().TryMove;
            _fixedUpdater.OnFixedUpdateEvent -= enemy.GetComponent<EnemyAttackAgent>().TryFire;
            enemy.GetComponent<HitPointsComponent>().OnEmptyHP -= OnDestroyEnemy;
            enemy.GetComponent<EnemyAttackAgent>().OnFire -= _enemySystemController.OnEnemyFire;
        }
    }
}
