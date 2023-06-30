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
    public GameState state = GameState.SERVE;
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
        this.state = GameState.SERVE;
        this.GoOverlayPinchToThrow.SetActive(true);
    }

    public void onBallServed()
    {
        this.state = GameState.PLAY;
        this.GoOverlayPinchToThrow.SetActive(false);
        this.GoPointsText.SetActive(true);
    }

    public void onBallDestroyed()
    {
        this.state = GameState.GAME_OVER;
        this.GoPointsText.SetActive(false);
        this.GoGameOverUi.SetActive(true);

        TextGameOver.SetText($"Game Over\nPoints: {this.points}");
    }

    public void IncreasePoints()
    {
        this.points++;
        this.UpdatePointOverlay();
    }

    private void UpdatePointOverlay()
    {
        this.TextPoints.text = $"Points: {this.points}";
    }
}
