using System.Collections.Generic;
using FrameworkUnity.Architecture.DI;
using UnityEngine;

// Готово.
namespace ShootEmUp
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private GameObject _character;

        public HashSet<GameObject> ActiveEnemies => _activeEnemies;

        private EnemyPool _enemyPool;
        private EnemyPositions _enemyPositions;
        private EnemySystemController _enemySystemController;
        
        private readonly HashSet<GameObject> _activeEnemies = new();


        [Inject]
        public void Construct(EnemyPool enemyPool, EnemyPositions enemyPositions, EnemySystemController enemySystemController)
        {
            _enemyPool = enemyPool;
            _enemyPositions = enemyPositions;
            _enemySystemController = enemySystemController;
        }

        public void SpawnEnemy()
        {
            var enemy = _enemyPool.TrySpawnEnemy();
            if (enemy != null)
            {
                var spawnPosition = _enemyPositions.RandomSpawnPosition();
                var attackPosition = _enemyPositions.GetRandomAttackPosition(enemy);

                enemy.transform.position = spawnPosition.position;
                enemy.GetComponent<EnemyMoveAgent>().SetDestination(attackPosition.position);
                enemy.GetComponent<CircleCollider2DComponent>().SetActiveCollider(false);

                var agent = enemy.GetComponent<EnemyAttackAgent>();
                agent.SetTarget(_character);
                agent.SetAllowAttack(false);

                _enemySystemController.OnSpawnEnemy(enemy);
                AddToList(enemy);
            }
        }

        public void AddToList(GameObject enemy)
        {
            _activeEnemies.Add(enemy);
        }

        public void RemoveFromList(GameObject enemy)
        {
            _activeEnemies.Remove(enemy);
        }
    }
}
