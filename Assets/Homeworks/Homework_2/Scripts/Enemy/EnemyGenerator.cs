using System;
using System.Collections;
using UnityEngine;
using FrameworkUnity.Interfaces.Listeners.GameListeners;

// Готово.
namespace ShootEmUp
{
    public sealed class EnemyGenerator : MonoBehaviour, IGameStartListener
    {
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
    }
}