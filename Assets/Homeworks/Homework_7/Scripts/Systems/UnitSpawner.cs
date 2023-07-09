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
        private readonly EcsPoolInject<ColorComponent> _poolColorC;
        private readonly EcsPoolInject<TeamComponent> _poolTeamC;

        public void Init(IEcsSystems systems)
        {

            foreach (var entity in _filterUnits.Value)
            {
                ref var colorC = ref _poolColorC.Value.Get(entity);
                ref var view = ref _poolViewC.Value.Get(entity);

                var unit =
                    GameObject.Instantiate(
                        Resources.Load<GameObject>(_sharedData.Value.UnitPrefabPath),
                        GetPosition(entity),
                        GetRotation(entity));

                MeshRenderer meshRenderer = unit.GetComponent<MeshRendererComponent>().MeshRenderer;
                colorC.MeshRenderer = meshRenderer;
                colorC.MeshRenderer.material.color = colorC.OriginColor;

                view.UnitObject = unit;
            }
        }

        private Vector3 GetPosition(int entity)
        {
            Vector3 spawnPosition;
            float count = 0;

            foreach (var item in _filterUnits.Value)
            {
                count++;
            }

            int columnNumber = entity % _sharedData.Value.ColumnCount;
            int rowNumber = Mathf.FloorToInt(count / entity);

            if (_poolTeamC.Value.Get(entity).Team == Teams.Team_1)
            {
                spawnPosition =
                    new Vector3(columnNumber, 0, -rowNumber) + _sharedData.Value.SpawnPointTeam1.position;
            }
            else if (_poolTeamC.Value.Get(entity).Team == Teams.Team_2)
            {
                spawnPosition =
                    new Vector3(columnNumber, 0, rowNumber) + _sharedData.Value.SpawnPointTeam2.position;
            }
            else
            {
                throw new Exception("Team not set");
            }

            return spawnPosition;
        }

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