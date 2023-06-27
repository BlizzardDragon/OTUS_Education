using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FrameworkUnity.Interfaces.Listeners.GameListeners;
using FrameworkUnity.Architecture.DI;

namespace ShootEmUp
{
    public class EnemyEmptyHPObserver : MonoBehaviour, IGameStartListener, IGameFinishListener
    {
        private EnemySpawner _enemySpawner;
        private EnemyDestroyObserver _enemyDestroyObserver;


        [Inject]
        public void Construct(EnemySpawner enemySpawner, EnemyDestroyObserver enemyDestroyObserver)
        {
            _enemySpawner = enemySpawner;
            _enemyDestroyObserver = enemyDestroyObserver;
        }

        public void OnStartGame() => _enemySpawner.OnEnemySpawned += OnSpawn;
        public void OnFinishGame() => _enemySpawner.OnEnemySpawned -= OnSpawn;

        private void OnSpawn(GameObject enemy)
        {
            enemy.GetComponent<HitPointsComponent>().OnEmptyHP += _enemyDestroyObserver.OnDestroyEnemy;
        }
    }
}
