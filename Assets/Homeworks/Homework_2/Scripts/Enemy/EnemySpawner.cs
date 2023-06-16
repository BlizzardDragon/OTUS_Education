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
        public HashSet<GameObject> ActiveEnemies => m_activeEnemies;
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
                OnEnemySpawned?.Invoke(enemy);
            }
        }

        public void OnDestroyed(GameObject enemy)
        {
            if (m_activeEnemies.Remove(enemy))
            {
                OnEnemyDestroyed?.Invoke(enemy);
            }
        }
    }
}