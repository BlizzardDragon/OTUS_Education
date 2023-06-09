using UnityEngine;


namespace Architecture.Locators
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
