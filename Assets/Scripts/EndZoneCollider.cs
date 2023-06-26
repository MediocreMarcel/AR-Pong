using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EndZoneCollider : MonoBehaviour
{
    public UnityEvent OnEndzoneCollision;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Ball")
        {
            Destroy(collision.gameObject.transform.parent.gameObject);
            OnEndzoneCollision.Invoke();
            Debug.Log("End of game");
        }
    }
}
