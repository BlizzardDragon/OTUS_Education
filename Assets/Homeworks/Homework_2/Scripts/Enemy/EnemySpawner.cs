using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FrameworkUnity.Interfaces.Services;
using FrameworkUnity.Interfaces.Listeners.GameListeners;

// Готово.
namespace ShootEmUp
{
    public sealed class EnemySpawner : MonoBehaviour, IService, IGameStartListener
    {
        private readonly HashSet<GameObject> m_activeEnemies = new();

        public event Action OnSpawnTime;
        public event Action<GameObject> OnEnemySpawned;
        public event Action<GameObject> OnEnemyDestroyed;


        public void OnStartGame() => StartCoroutine(SpawnProcess());

        private IEnumerator SpawnProcess()
        {
            while (true)
            {
                yield return new WaitForSeconds(1);
                OnSpawnTime?.Invoke();
            }
        }

        public void SpawnEnemy(GameObject enemy)
        {
            if (m_activeEnemies.Add(enemy))
            {
                enemy.GetComponent<HitPointsComponent>().OnEmptyHP += OnDestroyed;
                OnEnemySpawned?.Invoke(enemy);
            }
        }

        private void OnDestroyed(GameObject enemy)
        {
            if (m_activeEnemies.Remove(enemy))
            {
                enemy.GetComponent<HitPointsComponent>().OnEmptyHP -= OnDestroyed;
                OnEnemyDestroyed?.Invoke(enemy);
            }
        }
    }
}