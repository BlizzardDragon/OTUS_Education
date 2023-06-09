using FrameworkUnity.Interfaces.Services;
using UnityEngine;


namespace FrameworkUnity.Architecture.Locators
{
    public class ServiceLocatorInstaller : MonoBehaviour
    {
        public void InstallServices()
        {
            IService[] initListeners = GetComponentsInChildren<IService>();
            foreach (var listener in initListeners)
            {
                ServiceLocator.AddService(listener);
            }
        }
    }
}
