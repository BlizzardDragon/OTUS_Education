using UnityEngine;

// Готово.
namespace ShootEmUp
{
    public class EnemyController : MonoBehaviour
    {
        private MoveComponent _moveComponent;
        private EnemyMoveAgent _enemyMoveAgent;
        private WeaponComponent _weaponComponent;
        private EnemyAttackAgent _enemyAttackAgent;
        private CircleCollider2DComponent _collider;


        private void Awake()
        {
            _moveComponent = GetComponent<MoveComponent>();
            _enemyMoveAgent = GetComponent<EnemyMoveAgent>();
            _weaponComponent = GetComponent<WeaponComponent>();
            _enemyAttackAgent = GetComponent<EnemyAttackAgent>();
            _collider = GetComponent<CircleCollider2DComponent>();

            _enemyAttackAgent.SetWeapon(_weaponComponent.FirePoint);
        }

        private void OnEnable()
        {
            _enemyMoveAgent.OnReached += _collider.SetActiveCollider;
            _enemyMoveAgent.OnReached += _enemyAttackAgent.SetAllowAttack;
            _enemyMoveAgent.OnMove += _moveComponent.MoveByRigidbodyVelocity;
        }

        private void OnDisable()
        {
            _enemyMoveAgent.OnReached -= _collider.SetActiveCollider;
            _enemyMoveAgent.OnReached -= _enemyAttackAgent.SetAllowAttack;
            _enemyMoveAgent.OnMove -= _moveComponent.MoveByRigidbodyVelocity;
        }
    }
}
