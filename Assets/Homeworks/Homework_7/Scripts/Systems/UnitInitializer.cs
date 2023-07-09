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
            EcsPool<UnitViewComponent> poolViewC = world.GetPool<UnitViewComponent>();
            EcsPool<HealthComponent> poolHealthC = world.GetPool<HealthComponent>();
            EcsPool<AttackComponent> poolAttackC = world.GetPool<AttackComponent>();
            EcsPool<ColorComponent> poolColorC = world.GetPool<ColorComponent>();
            EcsPool<MoveComponent> poolMoveC = world.GetPool<MoveComponent>();
            EcsPool<TeamComponent> poolTeamC = world.GetPool<TeamComponent>();

            int teamCount = _sharedData.Value.TeamCount;
            int entityCount = _sharedData.Value.UnitsPerTeam * teamCount;
            
            for (int i = 0; i < entityCount; i++)
            {
                int entity = world.NewEntity();
                poolAttackC.Add(entity).AttackPeriod = _sharedData.Value.UnitAttackPeriod;
                poolHealthC.Add(entity).Health = _sharedData.Value.UnitHealth;
                poolMoveC.Add(entity).MoveSpeed = _sharedData.Value.UnitMoveSpeed;
                poolViewC.Add(entity);

                if (i < entityCount / teamCount)
                {
                    poolColorC.Add(entity).OriginColor = Color.blue;
                    poolTeamC.Add(entity).Team = Teams.Blue;
                }
                else
                {
                    poolColorC.Add(entity).OriginColor = Color.red;
                    poolTeamC.Add(entity).Team = Teams.Red;
                }
            }
        }
    }
}