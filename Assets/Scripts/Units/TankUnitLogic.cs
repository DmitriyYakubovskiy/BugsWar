using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TankUnitLogic : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private string detected_TAG1;
    [SerializeField] private string detected_TAG2;
    public List<GameObject> Enemies_list = new List<GameObject>();
    public List<GameObject> TARGETER_LIST = new List<GameObject>();
    private GameObjectManager unitsManager;
    private GameObject player;

    private void Start()
    {
        unitsManager=GameObject.FindAnyObjectByType<GameObjectManager>();
        foreach (GameObject Enemies in GameObject.FindGameObjectsWithTag(detected_TAG1))
        {
            Enemies_list.Add(Enemies);
        }

        foreach (GameObject TARGETER in GameObject.FindGameObjectsWithTag(detected_TAG2))
        {
            TARGETER_LIST.Add(TARGETER);
        }
    }

    private void Update()
    {
        //foreach (var TARGETER in TARGETER_LIST)
        //{
        //    var nearest = float.MaxValue;
        //    GameObject NearestEnemie = null;
        //    foreach (var Enemies in Enemies_list)
        //    {
        //        if (Vector3.Distance(transform.position, Enemies.transform.position) < nearest)
        //        {
        //            nearest = (Vector3.Distance(transform.position, Enemies.transform.position));
        //            NearestEnemie = Enemies;
        //            agent.SetDestination(NearestEnemie.transform.position);
        //        }
        //    }
        //}
        var nearest = float.MaxValue;
        GameObject NearestEnemie = null;
        foreach (var Enemies in Enemies_list)
        {
            if (Vector3.Distance(transform.position, Enemies.transform.position) < nearest)
            {
                nearest = (Vector3.Distance(transform.position, Enemies.transform.position));
                NearestEnemie = Enemies;
                agent.SetDestination(NearestEnemie.transform.position);
            }
        }
    }
}
