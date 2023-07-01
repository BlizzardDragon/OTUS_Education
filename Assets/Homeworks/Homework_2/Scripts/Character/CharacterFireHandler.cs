using FrameworkUnity.Architecture.DI;
using FrameworkUnity.Interfaces.Listeners.GameListeners;
using UnityEngine;

// Готово.
namespace ShootEmUp
{
    public class CharacterFireHandler : MonoBehaviour, IGameFixedUpdateListener
    {
        private Character _character;
        private BulletSpawner _bulletSpawner;
        private bool _fireRequired;


        [Inject]
        public void Construct(Character character, BulletSpawner bulletSpawner)
        {
            _character = character;
            _bulletSpawner = bulletSpawner;
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

            _bulletSpawner.SpawnBullet(new Bullet.Args
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
