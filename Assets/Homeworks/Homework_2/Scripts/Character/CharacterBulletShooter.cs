using FrameworkUnity.Architecture.DI;
using UnityEngine;

// Готово.
namespace ShootEmUp
{
    public class CharacterBulletShooter : MonoBehaviour
    {
        private Character _character;
        private BulletSpawner _bulletSpawner;


        [Inject]
        public void Construct(Character character, BulletSpawner bulletSpawner)
        {
            _character = character;
            _bulletSpawner = bulletSpawner;
        }

        public void OnFlyBullet()
        {
            WeaponComponent weapon = _character.WeaponComponent;
            BulletConfig bulletConfig = _character.BulletConfig;

            _bulletSpawner.InvokeSpawnBullet(new Bullet.Args
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
