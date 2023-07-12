
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using OTUS_Education.Assets.Homeworks.Homework_7.Scripts.Components;
using UnityEngine;

namespace OTUS_Education.Assets.Homeworks.Homework_7.Scripts.Systems
{
    public struct CollisionHandlingSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<HitComponent>, Exc<DamageComponent>> _filterHitC;
        private readonly EcsFilterInject<Inc<CollisionStayComponent>> _filterCollisionStayC;
        private readonly EcsFilterInject<Inc<CollisionExitComponent>> _filterCollisionExitC;

        private readonly EcsPoolInject<HitComponent> _poolHitC;
        private readonly EcsPoolInject<CollisionExitComponent> _poolCollisionExitC;
        private readonly EcsPoolInject<CollisionStayComponent> _poolCollisionStayC;

        private readonly EcsPoolInject<DisplacementComponent> _poolDisplacementC;
        private readonly EcsPoolInject<CollisionComponent> _poolCollisionC;
        private readonly EcsPoolInject<MoveComponent> _poolMoveC;
        private readonly EcsPoolInject<TeamComponent> _poolTeamC;

        private readonly EcsWorldInject _world;

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filterHitC.Value)
            {
                ref var hitC = ref _poolHitC.Value.Get(entity);

                if (hitC.FirstCollide.EcsPacked.Unpack(_world.Value, out int entitiyCollide1) &&
                    hitC.SecondCollide.EcsPacked.Unpack(_world.Value, out int entitiyCollide2))
                {
                    var unitTeamC_1 = _poolTeamC.Value.Get(entitiyCollide1);
                    var unitTeamC_2 = _poolTeamC.Value.Get(entitiyCollide2);

                    if (unitTeamC_1.Team == unitTeamC_2.Team)
                    {
                        // ref var DisplacementC = ref _poolDisplacementC.Value.Get(entity);
                        ref var CollisionC_1 = ref _poolCollisionC.Value.Get(entitiyCollide1);
                        ref var CollisionC_2 = ref _poolCollisionC.Value.Get(entitiyCollide2);
                        ref var MoveC_1 = ref _poolMoveC.Value.Get(entitiyCollide1);
                        ref var MoveC_2 = ref _poolMoveC.Value.Get(entitiyCollide2);

                        // ref var DisplacementC = ref _poolDisplacementC.Value.Get(entity);

                        if (CollisionC_2.IsInContact)
                        {
                            if (MoveC_2.MoveDirection == MoveDirections.Left)
                            {
                                MoveC_1.MoveDirection = MoveDirections.Right;
                            }
                            else if (MoveC_2.MoveDirection == MoveDirections.Right)
                            {
                                MoveC_1.MoveDirection = MoveDirections.Left;
                            }
                        }
                        else
                        {
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
                        }

                        MoveC_1.MoveAlloved = false;
                        MoveC_2.MoveAlloved = false;

                        CollisionC_1.IsInContact = true;
                        CollisionC_2.IsInContact = true;
                    }
                }
            }

            foreach (var entity in _filterCollisionStayC.Value)
            {
                ref var CollisionStaC = ref _poolCollisionStayC.Value.Get(entity);

                if (CollisionStaC.FirstCollide.EcsPacked.Unpack(_world.Value, out int entitiyCollide1) &&
                    CollisionStaC.SecondCollide.EcsPacked.Unpack(_world.Value, out int entitiyCollide2))
                {
                    var unitTeamC_1 = _poolTeamC.Value.Get(entitiyCollide1);
                    var unitTeamC_2 = _poolTeamC.Value.Get(entitiyCollide2);
                    ref var MoveC_1 = ref _poolMoveC.Value.Get(entitiyCollide1);
                    ref var MoveC_2 = ref _poolMoveC.Value.Get(entitiyCollide2);

                    if (unitTeamC_1.Team == unitTeamC_2.Team)
                    {
                        _poolCollisionC.Value.Get(entitiyCollide1).IsInContact = true;

                        MoveC_1.MoveAlloved = false;
                        MoveC_2.MoveAlloved = false;
                    }
                }
            }

            foreach (var entity in _filterCollisionExitC.Value)
            {
                ref var CollisionExitC = ref _poolCollisionExitC.Value.Get(entity);

                if (CollisionExitC.LeaveObject.EcsPacked.Unpack(_world.Value, out int entitiyCollide))
                {
                    ref var MoveC_1 = ref _poolMoveC.Value.Get(entitiyCollide);

                    _poolCollisionC.Value.Get(entitiyCollide).IsInContact = false;

                    MoveC_1.MoveAlloved = true;
                    Debug.Log("false");
                }
            }
        }
    }
}