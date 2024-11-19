using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ObjectPlacerManager : MonoBehaviour
{
    [SerializeField] private GameObject[] prefabs;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private Transform[] buttons;
    [SerializeField] private float rayDistanse = 100f;
    private GraphicRaycaster graphicRaycaster;
    private EventSystem eventSystem;
    private int chosenPrefabId = -1;

    private void Start()
    {
        graphicRaycaster = FindObjectOfType<GraphicRaycaster>();
        eventSystem = FindObjectOfType<EventSystem>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) PlaceObject();
        else if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began) PlaceObject();
    }

    public void ChosePrefab(int id)
    {
        if (chosenPrefabId >= 0) buttons[chosenPrefabId].transform.localScale /= 1.1f;
        if (chosenPrefabId == id) chosenPrefabId = -1;
        else chosenPrefabId = id;
        if (chosenPrefabId >= 0) buttons[chosenPrefabId].transform.localScale *= 1.1f;
    }

    private void PlaceObject()
    {
        if (IsPointerOverUI()) return;
        if (chosenPrefabId == -1) return;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, rayDistanse, layerMask))
        {
            float objectHeight = prefabs[chosenPrefabId].transform.localScale.y / 2;
            Vector3 spawnPosition = hit.point + Vector3.up * objectHeight;
            Instantiate(prefabs[chosenPrefabId], spawnPosition, Quaternion.identity);
        }
    }

    bool IsPointerOverUI()
    {
        PointerEventData pointerData = new PointerEventData(eventSystem)
        {
            position = Input.mousePosition
        };

        var results = new List<RaycastResult>();
        graphicRaycaster.Raycast(pointerData, results);

        return results.Count > 0; // Если есть результаты, значит указатель над UI
    }
}
