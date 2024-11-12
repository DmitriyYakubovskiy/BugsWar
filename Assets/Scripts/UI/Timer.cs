using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerText;

    private float timeLeft = 0f;

    private void Start()
    {
        timeLeft = 0;
    }

    private void Update()
    {
        timeLeft += Time.deltaTime;
        timerText.text = GetStringTime();
    }

    public string GetStringTime()
    {
        float minutes = Mathf.FloorToInt(timeLeft / 60);
        float seconds = Mathf.FloorToInt(timeLeft % 60);
        return string.Format("{0:00} : {1:00}", minutes, seconds);
    }
}
