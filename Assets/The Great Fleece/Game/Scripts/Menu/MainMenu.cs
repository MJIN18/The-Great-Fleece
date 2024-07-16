using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartButtonClicked()
    {
        SceneManager.LoadScene("Loading_Screen");
    }

    public void QuitButtonClicked()
    {
        Application.Quit();
    }
}
