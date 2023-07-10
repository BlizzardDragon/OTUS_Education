using System;
using Leopotam.EcsLite.Di;
using OTUS_Education.Assets.Homeworks.Homework_7.Scripts.Components;
using UnityEngine;

namespace OTUS_Education.Assets.Homeworks.Homework_7.Scripts.Systems
{
    public struct BulletSpawner
    {
        private readonly EcsPoolInject<UnitViewComponent> _poolUnitViewC;
        private readonly EcsPoolInject<AttackComponent> _poolAttackC;

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

            Teams team = _poolTeamC.Value.Get(currentEntity).Team;
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
                        _poolAttackC.Value.Get(currentEntity).BulletSpawn.position,
                        GetRotation(currentEntity, targerEntity),
                        bulletParent);
        }

        private Quaternion GetRotation(int currentEntity, int targerEntity)
        {
            Vector3 currenPos = _poolUnitViewC.Value.Get(currentEntity).UnitObject.transform.position;
            Vector3 targerPos = _poolUnitViewC.Value.Get(targerEntity).UnitObject.transform.position;

            Vector3 direction = currenPos - targerPos;

            Quaternion rotation = Quaternion.identity * Quaternion.Euler(direction);
            return rotation;
        }
    }
}