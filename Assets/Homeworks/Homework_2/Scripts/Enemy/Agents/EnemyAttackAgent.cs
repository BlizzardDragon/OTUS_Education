using UnityEngine;
using FrameworkUnity.Interfaces.Installed;
using FrameworkUnity.Interfaces.Listeners.GameListeners;

// Готово.
namespace ShootEmUp
{
    public sealed class EnemyAttackAgent : MonoBehaviour, IInstallableOnAwake, IGameFixedUpdateListener
    {
        [SerializeField] private float _countdown = 1;

        private WeaponComponent _weaponComponent;
        private EnemyMoveAgent _moveAgent;

        private GameObject _target;
        private float _currentTime;

        public delegate void FireHandler(Vector2 position, Vector2 direction);
        public event FireHandler OnFire;


        public void InstallOnAwake()
        {
            _weaponComponent = GetComponent<WeaponComponent>();
            _moveAgent = GetComponent<EnemyMoveAgent>();
        }

        public void OnFixedUpdate(float fixedDeltaTime)
        {
            if (!_moveAgent.IsReached)
            {
                return;
            }

            if (!_target.GetComponent<HitPointsComponent>().IsHitPointsExists())
            {
                return;
            }

            _currentTime -= fixedDeltaTime;
            if (_currentTime <= 0)
            {
                Fire();
                _currentTime += _countdown;
            }
        }

        public void SetTarget(GameObject target) => _target = target;
        public void Reset() => _currentTime = _countdown;

        private void Fire()
        {
            var startPosition = _weaponComponent.Position;
            var vector = (Vector2)_target.transform.position - startPosition;
            var direction = vector.normalized;
            OnFire?.Invoke(startPosition, direction);
        }
    }
}