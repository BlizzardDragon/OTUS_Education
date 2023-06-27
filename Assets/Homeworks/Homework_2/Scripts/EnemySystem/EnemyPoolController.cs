using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FrameworkUnity.Architecture.DI;
using FrameworkUnity.Interfaces.Installed;


namespace ShootEmUp
{
    public class EnemyPoolController : MonoBehaviour, IInstallableOnStart
    {
        private EnemyPool _enemyPool;
        private EnemyPositions _enemyPositions;


        [Inject]
        public void Construct(EnemyPool enemyPool, EnemyPositions enemyPositions)
        {
            _enemyPool = enemyPool;
            _enemyPositions = enemyPositions;
        }

        public void InstallOnStart()
        {
            int positionCount = _enemyPositions.AttackPositionsCount;
            _enemyPool.InstallPool(positionCount);
        }
    }
}
