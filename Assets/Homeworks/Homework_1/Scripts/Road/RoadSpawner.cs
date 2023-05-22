using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class RoadSpawner : MonoBehaviour, IGamePrepareListener
{
    [SerializeField] private Road _roadPrefab;
    private IRoadTarget _roadTarget;
    private float _targetStartPosZ;
    private Vector3 _spawnPosition;
    private List<Road> _roads = new();


    public void OnPrepareGame()
    {
        SpawnFirstRoad();

        Sequence delay = DOTween.Sequence()
            .SetDelay(0.5f);

        Sequence spawn = DOTween.Sequence()
            .AppendCallback(SpawnStartRoad)
            .AppendInterval(0.5f)
            .SetLoops(5);

        DOTween.Sequence()
            .SetLink(gameObject)
            .Append(delay)
            .Append(spawn);
    }

    private void SpawnFirstRoad()
    {
        Vector3 startPosition = _roadTarget.Transform.position - Vector3.forward * _roadPrefab.RoadLength * 2;
        SpawnNextRoad(startPosition, false);
    }

    private void SpawnStartRoad() => SpawnNextRoad(_spawnPosition, false);

    private void SpawnNextRoad(Vector3 currentPosition, bool removeLastRoad)
    {
        Road newRoad = Instantiate(_roadPrefab, GetSpawnPosition(currentPosition), Quaternion.identity, transform);
        _roads.Add(newRoad);

        if (removeLastRoad)
        {
            Destroy(_roads[0].gameObject);
            _roads.RemoveAt(0);
        }
    }

    private Vector3 GetSpawnPosition(Vector3 currentPosition)
    {
        _spawnPosition = currentPosition;
        return _spawnPosition += Vector3.forward * _roadPrefab.RoadLength;
    }

    public void SetRoadTarget(IRoadTarget roadTarget) => _roadTarget = roadTarget;
}
