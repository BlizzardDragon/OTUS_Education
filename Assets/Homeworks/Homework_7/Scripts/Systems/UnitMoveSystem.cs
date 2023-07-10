using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using OTUS_Education.Assets.Homeworks.Homework_7.Scripts.Components;
using UnityEngine;

namespace OTUS_Education.Assets.Homeworks.Homework_7.Scripts.Systems
{
    public struct UnitMoveSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<UnitViewComponent>> _filterUnit;

        private readonly EcsPoolInject<UnitViewComponent> _poolViewC;
        private readonly EcsPoolInject<AttackComponent> _poolAttackC;
        private readonly EcsPoolInject<MoveComponent> _poolMoveC;

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filterUnit.Value)
            {
                if (_poolAttackC.Value.Get(entity).AttackTarget) continue;

                ref var viewC = ref _poolViewC.Value.Get(entity);
                ref var moveC = ref _poolMoveC.Value.Get(entity);

                Transform unit = viewC.UnitObject.transform;
                unit.position += unit.forward * moveC.MoveSpeed * Time.deltaTime;
            }
        }
    }
}