using UnityEngine;
using FrameworkUnity.Interfaces.Services;
using FrameworkUnity.Interfaces.Listeners.GameListeners;
using System;


public class FixedUpdater : MonoBehaviour, IService, IGameFixedUpdateListener
{
    public event Action<float> OnFixedUpdateEvent;

    public void OnFixedUpdate(float fixedDeltaTime) => OnFixedUpdateEvent?.Invoke(fixedDeltaTime);
}
