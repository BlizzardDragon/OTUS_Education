using UnityEngine;
using FrameworkUnity.Interfaces.Services;

// Готово.
namespace ShootEmUp
{
    public class EnemyInstaller : MonoBehaviour, IService
    {
        [SerializeField] private GameObject _character;


        public GameObject InstallEnemy(GameObject enemy, Vector3 spawnPositon, Vector3 attackPositon)
        {
            enemy.transform.position = spawnPositon;
            enemy.GetComponent<EnemyAttackAgent>().SetTarget(_character);
            enemy.GetComponent<EnemyMoveAgent>().SetDestination(attackPositon);
            enemy.GetComponent<CircleCollider2DComponent>().DisableCollider();

            return enemy;
        }
    }
}
