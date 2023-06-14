using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using FrameworkUnity.Interfaces.Listeners.GameListeners;


namespace Homework_1
{
    public class CinemachineController : MonoBehaviour, IGamePauseListener, IGamePrepareListener, IGameResumeListener
    {
        [SerializeField] private Transform _playerCenter;
        [SerializeField] private Transform _player;
        [SerializeField] private Cinemachine.CinemachineVirtualCamera _virtualCamera;
        private CinemachineComposer _cinemachineComposer;
        private float _verticalDamping;
        private float _horizontalDamping;
        private bool _isFirstStart = true;


        private void Awake()
        {
            _cinemachineComposer = _virtualCamera.GetCinemachineComponent<CinemachineComposer>();
            _verticalDamping = _cinemachineComposer.m_VerticalDamping;
            _horizontalDamping = _cinemachineComposer.m_HorizontalDamping;
        }

        public void OnPauseGame()
        {
            LookAtPlayer();
        }

        public void OnPrepareGame()
        {
            LookAtPlayerCenter();
        }

        public void OnResumeGame()
        {
            SetCinemachineTransposerDamping(_horizontalDamping);
        }

        public void LookAtPlayerCenter()
        {
            if (_isFirstStart)
            {
                _virtualCamera.LookAt = _playerCenter;
                _isFirstStart = false;
            }
            else
            {
                SetCinemachineTransposerDamping(5);
                _virtualCamera.LookAt = _playerCenter;
            }
        }
        public void LookAtPlayer()
        {
            SetCinemachineTransposerDamping(5);
            _virtualCamera.LookAt = _player;
        }

        private void SetCinemachineTransposerDamping(float value)
        {
            _cinemachineComposer.m_VerticalDamping = value;
            _cinemachineComposer.m_HorizontalDamping = value;
        }
    }
}
