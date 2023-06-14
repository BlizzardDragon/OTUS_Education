using System.Collections;
using System.Collections.Generic;
using FrameworkUnity.Architecture.Locators;
using FrameworkUnity.Interfaces.Listeners.GameListeners;
using UnityEngine;


namespace Homework_1
{
    [RequireComponent(typeof(Rigidbody))]
    public class MoveComponent : MonoBehaviour, IGameUpdateListener, IGameStartListener, IGameResumeListener, IGamePauseListener, IGameFinishListener
    {
        [SerializeField] private Rigidbody _playerRigidbody;
        private Rigidbody _rigidbody;
        private Vector3 _oldVelocity;
        private float _acceleration = 50;
        private float _time;
        private float _timer;
        private const float COMPLEXITY = 0.1f;


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

        public void OnUpdate(float deltaTime)
        {
            _acceleration += deltaTime * COMPLEXITY;
            _time += deltaTime;

            _timer += deltaTime;
            if (_timer > 1)
            {
                _timer -= 1;
                Debug.Log($"Acceleration = {_acceleration}, Time = {(int)_time}");
            }
        }

        private void Move(Vector3 direction)
        {
            _playerRigidbody.interpolation = RigidbodyInterpolation.None;

            Vector3 force = new Vector3(0, 0, direction.z) * _acceleration;
            _rigidbody.AddForce(force, ForceMode.Acceleration);

            _playerRigidbody.interpolation = RigidbodyInterpolation.Interpolate;
        }

        private void StopMove()
        {
            _oldVelocity = _rigidbody.velocity;
            _rigidbody.velocity = Vector3.zero;
        }

        private void ResumeVelocity() => _rigidbody.velocity = _oldVelocity;
    }
}
