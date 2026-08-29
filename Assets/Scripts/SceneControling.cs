using UnityEngine;
using UnityEngine.SceneManagement;

//各シーンへの遷移を実行します。

public class SceneControling : MonoBehaviour
{   
    //タイトルシーンに行きます
    public void ToTitle()
    {
        SceneManager.LoadScene("Title");
    }
    
    //あみだくじの生成条件設定シーンに行きます
    public void ToGenerateSerect()
    {
        SceneManager.LoadScene("GenerateSerect");
    }

    //ゲームの汎用設定シーンに行きます
    public void ToSettingMenu()
    {
        SceneManager.LoadScene("SettingMenu");
    }

    //ゲームのメインとなるプレイシーンに行きます
    public void ToMain()
    {
        SceneManager.LoadScene("Main");
    }

    //ゲームを終了します
    public void GameQuit()
    {
        Debug.Log("ゲームを終了");
        Application.Quit();
    }
}
