using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit.Utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandTracker : MonoBehaviour
{
    private Handedness handedness = Handedness.Right;
    [SerializeField] private GameObject reflectorShield;

    //Update the position and rotation of the reflector shield based on the tracked hand position
    void Update()
    {
        MixedRealityPose pose;
        if (HandJointUtils.TryGetJointPose(TrackedHandJoint.Palm, this.handedness, out pose))
        {
            reflectorShield.transform.localPosition = pose.Position;
            reflectorShield.transform.rotation = pose.Rotation;
            reflectorShield.transform.rotation = reflectorShield.transform.rotation * Quaternion.Euler(new Vector3(90, 0, 0));
        }
    }
}
