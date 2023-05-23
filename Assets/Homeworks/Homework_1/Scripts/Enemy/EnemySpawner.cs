using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour, IGameUpdateListener, IGameStartListener, IGameFinishListener, IInitListener
{
    [SerializeField] private Enemy _enemyPrefab;
    [SerializeField] private Transform[] _jumpTargets;
    private const float TIME_MAX_DIFFICULTY = 300;
    private float _chanceThreshold;
    private float _time;


    public void OnStartGame()
    {
        ServiceLocator.GetService<RoadSpawner>().OnSpawnRoad += CalculateSpawnEnemy;
    }

    public void OnFinishGame()
    {
        ServiceLocator.GetService<RoadSpawner>().OnSpawnRoad -= CalculateSpawnEnemy;
    }

    public void OnUpdate(float deltaTime)
    {
        _time += deltaTime;
    }

    public void CalculateSpawnEnemy(Vector3 roadSpawnPosition)
    {
        _chanceThreshold = (_time / TIME_MAX_DIFFICULTY) * 0.8f;
        float randomValue = Random.value;
        int enemyCount = 0;

        if (randomValue > _chanceThreshold)
        {
            int index = Random.Range(0, _jumpTargets.Length);
            SpawnEnemy(index, roadSpawnPosition);
        }
        else
        {
            while (enemyCount < 2)
            {
                int index = Random.Range(0, _jumpTargets.Length);
                SpawnEnemy(index, roadSpawnPosition);
                enemyCount++;
            }
        }

    }

    private void SpawnEnemy(int index, Vector3 roadSpawnPosition)
    {
        Vector3 spawnPosition = new Vector3(_jumpTargets[index].position.x, roadSpawnPosition.y, roadSpawnPosition.z);
        Instantiate(_enemyPrefab, spawnPosition, Quaternion.identity, transform);
    }
}
