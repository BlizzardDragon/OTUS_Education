using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    public class EnemyAttackConfigurator : MonoBehaviour
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
