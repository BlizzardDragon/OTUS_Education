using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using OTUS_Education.Assets.Homeworks.Homework_7.Scripts.Components;

namespace OTUS_Education.Assets.Homeworks.Homework_7.Scripts.Systems
{
    public struct CollisionHandlingSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<HitComponent>> _filterHitC;
        private readonly EcsFilterInject<Inc<CollisionExitComponent>> _filterCollisionExitC;
        private readonly EcsFilterInject<Inc<CollisionStayComponent>> _filterCollisionStayC;

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
                ref var CollisionExitC = ref _poolCollisionExitC.Value.Get(entity);
                ref var CollisionStayC = ref _poolCollisionStayC.Value.Get(entity);

                if (hitC.FirstCollide.EcsPacked.Unpack(_world.Value, out int entitiyCollide1) &&
                    hitC.SecondCollide.EcsPacked.Unpack(_world.Value, out int entitiyCollide2))
                {
                    var unitTeamC_1 = _poolTeamC.Value.Get(entitiyCollide1);
                    var unitTeamC_2 = _poolTeamC.Value.Get(entitiyCollide2);

                    if (unitTeamC_1.Team == unitTeamC_2.Team)
                    {

                    }
                }


                _world.Value.DelEntity(entity);
            }
        }
    }
}