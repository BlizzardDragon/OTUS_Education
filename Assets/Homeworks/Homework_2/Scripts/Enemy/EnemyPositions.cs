using System.Collections.Generic;
using UnityEngine;

// Готово.
namespace ShootEmUp
{
    public sealed class EnemyPositions : MonoBehaviour
    {
        [SerializeField] private Transform[] _spawnPositions;
        [SerializeField] private Transform[] _attackPositions;

        public int AttackPositionsCount => _attackPositions.Length;

        private List<Transform> _availablePositions = new();
        private List<Transform> _cachePositions = new();


        private void Awake() => _availablePositions = new(_attackPositions);

        public Transform RandomSpawnPosition() => RandomTransform(_spawnPositions);

        private Transform RandomTransform(Transform[] transforms)
        {
            var index = Random.Range(0, transforms.Length);
            return transforms[index];
        }

        public Transform GetRandomAttackPosition()
        {
            int index = Random.Range(0, _availablePositions.Count);
            Transform position = _availablePositions[index];
            _cachePositions.Add(position);
            _availablePositions.Remove(position);
            return position;
        }

        public void RestoreAttackPosition()
        {
            int index = Random.Range(0, _cachePositions.Count);
            Transform position = _cachePositions[index];
            _availablePositions.Add(position);
            _cachePositions.Remove(position);
        }
    }
}