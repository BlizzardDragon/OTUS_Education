using System;
using System.Collections.Generic;
using UnityEngine;
using FrameworkUnity.Interfaces.Services;

// Готово.
namespace ShootEmUp
{
    public sealed class EnemyPool : MonoBehaviour, IService
    {
        [Header("Spawn")]
        [SerializeField] private int _enemyCount = 7;
        [SerializeField] private Transform worldTransform;
        [SerializeField] private GameObject character;

        [Header("Pool")]
        [SerializeField] private Transform container;
        [SerializeField] private GameObject prefab;

        private readonly Queue<GameObject> enemyPool = new();


        public void InstallPool(int positionCount)
        {
            if (positionCount < _enemyCount)
            {
                var message = "The number of enemies exceeds the number of attack points";
                throw new ArgumentOutOfRangeException(nameof(positionCount), message);
            }

            for (var i = 0; i < _enemyCount; i++)
            {
                var enemy = Instantiate(prefab, container);
                enemyPool.Enqueue(enemy);
            }
        }

        public GameObject TryDequeueEnemy()
        {
            if (!enemyPool.TryDequeue(out var enemy))
            {
                return null;
            }
            else
            {
                return enemy;
            }
        }

        public GameObject SpawnEnemy(GameObject enemy, Vector3 spawnPositon, Vector3 attackPositon)
        {
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
        }
    }
}