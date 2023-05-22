using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public class MoveComponent : MonoBehaviour, IGameStartListener, IGameFinishListener
{
    private Rigidbody _rigidbody;
    private const float ACCELERATION = 100;


    private void Awake() => _rigidbody = GetComponent<Rigidbody>();

    void IGameStartListener.OnStartGame()
    {
        ServiceLocator.GetService<MoveForwardPhysical>().OnMove += Move;
    }

    void IGameFinishListener.OnFinishGame()
    {
        ServiceLocator.GetService<MoveForwardPhysical>().OnMove -= Move;
    }

    private void Move(Vector3 direction)
    {
        Vector3 force = new Vector3(0, 0, direction.z) * ACCELERATION;
        _rigidbody.AddForce(force, ForceMode.Acceleration);
    }
}
