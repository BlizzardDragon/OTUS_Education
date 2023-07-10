using System.Collections.Generic;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using OTUS_Education.Assets.Homeworks.Homework_7.Scripts.Components;
using UnityEngine;

namespace OTUS_Education.Assets.Homeworks.Homework_7.Scripts.Systems
{
    public class EnemySearchSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<TeamComponent, UnitViewComponent>> _filterTeanms;

        private readonly EcsPoolInject<TeamComponent> _poolTeamC;
        private readonly EcsPoolInject<UnitViewComponent> _poolViewC;
        private readonly EcsPoolInject<AttackComponent> _poolAttackC;

        private readonly HashSet<int> _cacheTeam_1 = new();
        private readonly HashSet<int> _cacheTeam_2 = new();

        public void Run(IEcsSystems systems)
        {
            _cacheTeam_1.Clear();
            _cacheTeam_2.Clear();

            foreach (var entity in _filterTeanms.Value)
            {
                if (_poolTeamC.Value.Get(entity).Team == Teams.Team_1)
                {
                    _cacheTeam_1.Add(entity);
                }
                else if (_poolTeamC.Value.Get(entity).Team == Teams.Team_2)
                {
                    _cacheTeam_2.Add(entity);
                }
            }

            TrySetTargetsForEntities(_cacheTeam_1, _cacheTeam_2);
            TrySetTargetsForEntities(_cacheTeam_2, _cacheTeam_1);
        }

        private void TrySetTargetsForEntities(HashSet<int> currentEntities, HashSet<int> targetEntities)
        {
            foreach (var entity in currentEntities)
            {
                if (!_poolAttackC.Value.Get(entity).AttackTarget)
                {
                    (bool targetIsReceived, int targetEntity) = GetNearestEntity(entity, targetEntities);
                    if (targetIsReceived)
                    {
                        GameObject targer = _poolViewC.Value.Get(targetEntity).UnitObject;
                        _poolAttackC.Value.Get(entity).AttackTarget = targer;
                    }
                }
            }
        }

        private (bool, int) GetNearestEntity(int currentEntity, HashSet<int> targetEntities)
        {
            float attackDistance = _poolAttackC.Value.Get(currentEntity).AttackDistance;
            Vector3 currentEntityPos = _poolViewC.Value.Get(currentEntity).UnitObject.transform.position;
            Vector3 targetEntityPos;

            float minDistance = float.MaxValue;
            int nearestTargetEntity = int.MaxValue;

            foreach (var targetEntity in targetEntities)
            {
                targetEntityPos = _poolViewC.Value.Get(targetEntity).UnitObject.transform.position;
                float distance = Vector3.Distance(targetEntityPos, currentEntityPos);

                if (distance > attackDistance) continue;

                if (distance < minDistance)
                {
                    minDistance = distance;
                    nearestTargetEntity = targetEntity;
                }
            }

            if (nearestTargetEntity != int.MaxValue)
            {
                return (true, nearestTargetEntity);
            }
            else
            {
                return (false, nearestTargetEntity);
            }
        }
    }
}