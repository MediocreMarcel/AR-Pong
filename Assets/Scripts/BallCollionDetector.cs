using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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

    //Ball and Walls collision handler
    private void OnCollisionEnter(Collision collision)
    {
        //While playing in AreaMode collisions with anything but the designated playarea will kill the Ball
        if (GameHandler.mode.Equals(GameMode.AreaMode) && collision.gameObject.layer.Equals(31))
        {
            Destroy(gameObject.transform.parent.gameObject);
            GameHandler.onBallDestroyed();
        }

        Vector3 newDirection = Vector3.Reflect(transform.forward, collision.contacts[0].normal);
        transform.rotation = Quaternion.LookRotation(newDirection);

        Rigidbody ballRigidbody = GetComponent<Rigidbody>();
        ballRigidbody.velocity = transform.TransformDirection(new Vector3(0f, 0f, 1.1f));

        if (collision.gameObject.tag.Equals("ReflectorShield"))
        {
            this.GameHandler.IncreasePoints();
        }
    }

    //Ball and PowerUp collision handler
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
    private void RevertScaleChange()
    {
        this.ReflectorShield.transform.localScale -= scaleChange;
    }
    private void RevertPointMultiplier()
    {
        this.GameHandler.GetComponent<GameHandler>().pointmulitplier = this.GameHandler.GetComponent<GameHandler>().pointmulitplier / 2;
    }
}
