using System;
using System.Reflection;
using FrameworkUnity.Architecture.Locators;
using UnityEngine;

namespace FrameworkUnity.Architecture.DI
{
    public static class DependencyInjector
    {
        public static void Inject(object target)
        {
            Type type = target.GetType();
            MethodInfo[] methodInfos = type.GetMethods(
                BindingFlags.Instance |
                BindingFlags.Public |
                BindingFlags.FlattenHierarchy
            );

            foreach (var method in methodInfos)
            {
                if (method.IsDefined(typeof(InjectAttribute)))
                {
                    InvokeMethod(method, target);
                }
            }
        }

        private static void InvokeMethod(MethodInfo method, object target)
        {
            ParameterInfo[] parameterInfos = method.GetParameters();
            object[] args = new object[parameterInfos.Length];

            for (int i = 0; i < parameterInfos.Length; i++)
            {
                ParameterInfo parameterInfo = parameterInfos[i];
                Type type = parameterInfo.GetType();
                object arg = ServiceLocator.GetService(type);
                args[i] = arg;
            }

            method.Invoke(target, args);
        }
    }
}
