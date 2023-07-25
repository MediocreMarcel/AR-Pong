using Microsoft.MixedReality.Toolkit;
using Microsoft.MixedReality.Toolkit.Input;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class CubeGenerator : MonoBehaviour, IMixedRealityPointerHandler
{
    public UnityEvent OnBallServed;
    [SerializeField] private GameObject BallPrefab;
    [SerializeField] private GameObject reflectorShield;

    void Start()
    {
        //Register this class as Pointer Handler to get poses like the pinch gesture
        CoreServices.InputSystem?.RegisterHandler<IMixedRealityPointerHandler>(this);
    }

    /// <summary>
    /// Handles the gester pointer up, which is equivalent to "after pinch".
    /// Method will spawn an instance of the ballprefab and "shoot" it in direction, in which the reflector shield is aimed, if the game is in the state "Serve".
    /// </summary>
    /// <param name="eventData">MRTK Provieded event data</param>
    public void OnPointerUp(MixedRealityPointerEventData eventData)
    {
        //On start pinch motion spawn the ballprefab and "shoot" it in direction of the reflector shield  
        //Only spawn ball if you are in the correct GameState
        if (GameHandler.state.Equals(GameState.SERVE))
        {
            Vector3 reflectorShieldPos = reflectorShield.transform.position;
            Vector3 reflectorShieldDirection = reflectorShield.transform.forward;
            Quaternion reflectorShieldRotation = reflectorShield.transform.rotation;
            float spawnDistance = 0.15f;

            Vector3 spawnPos = reflectorShieldPos + reflectorShieldDirection * spawnDistance;

            GameObject ball = Instantiate(BallPrefab, spawnPos, reflectorShieldRotation).gameObject;
            Rigidbody ballRigidbody = ball.transform.Find("Ball").GetComponent<Rigidbody>();
            ballRigidbody.velocity = transform.TransformDirection(new Vector3(0f, 0f, 1.1f));
            this.OnBallServed.Invoke();
        }
    }

    private void OnEnable()
    {
        //Re-Register this class as Pointer Handler to get poses like the pinch gesture if enabled
        CoreServices.InputSystem?.RegisterHandler<IMixedRealityPointerHandler>(this);
    }

    private void OnDisable()
    {
        //Deregister this class as Pointer Handler if disabled
        CoreServices.InputSystem?.UnregisterHandler<IMixedRealityPointerHandler>(this);
    }

    //Not used
    public void OnPointerClicked(MixedRealityPointerEventData eventData)
    {
    }

    //Not used
    public void OnPointerDown(MixedRealityPointerEventData eventData)
    {
        
    }

    //Not used
    public void OnPointerDragged(MixedRealityPointerEventData eventData)
    {
        
    }
}
