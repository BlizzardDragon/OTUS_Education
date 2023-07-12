using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using OTUS_Education.Assets.Homeworks.Homework_7.Scripts.Components;
using UnityEngine;

namespace OTUS_Education.Assets.Homeworks.Homework_7.Scripts.Systems
{
    public struct UnitInitializer : IEcsInitSystem
    {
        private readonly EcsCustomInject<SharedData> _sharedData;

        public void Init(IEcsSystems systems)
        {
            EcsWorld world = systems.GetWorld();
            EcsPool<ViewComponent> poolViewC = world.GetPool<ViewComponent>();
            EcsPool<HealthComponent> poolHealthC = world.GetPool<HealthComponent>();
            EcsPool<AttackComponent> poolAttackC = world.GetPool<AttackComponent>();
            EcsPool<ColorComponent> poolColorC = world.GetPool<ColorComponent>();
            EcsPool<MoveComponent> poolMoveC = world.GetPool<MoveComponent>();
            EcsPool<TeamComponent> poolTeamC = world.GetPool<TeamComponent>();
            EcsPool<CollisionComponent> poolCollisionC = world.GetPool<CollisionComponent>();

            int teamCount = _sharedData.Value.TeamCount;
            int entityCount = _sharedData.Value.UnitsPerTeam * teamCount;

            for (int i = 0; i < entityCount; i++)
            {
                int entity = world.NewEntity();
                poolHealthC.Add(entity).Health = _sharedData.Value.UnitHealth;
                poolMoveC.Add(entity).MoveSpeed = _sharedData.Value.UnitMoveSpeed;
                poolViewC.Add(entity);
                poolCollisionC.Add(entity);

                poolAttackC.Add(entity);
                ref var attackC = ref poolAttackC.Get(entity);
                attackC.AttackPeriod = _sharedData.Value.UnitAttackPeriod;
                attackC.AttackDistance = _sharedData.Value.UnitAttackDistance;

                if (i < entityCount / teamCount)
                {
                    poolColorC.Add(entity).OriginColor = _sharedData.Value.ColorTeam1;
                    poolTeamC.Add(entity).Team = Teams.Team_1;
                }
                else
                {
                    poolColorC.Add(entity).OriginColor = _sharedData.Value.ColorTeam2;
                    poolTeamC.Add(entity).Team = Teams.Team_2;
                }
            }
        }
    }
}