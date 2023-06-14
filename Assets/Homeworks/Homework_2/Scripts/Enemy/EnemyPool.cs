using System;
using System.Collections.Generic;
using UnityEngine;
using FrameworkUnity.Interfaces.Services;


namespace ShootEmUp
{
    public sealed class EnemyPool : MonoBehaviour, IService
    {
        [Header("Spawn")]
        [SerializeField] private EnemyPositions enemyPositions;
        [SerializeField] private GameObject character;
        [SerializeField] private Transform worldTransform;

        [Header("Pool")]
        [SerializeField] private Transform container;
        [SerializeField] private GameObject prefab;

        private readonly Queue<GameObject> enemyPool = new();
        private const int ENEMY_COUNT = 7;


        private void Awake()
        {
            int positionCount = enemyPositions.AttackPositionsCount;
            if (positionCount < ENEMY_COUNT)
            {
                throw new ArgumentOutOfRangeException(nameof(positionCount), 
                    "The number of enemies exceeds the number of attack points");
            }

            for (var i = 0; i < ENEMY_COUNT; i++)
            {
                var enemy = Instantiate(prefab, container);
                enemyPool.Enqueue(enemy);
            }
        }

        public GameObject SpawnEnemy()
        {
            if (!enemyPool.TryDequeue(out var enemy))
            {
                return null;
            }

            enemy.transform.SetParent(worldTransform);

            var spawnPosition = enemyPositions.RandomSpawnPosition();
            enemy.transform.position = spawnPosition.position;

            var attackPosition = enemyPositions.GetRandomAttackPosition();
            enemy.GetComponent<EnemyMoveAgent>().SetDestination(attackPosition.position);

            enemy.GetComponent<EnemyAttackAgent>().SetTarget(character);
            return enemy;
        }

        public void UnspawnEnemy(GameObject enemy)
        {
            enemy.transform.SetParent(container);
            enemyPool.Enqueue(enemy);
            enemyPositions.RestoreAttackPosition();
        }
    }
}