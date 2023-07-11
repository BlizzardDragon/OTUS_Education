
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using OTUS_Education.Assets.Homeworks.Homework_7.Scripts.Components;
using UnityEngine;

namespace OTUS_Education.Assets.Homeworks.Homework_7.Scripts.Systems
{
    public class UnitAttackSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<AttackComponent>> _filterAttack;
        private readonly EcsPoolInject<AttackComponent> _poolAttackC;
        private readonly EcsCustomInject<SharedData> _sharedData;
        private readonly BulletSpawner _bulletSpawner;

        private readonly EcsPoolInject<TeamComponent> _poolTeamC;

        public UnitAttackSystem(BulletSpawner bulletSpawner) => _bulletSpawner = bulletSpawner;

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filterAttack.Value)
            {
                Debug.Log(_poolTeamC.Value.Get(entity).Team);

                ref var attackC = ref _poolAttackC.Value.Get(entity);

                if (attackC.AttackIsReady)
                {
                    if (attackC.AttackTarget == null) return;

                    float rate = _sharedData.Value.UnitAttackPeriod;
                    float multiplier = _sharedData.Value.UnitAttackPeriodRandomMultiplier;
                    
                    _bulletSpawner.SpawnBullet(entity);
                    attackC.AttackIsReady = false;
                    attackC.AttackTimer = 0;
                    attackC.AttackPeriod = Random.Range(rate - rate * multiplier, rate + rate * multiplier);
                }
                else
                {
                    attackC.AttackTimer += Time.deltaTime;

                    if (attackC.AttackTimer > attackC.AttackPeriod)
                    {
                        attackC.AttackIsReady = true;
                    }
                }
            }
        }
    }
}