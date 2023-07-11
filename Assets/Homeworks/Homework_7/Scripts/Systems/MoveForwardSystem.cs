using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using OTUS_Education.Assets.Homeworks.Homework_7.Scripts.Components;
using UnityEngine;

namespace OTUS_Education.Assets.Homeworks.Homework_7.Scripts.Systems
{
    public struct MoveForwardSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<MoveComponent>> _filterMove;

        private readonly EcsPoolInject<ViewComponent> _poolViewC;
        private readonly EcsPoolInject<MoveComponent> _poolMoveC;

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filterMove.Value)
            {
                if (!_poolMoveC.Value.Get(entity).MoveAlloved) continue;

                ref var viewC = ref _poolViewC.Value.Get(entity);
                ref var moveC = ref _poolMoveC.Value.Get(entity);

                Transform transform = viewC.ViewObject.transform;
                transform.position += transform.forward * moveC.MoveSpeed * Time.deltaTime;
            }
        }
    }
}