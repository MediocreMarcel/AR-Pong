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

    // Start is called before the first frame update
    void Start()
    {
        CoreServices.InputSystem?.RegisterHandler<IMixedRealityPointerHandler>(this);
    }

    //On start pinch motion spawn the ballprefab and "shoot" it in direction of the reflector shield  
    public void OnPointerUp(MixedRealityPointerEventData eventData)
    {
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
        CoreServices.InputSystem?.RegisterHandler<IMixedRealityPointerHandler>(this);
    }

    private void OnDisable()
    {
        CoreServices.InputSystem?.UnregisterHandler<IMixedRealityPointerHandler>(this);
    }

    public void OnPointerClicked(MixedRealityPointerEventData eventData)
    {
    }

    public void OnPointerDown(MixedRealityPointerEventData eventData)
    {
        
    }

    public void OnPointerDragged(MixedRealityPointerEventData eventData)
    {
        
    }
}
