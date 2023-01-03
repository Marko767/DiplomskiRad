using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartClick()
    {
        SceneManager.LoadScene("StartingScene");
    }

    public void OptionsClick()
    {
        Debug.Log("options coming soon");
    }

    public void MainMenuClick()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
