using UnityEngine;


namespace FrameworkUnity.Architecture.Locators
{
    public class ServiceLocatorInstaller : MonoBehaviour
    {
        [SerializeField] private MonoBehaviour[] _servoces;


        public void InstallServices()
        {
            foreach (var service in _servoces)
            {
                ServiceLocator.AddService(service);
            }
        }
    }
}
