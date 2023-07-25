using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Update position and rotation of the object that this script is attached to, to the position and rotation of the tracked object
/// </summary>
public class CameraTracking : MonoBehaviour
{
    [SerializeField] private GameObject TrackedObject;
    
    //Update position and rotation on every frame
    void Update()
    {
        this.transform.position = TrackedObject.transform.position;
        this.transform.rotation = TrackedObject.transform.rotation;
    }
}
