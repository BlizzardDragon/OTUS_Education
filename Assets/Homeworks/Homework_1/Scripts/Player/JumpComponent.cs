using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(Rigidbody))]
public class JumpComponent : MonoBehaviour, IGameStartListener, IGameResumeListener, IGamePauseListener, IGameFinishListener
{
    [SerializeField] private Transform _view;
    [SerializeField] private Transform[] _jumpTargets;
    private Rigidbody _rigidbody;
    private Coroutine _coroutine;
    private int _currentPosition = 1;
    private int _nextPosition;
    private bool _isGrounded = true;
    private bool _isPlaying;
    private const float JUMP_DURATION = 0.2f;


    private void Awake() => _rigidbody = GetComponent<Rigidbody>();

    public void OnStartGame() => Subscribe();
    public void OnResumeGame() => Subscribe();
    public void OnPauseGame() => Unsubscribe();
    public void OnFinishGame() => Unsubscribe();

    private void Subscribe()
    {
        ServiceLocator.GetService<JumpInput>().OnMovedToSide += PrepareToJump;
        _isPlaying = true;
    }

    private void Unsubscribe()
    {
        ServiceLocator.GetService<JumpInput>().OnMovedToSide -= PrepareToJump;
        _isPlaying = false;
    }

    private void PrepareToJump(int offsetDirection)
    {
        if (_isGrounded)
        {
            int newPosition = _currentPosition + offsetDirection;
            if (newPosition < 0)
            {
                newPosition = 0;
            }
            if (newPosition > _jumpTargets.Length - 1)
            {
                newPosition = _jumpTargets.Length - 1;
            }
            _coroutine = StartCoroutine(Jump(newPosition, offsetDirection));
        }
    }

    private IEnumerator Jump(int newPosition, int offsetDirection)
    {
        _isGrounded = false;
        int oldPosition = _currentPosition;
        _currentPosition = newPosition;

        for (float t = 0; t < 1; t += Time.deltaTime / JUMP_DURATION)
        {
            Vector3 position = Vector3.Lerp(_jumpTargets[oldPosition].position, _jumpTargets[newPosition].position, t);
            Vector3 sinPosition = new Vector3(position.x, position.y + (Mathf.Sin(t * Mathf.PI)), position.z);
            _rigidbody.MovePosition(sinPosition);

            Vector3 rotation;
            if (offsetDirection < 0)
            {
                rotation = new Vector3(0, 0, Mathf.Sin(t * Mathf.PI / 2) * 180);
            }
            else
            {
                rotation = new Vector3(0, 0, Mathf.Sin((1 - t) * Mathf.PI / 2) * 180);
            }
            _view.rotation = Quaternion.Euler(rotation);

            // Похоже, что WaitUntil замедляет работу корутины. Что можно придумать?
            yield return new WaitUntil(() => _isPlaying);
            yield return null;
        }
        _view.rotation = Quaternion.identity;
        _rigidbody.MovePosition(_jumpTargets[newPosition].position);
        CompliteJump();
    }

    private void CompliteJump()
    {
        _isGrounded = true;
    }
}
