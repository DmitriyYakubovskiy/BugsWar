using System.Collections;
using UnityEngine;
using UnityEngine.Animations;

public class ShootController : MonoBehaviour
{
    [SerializeField] private Bullet bullet;
    [SerializeField] private GameObject gunPoint;
    [SerializeField] private int force;
    private ShooterUnitLogic shooterUnitLogic;
    private SoundController soundController;
    private Unit objectForAttack = null;

    public bool ObjectForAttackInArea { get; set; } = false;
    public float attackInterval = 1, damage = 1;

    public void SetAttackObject(Unit objectForAttack)
    {
        if (objectForAttack != null) objectForAttack.die -= ObjectForAttackDied;
        this.objectForAttack = objectForAttack;
        objectForAttack.die += ObjectForAttackDied;
    }

    private void Start()
    {
        soundController = GetComponent<SoundController>();
        shooterUnitLogic = GetComponent<ShooterUnitLogic>();
        StartCoroutine(AttackCoroutine());
    }

    private void Update()
    {
        if (ObjectForAttackInArea)
        {
            transform.LookAt(objectForAttack.transform);
        }
    }

    private void ObjectForAttackDied(GameObject gameObject)
    {
        if (objectForAttack != null) objectForAttack.die -= ObjectForAttackDied;
        ObjectForAttackInArea = false;
        objectForAttack = null;
    }
    private void Shoot()
    {
        soundController.PlaySound(2, soundController.Volume);
        Bullet son = Instantiate(bullet, gunPoint.transform.position, gunPoint.transform.rotation);
        son.damage = damage;
        son.enemyTag = shooterUnitLogic.Unit.EnemyTag;
        son.GetComponent<Rigidbody>().AddForce(-(gunPoint.transform.position - objectForAttack.transform.position).normalized * force + Vector3.up, ForceMode.Impulse);
    }

    private IEnumerator AttackCoroutine()
    {
        while (true)
        {
            if (shooterUnitLogic.Unit?.EnemyTag == null)
            {
                yield return new WaitForSeconds(0.2f);
                continue;
            }
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
            Shoot();
            yield return new WaitForSeconds(attackInterval);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (objectForAttack == null) return;
        if (other.gameObject == objectForAttack.gameObject && other.isTrigger == false) ObjectForAttackInArea = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (objectForAttack == null) return;
        if (other.gameObject == objectForAttack.gameObject && other.isTrigger == false) ObjectForAttackInArea = false;
    }
}