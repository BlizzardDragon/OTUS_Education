using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(Rigidbody))]
public class JumpComponent : MonoBehaviour, IGameStartListener, IGamePauseListener, IGameFinishListener
{
    [SerializeField] private Transform[] _jumpTargets;
    private Rigidbody _rigidbody;
    private Coroutine _coroutine;
    private int _currentPosition = 1;
    private int _nextPosition;
    private bool _isGrounded = true;
    private bool _isPlaying;
    private const float JUMP_DURATION = 0.1f;


    private void Awake() => _rigidbody = GetComponent<Rigidbody>();

    public void OnStartGame()
    {
        ServiceLocator.GetService<JumpInput>().OnMovedToSide += PrepareToJump;
        _isPlaying = true;
    }

    public void OnPauseGame()
    {
        ServiceLocator.GetService<JumpInput>().OnMovedToSide -= PrepareToJump;
        _isPlaying = false;
    }

    public void OnFinishGame()
    {
        ServiceLocator.GetService<JumpInput>().OnMovedToSide -= PrepareToJump;
        _isPlaying = false;
    }

    private void PrepareToJump(int offsetDirection)
    {
        if (_isGrounded)
        {
            int newPosition = _currentPosition + offsetDirection;
            if (newPosition >= 0 && newPosition < _jumpTargets.Length)
            {
                _coroutine = StartCoroutine(Jump(newPosition));
            }
        }
    }

    private IEnumerator Jump(int newPosition)
    {
        _isGrounded = false;
        int oldPosition = _currentPosition;
        _currentPosition = newPosition;

        for (float t = 0; t < 1; t += Time.deltaTime / JUMP_DURATION)
        {
            Vector3 position = Vector3.Lerp(_jumpTargets[oldPosition].position, _jumpTargets[newPosition].position, t);
            _rigidbody.MovePosition(position);
            yield return new WaitUntil(() => _isPlaying);
            yield return null;
        }
        _rigidbody.MovePosition(_jumpTargets[newPosition].position);
        CompliteJump();
    }

    private void CompliteJump()
    {
        _isGrounded = true;
    }
}
