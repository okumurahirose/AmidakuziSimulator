using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneControling : MonoBehaviour
{
    public void TitleToGenerateSerect()
    {
        SceneManager.LoadScene("GenerateSerect");
    }

    public void GenerateSerectToMain()
    {
        SceneManager.LoadScene("Main");
    }

    public void GameQuit()
    {
        Debug.Log("ゲームを終了");
        Application.Quit();
    }
}
