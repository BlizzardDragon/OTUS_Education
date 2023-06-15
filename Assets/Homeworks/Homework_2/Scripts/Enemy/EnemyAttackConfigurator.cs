using System;
using UnityEngine;
using FrameworkUnity.Interfaces.Services;

// Готово.
namespace ShootEmUp
{
    public class EnemyAttackConfigurator : MonoBehaviour, IService
    {
        public event Action<Bullet.Args> OnFired;


        public void OnFire(Vector2 position, Vector2 direction)
        {
            OnFired?.Invoke(new Bullet.Args
            {
                IsPlayer = false,
                PhysicsLayer = (int)PhysicsLayer.ENEMY,
                Color = Color.red,
                Damage = 1,
                Position = position,
                Velocity = direction * 2.0f
            });
        }
    }
}