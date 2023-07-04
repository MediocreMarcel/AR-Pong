using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuHandler : MonoBehaviour
{
    public void StartSpacialMode()
    {
        SceneManager.LoadScene("SpacialMode");
    }

    public void StartAreaMode()
    {
        
    }

    public void OpenHighscores()
    {
        SceneManager.LoadScene("Highscores");
    }
}
