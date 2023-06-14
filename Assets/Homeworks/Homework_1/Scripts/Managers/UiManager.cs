using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using FrameworkUnity.Architecture.Locators;
using FrameworkUnity.Interfaces.Listeners.GameListeners;


namespace Homework_1
{
    public class UiManager : MonoBehaviour, IGameStartListener, IGameFinishListener
    {
        [SerializeField] private TextMeshProUGUI _scoreText;
        [SerializeField] private GameObject _loseScreen;
        private float _score;
        private const float INVOKE_TIME = 3;


        private void Awake() => UpdateScoreText();
        public void OnStartGame() => ServiceLocator.GetService<DeathTrigger>().OnEnemyKilled += AddScore;

        public void OnFinishGame()
        {
            ServiceLocator.GetService<DeathTrigger>().OnEnemyKilled -= AddScore;
            Invoke(nameof(ShowLoseScreen), INVOKE_TIME);
        }

        private void ShowLoseScreen() => _loseScreen.SetActive(true);

        private void AddScore()
        {
            _score++;
            UpdateScoreText();
        }

        public void UpdateScoreText() => _scoreText.text = _score.ToString();
    }
}
