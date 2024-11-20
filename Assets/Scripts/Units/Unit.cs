using System;
using UnityEngine;

public class Unit : MonoBehaviour, IUnit
{
    [SerializeField] private ProgressBar progressBar;
    [SerializeField] private float lives;
    [SerializeField] private TypesOfUnits typeOfUnit;
    private Tags enemyTag;

    public string EnemyTag { get => GameDataHelper.GetTag(enemyTag); set => enemyTag = GameDataHelper.GetTag(value); }
    public TypesOfUnits TypeOfUnit {  get { return typeOfUnit; } }

    public float Lives
    {
        get => lives;
        set
        {
            lives = value;
            livesChanged?.Invoke(lives);
        }
    }

    public event Action<float> livesChanged;
    public event Action<GameObject> die;

    private void Start()
    {
        if (progressBar == null) return;
        livesChanged += progressBar.ChangeValue;
        progressBar.MaxValue = lives;
        progressBar.StartValue = lives;

        if (gameObject.tag == GameDataHelper.GetTag(Tags.BlueTeam))
        {
            progressBar.SetColor(Color.green);
            EnemyTag=GameDataHelper.GetTag(Tags.RedTeam);
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
        die?.Invoke(gameObject);
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        if (progressBar != null) livesChanged -= progressBar.ChangeValue;
    }
}
