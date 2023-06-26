using System.Collections.Generic;
using UnityEngine;
using FrameworkUnity.Architecture.DI;
using FrameworkUnity.Interfaces.Listeners.GameListeners;

// Готово.
namespace ShootEmUp
{
    public class EnemiesContainer : MonoBehaviour, IGameStartListener, IGameFinishListener
    {
        public HashSet<GameObject> ActiveEnemies => _activeEnemies;
        private readonly HashSet<GameObject> _activeEnemies = new();

        private EnemySpawner _enemySpawner;


        [Inject]
        public void Construct(EnemySpawner enemySpawner) => _enemySpawner = enemySpawner;

        public void OnStartGame() => _enemySpawner.OnEnemySpawned += AddActiveEnemy;
        public void OnFinishGame() => _enemySpawner.OnEnemySpawned -= AddActiveEnemy;

        public void AddActiveEnemy(GameObject enemy) => _activeEnemies.Add(enemy);
    }
}
