using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using FrameworkUnity.Interfaces.Services;


namespace Homework_1
{
    public class DeathTrigger : MonoBehaviour, IService
    {
        public event Action OnEnemyKilled;


        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<EnemyCollider>())
            {
                OnEnemyKilled?.Invoke();
            }
        }
    }
}
