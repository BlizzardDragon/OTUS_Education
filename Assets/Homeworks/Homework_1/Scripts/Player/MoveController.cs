using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveController : MonoBehaviour, IGameStartListener, IGameFinishListener
{
    [SerializeField] private MoveForwardPhysical _input;
    [SerializeField] private Player _player;


    void IGameStartListener.OnStartGame()
    {
        _input.OnMove += OnMove;
    }

    void IGameFinishListener.OnFinishGame()
    {
        _input.OnMove -= OnMove;
    }

    private void OnMove(Vector3 direction)
    {
        Vector3 offset = new Vector3(0, 0, direction.z) * Time.fixedDeltaTime;
        _player.Move(offset);
    }
}
