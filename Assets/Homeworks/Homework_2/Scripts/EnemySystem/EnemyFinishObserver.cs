using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FrameworkUnity.Architecture.DI;


namespace ShootEmUp
{
    public class EnemyFinishObserver : MonoBehaviour
    {
        private EnemiesContainer _enemiesContainer;
        private EnemyDeactivator _enemyDeactivator;


        [Inject]
        public void Construct(EnemiesContainer enemiesContainer, EnemyDeactivator enemyDeactivator)
        {
            _enemiesContainer = enemiesContainer;
            _enemyDeactivator = enemyDeactivator;
        }

        public void OnFinishGame()
        {
            foreach (var enemy in _enemiesContainer.ActiveEnemies)
            {
                _enemyDeactivator.DeactivateEnemy(enemy);
            }
        }
    }
}
