using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using OTUS_Education.Assets.Homeworks.Homework_7.Scripts.Components;
using UnityEngine;

namespace OTUS_Education.Assets.Homeworks.Homework_7.Scripts.Systems
{
    public struct MoveForwardSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<MoveComponent>, Exc<DamageComponent>> _filterMoveCExcDamage;
        private readonly EcsFilterInject<Inc<MoveComponent, DamageComponent>> _filterMoveCIncDamageC;

        private readonly EcsPoolInject<ViewComponent> _poolViewC;
        private readonly EcsPoolInject<MoveComponent> _poolMoveC;
        private readonly EcsPoolInject<CollisionComponent> _poolCollisionC;

        public void Run(IEcsSystems systems)
        {
            // Юниты.
            foreach (var entity in _filterMoveCExcDamage.Value)
            {
                if (!_poolMoveC.Value.Get(entity).MoveAlloved) continue;
                if (_poolCollisionC.Value.Get(entity).IsInContact) continue;
                
                Move(entity);
            }

            // Пули.
            foreach (var entity in _filterMoveCIncDamageC.Value)
            {
                Move(entity);
            }
        }

        private void Move(int entity)
        {
            ref var viewC = ref _poolViewC.Value.Get(entity);
            ref var moveC = ref _poolMoveC.Value.Get(entity);

            Transform transform = viewC.ViewObject.transform;
            transform.position += transform.forward * moveC.MoveSpeed * Time.deltaTime;
        }
    }
}