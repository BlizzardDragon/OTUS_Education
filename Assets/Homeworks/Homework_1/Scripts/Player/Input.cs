using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class Input : MonoBehaviour, IGameStartListener, IGamePauseListener, IGameResumeListener, IGameFinishListener
{
    public Action<Vector3> OnMove;


    private void Awake()
    {
        enabled = false;
    }

    private void FixedUpdate()
    {
        OnMove?.Invoke(Vector3.forward);
    }

    void IGameStartListener.OnStartGame() => enabled = true;
    void IGamePauseListener.OnPauseGame() => enabled = false;
    void IGameResumeListener.OnResumeGame() => enabled = true;
    void IGameFinishListener.OnFinishGame() => enabled = false;
}
