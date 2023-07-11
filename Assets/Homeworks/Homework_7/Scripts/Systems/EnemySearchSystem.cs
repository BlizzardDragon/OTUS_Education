using System;
using System.Collections.Generic;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using OTUS_Education.Assets.Homeworks.Homework_7.Scripts.Components;
using UnityEngine;

namespace OTUS_Education.Assets.Homeworks.Homework_7.Scripts.Systems
{
    // Можно ли как-нибудь сделать структурой?
    public class EnemySearchSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<AttackComponent>> _filterTeanms;

        private readonly EcsPoolInject<AttackComponent> _poolAttackC;
        private readonly EcsPoolInject<TeamComponent> _poolTeamC;
        private readonly EcsPoolInject<ViewComponent> _poolViewC;
        private readonly EcsPoolInject<MoveComponent> _poolMoveC;

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
                else
                {
                    throw new Exception("Team not set");
                }
            }

            TrySetTargetsForEntities(_cacheTeam_1, _cacheTeam_2);
            TrySetTargetsForEntities(_cacheTeam_2, _cacheTeam_1);
        }

        private void TrySetTargetsForEntities(HashSet<int> currentEntities, HashSet<int> targetEntities)
        {
            foreach (var entity in currentEntities)
            {
                ref var attackC = ref _poolAttackC.Value.Get(entity);
                ref var moveC = ref _poolMoveC.Value.Get(entity);

                if (!attackC.AttackTarget)
                {
                    (bool targetIsReceived, int targetEntity) = GetNearestEntity(entity, targetEntities);

                    if (targetIsReceived)
                    {
                        GameObject targer = _poolViewC.Value.Get(targetEntity).ViewObject;
                        attackC.AttackTarget = targer;
                    }
                    
                    moveC.MoveAlloved = true;
                }
                else
                {
                    moveC.MoveAlloved = false;
                }
            }
        }

        private (bool, int) GetNearestEntity(int currentEntity, HashSet<int> targetEntities)
        {
            float attackDistance = _poolAttackC.Value.Get(currentEntity).AttackDistance;
            Vector3 currentEntityPos = _poolViewC.Value.Get(currentEntity).ViewObject.transform.position;
            Vector3 targetEntityPos;

            float minDistance = float.MaxValue;
            int nearestTargetEntity = int.MaxValue;

            foreach (var targetEntity in targetEntities)
            {
                targetEntityPos = _poolViewC.Value.Get(targetEntity).ViewObject.transform.position;
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