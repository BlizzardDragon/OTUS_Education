using UnityEngine;

// Готово.
namespace ShootEmUp
{
    public sealed class EnemyMoveAgent : MonoBehaviour
    {
        public bool IsReached => _isReached;

        private MoveComponent _moveComponent;
        private Vector2 _destination;
        private bool _isReached;


        private void Awake() => _moveComponent = GetComponent<MoveComponent>();

        public void SetDestination(Vector2 endPoint)
        {
            _destination = endPoint;
            _isReached = false;
        }

        public void TryMove(float fixedDeltaTime)
        {
            if (_isReached)
            {
                return;
            }

            var vector = _destination - (Vector2)transform.position;
            if (vector.magnitude <= 0.25f)
            {
                _isReached = true;
                return;
            }

            Vector2 direction = vector.normalized * fixedDeltaTime;
            _moveComponent.MoveByRigidbodyVelocity(direction);
        }
    }
}