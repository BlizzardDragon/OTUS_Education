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
        private readonly EcsPoolInject<ViewComponent> _poolViewC;
        private readonly EcsPoolInject<AttackComponent> _poolAttackC;
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

            _poolDamageC.Value.Add(bulletEntity).DamageValue = _sharedData.Value.BulletDamage;
            _poolTeamC.Value.Add(bulletEntity).Team = team;

            ref var colorC = ref _poolColorC.Value.Add(bulletEntity);
            ref var view = ref _poolViewC.Value.Add(bulletEntity);

            ref var moveC = ref _poolMoveC.Value.Add(bulletEntity);
            moveC.MoveSpeed = _sharedData.Value.BulletMoveSpeed;
            moveC.MoveAlloved = true;

            if (team == Teams.Team_1)
            {
                bulletParent = _sharedData.Value.BulletsParentTeam_1;
                colorC.OriginColor = _sharedData.Value.ColorTeam1;
            }
            else if (team == Teams.Team_2)
            {
                bulletParent = _sharedData.Value.BulletsParentTeam_2;
                colorC.OriginColor = _sharedData.Value.ColorTeam2;
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

            CollidingObject cObj = newBullet.GetComponent<StorageCollidingObject>().CollidingObject;
            colorC.MeshRenderer = newBullet.GetComponent<MeshRendererComponent>().MeshRenderer;
            colorC.MeshRenderer.material.color = colorC.OriginColor;
            view.ViewObject = newBullet;
            cObj.Init(_world);
            cObj.PackEntity(bulletEntity);
        }

        private Quaternion GetRotation(int entity, Vector3 targerPos)
        {
            Vector3 currenPos = _poolViewC.Value.Get(entity).ViewObject.transform.position;
            Vector3 targetDirection = targerPos - currenPos;
            float engle = Vector3.Angle(Vector3.forward, targetDirection);

            Quaternion rotation = Quaternion.Euler(new Vector3(0, engle, 0)); //Quaternion.LookRotation(direction); 
            return rotation;
        }
    }
}