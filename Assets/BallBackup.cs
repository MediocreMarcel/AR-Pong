using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBackup : MonoBehaviour
{
    Rigidbody rb;
    public float stationaryTolerance;
    // Start is called before the first frame update
    void Start()
    {
        stationaryTolerance = 0.5f;
        rb = this.gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (IsStationary)
        {
            StartCoroutine(LongerStationary());
        }
    }
    IEnumerator LongerStationary()
    {
        
        yield return new WaitForSeconds(0.5f);
        if (IsStationary)
        {
            rb.velocity = transform.TransformDirection(new Vector3(0f, 0f, 1.1f));
        }
    }
    public bool IsStationary
    {
        get
        {
            return rb.velocity.magnitude < stationaryTolerance;
        }
    }
}
