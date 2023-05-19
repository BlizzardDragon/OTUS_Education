using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadSpawner : MonoBehaviour
{
    [SerializeField] private IRoadTarget _roadTarget;
    private float _targetStartPosZ;

    private void Start()
    {
        _targetStartPosZ = _roadTarget.GetPosition().z;
    }
}
