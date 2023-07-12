using System;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using OTUS_Education.Assets.Homeworks.Homework_7.Scripts.Components;
using UnityEngine;

namespace OTUS_Education.Assets.Homeworks.Homework_7.Scripts.Systems
{
    public struct DisplacementSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<CollisionComponent>> _filterMoveC;
        private readonly EcsPoolInject<MoveComponent> _poolMoveC;
        private readonly EcsPoolInject<ViewComponent> _poolViewC;
        private readonly EcsPoolInject<AttackComponent> _poolAttackC;
        private readonly EcsPoolInject<CollisionComponent> _poolCollisionC;

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filterMoveC.Value)
            {
                ref var moveC = ref _poolMoveC.Value.Get(entity);

                var CollisionC = _poolCollisionC.Value.Get(entity);

                if (CollisionC.IsInContact)
                {
                    if (!_poolAttackC.Value.Get(entity).AttackTarget)
                    {
                        moveC.MoveAlloved = false;
                    }

                    ref var viewC = ref _poolViewC.Value.Get(entity);

                    Transform transform = viewC.ViewObject.transform;

                    Vector3 direction;
                    if (moveC.MoveDirection == MoveDirections.Left)
                    {
                        direction = Vector3.left;
                    }
                    else if (moveC.MoveDirection == MoveDirections.Right)
                    {
                        direction = Vector3.right;
                    }
                    else
                    {
                        throw new Exception("Dorection not set!");
                    }
                    Debug.Log("IsInContact");

                    transform.position += transform.TransformVector(direction) * moveC.MoveSpeed * Time.deltaTime;
                }
                else
                {
                    if (!_poolAttackC.Value.Get(entity).AttackTarget)
                    {
                        moveC.MoveAlloved = true;
                    }
                }
            }
        }
    }
}