using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class TankUnitLogic : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private float SearchInterval=5f;

    private GameObjectManager unitsManager;
    private GameObject nearestEnemy = null;
    private AttackController attackController;

    public string enemyTag;

    private void Start()
    {
        attackController = GetComponent<AttackController>();
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
        if (attackController.ObjectForAttackInArea) return;
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
                if(Enemies==null) continue;
                if (Enemies == gameObject) continue; 
                if (Vector3.Distance(transform.position, Enemies.transform.position) < nearest)
                {
                    nearest = (Vector3.Distance(transform.position, Enemies.transform.position));
                    nearestEnemy = Enemies;
                    attackController.SetAttackObject(nearestEnemy.GetComponent<Unit>());
                }
            }
            yield return new WaitForSeconds(SearchInterval);
        }
    }
}
