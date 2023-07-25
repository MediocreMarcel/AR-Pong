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

    public void OpenMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    //Simple StringBuilder for the highscore display
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
