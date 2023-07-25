using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTracking : MonoBehaviour
{
    [SerializeField] private GameObject TrackedObject;
    
    //Update position of the Backwall behind the player
    void Update()
    {
        this.transform.position = TrackedObject.transform.position;
        this.transform.rotation = TrackedObject.transform.rotation;
    }
}
