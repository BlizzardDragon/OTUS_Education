using UnityEngine;
using FrameworkUnity.Interfaces.Listeners.GameListeners;
using FrameworkUnity.Architecture.DI;

// Готово.
namespace ShootEmUp
{
    public sealed class MoveController : MonoBehaviour,
        IGameFixedUpdateListener,
        IGameStartListener,
        IGameFinishListener
    {
        [SerializeField] private MoveComponent _moveComponent;
        private PlayerInput _playerInput;
        private int _horizontalDirection;


        [Inject]
        public void Construct(PlayerInput playerInput) => _playerInput = playerInput;

        public void OnStartGame() => _playerInput.OnUpdateDirection += SetHorizontalDirection;
        public void OnFinishGame() => _playerInput.OnUpdateDirection -= SetHorizontalDirection;

        public void OnFixedUpdate(float fixedDeltaTime)
        {
            var direction = new Vector2(_horizontalDirection, 0) * fixedDeltaTime;
            _moveComponent.MoveByRigidbodyVelocity(direction);
        }

        private void SetHorizontalDirection(int direction) => _horizontalDirection = direction;
    }
}
