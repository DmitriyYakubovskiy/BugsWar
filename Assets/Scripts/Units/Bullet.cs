using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] int damage;

    private void OnTriggerEnter(Collider col)
    {
        if(col.TryGetComponent<Unit>(out Unit unit))
        {
            unit.TakeDamage(damage);
        }
        Destroy(gameObject);
    }
}
