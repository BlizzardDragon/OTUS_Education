using UnityEngine;
using FrameworkUnity.Interfaces.Installed;
using FrameworkUnity.Interfaces.Listeners.GameListeners;

// Готово.
namespace ShootEmUp
{
    public sealed class EnemyMoveAgent : MonoBehaviour, IInstallableOnAwake, IGameFixedUpdateListener
    {
        public bool IsReached => _isReached;

        private MoveComponent _moveComponent;
        private Vector2 _destination;
        private bool _isReached;


        public void InstallOnAwake() => _moveComponent = GetComponent<MoveComponent>();

        public void SetDestination(Vector2 endPoint)
        {
            _destination = endPoint;
            _isReached = false;
        }

        public void OnFixedUpdate(float fixedDeltaTime)
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

            var direction = vector.normalized * fixedDeltaTime;
            _moveComponent.MoveByRigidbodyVelocity(direction);
        }
    }
}