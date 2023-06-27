using System;
using System.Collections.Generic;
using UnityEngine;
using FrameworkUnity.Interfaces.Listeners.GameListeners;
using FrameworkUnity.Interfaces.Installed;


namespace FrameworkUnity.Architecture.GameManagers
{
    public enum GameState
    {
        Off = 0,
        Preparing = 1,
        Playing = 2,
        Pause = 3,
        Finished = 4,
        GameWin = 5,
        GameOver = 6
    }

    public sealed class GameManager : MonoBehaviour, IInstallableOnAwake
    {
        public GameState State { get; private set; }
        private float _fixedDeltaTime;

        private readonly List<IGameListener> _listeners = new();
        private readonly List<IGameUpdateListener> _updateListeners = new();
        private readonly List<IGameFixedUpdateListener> _fixedUpdateListeners = new();
        private readonly List<IGameLateUpdateListener> _lateUpdateListeners = new();

        public event Action OnPrepareForGame;
        public event Action OnStartGame;
        public event Action OnPauseGame;
        public event Action OnResumeGame;
        public event Action OnFinishGame;
        public event Action OnGameWin;
        public event Action OnGameOver;


        public void InstallOnAwake() => _fixedDeltaTime = Time.fixedDeltaTime;

        private void Update()
        {
            if (State != GameState.Playing) return;

            float deltaTime = Time.deltaTime;
            for (int i = 0; i < _updateListeners.Count; i++)
            {
                _updateListeners[i].OnUpdate(deltaTime);
            }
        }

        private void FixedUpdate()
        {
            if (State != GameState.Playing) return;

            for (int i = 0; i < _fixedUpdateListeners.Count; i++)
            {
                _fixedUpdateListeners[i].OnFixedUpdate(_fixedDeltaTime);
            }
        }

        private void LateUpdate()
        {
            if (State != GameState.Playing) return;

            float deltaTime = Time.deltaTime;
            for (int i = 0; i < _lateUpdateListeners.Count; i++)
            {
                _lateUpdateListeners[i].OnLateUpdate(deltaTime);
            }
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

        public void PrepareForGame()
        {
            foreach (var listener in _listeners)
            {
                if (listener is IGamePrepareListener prepareListener)
                {
                    prepareListener.OnPrepareGame();
                }
            }

            State = GameState.Preparing;
            OnPrepareForGame?.Invoke();
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

            State = GameState.Playing;
            OnStartGame?.Invoke();
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

            State = GameState.Pause;
            OnPauseGame?.Invoke();
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

            State = GameState.Playing;
            OnResumeGame?.Invoke();
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

            State = GameState.Finished;
            OnFinishGame?.Invoke();
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

            State = GameState.GameWin;
            OnGameWin?.Invoke();
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

            State = GameState.GameOver;
            OnGameOver?.Invoke();
        }
    }
}