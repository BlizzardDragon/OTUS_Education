using UnityEngine;
using FrameworkUnity.Architecture.GameManagers;

namespace ShootEmUp
{
    public sealed class GameManager : BaseGameManager
    {
        public override void FinishGame()
        {
            base.FinishGame();
            Debug.Log("Game over!");
        }
    }
}