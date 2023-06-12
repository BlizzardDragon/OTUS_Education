using UnityEngine;
using FrameworkUnity.Interfaces.Services;

namespace ShootEmUp
{
    public sealed class MoveComponent : MonoBehaviour, IService
    {
        [SerializeField] private Rigidbody2D _rigidbody2D;
        [SerializeField] private float _speed = 5.0f;


        public void MoveByRigidbodyVelocity(Vector2 vector)
        {
            Vector2 nextPosition = _rigidbody2D.position + vector * _speed;
            _rigidbody2D.MovePosition(nextPosition);
        }
    }
}