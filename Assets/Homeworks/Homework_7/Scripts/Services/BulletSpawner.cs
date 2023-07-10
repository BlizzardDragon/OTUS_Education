using System;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using OTUS_Education.Assets.Homeworks.Homework_7.Scripts.Components;
using UnityEngine;

namespace OTUS_Education.Assets.Homeworks.Homework_7.Scripts.Systems
{
    // Можно ли как-нибудь сделать структурой?
    public class BulletSpawner : IEcsInitSystem
    {
        private readonly EcsPoolInject<UnitViewComponent> _poolUnitViewC;
        private readonly EcsPoolInject<AttackComponent> _poolAttackC;

        private readonly EcsPoolInject<BulletViewComponent> _poolBulletViewC;
        private readonly EcsPoolInject<DamageComponent> _poolDamageC;
        private readonly EcsPoolInject<ColorComponent> _poolColorC;
        private readonly EcsPoolInject<MoveComponent> _poolMoveC;
        private readonly EcsPoolInject<TeamComponent> _poolTeamC;

        private readonly EcsCustomInject<SharedData> _sharedData;
        private EcsWorld _world;

        public void Init(IEcsSystems systems) => _world = systems.GetWorld();

        public void SpawnBullet(int entity)
        {
            var bulletEntity = _world.NewEntity();

            Vector3 targerPosition = _poolAttackC.Value.Get(entity).AttackTarget.transform.position;
            Teams team = _poolTeamC.Value.Get(entity).Team;
            Transform bulletParent;

            _poolBulletViewC.Value.Add(bulletEntity);
            _poolDamageC.Value.Add(bulletEntity).DamageValue = _sharedData.Value.BulletDamage;
            _poolColorC.Value.Add(bulletEntity);
            _poolMoveC.Value.Add(bulletEntity).MoveSpeed = _sharedData.Value.BulletMoveSpeed;
            _poolTeamC.Value.Add(bulletEntity).Team = team;

            if (team == Teams.Team_1)
            {
                bulletParent = _sharedData.Value.BulletsParentTeam_1;
            }
            else if (team == Teams.Team_2)
            {
                bulletParent = _sharedData.Value.BulletsParentTeam_2;
            }
            else
            {
                throw new Exception("Team not set");
            }

            var newBullet =
                GameObject.Instantiate(
                    Resources.Load<GameObject>(_sharedData.Value.BulletPrefabPath),
                    _poolAttackC.Value.Get(entity).BulletSpawn.position,
                    GetRotation(entity, targerPosition),
                    bulletParent);
        }

        private Quaternion GetRotation(int entity, Vector3 targerPos)
        {
            Vector3 currenPos = _poolUnitViewC.Value.Get(entity).UnitObject.transform.position;
            Vector3 direction = currenPos - targerPos;

            Quaternion rotation = Quaternion.identity * Quaternion.Euler(direction);
            return rotation;
        }
    }
}