using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class RoadSpawner : MonoBehaviour, IGamePrepareListener, IGameUpdateListener, IInitListener
{
    [SerializeField] private Road _roadPrefab;
    private IRoadTarget _roadTarget;
    private Vector3 _spawnPosition;
    private float _startPosZ;
    private List<Road> _roads = new();


    public void SetRoadTarget(IRoadTarget roadTarget) => _roadTarget = roadTarget;

    public void OnUpdate(float deltaTime)
    {
        float distance = _roadTarget.Transform.position.z - _startPosZ;
        if(distance >= _roadPrefab.RoadLength)
        {
            SpawnNextRoad(_spawnPosition, true);
            _startPosZ += _roadPrefab.RoadLength;
        }
    }

    public void OnPrepareGame()
    {
        SetTargetStartPosZ();
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

    private void SetTargetStartPosZ() => _startPosZ = _roadTarget.Transform.position.z;

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
}
