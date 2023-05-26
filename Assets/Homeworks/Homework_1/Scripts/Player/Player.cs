using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour, IRoadTarget, IGameFinishListener, IService
{
    public Transform Transform => transform;
    [SerializeField] private AudioSource _startSound;
    [SerializeField] private AudioSource _loseSound;
    private const int PLAYER_LAYER = 3;
    private const int ROAD_LAYER = 8;


    public void InstallTarget() => ServiceLocator.GetService<RoadSpawner>().SetRoadTarget(this);

    public void OnFinishGame()
    {
        _loseSound.Play();
        Rigidbody rigidbody = GetComponent<Rigidbody>();
        rigidbody.constraints = RigidbodyConstraints.None;
        rigidbody.isKinematic = false;
        Physics.IgnoreLayerCollision(PLAYER_LAYER, ROAD_LAYER, false);
        Debug.Log(Physics.GetIgnoreLayerCollision(PLAYER_LAYER, ROAD_LAYER));
    }
}
