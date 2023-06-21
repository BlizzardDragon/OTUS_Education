using System;
using System.Collections.Generic;
using UnityEngine;
using FrameworkUnity.Interfaces.Listeners.GameListeners;
using FrameworkUnity.Interfaces.Services;
using FrameworkUnity.Interfaces.Installed;
using Zenject;


namespace FrameworkUnity.Architecture.Zenject.GameManagers
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

    public class BaseGameManager : MonoBehaviour, IService, IInstallableOnAwake
    {
        public GameState State { get; private set; }
        protected float _fixedDeltaTime;

        public event Action OnPrepareForGame;
        public event Action OnStartGame;
        public event Action OnPauseGame;
        public event Action OnResumeGame;
        public event Action OnFinishGame;
        public event Action OnGameWin;
        public event Action OnGameOver;

        [Inject]
        protected GameManagerContext _context;


        public void InstallOnAwake() => _fixedDeltaTime = Time.fixedDeltaTime;

        protected virtual void Update()
        {
            if (State != GameState.Playing) return;

            _context.OnUpdate();
        }

        protected virtual void FixedUpdate()
        {
            if (State != GameState.Playing) return;

            _context.OnFixedUpdate(_fixedDeltaTime);
        }

        protected virtual void LateUpdate()
        {
            if (State != GameState.Playing) return;

            _context.OnLateUpdate();
        }

        public virtual void PrepareForGame()
        {
            _context.PrepareForGame();
            State = GameState.Preparing;
            OnPrepareForGame?.Invoke();
        }

        public virtual void StartGame()
        {
            _context.StartGame();
            State = GameState.Playing;
            OnStartGame?.Invoke();
        }

        public virtual void PauseGame()
        {
            _context.PauseGame();
            State = GameState.Pause;
            OnPauseGame?.Invoke();
        }

        public virtual void ResumeGame()
        {
            _context.ResumeGame();
            State = GameState.Playing;
            OnResumeGame?.Invoke();
        }

        public virtual void FinishGame()
        {
            _context.FinishGame();
            State = GameState.Finished;
            OnFinishGame?.Invoke();
        }

        public virtual void GameWin()
        {
            _context.GameWin();
            State = GameState.GameWin;
            OnGameWin?.Invoke();
        }

        public virtual void GameOver()
        {
            _context.GameOver();
            State = GameState.GameOver;
            OnGameOver?.Invoke();
        }
    }
}