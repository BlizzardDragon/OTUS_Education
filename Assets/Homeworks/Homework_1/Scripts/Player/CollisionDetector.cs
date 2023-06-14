using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using FrameworkUnity.Interfaces.Services;


namespace Homework_1
{
    public class CollisionDetector : MonoBehaviour, IService
    {
        public event Action OnEnemyCollision;


        private void OnCollisionEnter(Collision other)
        {
            if (other.collider.GetComponent<EnemyCollider>())
            {
                OnEnemyCollision?.Invoke();
            }
        }
    }
}
