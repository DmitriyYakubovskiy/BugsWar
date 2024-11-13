using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class TankUnitLogic : MonoBehaviour
{
    GameObject player;
    public string detected_TAG1;
    public string detected_TAG2;
    public List<GameObject> Enemies_list = new List<GameObject>();
    public List<GameObject> TARGETER_LIST = new List<GameObject>();

    void Start()
    {
        
        foreach (GameObject Enemies in GameObject.FindGameObjectsWithTag(detected_TAG1))
        {
            Enemies_list.Add(Enemies);
        }

        foreach (GameObject TARGETER in GameObject.FindGameObjectsWithTag(detected_TAG2))
        {
            TARGETER_LIST.Add(TARGETER);
        }
    }
    void Update()
    {
        foreach (var TARGETER in TARGETER_LIST)
        {
            var nearest = float.MaxValue;
            GameObject NearestEnemie = null;
            foreach (var Enemies in Enemies_list)
            {

                if (Vector3.Distance(transform.position, Enemies.transform.position) < nearest)
                {
                    nearest = (Vector3.Distance(transform.position, Enemies.transform.position));
                    NearestEnemie = Enemies;
                    GetComponent<NavMeshAgent>().SetDestination(NearestEnemie.transform.position);
                }
            }
        }
    }
}
