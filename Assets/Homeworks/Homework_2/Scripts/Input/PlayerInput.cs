using System;
using UnityEngine;
using FrameworkUnity.Interfaces.Services;
using FrameworkUnity.Interfaces.Listeners.GameListeners;

// Готово.
namespace ShootEmUp
{
    public sealed class PlayerInput : MonoBehaviour, IService, IGameUpdateListener
    {
        public event Action<int> OnUpdateDirection;
        public event Action<bool> OnUpdateSpace;


        void IGameUpdateListener.OnUpdate(float deltaTime)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                OnUpdateSpace?.Invoke(true);
            }

            if (Input.GetKey(KeyCode.LeftArrow))
            {
                OnUpdateDirection?.Invoke(-1);
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                OnUpdateDirection?.Invoke(1);
            }
            else
            {
                OnUpdateDirection?.Invoke(0);
            }
        }
    }
}