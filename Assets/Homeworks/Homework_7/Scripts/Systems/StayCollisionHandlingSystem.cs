
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using OTUS_Education.Assets.Homeworks.Homework_7.Scripts.Components;
using UnityEngine;

namespace OTUS_Education.Assets.Homeworks.Homework_7.Scripts.Systems
{
    public struct StayCollisionHandlingSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<CollisionStayComponent>> _filterCollisionStayC;
        private readonly EcsPoolInject<CollisionStayComponent> _poolCollisionStayC;

        private readonly EcsPoolInject<CollisionComponent> _poolCollisionC;
        private readonly EcsPoolInject<MoveComponent> _poolMoveC;
        private readonly EcsPoolInject<TeamComponent> _poolTeamC;

        private readonly EcsCustomInject<SharedData> _sharedDtata;
        private readonly EcsWorldInject _world;

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filterCollisionStayC.Value)
            {
                ref var CollisionStaC = ref _poolCollisionStayC.Value.Get(entity);

                if (CollisionStaC.FirstCollide.EcsPacked.Unpack(_world.Value, out int entitiyCollide1) &&
                    CollisionStaC.SecondCollide.EcsPacked.Unpack(_world.Value, out int entitiyCollide2))
                {
                    if (_poolCollisionC.Value.Has(entitiyCollide2))
                    {
                        var unitTeamC_1 = _poolTeamC.Value.Get(entitiyCollide1);
                        var unitTeamC_2 = _poolTeamC.Value.Get(entitiyCollide2);

                        if (unitTeamC_1.Team == unitTeamC_2.Team)
                        {
                            ref var CollisionC_1 = ref _poolCollisionC.Value.Get(entitiyCollide1);
                            ref var CollisionC_2 = ref _poolCollisionC.Value.Get(entitiyCollide2);

                            if (CollisionC_1.ContactTime > _sharedDtata.Value.MaxContactTime &&
                                CollisionC_2.ContactTime > _sharedDtata.Value.MaxContactTime)
                            {
                                ref var MoveC_1 = ref _poolMoveC.Value.Get(entitiyCollide1);
                                ref var MoveC_2 = ref _poolMoveC.Value.Get(entitiyCollide2);

                                float value = Random.value;
                                if (value < 0.5f)
                                {
                                    MoveC_1.MoveDirection = MoveDirections.Left;
                                    MoveC_2.MoveDirection = MoveDirections.Right;
                                }
                                else
                                {
                                    MoveC_1.MoveDirection = MoveDirections.Right;
                                    MoveC_2.MoveDirection = MoveDirections.Left;
                                }

                                CollisionC_1.ContactTime = 0;
                                CollisionC_2.ContactTime = 0;
                            }

                            CollisionC_1.ContactTime += Time.deltaTime;
                            CollisionC_2.ContactTime += Time.deltaTime;

                            _poolCollisionC.Value.Get(entitiyCollide1).IsInContact = true;
                        }
                    }
                }
            }
        }
    }
}