using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FrameworkUnity.Interfaces.Listeners.GameListeners;


namespace FrameworkUnity.Architecture.DI
{
    public sealed class GameManagerContext : MonoBehaviour
    {
        public readonly List<IGameListener> Listeners = new();
        public readonly List<IGameUpdateListener> UpdateListeners = new();
        public readonly List<IGameFixedUpdateListener> FixedUpdateListeners = new();
        public readonly List<IGameLateUpdateListener> LateUpdateListeners = new();
        

        public void AddListener(IGameListener listener)
        {
            if (listener == null) return;

            Listeners.Add(listener);

            if (listener is IGameUpdateListener updateListener)
            {
                UpdateListeners.Add(updateListener);
            }

            if (listener is IGameFixedUpdateListener fixedUpdateListener)
            {
                FixedUpdateListeners.Add(fixedUpdateListener);
            }

            if (listener is IGameLateUpdateListener lateUpdateListener)
            {
                LateUpdateListeners.Add(lateUpdateListener);
            }
        }

        public void RemoveListener(IGameListener listener)
        {
            if (listener == null) return;

            Listeners.Remove(listener);

            if (listener is IGameUpdateListener updateListener)
            {
                UpdateListeners.Remove(updateListener);
            }

            if (listener is IGameFixedUpdateListener fixedUpdateListener)
            {
                FixedUpdateListeners.Remove(fixedUpdateListener);
            }

            if (listener is IGameLateUpdateListener lateUpdateListener)
            {
                LateUpdateListeners.Remove(lateUpdateListener);
            }
        }

        public void PrepareForGame()
        {
            foreach (var listener in Listeners)
            {
                if (listener is IGamePrepareListener prepareListener)
                {
                    prepareListener.OnPrepareGame();
                }
            }
        }

        public void StartGame()
        {
            foreach (var listener in Listeners)
            {
                if (listener is IGameStartListener startListener)
                {
                    startListener.OnStartGame();
                }
            }
        }

        public void PauseGame()
        {
            foreach (var listener in Listeners)
            {
                if (listener is IGamePauseListener pauseListener)
                {
                    pauseListener.OnPauseGame();
                }
            }
        }

        public void ResumeGame()
        {
            foreach (var listener in Listeners)
            {
                if (listener is IGameResumeListener resumeListener)
                {
                    resumeListener.OnResumeGame();
                }
            }
        }

        public void FinishGame()
        {
            foreach (var listener in Listeners)
            {
                if (listener is IGameFinishListener finishListener)
                {
                    finishListener.OnFinishGame();
                }
            }
        }

        public void GameWin()
        {
            foreach (var listener in Listeners)
            {
                if (listener is IGameWinListener gameWinListener)
                {
                    gameWinListener.OnGameWin();
                }
            }
        }

        public void GameOver()
        {
            foreach (var listener in Listeners)
            {
                if (listener is IGameOverListener gameOverListener)
                {
                    gameOverListener.OnGameOver();
                }
            }
        }
    }
}
