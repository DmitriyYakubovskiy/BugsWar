using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController : MonoBehaviour
{
    [SerializeField] private string attackTag="";
    [SerializeField] private float damage=1;
    [SerializeField] private float attackInterval = 1;
    private Unit objectForAttack;

    private void Start()
    {
        StartCoroutine(AttackCoroutine());
    }

    private IEnumerator AttackCoroutine()
    {
        while (true)
        {
            if (objectForAttack == null)
            {
                yield return new WaitForSeconds(0.5f);
                continue;
            }
            objectForAttack.TakeDamage(damage);
            yield return new WaitForSeconds(attackInterval);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == attackTag)
        {
            if (!other.gameObject.GetComponent<Unit>()) return;
            objectForAttack = other.gameObject.GetComponent<Unit>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == attackTag)
        {
            if(other.gameObject==objectForAttack.gameObject) objectForAttack=null;
        }
    }
}
