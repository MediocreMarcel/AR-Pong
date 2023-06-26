using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public enum GameState
{
    MENU, SERVE, PLAY
}

public class GameHandler : MonoBehaviour
{
    int points = 0;
    GameState state = GameState.SERVE;
    [SerializeField] GameObject GoOverlayPinchToThrow;
    [SerializeField] GameObject GoOverlayPoints;
    TMP_Text OverlayPoints;
    
    // Start is called before the first frame update
    void Start()
    {
        OverlayPoints = GoOverlayPoints.GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onBallServed()
    {
        this.state = GameState.PLAY;
        this.GoOverlayPinchToThrow.SetActive(false);
        this.GoOverlayPoints.SetActive(true);
    }

    public void onBallDestroyed()
    {
        this.state = GameState.SERVE;
        this.GoOverlayPinchToThrow.SetActive(true);
        this.GoOverlayPoints.SetActive(false);
        this.points = 0;
        this.UpdatePointOverlay();
    }

    public void IncreasePoints()
    {
        this.points++;
        this.UpdatePointOverlay();
    }

    private void UpdatePointOverlay()
    {
        this.OverlayPoints.text = $"Points: {this.points}";
    }
}
