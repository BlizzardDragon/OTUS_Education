using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FrameworkUnity.Architecture.DI;
using FrameworkUnity.Interfaces.Listeners.GameListeners;
using System;

namespace ShootEmUp
{
    public class EnemyFireObserver : MonoBehaviour, IGameStartListener, IGameFinishListener
    {
        private EnemySpawner _enemySpawner;
        private BulletSpawner _bulletSpawner;
        private EnemyBulletConfigProvider _enemyBulletConfigProvider;


        [Inject]
        public void Construct(
            EnemySpawner enemySpawner,
            BulletSpawner bulletSpawner,
            EnemyBulletConfigProvider enemyBulletConfigProvider)
        {
            _enemySpawner = enemySpawner;
            _bulletSpawner = bulletSpawner;
            _enemyBulletConfigProvider = enemyBulletConfigProvider;
        }

        public void OnStartGame()
        {
            _enemySpawner.OnEnemySpawned += OnFire;
            _enemySpawner.OnEnemyUnspawned += OnDestroyEnemy;
        }

        public void OnFinishGame()
        {
            _enemySpawner.OnEnemySpawned -= OnFire;
            _enemySpawner.OnEnemyUnspawned -= OnDestroyEnemy;
        }

        private void OnFire(GameObject enemy)
        {
            var agent = enemy.GetComponent<EnemyAttackAgent>();
            agent.OnFire += OnEnemyFire;
        }

        private void OnDestroyEnemy(GameObject enemy)
        {
            var agent = enemy.GetComponent<EnemyAttackAgent>();
            agent.OnFire -= OnEnemyFire;
        }

        public void OnEnemyFire(Vector2 position, Vector2 direction)
        {
            Bullet.Args config = _enemyBulletConfigProvider.GetConfig(position, direction);
            _bulletSpawner.InvokeSpawnBullet(config);
        }
    }
}
