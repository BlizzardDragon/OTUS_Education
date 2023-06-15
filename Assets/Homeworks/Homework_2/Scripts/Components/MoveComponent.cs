using UnityEngine;
using FrameworkUnity.Interfaces.Services;
using FrameworkUnity.Interfaces.Installed;

// Готово.
namespace ShootEmUp
{
    public sealed class MoveComponent : MonoBehaviour, IService, IInstallableOnAwake
    {
        [SerializeField] private float _speed = 5.0f;
        private Rigidbody2D _rigidbody2D;


        public void InstallOnAwake() => _rigidbody2D = GetComponent<Rigidbody2D>();

        public void MoveByRigidbodyVelocity(Vector2 vector)
        {
            Vector2 nextPosition = _rigidbody2D.position + vector * _speed;
            _rigidbody2D.MovePosition(nextPosition);
        }
    }
}