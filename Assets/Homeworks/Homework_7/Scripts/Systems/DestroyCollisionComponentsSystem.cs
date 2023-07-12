using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using OTUS_Education.Assets.Homeworks.Homework_7.Scripts.Components;
using UnityEngine;

namespace OTUS_Education.Assets.Homeworks.Homework_7.Scripts.Systems
{
    public struct DestroyCollisionComponentsSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<HitComponent>> _filterHitC;
        private readonly EcsFilterInject<Inc<CollisionExitComponent>> _filterCollisionExitC;
        private readonly EcsFilterInject<Inc<CollisionStayComponent>> _filterCollisionStayC;
        private readonly EcsWorldInject _world;

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filterHitC.Value)
            {
                _world.Value.DelEntity(entity);
            }

            foreach (var entity in _filterCollisionExitC.Value)
            {
                _world.Value.DelEntity(entity);
            }
            
            foreach (var entity in _filterCollisionStayC.Value)
            {
                _world.Value.DelEntity(entity);
            }
        }
    }
}