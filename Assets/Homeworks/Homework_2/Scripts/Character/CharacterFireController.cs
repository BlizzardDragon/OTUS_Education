using UnityEngine;
using FrameworkUnity.Interfaces.Listeners.GameListeners;
using FrameworkUnity.Architecture.DI;

// Готово.
namespace ShootEmUp
{
    public sealed class CharacterFireController : MonoBehaviour, IGameStartListener, IGameFinishListener, IGameFixedUpdateListener
    {
        private PlayerInput _playerInput;
        private CharacterBulletShooter _characterBulletShooter;
        private bool _fireRequired;


        [Inject]
        public void Construct(PlayerInput playerInput, CharacterBulletShooter characterBulletShooter)
        {
            _playerInput = playerInput;
            _characterBulletShooter = characterBulletShooter;
        }

        public void OnStartGame() => _playerInput.OnFire += SetFireRequired;
        public void OnFinishGame() => _playerInput.OnFire -= SetFireRequired;

        public void SetFireRequired(bool value) => _fireRequired = value;

        public void OnFixedUpdate(float fixedDeltaTime)
        {
            if (_fireRequired)
            {
                _characterBulletShooter.OnFlyBullet();
                _fireRequired = false;
            }
        }
    }
}