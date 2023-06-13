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
        private CharacterController _characterController;
        private int _horizontalDirection;


        [Inject]
        public void Construct(PlayerInput playerInput, MoveComponent moveComponent, CharacterController characterController)
        {
            _playerInput = playerInput;
            _moveComponent = moveComponent;
            _characterController = characterController;
        }

        public void OnStartGame()
        {
            _playerInput.OnUpdateSpace += SetFireRequired;
            _playerInput.OnUpdateDirection += SetHorizontalDirection;
        }

        public void OnFinishGame()
        {
            _playerInput.OnUpdateSpace -= SetFireRequired;
            _playerInput.OnUpdateDirection -= SetHorizontalDirection;
        }

        public void OnFixedUpdate(float fixedDeltaTime)
        {
            Vector2 direction = new Vector2(_horizontalDirection, 0) * fixedDeltaTime;
            _moveComponent.MoveByRigidbodyVelocity(direction);
        }

        private void SetFireRequired(bool value) => _characterController.SetFireRequired(value);
        private void SetHorizontalDirection(int direction) => _horizontalDirection = direction;
    }
}
