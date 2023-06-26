using System;
using System.Collections.Generic;
using FrameworkUnity.Architecture.DI;
using UnityEngine;

// Готово.
namespace ShootEmUp
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private GameObject _character;

        private EnemyPool _enemyPool;
        private FixedUpdater _fixedUpdater;
        private EnemyPositions _enemyPositions;
        private EnemyFixedUpdateObserver _enemySystemController;

        public event Action<GameObject> OnEnemySpawned;


        [Inject]
        public void Construct(EnemyPool enemyPool, FixedUpdater fixedUpdater, EnemyPositions enemyPositions, EnemyFixedUpdateObserver enemySystemController)
        {
            _enemyPool = enemyPool;
            _fixedUpdater = fixedUpdater;
            _enemyPositions = enemyPositions;
            _enemySystemController = enemySystemController;
        }

        public void SpawnEnemy()
        {
            // Прочитал в Майкрософт код конвеншене, что при не явном присваивании нельзя писать var.
            // Но это ведь противоречит OCP? 
            GameObject enemy = _enemyPool.TrySpawnEnemy();
            if (enemy != null)
            {
                var spawnPosition = _enemyPositions.RandomSpawnPosition();
                var attackPosition = _enemyPositions.GetRandomAttackPosition(enemy);

                enemy.transform.position = spawnPosition.position;
                enemy.GetComponent<EnemyMoveAgent>().SetDestination(attackPosition.position);
                enemy.GetComponent<CircleCollider2DComponent>().SetActiveCollider(false);
                enemy.GetComponent<EnemyInstaller>().Install(_fixedUpdater);


                var agent = enemy.GetComponent<EnemyAttackAgent>();
                agent.SetTarget(_character);
                agent.SetAllowAttack(false);

                OnEnemySpawned?.Invoke(enemy);
            }
        }
    }
}
