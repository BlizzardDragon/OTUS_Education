using UnityEngine;


namespace FrameworkUnity.Architecture.DI
{
    public class DependencyResolver : MonoBehaviour
    {
        public void ResolveDependencies() => Resolve(transform);

        private void Resolve(Transform node)
        {
            var behaviours = node.GetComponents<MonoBehaviour>();

            foreach (var behaviour in behaviours)
            {
                DependencyInjector.Inject(behaviour);
            }

            foreach (Transform child in node)
            {
                Resolve(child);
            }
        }
    }
}
