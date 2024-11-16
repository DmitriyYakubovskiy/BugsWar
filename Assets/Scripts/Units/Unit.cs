using System;
using UnityEngine;

public class Unit : MonoBehaviour, IUnit
{
    [SerializeField] private ProgressBar progressBar;
    [SerializeField] private float lives;

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

    private void Start()
    {
        if (progressBar == null) return;
        livesChanged += progressBar.ChangeValue;
        progressBar.MaxValue = lives;
        progressBar.StartValue = lives;
    }

    public void TakeDamage(float damage)
    {
        Lives -= damage;
        if(Lives<=0) Destroy(gameObject);
    }

    private void OnDestroy()
    {
        if (progressBar != null) livesChanged -= progressBar.ChangeValue;
    }
}
