using UnityEngine;

// Готово.
namespace ShootEmUp
{
    public class EnemyInstaller : MonoBehaviour
    {
        [SerializeField] private GameObject _character;


        public GameObject InstallEnemy(GameObject enemy, Vector3 spawnPositon, Vector3 attackPositon)
        {
            enemy.transform.position = spawnPositon;
            enemy.GetComponent<EnemyMoveAgent>().SetDestination(attackPositon);
            enemy.GetComponent<CircleCollider2DComponent>().SetActiveCollider(false);
            
            var agent = enemy.GetComponent<EnemyAttackAgent>();
            agent.SetTarget(_character);
            agent.SetAllowAttack(false);

            return enemy;
        }
    }
}
