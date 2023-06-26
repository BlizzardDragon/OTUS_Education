using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ShootEmUp
{
    public class EnemyInstaller : MonoBehaviour
    {
        private FixedUpdater _fixedUpdater;


        public void Install(FixedUpdater fixedUpdater)
        {
            _fixedUpdater = fixedUpdater;

            _fixedUpdater.OnFixedUpdateEvent += GetComponent<EnemyMoveAgent>().TryMove;
            _fixedUpdater.OnFixedUpdateEvent += GetComponent<EnemyAttackAgent>().TryFire;
        }

        public void Uninstall()
        {
            _fixedUpdater.OnFixedUpdateEvent -= GetComponent<EnemyMoveAgent>().TryMove;
            _fixedUpdater.OnFixedUpdateEvent -= GetComponent<EnemyAttackAgent>().TryFire;
        }
    }
}
