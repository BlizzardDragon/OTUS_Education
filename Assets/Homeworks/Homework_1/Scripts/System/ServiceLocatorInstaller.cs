using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServiceLocatorInstaller : MonoBehaviour
{
    public void InstallServices()
    {
        IInitListener[] initListeners = GetComponentsInChildren<IInitListener>();
        foreach (var listener in initListeners)
        {
            ServiceLocator.AddService(listener);
        }
    }
}
