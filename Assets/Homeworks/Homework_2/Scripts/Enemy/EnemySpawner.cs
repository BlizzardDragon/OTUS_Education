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
        public event Action<Bullet.Args> OnFired;


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
                enemy.GetComponent<EnemyAttackAgent>().OnFire += OnFire;
                
                
                OnEnemySpawned?.Invoke(enemy);
            }
        }

        private void OnDestroyed(GameObject enemy)
        {
            if (m_activeEnemies.Remove(enemy))
            {
                enemy.GetComponent<HitPointsComponent>().OnEmptyHP -= OnDestroyed;
                enemy.GetComponent<EnemyAttackAgent>().OnFire -= OnFire;

                OnEnemyDestroyed?.Invoke(enemy);
            }
        }

        private void OnFire(Vector2 position, Vector2 direction)
        {
            OnFired?.Invoke(new Bullet.Args
            {
                IsPlayer = false,
                PhysicsLayer = (int)PhysicsLayer.ENEMY,
                Color = Color.red,
                Damage = 1,
                Position = position,
                Velocity = direction * 2.0f
            });
        }
    }
}