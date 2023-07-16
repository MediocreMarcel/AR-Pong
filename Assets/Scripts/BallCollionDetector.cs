using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BallCollionDetector : MonoBehaviour
{
    public UnityEvent OnPointCollission;
    GameHandler GameHandler;
    GameObject ReflectorShield;
    float PowerUpUptime;

    private Vector3 scaleChange;
    // Start is called before the first frame update
    void Start()
    {
        GameHandler = GameObject.Find("Game").GetComponent<GameHandler>();
        scaleChange = new Vector3(0.05f, 0.05f, 0f);
        this.ReflectorShield = GameObject.Find("ReflectorShield");
        PowerUpUptime = 15f;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        Vector3 newDirection = Vector3.Reflect(transform.forward, collision.contacts[0].normal);
        //Rotate bullet to new direction
        transform.rotation = Quaternion.LookRotation(newDirection);

        Rigidbody ballRigidbody = GetComponent<Rigidbody>();
        ballRigidbody.velocity = transform.TransformDirection(new Vector3(0f, 0f, 1.5f));
        if (collision.gameObject.tag.Equals("ReflectorShield"))
        {
            this.GameHandler.IncreasePoints();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("PowerUpReflectorShield"))
        {
            this.ReflectorShield.transform.localScale += scaleChange;
            Invoke("RevertScaleChange", PowerUpUptime);
            GameHandler.GetComponent<GameHandler>().powerUpList.Remove(other.gameObject);
            Destroy(other.gameObject);
        }
        if (other.gameObject.tag.Equals("PowerUpDoublePoints"))
        {
            GameHandler.GetComponent<GameHandler>().pointmulitplier = GameHandler.GetComponent<GameHandler>().pointmulitplier * 2;
            Invoke("RevertPointMultiplier", PowerUpUptime * 3);
            GameHandler.GetComponent<GameHandler>().powerUpList.Remove(other.gameObject);
            Destroy(other.gameObject);
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
