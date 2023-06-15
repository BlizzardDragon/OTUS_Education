using System;
using UnityEngine;

// Готово.
namespace ShootEmUp
{
    public sealed class HitPointsComponent : MonoBehaviour
    {
        public event Action<GameObject> OnEmptyHP;
        [SerializeField] private int _hitPoints;


        public bool IsHitPointsExists() => _hitPoints > 0;

        public void TakeDamage(int damage)
        {
            _hitPoints -= damage;
            if (_hitPoints <= 0)
            {
                OnEmptyHP?.Invoke(gameObject);
            }
        }
    }
}