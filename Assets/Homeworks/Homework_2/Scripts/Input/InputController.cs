using UnityEngine;
using FrameworkUnity.Interfaces.Listeners.GameListeners;
using FrameworkUnity.Interfaces.Services;
using FrameworkUnity.Architecture.DI;

// Готово.
namespace ShootEmUp
{
    public sealed class InputController : MonoBehaviour,
        IService,
        IGameFixedUpdateListener,
        IGameStartListener,
        IGameFinishListener
    {
        private PlayerInput _playerInput;
        private MoveComponent _moveComponent;
        private int _horizontalDirection;


        [Inject]
        public void Construct(PlayerInput playerInput, MoveComponent moveComponent)
        {
            _playerInput = playerInput;
            _moveComponent = moveComponent;
        }

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
