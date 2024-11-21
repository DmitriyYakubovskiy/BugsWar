using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class ShooterUnitLogic : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private float searchInterval = 5f;
    [SerializeField] private TypesOfUnits[] typesForAttack;

    private GameObjectManager unitsManager;
    private GameObject nearestEnemy = null;
    private ShootController shootController;
    private Unit unit;

    public Unit Unit { get { return unit; } }

    private void Start()
    {
        unit = GetComponent<Unit>();
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
                    shootController.SetAttackObject(nearestEnemy.GetComponent<Unit>());
                }
            }
            yield return new WaitForSeconds(searchInterval);
        }
    }
}


