using System.Collections.Generic;
using FrameworkUnity.Interfaces.Listeners.GameListeners;
using FrameworkUnity.Interfaces.Services;
using UnityEngine;


namespace FrameworkUnity.Architecture
{
    public enum GameState
    {
        Off = 0,
        Preparing = 1,
        Playing = 2,
        Pause = 3,
        Finished = 4
    }

    public class DefaultGameManager : MonoBehaviour, IService
    {
        public GameState State { get; private set; }
        protected readonly List<IGameListener> _listeners = new();
        protected readonly List<IGameUpdateListener> _updateListeners = new();
        protected readonly List<IGameFixedUpdateListener> _fixedUpdateListeners = new();
        protected readonly List<IGameLateUpdateListener> _lateUpdateListeners = new();
        protected float _fixedDeltaTime;


        private void Awake() => _fixedDeltaTime = Time.fixedDeltaTime;

        protected virtual void Update()
        {
            if (State != GameState.Playing) return;

            float deltaTime = Time.deltaTime;
            for (int i = 0; i < _updateListeners.Count; i++)
            {
                _updateListeners[i].OnUpdate(deltaTime);
            }
        }

        protected virtual void FixedUpdate()
        {
            if (State != GameState.Playing) return;

            for (int i = 0; i < _fixedUpdateListeners.Count; i++)
            {
                _fixedUpdateListeners[i].OnFixedUpdate(_fixedDeltaTime);
            }
        }

        protected virtual void LateUpdate()
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

        public virtual void PrepareForGame()
        {
            foreach (var listener in _listeners)
            {
                if (listener is IGamePrepareListener prepareListener)
                {
                    prepareListener.OnPrepareGame();
                }
            }

            State = GameState.Preparing;
        }

        public virtual void StartGame()
        {
            foreach (var listener in _listeners)
            {
                if (listener is IGameStartListener startListener)
                {
                    startListener.OnStartGame();
                }
            }

            State = GameState.Playing;
        }

        public virtual void PauseGame()
        {
            foreach (var listener in _listeners)
            {
                if (listener is IGamePauseListener pauseListener)
                {
                    pauseListener.OnPauseGame();
                }
            }

            State = GameState.Pause;
        }

        public virtual void ResumeGame()
        {
            foreach (var listener in _listeners)
            {
                if (listener is IGameResumeListener resumeListener)
                {
                    resumeListener.OnResumeGame();
                }
            }

            State = GameState.Playing;
        }

        public virtual void FinishGame()
        {
            foreach (var listener in _listeners)
            {
                if (listener is IGameFinishListener finishListener)
                {
                    finishListener.OnFinishGame();
                }
            }

            State = GameState.Finished;
        }
    }
}