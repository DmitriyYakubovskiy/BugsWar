using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingNew : MonoBehaviour
{
    [SerializeField] int attackDistance;
    [SerializeField] LayerMask turretLayerMask;
    [SerializeField] string unitTag, unitTag2; //теги юнитов, которых будет атаковать стрелок
    [SerializeField] GameObject target, towerHead;
    [SerializeField] float reloadCooldown;
    [SerializeField] Bullet bullet;
    private float reloadTimer;

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
                    son.GetComponent<Rigidbody>().AddForce(-(towerHead.transform.position - target.transform.position), ForceMode.Impulse);
                    reloadTimer = reloadCooldown;
                }
            }
        }
        else
        {
            target = FindNearest();
        }
    }

    private GameObject FindNearest()
    {
        float closestMobSquaredDistance = 0;
        GameObject nearestmob = null;
        Collider[] mobColliders = Physics.OverlapSphere(transform.position, attackDistance, turretLayerMask.value);
        foreach (var mobCollider in mobColliders)
        {
            float distance = (mobCollider.transform.position - transform.position).sqrMagnitude;
            if (distance < closestMobSquaredDistance || closestMobSquaredDistance == 0)
            {
                closestMobSquaredDistance = distance;
                nearestmob = mobCollider.gameObject;
            }
        }
        nearestmob = GameObject.FindGameObjectWithTag(unitTag);
        return nearestmob;
    }
}
