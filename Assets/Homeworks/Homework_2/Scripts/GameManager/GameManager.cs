using UnityEngine;
using FrameworkUnity.Architecture;

namespace ShootEmUp
{
    public sealed class GameManager : DefaultGameManager
    {
        public override void FinishGame()
        {
            base.FinishGame();
            Debug.Log("Game over!");
            Time.timeScale = 0;
        }
    }
}