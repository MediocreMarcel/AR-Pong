using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuHandler : MonoBehaviour
{
     /// <summary>
     /// Starts Spacial Game Mode
     /// </summary>
    public void StartSpacialMode()
    {
        SceneManager.LoadScene("SpacialMode");
    }

    /// <summary>
    /// Opens Area Game Mode
    /// </summary>
    public void StartAreaMode()
    {
        SceneManager.LoadScene("AreaMode");
    }

    /// <summary>
    /// Opens Highscore Board
    /// </summary>
    public void OpenHighscores()
    {
        SceneManager.LoadScene("Highscores");
    }
}
