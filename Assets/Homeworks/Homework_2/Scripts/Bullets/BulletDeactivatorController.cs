using FrameworkUnity.Architecture.DI;
using FrameworkUnity.Interfaces.Listeners.GameListeners;
using UnityEngine;

// Готово.
namespace ShootEmUp
{
    public class BulletDeactivatorController : MonoBehaviour, IGameFinishListener
    {
        private BulletSpawner _bulletSpawner;
        private BulletDeactivator _bulletDeactivator;


        [Inject]
        public void Construct(BulletSpawner bulletSpawner, BulletDeactivator bulletDeactivator)
        {
            _bulletSpawner = bulletSpawner;
            _bulletDeactivator = bulletDeactivator;
        }

        public void OnFinishGame() => _bulletDeactivator.DisableActiveBullets(_bulletSpawner.ActiveBullets);
    }
}
