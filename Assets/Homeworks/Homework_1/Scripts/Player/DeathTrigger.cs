using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DeathTrigger : MonoBehaviour, IInitListener
{
    public Action OnEnemyKilled;


    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<EnemyCollider>())
        {
            OnEnemyKilled?.Invoke();
        }
    }
}
