using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public partial class SaveData
{
    //про писывай тут данные для сохранения, например public int a = 0;
    public SaveData() { }
}

public class SaveSystem : MonoBehaviour
{
    [SerializeField] private ISaveService service;
    private const float AutoSaveInterval = 5f;
    private bool isAutoSave = true;
    private float time = 0;

    public static SaveData Data;

    private void Awake()
    {
        Data = new SaveData();
        service = new BinarySaveService();
        Load();
    }

    private void Update()
    {
        if (isAutoSave)
        {
            if (time <= 0)
            {
                time = AutoSaveInterval;
                Save();
            }
            time -= Time.deltaTime;
        }
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

    private void OnDestroy()
    {
        Save();
    }
}
