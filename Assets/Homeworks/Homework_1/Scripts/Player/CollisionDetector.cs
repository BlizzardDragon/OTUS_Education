using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class CollisionDetector : MonoBehaviour, IInitListener
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
