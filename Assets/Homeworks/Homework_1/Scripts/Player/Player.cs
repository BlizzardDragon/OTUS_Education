using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour, IRoadTarget
{
    public Transform Transform => transform;
    [SerializeField] private Rigidbody _rigidbody;
    

    public void Move(Vector3 offset)
    {

    }
}
