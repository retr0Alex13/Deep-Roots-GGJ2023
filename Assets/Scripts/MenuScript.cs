using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.SceneManagement;

public class MenuScript : MonoBehaviour
{
    private int nextSceneToLoad;
    // Start is called before the first frame update
    void Start()
    {
        nextSceneToLoad = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex + 1;
    }

    public void NextLevel()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(nextSceneToLoad);
    }
    public void SettingsLevel()
    {
        // реализовать настройки
    }
    public void ExitGame()
    {
        Application.Quit();
    }
}
