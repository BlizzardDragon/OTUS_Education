using System;
using UnityEngine;
using FrameworkUnity.Architecture.DI;
using FrameworkUnity.Interfaces.Listeners.GameListeners;

// Готово.
namespace ShootEmUp
{
    public class EnemyDestroyObserver : MonoBehaviour, IGameStartListener, IGameFinishListener
    {
        private ScoreManager _scoreManager;
        private EnemySpawner _enemySpawner;


        [Inject]
        public void Construct(ScoreManager scoreManager,
            EnemySpawner enemySpawner)
        {
            _scoreManager = scoreManager;
            _enemySpawner = enemySpawner;
        }

        public void OnStartGame() => _enemySpawner.OnEnemyUnspawned += OnDestroyEnemy;
        public void OnFinishGame() => _enemySpawner.OnEnemyUnspawned -= OnDestroyEnemy;

        public void OnDestroyEnemy(GameObject enemy) => _scoreManager.AddScore();
    }
}
