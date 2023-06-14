using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using FrameworkUnity.Interfaces.Listeners.GameListeners;
using FrameworkUnity.Interfaces.Services;


namespace Homework_1
{
    public class MoveForwardPhysical : MonoBehaviour, IGameFixedUpdateListener, IService
    {
        public event Action<Vector3> OnMove;


        void IGameFixedUpdateListener.OnFixedUpdate(float fixedDeltaTime)
        {
            var direction = Vector3.forward * fixedDeltaTime;
            OnMove?.Invoke(direction);
        }
    }
}
