using System;
using UnityEngine;
using FrameworkUnity.Interfaces.Installed;
using FrameworkUnity.Interfaces.Listeners.GameListeners;

// Готово.
namespace ShootEmUp
{
    public sealed class LevelBackground : MonoBehaviour, IInstallableOnStart, IGameFixedUpdateListener
    {
        [SerializeField] private Params m_params;

        private float _startPositionY;
        private float _endPositionY;
        private float _movingSpeedY;
        private float _startPositionX;
        private float _startPositionZ;
        private Transform _myTransform;


        public void InstallOnStart()
        {
            _startPositionY = m_params.m_startPositionY;
            _endPositionY = m_params.m_endPositionY;
            _movingSpeedY = m_params.m_movingSpeedY;
            _myTransform = transform;
            var position = _myTransform.position;
            _startPositionX = position.x;
            _startPositionZ = position.z;
        }

        public void OnFixedUpdate(float fixedDeltaTime)
        {
            if (_myTransform.position.y <= _endPositionY)
            {
                _myTransform.position = new Vector3(
                    _startPositionX,
                    _startPositionY,
                    _startPositionZ
                );
            }

            _myTransform.position -= new Vector3(
                _startPositionX,
                _movingSpeedY * fixedDeltaTime,
                _startPositionZ
            );
        }

        [Serializable]
        public sealed class Params
        {
            [SerializeField] public float m_startPositionY = 19;
            [SerializeField] public float m_endPositionY = -38;
            [SerializeField] public float m_movingSpeedY = 5;
        }
    }
}