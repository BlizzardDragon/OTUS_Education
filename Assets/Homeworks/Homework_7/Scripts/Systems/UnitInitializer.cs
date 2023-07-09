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
            EcsPool<UnitViewComponent> poolView = world.GetPool<UnitViewComponent>();
            EcsPool<HealthComponent> poolHealth = world.GetPool<HealthComponent>();
            EcsPool<AttackComponent> poolAttack = world.GetPool<AttackComponent>();
            EcsPool<ColorComponent> poolColor = world.GetPool<ColorComponent>();
            EcsPool<MoveComponent> poolMove = world.GetPool<MoveComponent>();
            EcsPool<TeamComponent> poolTeam = world.GetPool<TeamComponent>();

            int entityCount = _sharedData.Value.UnitsPerTeam * _sharedData.Value.TeamCount;
            for (int i = 0; i < entityCount; i++)
            {
                int entity = world.NewEntity();
                poolHealth.Add(entity).Health = _sharedData.Value.UnitHealth;
                poolAttack.Add(entity);
                poolColor.Add(entity);
                poolMove.Add(entity);
                poolTeam.Add(entity);
                poolView.Add(entity);
            }
        }
    }
}