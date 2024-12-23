using System;
using UnityEngine;

public class Unit : MonoBehaviour, IUnit
{
    [SerializeField] private ProgressBar progressBar;
    [SerializeField] private float lives;
    [SerializeField] private TypesOfUnits typeOfUnit;
    [SerializeField] private float cost = 5;
    private SoundController soundController;
    private Tags enemyTag;

    public float Cost { get => cost; }
    public string EnemyTag { get => GameDataHelper.GetTag(enemyTag); set => enemyTag = GameDataHelper.GetTag(value); }
    public TypesOfUnits TypeOfUnit {  get { return typeOfUnit; } }

    public float Lives
    {
        get => lives;
        set
        {
            if (value < lives && soundController!=null) soundController.PlaySound(0, soundController.Volume);
            lives = value;
            livesChanged?.Invoke(lives);
        }
    }

    public event Action<float> livesChanged;
    public event Action<GameObject> die;

    private void Start()
    {
        soundController = GetComponent<SoundController>();
        if (progressBar == null) return;
        livesChanged += progressBar.ChangeValue;
        progressBar.MaxValue = lives;
        progressBar.StartValue = lives;

        if (gameObject.tag == GameDataHelper.GetTag(Tags.BlueTeam))
        {
            progressBar.SetColor(Color.green);
            EnemyTag = GameDataHelper.GetTag(Tags.RedTeam);
        }
        else
        {
            progressBar.SetColor(Color.red);
            EnemyTag = GameDataHelper.GetTag(Tags.BlueTeam);
        }
        
    }

    public void TakeDamage(float damage)
    {
        Lives -= damage;
        if (Lives <= 0) Die();
    }

    private void Die()
    {
        if (gameObject.name == "Main Tower" && gameObject.CompareTag("RedTeam"))
        {
            WinDefeatViewer winDefeatViewer = GameObject.FindAnyObjectByType<WinDefeatViewer>();
            winDefeatViewer.Result(true);
        }
        else if (gameObject.name == "Main Tower" && gameObject.CompareTag("BlueTeam"))
        {
            WinDefeatViewer winDefeatViewer = GameObject.FindAnyObjectByType<WinDefeatViewer>();
            winDefeatViewer.Result(false);
        }
        if (typeOfUnit != TypesOfUnits.Tower)
        {
            soundController.PlaySound(3, soundController.Volume, isDestroyed:true);
        }
        die?.Invoke(gameObject);
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        if (progressBar != null) livesChanged -= progressBar.ChangeValue;
    }
}
