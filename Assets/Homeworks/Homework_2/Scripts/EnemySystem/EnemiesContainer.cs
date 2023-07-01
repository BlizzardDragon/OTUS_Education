using System.Collections.Generic;
using UnityEngine;

// Готово.
namespace ShootEmUp
{
    public class EnemiesContainer : MonoBehaviour
    {
        public HashSet<GameObject> ActiveEnemies => _activeEnemies;
        private readonly HashSet<GameObject> _activeEnemies = new();


        public void AddActiveEnemy(GameObject enemy) => _activeEnemies.Add(enemy);
    }
}
