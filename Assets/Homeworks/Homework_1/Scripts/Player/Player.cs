using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour, IRoadTarget
{
    public Transform Transform => transform;
    private Rigidbody _rigidbody;


    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        FindRoadSpawner();
    }

    public void FindRoadSpawner() => FindObjectOfType<RoadSpawner>().AddRoadTarget(this);

    public void Move(Vector3 offset)
    {

    }

}
