using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HighScoreSlateHandler : MonoBehaviour
{
    [SerializeField] GameObject GoHighscoreText;
    private TMP_Text TextHighscore;

    // Start is called before the first frame update
    void Start()
    {
        TextHighscore = GoHighscoreText.GetComponent<TMP_Text>();
        this.SetHighscoreText();
    }

    /// <summary>
    /// Switches to main menu scene
    /// </summary>
    public void OpenMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    /// <summary>
    /// Updates the UI to display the current highscores
    /// </summary>
    private void SetHighscoreText()
    {
        List<HighScoreEntry> highscores = GameHandler.HighScores;
        StringBuilder highscoreText = new StringBuilder("Score\tMode\n\n");
        foreach (HighScoreEntry entry in highscores)
        {
            highscoreText.Append($"{entry.Score}\t");
            highscoreText.Append(entry.Mode.Equals(GameMode.AreaMode) ? "   Area" : "Spacial");
            highscoreText.Append(" Mode\n");
        }
        if (highscoreText.Length > 12)
        {
            TextHighscore.SetText(highscoreText);
        }
    }
}
