using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using OTUS_Education.Assets.Homeworks.Homework_7.Scripts.Components;
using OTUS_Education.Assets.Homeworks.Homework_7.Scripts.Services;
using UnityEngine;

namespace OTUS_Education.Assets.Homeworks.Homework_7.Scripts.Systems
{
    public struct DamageSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<HitComponent>> _filterHitC;
        private readonly EcsPoolInject<HitComponent> _poolHitC;
        private readonly EcsPoolInject<DamageComponent> _poolDamegeC;
        private readonly EcsPoolInject<HealthComponent> _poolHealthC;
        private readonly EcsPoolInject<ViewComponent> _poolViewC;
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
                    var unitTeamC = _poolTeamC.Value.Get(entitiyCollide1);
                    var bulletTeamC = _poolTeamC.Value.Get(entitiyCollide2);

                    if (bulletTeamC.Team != unitTeamC.Team)
                    {
                        ref var healthC = ref _poolHealthC.Value.Get(entitiyCollide1);

                        var damageC = _poolDamegeC.Value.Get(entitiyCollide2);
                        var bulletView = _poolViewC.Value.Get(entitiyCollide2).ViewObject;

                        healthC.Health -= damageC.DamageValue;

                        Object.DestroyImmediate(bulletView);
                        _world.Value.DelEntity(entitiyCollide2);
                    }
                }
            }
        }
    }
}