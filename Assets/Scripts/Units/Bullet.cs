using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public string enemyTag;
    public float damage { get; set; } = 0;


    private void OnTriggerEnter(Collider col)
    {
        if (col.TryGetComponent<Unit>(out Unit unit) && col.CompareTag(enemyTag))
        {
            unit.TakeDamage(damage);
            Destroy(gameObject);
        }   
    }
}
