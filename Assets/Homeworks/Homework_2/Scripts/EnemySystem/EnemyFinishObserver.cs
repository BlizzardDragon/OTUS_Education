using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FrameworkUnity.Architecture.DI;


namespace ShootEmUp
{
    public class EnemyFinishObserver : MonoBehaviour
    {
        private EnemiesContainer _enemiesContainer;
        private EnemySpawner _enemySpawner;


        [Inject]
        public void Construct(EnemiesContainer enemiesContainer, EnemySpawner enemyDeactivator)
        {
            _enemiesContainer = enemiesContainer;
            _enemySpawner = enemyDeactivator;
        }

        public void OnFinishGame()
        {
            foreach (var enemy in _enemiesContainer.ActiveEnemies)
            {
                _enemySpawner.UnspawnEnemy(enemy);
            }
        }
    }
}
