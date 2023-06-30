using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BallCollionDetector : MonoBehaviour
{
    public UnityEvent OnPointCollission;
    GameHandler GameHandler;
    // Start is called before the first frame update
    void Start()
    {
        this.GameHandler = GameObject.Find("Game").GetComponent<GameHandler>();
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
        if (!collision.gameObject.tag.Equals("ReflectorShield") && !collision.gameObject.tag.Equals("Backwall"))
        {
            this.GameHandler.IncreasePoints();
        }
        
    }
}
