using System;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using OTUS_Education.Assets.Homeworks.Homework_7.Scripts.Components;
using UnityEngine;

namespace OTUS_Education.Assets.Homeworks.Homework_7.Scripts.Systems
{
    public struct UnitSpawner : IEcsInitSystem
    {
        private readonly EcsFilterInject<Inc<UnitViewComponent>> _filterUnits;
        private readonly EcsCustomInject<SharedData> _sharedData;

        private readonly EcsPoolInject<UnitViewComponent> _poolViewC;
        private readonly EcsPoolInject<AttackComponent> _poolAttackC;
        private readonly EcsPoolInject<ColorComponent> _poolColorC;
        private readonly EcsPoolInject<TeamComponent> _poolTeamC;

        public void Init(IEcsSystems systems)
        {
            foreach (var entity in _filterUnits.Value)
            {
                ref var attackC = ref _poolAttackC.Value.Get(entity);
                ref var colorC = ref _poolColorC.Value.Get(entity);
                ref var view = ref _poolViewC.Value.Get(entity);

                var unit =
                    GameObject.Instantiate(
                        Resources.Load<GameObject>(_sharedData.Value.UnitPrefabPath),
                        GetPosition(entity),
                        GetRotation(entity));

                MeshRenderer meshRenderer = unit.GetComponent<MeshRendererComponent>().MeshRenderer;
                UnitGun unitGun = unit.GetComponent<UnitGun>();
                colorC.MeshRenderer = meshRenderer;
                colorC.MeshRenderer.material.color = colorC.OriginColor;
                attackC.BulletSpawn = unitGun.Gun;

                if (_poolTeamC.Value.Get(entity).Team == Teams.Team_1)
                {
                    unit.transform.parent = _sharedData.Value.SpawnPointTeam_1;
                }
                else if (_poolTeamC.Value.Get(entity).Team == Teams.Team_2)
                {
                    unit.transform.parent = _sharedData.Value.SpawnPointTeam_2;
                }

                view.UnitObject = unit;
            }
        }

        private Vector3 GetPosition(int entity)
        {
            Vector3 spawnPosition;
            Vector3 position;
            Vector3 offset;
            int unitIndex;
            int rowNumber;
            int columnCount = _sharedData.Value.ColumnCount;
            float unitSpawnOffset = _sharedData.Value.UnitSpawnOffset;

            if (_poolTeamC.Value.Get(entity).Team == Teams.Team_1)
            {
                unitIndex = entity;
                int columnNumber = unitIndex % columnCount;
                rowNumber = GetRowNumber(unitIndex);

                position = new Vector3(columnNumber, 0, -rowNumber);
                offset = position * unitSpawnOffset;
                spawnPosition = position + offset + _sharedData.Value.SpawnPointTeam_1.position;
            }
            else if (_poolTeamC.Value.Get(entity).Team == Teams.Team_2)
            {
                int team1Count = 0;

                foreach (var unit in _filterUnits.Value)
                {
                    if (_poolTeamC.Value.Get(unit).Team == Teams.Team_1)
                    {
                        team1Count++;
                    }
                }

                unitIndex = entity - team1Count;
                int columnNumber = unitIndex % columnCount;
                rowNumber = GetRowNumber(unitIndex);

                position = new Vector3(columnNumber, 0, rowNumber);
                offset = position * unitSpawnOffset;
                spawnPosition = position + offset + _sharedData.Value.SpawnPointTeam_2.position;
            }
            else
            {
                throw new Exception("Team not set");
            }

            return spawnPosition;
        }

        private int GetRowNumber(int unitIndex) => Mathf.CeilToInt(unitIndex / _sharedData.Value.ColumnCount);

        private Quaternion GetRotation(int entity)
        {
            Quaternion rotation;

            if (_poolTeamC.Value.Get(entity).Team == Teams.Team_2)
            {
                rotation = Quaternion.Euler(new Vector3(0, 180, 0));
            }
            else
            {
                rotation = Quaternion.identity;
            }

            return rotation;
        }
    }
}