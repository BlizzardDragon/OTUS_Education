using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Homework_1
{
    public class EnemyCollider : MonoBehaviour
    {
        [SerializeField] private Enemy _enemy;


        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<DeathTrigger>())
            {
                Destroy(_enemy.gameObject);
            }
        }
    }
}
