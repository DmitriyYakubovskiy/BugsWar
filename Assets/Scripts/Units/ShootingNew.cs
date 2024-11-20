using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingNew : MonoBehaviour
{
    [SerializeField] int attackDistance;
    [SerializeField] string unitTag; //теги юнитов, которых будет атаковать стрелок
    [SerializeField] GameObject target, towerHead;
    [SerializeField] float reloadCooldown, force = 2;
    [SerializeField] Bullet bullet;
    [SerializeField] int damage;
    [SerializeField] private float SearchInterval = 5f;

    private GameObjectManager unitsManager;
    private GameObject nearestmob = null;
    private float reloadTimer;

    private void Start()
    {
        unitsManager = GameObject.FindAnyObjectByType<GameObjectManager>();
        StartCoroutine(SearchNearestEnemyCoroutine());
    }

    private void Update()
    {
        if (target != null)
        {
            float squaredDistance = (transform.position - target.transform.position).sqrMagnitude;
            if (squaredDistance < Mathf.Pow(attackDistance, 2))
            {
                if (reloadTimer > 0)
                {
                    reloadTimer -= Time.deltaTime;
                }
                else
                {
                    Bullet son = Instantiate(bullet, towerHead.transform.position, towerHead.transform.rotation);
                    son.enemyTag = unitTag;
                    son.damage = damage;
                    son.GetComponent<Rigidbody>().AddForce(-(towerHead.transform.position - target.transform.position).normalized * force + Vector3.up, ForceMode.Impulse);
                    son.enemyTag = unitTag;
                    son.damage = damage;
                    reloadTimer = reloadCooldown;
                }
            }
        }
        //else
        //{
        //    target = FindNearest();
        //}
    }

    //private GameObject FindNearest()
    //{
    //    float closestMobSquaredDistance = 0;
    //    GameObject nearestmob = null;
    //    Collider[] mobColliders = Physics.OverlapSphere(transform.position, attackDistance, turretLayerMask.value);
    //    foreach (var mobCollider in mobColliders)
    //    {
    //        float distance = (mobCollider.transform.position - transform.position).sqrMagnitude;
    //        if (distance < closestMobSquaredDistance || closestMobSquaredDistance == 0)
    //        {
    //            closestMobSquaredDistance = distance;
    //            nearestmob = mobCollider.gameObject;
    //        }
    //    }
    //    nearestmob = GameObject.FindGameObjectWithTag(unitTag);
    //    return nearestmob;
    //}

    private IEnumerator SearchNearestEnemyCoroutine()
    {
        while (true)
        {
            var nearest = float.MaxValue;
            if (!unitsManager.Units.ContainsKey(unitTag))
            {
                yield return new WaitForSeconds(0.2f);
                continue;
            }
            foreach (var Enemies in unitsManager.Units[unitTag])
            {
                if (Enemies == null) continue;
                if (Enemies == gameObject) continue;
                if (Vector3.Distance(transform.position, Enemies.transform.position) < nearest)
                {
                    nearest = (Vector3.Distance(transform.position, Enemies.transform.position));
                    nearestmob = Enemies;
                    target = nearestmob;
                }
            }
            yield return new WaitForSeconds(SearchInterval);
        }
    }
}
