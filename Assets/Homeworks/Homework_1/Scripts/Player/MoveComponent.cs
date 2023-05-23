using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public class MoveComponent : MonoBehaviour, IGameStartListener, IGameResumeListener, IGamePauseListener, IGameFinishListener
{
    private Rigidbody _rigidbody;
    private Vector3 _oldVelocity;
    private const float ACCELERATION = 50;


    private void Awake() => _rigidbody = GetComponent<Rigidbody>();

    void IGameStartListener.OnStartGame()
    {
        ServiceLocator.GetService<MoveForwardPhysical>().OnMove += Move;
        _rigidbody.isKinematic = false;
    }

    public void OnPauseGame() => StopMove();
    public void OnResumeGame() => ResumeVelocity();

    void IGameFinishListener.OnFinishGame()
    {
        ServiceLocator.GetService<MoveForwardPhysical>().OnMove -= Move;
    }

    private void Move(Vector3 direction)
    {
        Vector3 force = new Vector3(0, 0, direction.z) * ACCELERATION;
        _rigidbody.AddForce(force, ForceMode.Acceleration);
    }

    private void StopMove()
    {
        _oldVelocity = _rigidbody.velocity;
        _rigidbody.velocity = Vector3.zero;
    }

    private void ResumeVelocity() => _rigidbody.velocity = _oldVelocity;
}
