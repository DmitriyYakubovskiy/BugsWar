using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinDefeatViewer : MonoBehaviour
{
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private Button retryButton, menuButton;
    [SerializeField] private TMP_Text text, textOnPanel;

    void Start()
    {
        GameSceneManager sceneManager = GameObject.FindAnyObjectByType<GameSceneManager>();
        gameOverPanel.SetActive(false);
        menuButton.onClick.AddListener(GoToMenu);
        print(GameData.score);
    }

    public void Result(bool result)
    {
        Time.timeScale = 0;
        if (result)
        {
            gameOverPanel.SetActive(true);
            text.text = "��������� �������";
            GameData.score++;
            retryButton.onClick.AddListener(NextLevel);
        }
        else
        {
            textOnPanel.text = "���� �� ��������";
            if (GameData.score != 2)
            {
                gameOverPanel.SetActive(true);
                text.text = "������ ������";
                retryButton.onClick.AddListener(RetryGame);
            }
            else
            {
                gameOverPanel.SetActive(true); 
                textOnPanel.text = "��� �� ������!";
                text.text = "������� ������";
                retryButton.onClick.AddListener(RetryGame);
            }
        }
        
    }

    void RetryGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1;
    }

    void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1;

    }

    void GoToMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Menu");
    }
}
