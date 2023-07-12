
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using OTUS_Education.Assets.Homeworks.Homework_7.Scripts.Components;

namespace OTUS_Education.Assets.Homeworks.Homework_7.Scripts.Systems
{
    public struct ExitCollisionHandlingSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<CollisionExitComponent>> _filterCollisionExitC;
        private readonly EcsPoolInject<CollisionExitComponent> _poolCollisionExitC;
        private readonly EcsPoolInject<CollisionComponent> _poolCollisionC;
        private readonly EcsWorldInject _world;

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filterCollisionExitC.Value)
            {
                ref var CollisionExitC = ref _poolCollisionExitC.Value.Get(entity);

                if (CollisionExitC.LeaveObject.EcsPacked.Unpack(_world.Value, out int entitiyCollide))
                {
                    ref var CollisionC = ref _poolCollisionC.Value.Get(entitiyCollide);

                    _poolCollisionC.Value.Get(entitiyCollide).IsInContact = false;
                    CollisionC.ContactTime = 0;
                }
            }
        }
    }
}