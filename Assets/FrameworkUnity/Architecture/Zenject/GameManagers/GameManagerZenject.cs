using System;
using UnityEngine;
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

    public sealed class GameManagerZenject : MonoBehaviour, IInstallableOnAwake
    {
        public GameState State { get; private set; }
        private float _fixedDeltaTime;

        public event Action OnPrepareForGame;
        public event Action OnStartGame;
        public event Action OnPauseGame;
        public event Action OnResumeGame;
        public event Action OnFinishGame;
        public event Action OnGameWin;
        public event Action OnGameOver;

        [Inject]
        private GameManagerContext _context;


        public void InstallOnAwake() => _fixedDeltaTime = Time.fixedDeltaTime;

        private void Update()
        {
            if (State != GameState.Playing) return;

            _context.OnUpdate();
        }

        private void FixedUpdate()
        {
            if (State != GameState.Playing) return;

            _context.OnFixedUpdate(_fixedDeltaTime);
        }

        private void LateUpdate()
        {
            if (State != GameState.Playing) return;

            _context.OnLateUpdate();
        }

        public void PrepareForGame()
        {
            _context.PrepareForGame();
            State = GameState.Preparing;
            OnPrepareForGame?.Invoke();
        }

        public void StartGame()
        {
            _context.StartGame();
            State = GameState.Playing;
            OnStartGame?.Invoke();
        }

        public void PauseGame()
        {
            _context.PauseGame();
            State = GameState.Pause;
            OnPauseGame?.Invoke();
        }

        public void ResumeGame()
        {
            _context.ResumeGame();
            State = GameState.Playing;
            OnResumeGame?.Invoke();
        }

        public void FinishGame()
        {
            _context.FinishGame();
            State = GameState.Finished;
            OnFinishGame?.Invoke();
        }

        public void GameWin()
        {
            _context.GameWin();
            State = GameState.GameWin;
            OnGameWin?.Invoke();
        }

        public void GameOver()
        {
            _context.GameOver();
            State = GameState.GameOver;
            OnGameOver?.Invoke();
        }
    }
}