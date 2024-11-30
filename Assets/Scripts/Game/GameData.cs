using UnityEngine;

public class GameData : MonoBehaviour
{
    [SerializeField] private GameObject AI;
    public static int score = 0;

    private void Awake()
    {
        if (FindObjectsOfType<GameData>().Length > 1)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        if (score == 1)
        {
            RandomSpawnUnitsManager spawnUnitsManager = AI.GetComponent<RandomSpawnUnitsManager>();
            spawnUnitsManager.spawnInterval = 1f;
        }
        else if (score == 2)
        {
            RandomSpawnUnitsManager spawnUnitsManager = AI.GetComponent<RandomSpawnUnitsManager>();
            spawnUnitsManager.spawnInterval = 9999999f;
        }
    }
}