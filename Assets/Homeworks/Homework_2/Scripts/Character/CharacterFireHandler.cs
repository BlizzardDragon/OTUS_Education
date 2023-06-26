using FrameworkUnity.Architecture.DI;
using FrameworkUnity.Interfaces.Listeners.GameListeners;
using UnityEngine;

// Готово.
namespace ShootEmUp
{
    public class CharacterFireHandler : MonoBehaviour, IGameFixedUpdateListener
    {
        private Character _character;
        private BulletManager _bulletSystem;
        private bool _fireRequired;


        [Inject]
        public void Construct(Character character, BulletManager bulletSystem)
        {
            _character = character;
            _bulletSystem = bulletSystem;
        }

        public void SetFireRequired(bool value) => _fireRequired = value;

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
