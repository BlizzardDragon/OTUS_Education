using UnityEngine;
using FrameworkUnity.Interfaces.Listeners.GameListeners;
using System;


public class FixedUpdater : MonoBehaviour, IGameFixedUpdateListener
{
    public event Action<float> OnFixedUpdateEvent;

    public void OnFixedUpdate(float fixedDeltaTime) => OnFixedUpdateEvent?.Invoke(fixedDeltaTime);
}
