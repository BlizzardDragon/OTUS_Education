using UnityEngine;
using FrameworkUnity.Interfaces.Listeners.GameListeners;
using FrameworkUnity.Architecture.DI;


namespace ShootEmUp
{
    public class BulletDeactivator : MonoBehaviour, IGameFinishListener
    {
        private BulletSpawner _bulletSpawner;


        [Inject]
        public void Construct(BulletSpawner bulletSpawner)
        {
            _bulletSpawner = bulletSpawner;
        }

        public void OnFinishGame() => DisableActiveBullets();

        public void DisableActiveBullets()
        {
            foreach (var bullet in _bulletSpawner.ActiveBullets)
            {
                bullet.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            }
        }
    }
}
