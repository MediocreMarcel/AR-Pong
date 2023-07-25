using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EndZoneCollider : MonoBehaviour
{
    public UnityEvent OnEndzoneCollision;

    /// <summary>
    /// Handles a collision with the EndZone Backup-Walls.
    /// Checks if the collission occured between the backup walls and the ball.
    /// This can happen if the spacial scan of the HoloLens is incomplice or improper
    /// </summary>
    /// <param name="collision">Unity Collision Object</param>
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Ball")
        {
            Destroy(collision.gameObject.transform.parent.gameObject);
            OnEndzoneCollision.Invoke();
        }
    }
}
