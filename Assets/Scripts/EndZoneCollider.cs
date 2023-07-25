using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EndZoneCollider : MonoBehaviour
{
    public UnityEvent OnEndzoneCollision;

    //Checks if the Ball collided with the Backup walls in case of improper deployment of wallmesh from Hololens
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Ball")
        {
            Destroy(collision.gameObject.transform.parent.gameObject);
            OnEndzoneCollision.Invoke();
        }
    }
}
