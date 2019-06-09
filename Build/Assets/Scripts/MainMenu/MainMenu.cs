using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string level;

    public void PlayGame()
    {
        StaticValues.level = "1";
        SceneManager.LoadScene(1);
    }

    public void PlaySelected()
    {
        SceneManager.LoadScene(level);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
