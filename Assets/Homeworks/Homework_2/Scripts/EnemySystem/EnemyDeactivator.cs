using System.Collections;
using System.Collections.Generic;
using FrameworkUnity.Architecture.DI;
using UnityEngine;


namespace ShootEmUp
{
    public class EnemyDeactivator : MonoBehaviour
    {
        private EnemyDestroyObserver _enemyDestroyObserver;
        private EnemyFireObserver _enemyFireObserver;
        

        [Inject]
        public void Construct(EnemyDestroyObserver enemyDestroyObserver, EnemyFireObserver enemyFireObserver)
        {
            _enemyDestroyObserver = enemyDestroyObserver;
            _enemyFireObserver = enemyFireObserver;
        }
        
        public void DeactivateEnemy(GameObject enemy)
        {
            enemy.GetComponent<EnemyInstaller>().Uninstall();
            enemy.GetComponent<HitPointsComponent>().OnEmptyHP -= _enemyDestroyObserver.OnDestroyEnemy;
            enemy.GetComponent<EnemyAttackAgent>().OnFire -= _enemyFireObserver.OnEnemyFire;
        }
    }
}
