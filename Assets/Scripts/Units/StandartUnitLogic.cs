using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class StandartUnitLogic : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private float searchInterval = 5f;
    [SerializeField] private TypesOfUnits[] typesForAttack;

    private GameObjectManager unitsManager;
    public GameObject nearestEnemy = null;
    private AttackController attackController;
    public Unit unit;

    public Unit Unit { get { return unit; } }

    private void Start()
    {
        unit = GetComponent<Unit>();
        unitsManager = GameObject.FindAnyObjectByType<GameObjectManager>();
        attackController = GetComponent<AttackController>();
        StartCoroutine(SearchNearestEnemyCoroutine());
    }

    private void Update()
    {
        if (nearestEnemy == null) return;
        Move();
    }

    private void Move()
    {
        if (attackController.ObjectForAttackInArea)
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
            if (unit.EnemyTag == null)
            {
                yield return new WaitForSeconds(0.2f);
                continue;
            }
            if (!unitsManager.Units.ContainsKey(unit.EnemyTag))
            {
                yield return new WaitForSeconds(0.2f);
                continue;
            }
            foreach (var unit in unitsManager.Units[unit.EnemyTag])
            {
                if (unit == null) continue;
                if (unit == gameObject) continue;
                if (!typesForAttack.Contains(unit.GetComponent<Unit>().TypeOfUnit)) continue;
                if (Vector3.Distance(transform.position, unit.transform.position) < nearest)
                {
                    nearest = (Vector3.Distance(transform.position, unit.transform.position));
                    nearestEnemy = unit;
                    attackController.SetAttackObject(nearestEnemy.GetComponent<Unit>());
                }
            }
            yield return new WaitForSeconds(searchInterval);
        }
    }
}