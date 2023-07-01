using UnityEngine;
using FrameworkUnity.Architecture.DI;
using FrameworkUnity.Interfaces.Installed;
using System;

// Готово.
namespace ShootEmUp
{
    public class EnemyPoolController : MonoBehaviour, IInstallableOnStart
    {
        [SerializeField] private int _enemyCount = 7;

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

            if (positionCount < _enemyCount)
            {
                var message = "The number of enemies exceeds the number of attack points";
                throw new ArgumentOutOfRangeException(nameof(positionCount), message);
            }
            else
            {
                _enemyPool.InstallPool(_enemyCount);
            }
        }
    }
}
