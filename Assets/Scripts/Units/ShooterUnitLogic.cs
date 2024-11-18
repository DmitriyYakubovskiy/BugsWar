using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class ShooterUnitLogic : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private float SearchInterval = 5f;
    [SerializeField] public string enemyTag;

    private GameObjectManager unitsManager;
    private GameObject nearestEnemy = null;
    private ShootController shootController;
    


    private void Start()
    {
        shootController = GetComponent<ShootController>();
        unitsManager = GameObject.FindAnyObjectByType<GameObjectManager>();
        StartCoroutine(SearchNearestEnemyCoroutine());
    }

    private void Update()
    {
        if (nearestEnemy == null) return;
        Move();
    }

    private void Move()
    {
        if (shootController.ObjectForAttackInArea)
        {
            agent.SetDestination(transform.position);
            return;
        }
        agent.SetDestination(nearestEnemy.transform.position);
    }

    private IEnumerator SearchNearestEnemyCoroutine()
    {
        while (true)
        {
            var nearest = float.MaxValue;
            if (!unitsManager.Units.ContainsKey(enemyTag))
            {
                yield return new WaitForSeconds(0.2f);
                continue;
            }
            foreach (var Enemies in unitsManager.Units[enemyTag])
            {
                if (Enemies == null) continue;
                if (Enemies == gameObject) continue;
                if (Vector3.Distance(transform.position, Enemies.transform.position) < nearest)
                {
                    nearest = (Vector3.Distance(transform.position, Enemies.transform.position));
                    nearestEnemy = Enemies;
                    shootController.SetAttackObject(nearestEnemy.GetComponent<Unit>());
                }
            }
            yield return new WaitForSeconds(SearchInterval);
        }
    }
}


