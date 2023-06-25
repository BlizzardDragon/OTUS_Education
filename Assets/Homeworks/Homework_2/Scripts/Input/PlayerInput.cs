using System;
using UnityEngine;
using FrameworkUnity.Interfaces.Listeners.GameListeners;

// Готово.
namespace ShootEmUp
{
    public sealed class PlayerInput : MonoBehaviour, IGameUpdateListener
    {
        public event Action<int> OnUpdateDirection;
        public event Action<bool> OnSpacePushed;


        void IGameUpdateListener.OnUpdate(float deltaTime)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                OnSpacePushed?.Invoke(true);
            }

            if (Input.GetKey(KeyCode.LeftArrow) || (Input.GetKey(KeyCode.A)))
            {
                OnUpdateDirection?.Invoke(-1);
            }
            else if (Input.GetKey(KeyCode.RightArrow) || (Input.GetKey(KeyCode.D)))
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