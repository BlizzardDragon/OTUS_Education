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
        private readonly EcsPoolInject<HitComponent> _poolHitC;
        private readonly EcsWorldInject _world;

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filterHitC.Value)
            {
                ref var hitC = ref _poolHitC.Value.Get(entity);
            }
        }
    }
}