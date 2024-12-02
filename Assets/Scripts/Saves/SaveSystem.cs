using System;
using System.Collections;
using UnityEngine;

[Serializable]
public partial class SaveData
{
    public float musicVolume = 1;
    public float gameVolume = 1;
    public bool[] levels=new bool[3];
    public SaveData()
    {
        levels[0] = true;
    }
}

public class SaveSystem : MonoBehaviour
{
    [SerializeField] private ISaveService service;
    [SerializeField] private float AutoSaveInterval = 5f;

    public static SaveData Data;

    private void Awake()
    {
        Data = new SaveData();
        service = new BinarySaveService();
        Load();
    }

    private void Start()
    {
        StartCoroutine(AutoSaveCoroutine());
    }

    public void Save()
    {
        service.Save(Data);
    }

    public void ResetProgress()
    {
        var emptyData = new SaveData();
        service.Save(emptyData);
        Load();
        GameSceneManager.Restart();
    }

    private void Load()
    {
        Data = service.Load();
    }

    private IEnumerator AutoSaveCoroutine()
    {
        while (true)
        {
            Debug.Log("Autosave");
            Save();
            yield return new WaitForSeconds(AutoSaveInterval);
        }
    }

    private void OnDestroy()
    {
        Save();
    }
}
