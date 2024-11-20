using System.Drawing;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    [SerializeField] private Image currentLine;
    [SerializeField] private float maxValue = 100;
    [SerializeField] private float startValue = 0;
    private float fill = 0;

    public float MaxValue
    {
        get => maxValue;
        set => maxValue = value;
    }

    public float StartValue
    {
        get => startValue;
        set
        { 
            startValue = value; 
            Init(); 
        }
    }

    private void Start()
    {
        Init();
    }

    private void FixedUpdate()
    {
        if (currentLine.fillAmount != fill) SmoothAddValue();
    }

    private void Init()
    {
        fill = startValue;
        currentLine.fillAmount = fill;
    }

    public void ChangeValue(float value)
    {
        if (value >= 0) fill = value / maxValue;
    }

    private void SmoothAddValue()
    {
        if (fill>=1 && currentLine.fillAmount>=1) return;

        if (currentLine.fillAmount > fill + 0.05f) currentLine.fillAmount -= 0.05f;
        else if (currentLine.fillAmount < fill - 0.05f) currentLine.fillAmount += 0.05f;
        else currentLine.fillAmount = fill;
    }
}
