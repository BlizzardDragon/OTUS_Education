using UnityEngine;


public class Player : MonoBehaviour, IRoadTarget, IInitListener
{
    public Transform Transform => transform;

    public void InstallTarget() => ServiceLocator.GetService<RoadSpawner>().SetRoadTarget(this);
}
