using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class TankUnitLogic : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private string enemyTag;
    [SerializeField] private float SearchInterval=5f;

    private GameObjectManager unitsManager;
    private GameObject player;
    private GameObject nearestEnemy = null;

    private void Start()
    {
        unitsManager=GameObject.FindAnyObjectByType<GameObjectManager>();
        StartCoroutine(SearchNearestEnemyCoroutine());
    }

    private void Update()
    {
        if (nearestEnemy == null) return;
        Move();
    }

    private void Move()
    {
        agent.SetDestination(nearestEnemy.transform.position);
    }

    private IEnumerator SearchNearestEnemyCoroutine()
    {
        while (true)
        {
            var nearest = float.MaxValue;
            if (!unitsManager.Units.ContainsKey(enemyTag))
            {
                yield return new WaitForSeconds(0.5f);
                continue;
            }
            if (nearestEnemy != null)
            {
                yield return new WaitForSeconds(0.5f);
                continue;
            }
            foreach (var Enemies in unitsManager.Units[enemyTag])
            {
                if(Enemies==null) continue;
                if (Vector3.Distance(transform.position, Enemies.transform.position) < nearest)
                {
                    nearest = (Vector3.Distance(transform.position, Enemies.transform.position));
                    nearestEnemy = Enemies;
                }
            }
            yield return new WaitForSeconds(SearchInterval);
        }
    }
}
