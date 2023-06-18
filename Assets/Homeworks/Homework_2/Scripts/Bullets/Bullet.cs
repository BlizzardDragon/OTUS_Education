using System;
using UnityEngine;

// Готово.
namespace ShootEmUp
{
    [RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer))]
    public sealed class Bullet : MonoBehaviour
    {
        public bool IsPlayer { get; set; }
        public int Damage { get; set; }

        private Rigidbody2D _rigidbody2D;
        private SpriteRenderer _spriteRenderer;
        private CircleCollider2D _circleCollider2D;

        public event Action<Bullet, GameObject> OnCollisionEntered;


        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }
        
        private void OnTriggerEnter2D(Collider2D collider2D) => OnCollisionEntered?.Invoke(this, collider2D.gameObject);
        public void SetVelocity(Vector2 velocity) => _rigidbody2D.velocity = velocity;
        public void SetPhysicsLayer(int physicsLayer) => gameObject.layer = physicsLayer;
        public void SetPosition(Vector3 position) => transform.position = position;
        public void SetColor(Color color) => _spriteRenderer.color = color;

        public struct Args
        {
            public Vector2 Position;
            public Vector2 Velocity;
            public Color Color;
            public int PhysicsLayer;
            public int Damage;
            public bool IsPlayer;
        }
    }
}