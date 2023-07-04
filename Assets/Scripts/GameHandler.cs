using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState
{
    SERVE, PLAY, GAME_OVER
}

public class GameHandler : MonoBehaviour
{
    public static GameState state = GameState.SERVE;
    public static GameMode mode;
    public static List<HighScoreEntry> HighScores = new List<HighScoreEntry>();

    int points = 0;

    [SerializeField] GameObject GoOverlayPinchToThrow;

    [SerializeField] GameObject GoGameOverUi;
    TMP_Text TextGameOver;

    [SerializeField] GameObject GoPointsText;
    TMP_Text TextPoints;

    private void Start()
    {
        TextGameOver = GoGameOverUi.transform.GetChild(0).GetComponent<TMP_Text>();
        TextPoints = GoPointsText.GetComponent<TMP_Text>();
        mode = SceneManager.GetActiveScene().name.Equals("SpacialMode") ? GameMode.SpacialMode : GameMode.AreaMode;
    }

    public void SwitchToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void Restart()
    {
        this.points = 0;
        this.UpdatePointOverlay();
        this.GoGameOverUi.SetActive(false);
        GameHandler.state = GameState.SERVE;
        this.GoOverlayPinchToThrow.SetActive(true);
    }

    public void onBallServed()
    {
        GameHandler.state = GameState.PLAY;
        this.GoOverlayPinchToThrow.SetActive(false);
        this.GoPointsText.SetActive(true);
    }

    public void onBallDestroyed()
    {
        GameHandler.state = GameState.GAME_OVER;
        this.GoPointsText.SetActive(false);
        this.GoGameOverUi.SetActive(true);

        int scorePosition = this.PlaceHighScore(new HighScoreEntry(this.points, GameHandler.mode));

        if (scorePosition < 5 && scorePosition >= 0)
        {
            TextGameOver.SetText($"Game Over\nPoints: {this.points}\n\nNew Highscore at Position {scorePosition + 1}!");
        }
        else
        {
            TextGameOver.SetText($"Game Over\nPoints: {this.points}");
        }

    }

    public void IncreasePoints()
    {
        this.points++;
        this.UpdatePointOverlay();
    }

    private int PlaceHighScore(HighScoreEntry score)
    {
        int posOfNewScore = -1;

        GameHandler.HighScores.Add(score);
        GameHandler.HighScores.Sort((x, y) => y.Score.CompareTo(x.Score));
        posOfNewScore = GameHandler.HighScores.IndexOf(score);

        if (GameHandler.HighScores.Count > 5)
        {
            GameHandler.HighScores = GameHandler.HighScores.GetRange(0, 5);
        }

        return posOfNewScore;
    }

    private void UpdatePointOverlay()
    {
        this.TextPoints.text = $"Points: {this.points}";
    }
}
