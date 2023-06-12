using UnityEngine;
using FrameworkUnity.Interfaces.Services;
using FrameworkUnity.Interfaces.Listeners.GameListeners;
using FrameworkUnity.Architecture.DI;

namespace ShootEmUp
{
    public sealed class CharacterController : MonoBehaviour, IService, IGameFixedUpdateListener
    {
        private Character _character;
        private GameManager _gameManager;
        private BulletSystem _bulletSystem;
        private bool _fireRequired;


        [Inject]
        public void Construct(Character character, GameManager gameManager, BulletSystem bulletSystem)
        {
            _character = character;
            _gameManager = gameManager;
            _bulletSystem = bulletSystem;
        }

        private void OnEnable() => _character.HitPointsComponent.OnEmptyHP += OnCharacterDeath;
        private void OnDisable() => _character.HitPointsComponent.OnEmptyHP -= OnCharacterDeath;

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
            var weapon = _character.WeaponComponent;
            var bulletConfig = _character.BulletConfig;
            
            _bulletSystem.FlyBulletByArgs(new BulletSystem.Args
            {
                isPlayer = true,
                physicsLayer = (int)bulletConfig.physicsLayer,
                color = bulletConfig.color,
                damage = bulletConfig.damage,
                position = weapon.Position,
                velocity = weapon.Rotation * Vector3.up * bulletConfig.speed
            });
        }
    }
}