
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using OTUS_Education.Assets.Homeworks.Homework_7.Scripts.Components;
using UnityEngine;

namespace OTUS_Education.Assets.Homeworks.Homework_7.Scripts.Systems
{
    public struct UnitAttackSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<AttackComponent>> _filterAttack;
        private readonly EcsPoolInject<AttackComponent> _poolAttackC;
        private readonly EcsCustomInject<SharedData> _sharedData;

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filterAttack.Value)
            {
                ref var attackC = ref _poolAttackC.Value.Get(entity);

                if (attackC.IsReady)
                {
                    // Проверка дистанции до ближайшегов врага.

                    if (true)
                    {
                        float rate = _sharedData.Value.UnitAttackPeriod;
                        float multiplier = _sharedData.Value.UnitAttackPeriodRandomMultiplier;

                        // Bullet Spawn
                        attackC.IsReady = false;
                        attackC.AttackTimer = 0;
                        attackC.AttackPeriod = Random.Range(rate - rate * multiplier, rate + rate * multiplier);

                    }
                }
                else
                {
                    if (attackC.AttackTimer < attackC.AttackPeriod)
                    {
                        attackC.AttackTimer += Time.deltaTime;
                    }
                    else
                    {
                        attackC.IsReady = true;
                    }
                }
            }

            // int count = 0;
            //     Debug.Log($"{count} : {entity} = count : entity");
            //     count++;
        }
    }
}