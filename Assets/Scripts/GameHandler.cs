using Microsoft.MixedReality.Toolkit.UI.BoundsControl;
using Microsoft.MixedReality.Toolkit.Utilities.Solvers;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public enum GameState
{
    PLACE, SERVE, PLAY, GAME_OVER
}

public class GameHandler : MonoBehaviour
{
    public static GameState state = GameState.SERVE;
    public static GameMode mode;
    public static List<HighScoreEntry> HighScores = new List<HighScoreEntry>();
    public int pointmulitplier = 1;
    public List<GameObject> powerUpList = new List<GameObject>();
    public float PowerUpInterval = 5f;
    int points = 0;
    public int PowerUpAmountLimit = 6;
    private float distanceToWall = 0;
    RaycastHit hit;
    
    [SerializeField] GameObject GoOverlayPinchToThrow;

    [SerializeField] GameObject GoGameOverUi;
    TMP_Text TextGameOver;
    [SerializeField] GameObject PlayableWall;
    [SerializeField] GameObject GoPointsText;
    [SerializeField] GameObject ReflectorShield;
    TMP_Text TextPoints;
    [SerializeField] GameObject PlacementDoneOverlay;

    [SerializeField] GameObject PowerUpPrefabReflectorShield, PowerUpPrefabPointMultiplier;
    private GameObject PlayZone;

    private void Start()
    {
        TextGameOver = GoGameOverUi.transform.GetChild(0).GetComponent<TMP_Text>();
        TextPoints = GoPointsText.GetComponent<TMP_Text>();
        mode = SceneManager.GetActiveScene().name.Equals("SpacialMode") ? GameMode.SpacialMode : GameMode.AreaMode;
        
        Restart();  
    }

    public void SwitchToMainMenu()
    {
        Destroy(PlayZone);
        SceneManager.LoadScene("MainMenu");
    }

    public void Restart()
    {
        
        this.points = 0;
        this.UpdatePointOverlay();
        this.GoGameOverUi.SetActive(false);
        if (mode.Equals(GameMode.SpacialMode))
        {
            GameHandler.state = GameState.SERVE;
            this.ReflectorShield.SetActive(true);
            this.GoOverlayPinchToThrow.SetActive(true);
        }
        if (mode.Equals(GameMode.AreaMode))
        {
            GameHandler.state = GameState.PLACE;
            this.PlacementDoneOverlay.SetActive(true);
            if(PlayZone == null)
            {
                Vector3 CameraPos = Camera.main.transform.position;
                Vector3 CameraDirection = Camera.main.transform.forward;
                Quaternion CameraRotation = Camera.main.transform.rotation;
                float spawnDistance = 0.5f;

                Vector3 spawnPos = CameraPos + CameraDirection * spawnDistance;

                PlayZone = Instantiate(PlayableWall, spawnPos, CameraRotation).gameObject;
            }
            else
            {
                PlayZone.transform.GetChild(0).gameObject.GetComponent<TapToPlace>().enabled = true;
                PlayZone.transform.GetChild(0).gameObject.GetComponent<BoundsControl>().enabled = true;
            }
            
        }
    }
     
    public void onBallServed()
    {
        GameHandler.state = GameState.PLAY;
        this.GoOverlayPinchToThrow.SetActive(false);
        this.GoPointsText.SetActive(true);
        InvokeRepeating("SpawnPowerUps", 2f, PowerUpInterval);
    }
    public void onBallDestroyed()
    {
        GameHandler.state = GameState.GAME_OVER;
        this.GoPointsText.SetActive(false);
        this.GoGameOverUi.SetActive(true);
        this.ReflectorShield.SetActive(false);
        CancelInvoke("SpawnPowerUps");
        if(mode.Equals(GameMode.AreaMode)) 
        {
            //Destroy(PlayZone);
        }
        foreach (GameObject powerUp in powerUpList) { Destroy(powerUp); }

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
    public void onPlacementDone()
    {
        PlayZone.transform.GetChild(0).gameObject.GetComponent<TapToPlace>().enabled = false;
        PlayZone.transform.GetChild(0).gameObject.GetComponent<BoundsControl>().enabled = false;
        GameHandler.state = GameState.SERVE;
        this.PlacementDoneOverlay.SetActive(false);
        this.ReflectorShield.SetActive(true);
        this.GoOverlayPinchToThrow.SetActive(true);

    }

    public void IncreasePoints()
    {
        this.points = this.points + (1 * pointmulitplier);
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
    
    private void SpawnPowerUps()
    {
        if (powerUpList.Count < PowerUpAmountLimit && distanceToWall >= 1.5f) 
        {
            float x = Random.Range(0.1f, 0.90f);
            float y = Random.Range(0.1f, 0.90f);
            Vector3 pos = new Vector3(x, y, distanceToWall / 1.5f);
            //Vector3 pos = new Vector3(0.5f, 0.5f, distanceToWall / 1.5f);
            pos = Camera.main.ViewportToWorldPoint(pos);
            
            float powerUpType = Random.Range(0f, 1f);
            Physics.Raycast(Camera.main.transform.transform.position, Camera.main.transform.forward, out hit);
           
            if (powerUpType <= 0.5f) //Point Multiplier Buff
            {
                GameObject realPowerUp = Instantiate(PowerUpPrefabPointMultiplier, pos, Camera.main.transform.rotation).gameObject;
                powerUpList.Add(realPowerUp);
            }
            else //ReflectorShield Buff
            {
                GameObject realPowerUp = Instantiate(PowerUpPrefabReflectorShield, pos, Camera.main.transform.rotation).gameObject;
                powerUpList.Add(realPowerUp);
            }
        }
    }
    
    private void Update()
    {
        
        if (mode.Equals(GameMode.SpacialMode))
        {
            //Cast a Ray to determine the current distance from the next wall that is facing the player
            Physics.Raycast(Camera.main.transform.transform.position, Camera.main.transform.forward, out hit);

            if (hit.transform.gameObject.layer.Equals(13) || hit.transform.gameObject.layer.Equals(31))
            {
                //Debug.Log("success distance: "+ hit.distance);
                distanceToWall = hit.distance;
            }
        }
    }
}
