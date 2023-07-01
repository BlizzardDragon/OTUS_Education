using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FrameworkUnity.Interfaces.Listeners.GameListeners;
using FrameworkUnity.Architecture.DI;

// Готово.
namespace ShootEmUp
{
    public class EnemyEmptyHPObserver : MonoBehaviour, IGameStartListener, IGameFinishListener
    {
        private EnemySpawner _enemySpawner;


        [Inject]
        public void Construct(EnemySpawner enemySpawner)
        {
            _enemySpawner = enemySpawner;
        }

        public void OnStartGame()
        {
            _enemySpawner.OnEnemySpawned += OnSpawn;
            _enemySpawner.OnEnemyUnspawned += OnUnspawn;
        }

        public void OnFinishGame()
        {
            _enemySpawner.OnEnemySpawned -= OnSpawn;
            _enemySpawner.OnEnemyUnspawned -= OnUnspawn;
        }

        private void OnSpawn(GameObject enemy)
        {
            enemy.GetComponent<HitPointsComponent>().OnEmptyHP += _enemySpawner.UnspawnEnemy;
        }

        private void OnUnspawn(GameObject enemy)
        {
            enemy.GetComponent<HitPointsComponent>().OnEmptyHP -= _enemySpawner.UnspawnEnemy;
        }
    }
}
