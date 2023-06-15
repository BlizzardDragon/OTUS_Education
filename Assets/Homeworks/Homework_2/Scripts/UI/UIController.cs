using UnityEngine;
using FrameworkUnity.Architecture.DI;
using FrameworkUnity.Interfaces.Installed;

// Готово.
namespace ShootEmUp
{
    public class UIController : MonoBehaviour, IInstallableOnEnable, IInstallableOnDisable
    {
        private UIManager _uIManager;
        private ScoreManager _scoreManager;


        [Inject]
        public void Construct(UIManager uIManager, ScoreManager scoreManager)
        {
            _uIManager = uIManager;
            _scoreManager = scoreManager;
        }

        public void InstallOnEnable() => _scoreManager.OnScoreChanged += UpdateScore;
        public void InstallOnDisable() => _scoreManager.OnScoreChanged -= UpdateScore;

        private void UpdateScore(int score) => _uIManager.UpdateScore(score.ToString());
    }
}
