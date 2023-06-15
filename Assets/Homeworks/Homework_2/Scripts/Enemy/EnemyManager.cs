using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FrameworkUnity.Interfaces.Services;

// Готово.
namespace ShootEmUp
{
    public sealed class EnemyManager : MonoBehaviour, IService
    {
        private readonly HashSet<GameObject> m_activeEnemies = new();

        public event Action OnEnemySpawned;
        public event Action<GameObject> OnEnemyDestroyed;
        public event Action<Bullet.Args> OnFired;


        private IEnumerator Start()
        {
            while (true)
            {
                yield return new WaitForSeconds(1);
                OnEnemySpawned?.Invoke();
            }
        }

        public void SpawnEnemy(GameObject enemy)
        {
            if (m_activeEnemies.Add(enemy))
            {
                enemy.GetComponent<HitPointsComponent>().OnEmptyHP += OnDestroyed;
                enemy.GetComponent<EnemyAttackAgent>().OnFire += OnFire;
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