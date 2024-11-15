using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameObjectManager : MonoBehaviour
{
    [SerializeField] private string[] targetTags;
    private Dictionary<string, List<GameObject>> units = new Dictionary<string, List<GameObject>>();

    private void OnTriggerEnter(Collider other)
    {
        if(!targetTags.Contains(other.tag)) return;
        if(!units.ContainsKey(other.tag)) units.Add(other.tag, new List<GameObject>());
        units[other.tag].Add(other.gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        if (!targetTags.Contains(other.tag)) return;
        if (!units.ContainsKey(other.tag)) return;
        units[other.tag].Remove(other.gameObject);
    }
}
