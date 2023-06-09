using System.Collections;
using System.Collections.Generic;
using Architecture.Locators;
using UnityEngine;

public class EnemySpawner : MonoBehaviour, IGameUpdateListener, IGameStartListener, IGameFinishListener, IService
{
    [SerializeField] private Enemy _enemyPrefab;
    [SerializeField] private Transform[] _jumpTargets;
    private float _chanceThreshold;
    private float _time;
    private const float TIME_MAX_DIFFICULTY = 300;


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
            int oldIndex = -1;
            while (enemyCount < 2)
            {
                int index = Random.Range(0, _jumpTargets.Length);
                if (enemyCount > 0)
                {
                    if (index == oldIndex) continue;
                }
                SpawnEnemy(index, roadSpawnPosition);
                oldIndex = index;
                enemyCount++;
            }
        }
    }

    private void SpawnEnemy(int index, Vector3 roadSpawnPosition)
    {
        Vector3 spawnPosition = new Vector3(_jumpTargets[index].position.x, roadSpawnPosition.y, roadSpawnPosition.z);
        Enemy newEnemy = Instantiate(_enemyPrefab, spawnPosition, Quaternion.identity, transform);
        newEnemy.Install();
    }
}
