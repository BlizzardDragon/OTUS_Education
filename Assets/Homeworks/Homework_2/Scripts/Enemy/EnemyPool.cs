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
        [SerializeField] private Transform _worldTransform;

        [Header("Pool")]
        [SerializeField] private Transform _container;
        [SerializeField] private GameObject _enemyPrefab;

        private readonly Queue<GameObject> _enemyPool = new();


        public void InstallPool(int positionCount)
        {
            if (positionCount < _enemyCount)
            {
                var message = "The number of enemies exceeds the number of attack points";
                throw new ArgumentOutOfRangeException(nameof(positionCount), message);
            }

            for (var i = 0; i < _enemyCount; i++)
            {
                var enemy = Instantiate(_enemyPrefab, _container);
                _enemyPool.Enqueue(enemy);
            }
        }

        public GameObject TrySpawnEnemy()
        {
            if (!_enemyPool.TryDequeue(out var enemy))
            {
                return null;
            }
            else
            {
                enemy.transform.SetParent(_worldTransform);
                return enemy;
            }
        }

        public void UnspawnEnemy(GameObject enemy)
        {
            enemy.transform.SetParent(_container);
            _enemyPool.Enqueue(enemy);
        }
    }
}