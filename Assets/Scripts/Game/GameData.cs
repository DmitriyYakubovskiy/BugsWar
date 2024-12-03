using UnityEngine;
using UnityEngine.AI;

public class GameData : MonoBehaviour
{
    [SerializeField] private GameObject AI, juk, boss;
    [SerializeField] private Tags aiTag;

    public static bool activeted = false;

    public static int score = 0;
    public string AiTag { get => GameDataHelper.GetTag(aiTag); set => aiTag = GameDataHelper.GetTag(value); }

    public static void SetScore(int score)
    {
        GameData.score = score;   
    }

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
            spawnUnitsManager.spawnInterval = 2f;
        }
        else if (score == 2)
        {
            RandomSpawnUnitsManager spawnUnitsManager = AI.GetComponent<RandomSpawnUnitsManager>();
            spawnUnitsManager.spawnInterval = 9999999f;
            GameObject son = Instantiate(juk, new Vector3(0, 0, 3.5f), Quaternion.Euler(0, 180, 0));
            son.transform.localScale = new Vector3(3, 3, 3);
            son.transform.tag = AiTag;
            NavMeshAgent agent = son.GetComponent<NavMeshAgent>();
            agent.speed = 0.2f;
            Unit unit = son.GetComponent<Unit>();
            unit.Lives = 1000000000;
            AttackController attackController = son.GetComponent<AttackController>();
            attackController.damage = 10000;
            son.GetComponent<Rigidbody>().mass *= 100;
            if (activeted)
            {
                GameObject sonBoss = Instantiate(boss, new Vector3(0, 0, -3.5f), Quaternion.identity);
                sonBoss.transform.tag = "BlueTeam";
                NavMeshAgent agentBoss = sonBoss.GetComponent<NavMeshAgent>();
                agentBoss.speed = 1f;
                Unit unitBoss = sonBoss.GetComponent<Unit>();
                unitBoss.Lives = 1000000000;
                ShootController shootController = sonBoss.GetComponent<ShootController>();
                shootController.attackInterval = 0.2f;
                shootController.damage = 40000000;
            }
        }
    }
}