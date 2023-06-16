using UnityEngine;

// Готово.
namespace ShootEmUp
{
    public sealed class EnemyAttackAgent : MonoBehaviour
    {
        [SerializeField] private float _countdown = 1;

        private GameObject _target;
        private Transform _weapon;
        private float _currentTime;
        private bool _attackAllowed;

        public delegate void FireHandler(Vector2 position, Vector2 direction);
        public event FireHandler OnFire;


        public void TryFire(float fixedDeltaTime)
        {
            if (!_attackAllowed) return;

            bool targetIsDead = !_target.GetComponent<HitPointsComponent>().IsHitPointsExists();
            if (targetIsDead) return;

            _currentTime -= fixedDeltaTime;
            if (_currentTime <= 0)
            {
                Fire();
                _currentTime += _countdown;
            }
        }

        public void SetWeapon(Transform weapon) => _weapon = weapon;
        public void SetTarget(GameObject target) => _target = target;
        public void Reset() => _currentTime = _countdown;
        public void SetAllowAttack(bool value) => _attackAllowed = value;

        private void Fire()
        {
            Vector2 startPosition = _weapon.position;
            var vector = (Vector2)_target.transform.position - startPosition;
            Vector2 direction = vector.normalized;
            OnFire?.Invoke(startPosition, direction);
        }
    }
}