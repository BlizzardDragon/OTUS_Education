using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorageCollidingObject : MonoBehaviour
{
    [SerializeField] private CollidingObject _collidingObject;
    public CollidingObject CollidingObject => _collidingObject;
}
