using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using OTUS_Education.Assets.Homeworks.Homework_7.Scripts.Components;

namespace OTUS_Education.Assets.Homeworks.Homework_7.Scripts.Systems
{
    public struct DestroyCollisionComponentsSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<HitComponent, CollisionExitComponent, CollisionStayComponent>> _filterCollisions;
        private readonly EcsWorldInject _world;

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filterCollisions.Value)
            {
                _world.Value.DelEntity(entity);
            }
        }
    }
}