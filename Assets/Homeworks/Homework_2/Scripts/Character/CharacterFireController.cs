using UnityEngine;
using FrameworkUnity.Interfaces.Listeners.GameListeners;
using FrameworkUnity.Architecture.DI;

// Готово.
namespace ShootEmUp
{
    public sealed class CharacterFireController : MonoBehaviour, IGameStartListener, IGameFinishListener
    {
        private PlayerInput _playerInput;
        private CharacterFireHandler _characterFireHandler;


        [Inject]
        public void Construct(PlayerInput playerInput, CharacterFireHandler characterFireHandler)
        {
            _playerInput = playerInput;
            _characterFireHandler = characterFireHandler;
        }

        public void OnStartGame() => _playerInput.OnSpacePushed += _characterFireHandler.SetFireRequired;
        public void OnFinishGame() => _playerInput.OnSpacePushed -= _characterFireHandler.SetFireRequired;
    }
}