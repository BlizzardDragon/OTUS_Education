using System;
using System.Collections;
using UnityEngine;
using FrameworkUnity.Interfaces.Listeners.GameListeners;
using FrameworkUnity.Architecture.DI;

// Готово.
namespace ShootEmUp
{
    public sealed class EnemyGenerator : MonoBehaviour, IGameStartListener
    {
        private EnemySpawner _enemySpawner;


        [Inject]
        public void Construct(EnemySpawner enemySpawner) => _enemySpawner = enemySpawner;

        public void OnStartGame() => StartCoroutine(SpawnProcess());

        private IEnumerator SpawnProcess()
        {
            while (true)
            {
                yield return new WaitForSeconds(1);
                _enemySpawner.SpawnEnemy();
            }
        }
    }
}