using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FrameworkUnity.Architecture.DI;
using FrameworkUnity.Interfaces.Listeners.GameListeners;


namespace ShootEmUp
{
    public class EnemyGenerationObserver : MonoBehaviour, IGameStartListener, IGameFinishListener
    {
        private EnemyGenerator _enemyGenerator;
        private EnemySpawner _enemySpawner;


        [Inject]
        public void Construct(EnemySpawner enemySpawner, EnemyGenerator enemyGenerator)
        {
            _enemyGenerator = enemyGenerator;
            _enemySpawner = enemySpawner;
        }

        public void OnStartGame()
        {
            _enemyGenerator.OnSpawnTime += _enemySpawner.SpawnEnemy;
        }

        public void OnFinishGame()
        {
            _enemyGenerator.OnSpawnTime += _enemySpawner.SpawnEnemy;
        }
    }
}
