using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ObjectPlacer : MonoBehaviour
{
    [SerializeField]  private GraphicRaycaster graphicRaycaster;
    [SerializeField] private SupplyController supplyController;
    [SerializeField] private GameObject[] prefabs;
    [SerializeField] private Transform[] buttons;
    [SerializeField] private Sprite image;
    [SerializeField] private Sprite pressedImage;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private Tags playerTag;
    [SerializeField] private float rayDistanse = 100f;

    private EventSystem eventSystem;
    private int chosenPrefabId = -1;

    public string PlayerTag { get => GameDataHelper.GetTag(playerTag); set => playerTag = GameDataHelper.GetTag(value); }

    private void Start()
    {
        eventSystem = FindObjectOfType<EventSystem>();
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].GetComponentInChildren<TextMeshProUGUI>().text = prefabs[i].GetComponent<Unit>().Cost.ToString();  
        }

    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) PlaceObject();
        else if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began) PlaceObject();
    }

    public void ChosePrefab(int id)
    {
        if (chosenPrefabId >= 0)
        {
            SelectButton(false);
        }
        if (chosenPrefabId == id) chosenPrefabId = -1;
        else chosenPrefabId = id;
        if (chosenPrefabId >= 0) SelectButton(true);
    }

    private void SelectButton(bool isSelect)
    {
        if (isSelect) buttons[chosenPrefabId].gameObject.GetComponent<Button>().image.sprite = pressedImage;
        else
        {
            buttons[chosenPrefabId].gameObject.GetComponent<Button>().image.sprite = image;
            chosenPrefabId = -1;
        }
    }

    private void PlaceObject()
    {
        if(chosenPrefabId!=-1) if (supplyController.SupplyValue < prefabs[chosenPrefabId].GetComponent<Unit>().Cost) return;
        if (IsPointerOverUI()) return;
        if (chosenPrefabId == -1) return;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, rayDistanse, layerMask))
        {
            if (supplyController.SupplyValue < prefabs[chosenPrefabId].GetComponent<Unit>().Cost)
            {
                SelectButton(false);
                return;
            }
            supplyController.SupplyValue -= prefabs[chosenPrefabId].GetComponent<Unit>().Cost;
            float objectHeight = prefabs[chosenPrefabId].transform.localScale.y / 2;
            Vector3 spawnPosition = hit.point + Vector3.up * objectHeight;
            var unit = Instantiate(prefabs[chosenPrefabId], spawnPosition, Quaternion.identity);
            unit.gameObject.tag=PlayerTag;
            SelectButton(false);
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
