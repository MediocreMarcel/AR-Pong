using Microsoft.MixedReality.Toolkit;
using Microsoft.MixedReality.Toolkit.Input;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class BallGenerator : MonoBehaviour, IMixedRealityPointerHandler
{
    public UnityEvent OnBallServed;
    [SerializeField] private GameObject BallPrefab;
    [SerializeField] private GameObject reflectorShield;

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
        //throw new System.NotImplementedException();
    }

    public void OnPointerDragged(MixedRealityPointerEventData eventData)
    {
        //throw new System.NotImplementedException();
    }

    public void OnPointerUp(MixedRealityPointerEventData eventData)
    {
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
        if (GameHandler.state.Equals(GameState.PLACE))
        {
            Debug.Log("I would place");
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        CoreServices.InputSystem?.RegisterHandler<IMixedRealityPointerHandler>(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
