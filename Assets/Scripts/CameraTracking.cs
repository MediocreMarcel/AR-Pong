using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTracking : MonoBehaviour
{

    [SerializeField] private GameObject TrackedObject;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = TrackedObject.transform.position;
        this.transform.rotation = TrackedObject.transform.rotation;
    }
}
