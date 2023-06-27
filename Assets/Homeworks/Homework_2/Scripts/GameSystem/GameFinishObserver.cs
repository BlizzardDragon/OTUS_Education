using UnityEngine;
using FrameworkUnity.Interfaces.Listeners.GameListeners;

// Готово.
namespace ShootEmUp
{
    public class GameFinishObserver : MonoBehaviour, IGameFinishListener
    {
        public void OnFinishGame() => PrintFinishLog();
        private void PrintFinishLog() => Debug.Log("Game over!");
    }
}
