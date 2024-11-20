using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawnUnitsManager : MonoBehaviour
{
    [SerializeField] private GameObject prefab1, prefab2; // ������ ��� ������
    [SerializeField] private float spawnInterval = 4f; // �������� ������ � ��������
    [SerializeField] private Vector3 spawnAreaSize = new Vector3(5f, 0f, 5f); // ������ ������� ������

    private bool duo;

    private void Start()
    {
        InvokeRepeating("SpawnPrefab", 0f, spawnInterval);
    }

    private void SpawnPrefab()
    {
        Vector3 spawnPosition = new Vector3(
            transform.position.x + Random.Range(-spawnAreaSize.x / 2, spawnAreaSize.x / 2),
            transform.position.y, // ����� ��������, ���� ����� ������������ ������
            transform.position.z + Random.Range(-spawnAreaSize.z / 2, spawnAreaSize.z / 2)
        );
        if (duo)
        {
            GameObject son = Instantiate(prefab1, spawnPosition, Quaternion.identity);
            son.transform.tag = "RedUnit";
            son.GetComponent<ShooterUnitLogic>().enemyTag = "BlueTower";
            duo = false;
        }
        else
        {
            GameObject son = Instantiate(prefab2, spawnPosition, Quaternion.identity);
            son.tag = "RedUnit";
            son.GetComponent<TankUnitLogic>().enemyTag = "BlueUnit";
            duo =true;
        }
    }
}
