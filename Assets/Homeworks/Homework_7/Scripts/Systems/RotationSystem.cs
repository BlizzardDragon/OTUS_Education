using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using OTUS_Education.Assets.Homeworks.Homework_7.Scripts.Components;
using UnityEngine;

namespace OTUS_Education.Assets.Homeworks.Homework_7.Scripts.Systems
{
    public struct RotationSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<RotationComponent>> _filterRotationC;

        private readonly EcsPoolInject<ViewComponent> _poolViewC;
        private readonly EcsPoolInject<RotationComponent> _poolRotationC;
        private readonly EcsPoolInject<AttackComponent> _poolAttackC;

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filterRotationC.Value)
            {
                var attackC = _poolAttackC.Value.Get(entity);
                if (!attackC.AttackTarget) continue;

                ref var viewC = ref _poolViewC.Value.Get(entity);
                var rotationC = _poolRotationC.Value.Get(entity);

                Transform transform = viewC.ViewObject.transform;
                Vector3 direction = attackC.AttackTarget.transform.position - transform.position;
                float speed = rotationC.RotationSpeed * Time.deltaTime;
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), speed);

                // float currentY = transform.eulerAngles.y;
                // float targetY = Quaternion.LookRotation(direction).eulerAngles.y;

                // float y = Mathf.MoveTowards(currentY, targetY, delta);

                // transform.eulerAngles = new Vector3(0, y, 0);
                // Debug.Log(currentY +" " + y);
            }
        }
    }
}