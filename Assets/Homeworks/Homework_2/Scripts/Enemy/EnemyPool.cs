using System;
using System.Collections.Generic;
using UnityEngine;
using FrameworkUnity.Interfaces.Services;


namespace ShootEmUp
{
    public sealed class EnemyPool : MonoBehaviour, IService
    {
        [Header("Spawn")]
        [SerializeField] private Transform worldTransform;
        [SerializeField] private GameObject character;

        [Header("Pool")]
        [SerializeField] private Transform container;
        [SerializeField] private GameObject prefab;

        private readonly Queue<GameObject> enemyPool = new();
        private const int ENEMY_COUNT = 7;

        public event Action OnUnspawnEnemy;


        public void InstallPool(int positionCount)
        {
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

        public GameObject SpawnEnemy(Vector3 spawnPositon, Vector3 attackPositon)
        {
            if (!enemyPool.TryDequeue(out var enemy))
            {
                return null;
            }

            enemy.transform.SetParent(worldTransform);
            enemy.transform.position = spawnPositon;
            enemy.GetComponent<EnemyMoveAgent>().SetDestination(attackPositon);
            enemy.GetComponent<EnemyAttackAgent>().SetTarget(character);

            return enemy;
        }

        public void UnspawnEnemy(GameObject enemy)
        {
            enemy.transform.SetParent(container);
            enemyPool.Enqueue(enemy);
            OnUnspawnEnemy?.Invoke();
        }
    }
}