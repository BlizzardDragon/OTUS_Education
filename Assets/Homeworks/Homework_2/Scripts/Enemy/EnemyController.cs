using UnityEngine;

// Готово.
namespace ShootEmUp
{
    public class EnemyController : MonoBehaviour
    {
        private EnemyMoveAgent _enemyMoveAgent;
        private CircleCollider2DComponent _collider;


        private void Awake()
        {
            _enemyMoveAgent = GetComponent<EnemyMoveAgent>();
            _collider = GetComponent<CircleCollider2DComponent>();
        }

        private void OnEnable()
        {
            _enemyMoveAgent.OnReached += _collider.EnableCollider;
        }

        private void OnDisable()
        {
            _enemyMoveAgent.OnReached -= _collider.EnableCollider;
        }
    }
}
