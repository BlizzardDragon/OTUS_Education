using System;
using UnityEngine;

// Готово.
namespace ShootEmUp
{
    public class EnemyBulletConfigProvider : MonoBehaviour
    {
        public Bullet.Args GetConfig(Vector2 position, Vector2 direction)
        {
            var config = new Bullet.Args
            {
                IsPlayer = false,
                PhysicsLayer = (int)PhysicsLayer.ENEMY,
                Color = Color.red,
                Damage = 1,
                Position = position,
                Velocity = direction * 2.0f
            };
            
            return config;
        }
    }
}
