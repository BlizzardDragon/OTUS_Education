using System;
using UnityEngine;
using FrameworkUnity.Interfaces.Services;
using FrameworkUnity.Interfaces.Installed;

// Готово.
namespace ShootEmUp
{
    public class ScoreManager : MonoBehaviour, IService, IInstallableOnStart
    {
        private int _score;
        public event Action<int> OnScoreChanged;


        public void InstallOnStart() => ActivateScoreEvent();

        public void AddScore()
        {
            _score++;
            ActivateScoreEvent();
        }

        private void ActivateScoreEvent() => OnScoreChanged?.Invoke(_score);
    }
}
