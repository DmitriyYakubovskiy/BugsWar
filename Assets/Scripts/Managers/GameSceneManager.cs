using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour
{
    public static void NextScene()
    {
        if (SceneManager.sceneCount >= SceneManager.GetActiveScene().buildIndex + 1) return;
        ChangeScene((SceneManager.GetActiveScene().buildIndex) + 1);
    }

    public static void ChangeScene(int numberScenes)
    {
        SceneManager.LoadScene(numberScenes);
    }

    public static void Restart()
    {
        ChangeScene(SceneManager.GetActiveScene().buildIndex);
    }
}
