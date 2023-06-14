using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FrameworkUnity.Interfaces.Services;


namespace ShootEmUp
{
    public sealed class EnemyManager : MonoBehaviour, IService
    {
        [SerializeField]
        private EnemyPool _enemyPool;

        [SerializeField]
        private BulletSystem _bulletSystem;
        
        private readonly HashSet<GameObject> m_activeEnemies = new();

        private IEnumerator Start()
        {
            while (true)
            {
                yield return new WaitForSeconds(1);
                var enemy = _enemyPool.SpawnEnemy();
                if (enemy != null)
                {
                    if (m_activeEnemies.Add(enemy))
                    {
                        enemy.GetComponent<HitPointsComponent>().OnEmptyHP += OnDestroyed;
                        enemy.GetComponent<EnemyAttackAgent>().OnFire += OnFire;
                    }    
                }
            }
        }

        private void OnDestroyed(GameObject enemy)
        {
            if (m_activeEnemies.Remove(enemy))
            {
                enemy.GetComponent<HitPointsComponent>().OnEmptyHP -= OnDestroyed;
                enemy.GetComponent<EnemyAttackAgent>().OnFire -= OnFire;

                _enemyPool.UnspawnEnemy(enemy);
            }
        }

        private void OnFire(GameObject enemy, Vector2 position, Vector2 direction)
        {
            _bulletSystem.FlyBulletByArgs(new Bullet.Args
            {
                IsPlayer = false,
                PhysicsLayer = (int) PhysicsLayer.ENEMY,
                Color = Color.red,
                Damage = 1,
                Position = position,
                Velocity = direction * 2.0f
            });
        }
    }
}