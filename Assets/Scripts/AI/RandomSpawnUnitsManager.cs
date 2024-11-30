using System.Collections;
using UnityEngine;

public class RandomSpawnUnitsManager : MonoBehaviour
{
    [SerializeField] private GameObject[] prefabs; // Префаб для спавна
    [SerializeField] private Vector3 spawnAreaSize = new Vector3(5f, 0f, 5f); // Размер области спавна
    [SerializeField] private Tags aiTag; 

    private bool duo;

    public float spawnInterval = 5f;

    public string AiTag { get => GameDataHelper.GetTag(aiTag); set => aiTag = GameDataHelper.GetTag(value); }

    private void Start()
    {
        StartCoroutine(SpawnCoroutine());
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
            GameObject son = Instantiate(prefabs[0], spawnPosition, Quaternion.Euler(0, 180, 0));
            son.transform.tag = AiTag;
            duo = false;
        }
        else
        {
            GameObject son = Instantiate(prefabs[1], spawnPosition, Quaternion.Euler(0, 180, 0));
            son.tag = AiTag;
            duo =true;
        }
    }
    private IEnumerator SpawnCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.2f);
            yield return new WaitForSeconds(spawnInterval);
            SpawnPrefab();
        }
    }
}
