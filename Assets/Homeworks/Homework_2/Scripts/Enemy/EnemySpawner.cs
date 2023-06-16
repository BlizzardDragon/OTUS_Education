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


        public void OnStartGame() => StartCoroutine(SpawnProcess());

        private IEnumerator SpawnProcess()
        {
            while (true)
            {
                yield return new WaitForSeconds(1);
                OnSpawnTime?.Invoke();
            }
        }

        public void AddToList(GameObject enemy)
        {
            m_activeEnemies.Add(enemy);
        }

        public void RemoveFromList(GameObject enemy)
        {
            m_activeEnemies.Remove(enemy);
        }
    }
}