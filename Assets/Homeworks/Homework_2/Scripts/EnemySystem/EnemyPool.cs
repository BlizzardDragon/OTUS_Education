using System.Collections.Generic;
using UnityEngine;

// Готово.
namespace ShootEmUp
{
    public sealed class EnemyPool : MonoBehaviour
    {
        [Header("Spawn")]
        [SerializeField] private Transform _worldTransform;

        [Header("Pool")]
        [SerializeField] private Transform _container;
        [SerializeField] private GameObject _enemyPrefab;

        private readonly Queue<GameObject> _enemyPool = new();


        public void InstallPool(int enemyCount)
        {
            for (var i = 0; i < enemyCount; i++)
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