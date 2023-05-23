using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour, IGameStartListener, IGameFinishListener, IInitListener
{
    [SerializeField] private Enemy _enemyPrefab;
    [SerializeField] private Transform[] _jumpTargets;


    public void OnStartGame()
    {
        ServiceLocator.GetService<RoadSpawner>().OnSpawnRoad += SpawnEnemy;
    }

    public void OnFinishGame()
    {
        ServiceLocator.GetService<RoadSpawner>().OnSpawnRoad -= SpawnEnemy;
    }

    public void SpawnEnemy(Vector3 roadSpawnPosition)
    {
        int randomJumpTarget = Random.Range(0, _jumpTargets.Length);
        Vector3 spawnPosition = new Vector3(_jumpTargets[randomJumpTarget].position.x, roadSpawnPosition.y, roadSpawnPosition.z);
        Instantiate(_enemyPrefab, spawnPosition, Quaternion.identity, transform);
    }
}
