using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using OTUS_Education.Assets.Homeworks.Homework_7.Scripts.Components;
using UnityEngine;

namespace OTUS_Education.Assets.Homeworks.Homework_7.Scripts.Systems
{
    public struct UnitMoveSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<MoveComponent, UnitViewComponent>> _filterMove;

        public void Run(IEcsSystems systems)
        {
            var poolMoveC = _filterMove.Pools.Inc1;
            var poolViewC = _filterMove.Pools.Inc2;
            foreach (var entity in _filterMove.Value)
            {
                ref var viewC = ref poolViewC.Get(entity); 
                ref var moveC = ref poolMoveC.Get(entity); 

                Transform unit = viewC.UnitObject.transform;
                unit.position += unit.forward * moveC.MoveSpeed * Time.deltaTime;
            }
        }
    }
}