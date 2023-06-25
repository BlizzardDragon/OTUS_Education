using UnityEngine;
using TMPro;
using FrameworkUnity.Interfaces.Listeners.GameListeners;

// Готово.
namespace ShootEmUp
{
    public class UIManager : MonoBehaviour, IGameFinishListener
    {
        [SerializeField] private TMP_Text _gameOver;
        [SerializeField] private TMP_Text _score;


        private void Awake() => _gameOver.enabled = false;
        public void OnFinishGame() => ShowGameOver();

        public void ShowGameOver() => _gameOver.enabled = true;
        public void UpdateScore(string score) => _score.text = score;

    }
}
