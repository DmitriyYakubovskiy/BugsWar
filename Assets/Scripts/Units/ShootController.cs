using System.Collections;
using UnityEngine;

public class ShootController : MonoBehaviour
{
    [SerializeField] private float damage = 1;
    [SerializeField] private float attackInterval = 1;
    [SerializeField] private Bullet bullet;
    [SerializeField] private GameObject gunPoint;
    private Unit objectForAttack = null;


    public bool ObjectForAttackInArea { get; set; } = false;

    public void SetAttackObject(Unit objectForAttack)
    {
        if (objectForAttack != null) objectForAttack.die -= ObjectForAttackDied;
        this.objectForAttack = objectForAttack;
        objectForAttack.die += ObjectForAttackDied;
    }

    private void Start()
    {
        StartCoroutine(AttackCoroutine());
    }

    private void ObjectForAttackDied(GameObject gameObject)
    {
        if (objectForAttack != null) objectForAttack.die -= ObjectForAttackDied;
        ObjectForAttackInArea = false;
        objectForAttack = null;
    }
    private void Shoot()
    {
        Bullet son = Instantiate(bullet, gunPoint.transform.position, gunPoint.transform.rotation);
        son.GetComponent<Rigidbody>().AddForce(-(gunPoint.transform.position - objectForAttack.transform.position), ForceMode.Impulse);
 
    }

    private IEnumerator AttackCoroutine()
    {
        while (true)
        {
            if (objectForAttack == null)
            {
                ObjectForAttackInArea = false;
                yield return new WaitForSeconds(0.2f);
                continue;
            }
            if (!ObjectForAttackInArea)
            {
                yield return new WaitForSeconds(0.2f);
                continue;
            }
            objectForAttack.TakeDamage(damage);
            yield return new WaitForSeconds(attackInterval);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (objectForAttack == null) return;
        if (other.gameObject == objectForAttack.gameObject) ObjectForAttackInArea = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (objectForAttack == null) return;
        if (other.gameObject == objectForAttack.gameObject) ObjectForAttackInArea = false;
    }
}