using UnityEngine;

public class RandomSpawnUnitsManager : MonoBehaviour
{
    [SerializeField] private GameObject[] prefabs; // Префаб для спавна
    [SerializeField] private float spawnInterval = 4f; // Интервал спавна в секундах
    [SerializeField] private Vector3 spawnAreaSize = new Vector3(5f, 0f, 5f); // Размер области спавна
    [SerializeField] private Tags aiTag; 

    private bool duo;

    public string AiTag { get => GameDataHelper.GetTag(aiTag); set => aiTag = GameDataHelper.GetTag(value); }

    private void Start()
    {
        InvokeRepeating("SpawnPrefab", 0f, spawnInterval);
    }

    private void SpawnPrefab()
    {
        Vector3 spawnPosition = new Vector3(
            transform.position.x + Random.Range(-spawnAreaSize.x / 2, spawnAreaSize.x / 2),
            transform.position.y, // Можно изменить, если нужно регулировать высоту
            transform.position.z + Random.Range(-spawnAreaSize.z / 2, spawnAreaSize.z / 2)
        );
        if (duo)
        {
            GameObject son = Instantiate(prefabs[0], spawnPosition, Quaternion.identity);
            son.transform.tag = AiTag;
            duo = false;
        }
        else
        {
            GameObject son = Instantiate(prefabs[1], spawnPosition, Quaternion.identity);
            son.tag = AiTag;
            duo =true;
        }
    }
}
