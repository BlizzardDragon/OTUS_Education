using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FrameworkUnity.Interfaces.Listeners.GameListeners;


namespace FrameworkUnity.Architecture.Zenject
{
    public sealed class GameManagerContext
    {
        private readonly List<IGameListener> _listeners;
        private readonly List<IGameUpdateListener> _updateListeners;
        private readonly List<IGameFixedUpdateListener> _fixedUpdateListeners;
        private readonly List<IGameLateUpdateListener> _lateUpdateListeners;

        public GameManagerContext(IEnumerable<IGameListener> listeners,
                                  IEnumerable<IGameUpdateListener> updateListeners,
                                  IEnumerable<IGameFixedUpdateListener> fixedUpdateListeners,
                                  IEnumerable<IGameLateUpdateListener> lateUpdateListeners)
        {
            _listeners = new List<IGameListener>(listeners);
            _updateListeners = new List<IGameUpdateListener>(updateListeners);
            _fixedUpdateListeners = new List<IGameFixedUpdateListener>(fixedUpdateListeners);
            _lateUpdateListeners = new List<IGameLateUpdateListener>(lateUpdateListeners);
        }


        public void AddListener(IGameListener listener)
        {
            if (listener == null) return;

            _listeners.Add(listener);

            if (listener is IGameUpdateListener updateListener)
            {
                _updateListeners.Add(updateListener);
            }

            if (listener is IGameFixedUpdateListener fixedUpdateListener)
            {
                _fixedUpdateListeners.Add(fixedUpdateListener);
            }

            if (listener is IGameLateUpdateListener lateUpdateListener)
            {
                _lateUpdateListeners.Add(lateUpdateListener);
            }
        }

        public void RemoveListener(IGameListener listener)
        {
            if (listener == null) return;

            _listeners.Remove(listener);

            if (listener is IGameUpdateListener updateListener)
            {
                _updateListeners.Remove(updateListener);
            }

            if (listener is IGameFixedUpdateListener fixedUpdateListener)
            {
                _fixedUpdateListeners.Remove(fixedUpdateListener);
            }

            if (listener is IGameLateUpdateListener lateUpdateListener)
            {
                _lateUpdateListeners.Remove(lateUpdateListener);
            }
        }

        public void OnUpdate()
        {
            float deltaTime = Time.deltaTime;
            for (int i = 0; i < _updateListeners.Count; i++)
            {
                _updateListeners[i].OnUpdate(deltaTime);
            }
        }

        public void OnFixedUpdate(float fixedDeltaTime)
        {
            for (int i = 0; i < _fixedUpdateListeners.Count; i++)
            {
                _fixedUpdateListeners[i].OnFixedUpdate(fixedDeltaTime);
            }
        }

        public void OnLateUpdate()
        {
            float deltaTime = Time.deltaTime;
            for (int i = 0; i < _lateUpdateListeners.Count; i++)
            {
                _lateUpdateListeners[i].OnLateUpdate(deltaTime);
            }
        }

        public void PrepareForGame()
        {
            foreach (var listener in _listeners)
            {
                if (listener is IGamePrepareListener prepareListener)
                {
                    prepareListener.OnPrepareGame();
                }
            }
        }

        public void StartGame()
        {
            foreach (var listener in _listeners)
            {
                if (listener is IGameStartListener startListener)
                {
                    startListener.OnStartGame();
                }
            }
        }

        public void PauseGame()
        {
            foreach (var listener in _listeners)
            {
                if (listener is IGamePauseListener pauseListener)
                {
                    pauseListener.OnPauseGame();
                }
            }
        }

        public void ResumeGame()
        {
            foreach (var listener in _listeners)
            {
                if (listener is IGameResumeListener resumeListener)
                {
                    resumeListener.OnResumeGame();
                }
            }
        }

        public void FinishGame()
        {
            foreach (var listener in _listeners)
            {
                if (listener is IGameFinishListener finishListener)
                {
                    finishListener.OnFinishGame();
                }
            }
        }

        public void GameWin()
        {
            foreach (var listener in _listeners)
            {
                if (listener is IGameWinListener gameWinListener)
                {
                    gameWinListener.OnGameWin();
                }
            }
        }

        public void GameOver()
        {
            foreach (var listener in _listeners)
            {
                if (listener is IGameOverListener gameOverListener)
                {
                    gameOverListener.OnGameOver();
                }
            }
        }
    }
}
