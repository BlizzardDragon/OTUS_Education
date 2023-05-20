using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum GameState
{
    Off = 0,
    Playing = 1,
    Pause = 2,
    Finished = 3
}

public class GameManager : MonoBehaviour
{
    public GameState State { get; private set; }
    private List<IGameListener> _listeners = new();


    public void AddListener(IGameListener listener)
    {
        if (listener == null) return;

        _listeners.Add(listener);
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
    }
}
