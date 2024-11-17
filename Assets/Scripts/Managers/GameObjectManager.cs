using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameObjectManager : MonoBehaviour
{
    [SerializeField] private string[] targetTags;
    private Dictionary<string, List<GameObject>> units = new Dictionary<string, List<GameObject>>();

    public Dictionary<string, List<GameObject>> Units { get => units; private set=>units=value; }

    private void RemoveObject(GameObject gameObject)
    {
        if (!units.ContainsKey(gameObject.tag)) return;
        units[gameObject.tag].Remove(gameObject);
        gameObject.GetComponent<Unit>().die -= RemoveObject;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(!targetTags.Contains(other.tag)) return;
        if(!units.ContainsKey(other.tag)) units.Add(other.tag, new List<GameObject>());
        units[other.tag].Add(other.gameObject);
        other.gameObject.GetComponent<Unit>().die += RemoveObject;
    }
}
