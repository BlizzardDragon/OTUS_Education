using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Leopotam.EcsLite.Di;
using OTUS_Education.Assets.Homeworks.Homework_7.Scripts.Components;
using UnityEngine;

namespace OTUS_Education.Assets.Homeworks.Homework_7.Scripts.Systems
{
    public struct BulletSpawner
    {
        private readonly EcsFilterInject<Inc<UnitViewComponent>> _filterUnits;
        private readonly EcsPoolInject<UnitViewComponent> _poolUnitViewC;

        private readonly EcsPoolInject<BulletViewComponent> _poolBulletViewC;
        private readonly EcsPoolInject<DamageComponent> _poolDamageC;
        private readonly EcsPoolInject<ColorComponent> _poolColorC;
        private readonly EcsPoolInject<MoveComponent> _poolMoveC;
        private readonly EcsPoolInject<TeamComponent> _poolTeamC;

        private readonly EcsCustomInject<SharedData> _sharedData;
        private readonly EcsWorldInject _world;

        public void SpawnBullet(int currentEntity, int targerEntity)
        {
            var bulletEntity = _world.Value.NewEntity();

            _poolBulletViewC.Value.Add(bulletEntity);
            _poolDamageC.Value.Add(bulletEntity).DamageValue = _sharedData.Value.BulletDamage;
            _poolColorC.Value.Add(bulletEntity);
            _poolMoveC.Value.Add(bulletEntity).MoveSpeed = _sharedData.Value.BulletMoveSpeed;
            _poolTeamC.Value.Add(bulletEntity);


        }
    }
}