using UnityEngine;


namespace FrameworkUnity.Architecture.DI
{
    public class DependencyResolver : MonoBehaviour
    {
        private void Start()
        {
            ResolveDependencies(transform);
        }

        private void ResolveDependencies(Transform node)
        {
            var behaviours = node.GetComponents<MonoBehaviour>();

            foreach (var behaviour in behaviours)
            {
                DependencyInjector.Inject(behaviour);
            }

            foreach (Transform child in node)
            {
                ResolveDependencies(child);
            }
        }
    }
}
