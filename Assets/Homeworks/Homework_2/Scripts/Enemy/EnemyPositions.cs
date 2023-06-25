using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using FrameworkUnity.Interfaces.Installed;

// Готово.
namespace ShootEmUp
{
    public sealed class EnemyPositions : MonoBehaviour, IInstallableOnAwake
    {
        [SerializeField] private Transform[] _spawnPositions;
        [SerializeField] private Transform[] _attackPositions;

        public int AttackPositionsCount => _attackPositions.Length;

        private List<Transform> _availablePositions = new();
        private Dictionary<GameObject, Transform> _dictionaryCache = new();


        public void InstallOnAwake() => _availablePositions = new(_attackPositions);

        public Transform RandomSpawnPosition() => RandomTransform(_spawnPositions);

        private Transform RandomTransform(Transform[] transforms)
        {
            var index = Random.Range(0, transforms.Length);
            return transforms[index];
        }

        public Transform GetRandomAttackPosition(GameObject enemy)
        {
            int index = Random.Range(0, _availablePositions.Count);
            Transform position = _availablePositions[index];
            _dictionaryCache.Add(enemy, position);
            _availablePositions.Remove(position);
            return position;
        }

        public void RestoreAttackPosition(GameObject enemy)
        {
            if (!_dictionaryCache.TryGetValue(enemy, out Transform position))
            {
                var message = "The object is not in the dictionary";
                throw new ArgumentNullException(nameof(enemy), message);
            }
            _dictionaryCache.Remove(enemy);
            _availablePositions.Add(position);
        }
    }
}