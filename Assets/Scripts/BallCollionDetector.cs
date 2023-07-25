using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Class handles collision events of the ball
/// </summary>
public class BallCollionDetector : MonoBehaviour
{
    public UnityEvent OnPointCollission;
    private GameHandler GameHandler;
    private GameObject ReflectorShield;
    private float PowerUpUptime;
    private Vector3 scaleChange;

    void Start()
    {
        GameHandler = GameObject.Find("Game").GetComponent<GameHandler>();
        scaleChange = new Vector3(0.05f, 0.05f, 0f);
        this.ReflectorShield = GameObject.Find("ReflectorShield");
        PowerUpUptime = 15f;
    }

    /// <summary>
    /// Hanles Collisions of the ball
    /// </summary>
    /// <param name="collision">Unity collision object</param>
    private void OnCollisionEnter(Collision collision)
    {
        //While playing in AreaMode collisions with anything but the designated playarea will kill the Ball
        if (GameHandler.mode.Equals(GameMode.AreaMode) && collision.gameObject.layer.Equals(31))
        {
            Destroy(gameObject.transform.parent.gameObject);
            GameHandler.onBallDestroyed();
        }

        //Reflect Ball
        Vector3 newDirection = Vector3.Reflect(transform.forward, collision.contacts[0].normal);
        transform.rotation = Quaternion.LookRotation(newDirection);

        //Give Ball Velocity
        Rigidbody ballRigidbody = GetComponent<Rigidbody>();
        ballRigidbody.velocity = transform.TransformDirection(new Vector3(0f, 0f, 1.1f));

        //If collided with reflector shield, give player a point
        if (collision.gameObject.tag.Equals("ReflectorShield"))
        {
            this.GameHandler.IncreasePoints();
        }
    }

    /// <summary>
    /// Method handles collision between balls and powerups
    /// </summary>
    /// <param name="other">Unity provided Collider Object</param>
    private void OnTriggerEnter(Collider other)
    {
        GameObject parentObjectForRemoval = other.gameObject.transform.parent.gameObject;

        //On colliding with reflector shield powerup, increase size by a flat amount and remove the increase after a duration defined by PowerUpUptime
        if (other.gameObject.tag.Equals("PowerUpReflectorShield"))
        {
            this.ReflectorShield.transform.localScale += scaleChange;
            Invoke("RevertScaleChange", PowerUpUptime);
            GameHandler.powerUpList.Remove(parentObjectForRemoval);

            Destroy(parentObjectForRemoval);
        }
        //On colliding with double points powerup, double the current multiplier applied on gained points and remove the increase after a duration defined by PowerUpUptime
        if (other.gameObject.tag.Equals("PowerUpDoublePoints"))
        {
            GameHandler.GetComponent<GameHandler>().pointmulitplier = GameHandler.GetComponent<GameHandler>().pointmulitplier * 2;
            Invoke("RevertPointMultiplier", PowerUpUptime);
            GameHandler.powerUpList.Remove(parentObjectForRemoval);
            Destroy(parentObjectForRemoval);
        }
    }

    /// <summary>
    /// Method reverts the effect of a single reflector shield size powerup 
    /// </summary>
    private void RevertScaleChange()
    {
        this.ReflectorShield.transform.localScale -= scaleChange;
    }

    /// <summary>
    /// Method reverts the effect of a single Point Multiplier Powerup
    /// </summary>
    private void RevertPointMultiplier()
    {
        this.GameHandler.GetComponent<GameHandler>().pointmulitplier = this.GameHandler.GetComponent<GameHandler>().pointmulitplier / 2;
    }
}
