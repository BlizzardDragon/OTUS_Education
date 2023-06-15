using UnityEngine;
using FrameworkUnity.Interfaces.Services;
using FrameworkUnity.Interfaces.Listeners.GameListeners;
using FrameworkUnity.Architecture.DI;

// Готово.
namespace ShootEmUp
{
    public sealed class CharacterController : MonoBehaviour, 
        IService, 
        IGameFixedUpdateListener,
        IGameStartListener,
        IGameFinishListener
    {
        private PlayerInput _playerInput;
        private Character _character;
        private GameManager _gameManager;
        private BulletSystem _bulletSystem;
        private bool _fireRequired;


        [Inject]
        public void Construct(PlayerInput playerInput, Character character, GameManager gameManager, BulletSystem bulletSystem)
        {
            _playerInput = playerInput;
            _character = character;
            _gameManager = gameManager;
            _bulletSystem = bulletSystem;
        }

        public void OnStartGame()
        {
            _playerInput.OnSpacePushed += SetFireRequired;
            _character.HitPointsComponent.OnEmptyHP += OnCharacterDeath;
        }

        public void OnFinishGame()
        {
            _playerInput.OnSpacePushed -= SetFireRequired;
            _character.HitPointsComponent.OnEmptyHP -= OnCharacterDeath;
        }

        public void SetFireRequired(bool value) => _fireRequired = value;
        private void OnCharacterDeath(GameObject _) => _gameManager.FinishGame();

        public void OnFixedUpdate(float fixedDeltaTime)
        {
            if (_fireRequired)
            {
                OnFlyBullet();
                _fireRequired = false;
            }
        }

        private void OnFlyBullet()
        {
            WeaponComponent weapon = _character.WeaponComponent;
            BulletConfig bulletConfig = _character.BulletConfig;

            _bulletSystem.FlyBulletByArgs(new Bullet.Args
            {
                IsPlayer = true,
                PhysicsLayer = (int)bulletConfig.PhysicsLayer,
                Color = bulletConfig.Color,
                Damage = bulletConfig.Damage,
                Position = weapon.Position,
                Velocity = weapon.Rotation * Vector3.up * bulletConfig.Speed
            });
        }
    }
}