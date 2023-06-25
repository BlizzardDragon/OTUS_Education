using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


namespace FrameworkUnity.Architecture.Locators
{
    public static class ServiceLocator
    {
        private static readonly List<object> _services = new List<object>();

        public static T GetService<T>()
        {
            foreach (var service in _services)
            {
                if (service is T result)
                {
                    return result;
                }
            }

            throw new System.Exception($"Service of type {typeof(T).Name} is not found!");
        }

        public static object GetService(Type serviceType)
        {
            foreach (var service in _services)
            {
                if (service.GetType() == serviceType)
                {
                    return service;
                }
            }

            throw new System.Exception($"Service of type {serviceType.Name} is not found!");
        }

        public static List<T> GetServices<T>()
        {
            var result = new List<T>();
            foreach (var service in _services)
            {
                if (service is T tService)
                {
                    result.Add(tService);
                }
            }

            return result;
        }

        public static void AddService(object newService)
        {
            foreach (var service in _services)
            {
                if (service == newService)
                {
                    throw new System.Exception($"Service object of type {newService.GetType().Name} is already in the list!");
                }
                else
                {
                    if (service.GetType() == newService.GetType())
                    {
                        Debug.LogWarning($"Service of type {newService.GetType().Name} is already in the list!");
                    }
                }
            }

            _services.Add(newService);
        }

        public static void ClearServices() => _services.Clear();
    }
}
