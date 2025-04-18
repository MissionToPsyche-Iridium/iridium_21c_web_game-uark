using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    public void LoadTitle()
    {
        SceneManager.LoadScene("title screen");
    }
    public void LoadHome()
    {
        Debug.Log("Loading Home Scene");
        SceneManager.LoadScene("home");
    }

    public void LoadGame1()
    {
        SceneManager.LoadScene("Game1");
    }

    public void LoadGame2()
    {
        SceneManager.LoadScene("Game2");
    }

    public void LoadGame3()
    {
        SceneManager.LoadScene("Game3");
    }

    public void LoadGame4()
    {
        SceneManager.LoadScene("Game4");
    }
}
