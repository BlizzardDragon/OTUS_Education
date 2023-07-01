using System;
using UnityEngine;
using FrameworkUnity.Interfaces.Listeners.GameListeners;

// Готово.
namespace ShootEmUp
{
    public sealed class PlayerInput : MonoBehaviour, IGameUpdateListener
    {
        [SerializeField] private KeyCode _fireKey = KeyCode.Space;
        [SerializeField] private KeyCode _moveLeft_1 = KeyCode.A;
        [SerializeField] private KeyCode _moveLeft_2 = KeyCode.LeftArrow;
        [SerializeField] private KeyCode _moveRight_1 = KeyCode.D;
        [SerializeField] private KeyCode _moveRight_2 = KeyCode.RightArrow;

        public event Action<int> OnMove;
        public event Action<bool> OnFire;


        void IGameUpdateListener.OnUpdate(float deltaTime)
        {
            if (Input.GetKeyDown(_fireKey))
            {
                OnFire?.Invoke(true);
            }

            if (Input.GetKey(_moveLeft_1) || (Input.GetKey(_moveLeft_2)))
            {
                OnMove?.Invoke(-1);
            }
            else if (Input.GetKey(_moveRight_1) || (Input.GetKey(_moveRight_2)))
            {
                OnMove?.Invoke(1);
            }
            else
            {
                OnMove?.Invoke(0);
            }
        }
    }
}