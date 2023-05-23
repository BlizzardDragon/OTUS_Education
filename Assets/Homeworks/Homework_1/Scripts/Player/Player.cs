using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour, IRoadTarget, IGameFinishListener, IInitListener
{
    public Transform Transform => transform;

    public void InstallTarget() => ServiceLocator.GetService<RoadSpawner>().SetRoadTarget(this);

    public void OnFinishGame()
    {
        Rigidbody rigidbody = GetComponent<Rigidbody>();
        rigidbody.constraints = RigidbodyConstraints.None;
        rigidbody.isKinematic = false;
    }
}
