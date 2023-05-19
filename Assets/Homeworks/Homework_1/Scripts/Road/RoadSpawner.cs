using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadSpawner : MonoBehaviour
{
    [SerializeField] private IRoadTarget _roadTarget;
    private Transform _targetTransform;
    private float _targetStartPosZ;

    private void Start()
    {
        _targetTransform = _roadTarget.Transform;
    }
}
