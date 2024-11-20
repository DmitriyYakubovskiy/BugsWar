using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public string enemyTag;
    public float damage { get; set; } = 0;


    private void OnTriggerEnter(Collider col)
    {
        if (col.TryGetComponent<Unit>(out Unit unit) && col.CompareTag(enemyTag) && col.isTrigger == false)
        {
            unit.TakeDamage(damage);
            Destroy(gameObject);
        }
        else if (col.CompareTag("Platform"))
        {
            Destroy(gameObject);
        }
    }
}
