using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit.Utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandTracker : MonoBehaviour
{
    private Handedness handedness = Handedness.Right;
    [SerializeField] private GameObject reflectorShild;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MixedRealityPose pose;
        if (HandJointUtils.TryGetJointPose(TrackedHandJoint.Palm, this.handedness, out pose))
        {
            reflectorShild.transform.localPosition = pose.Position;
        }
    }
}
