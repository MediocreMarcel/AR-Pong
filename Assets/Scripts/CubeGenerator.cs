using Microsoft.MixedReality.Toolkit;
using Microsoft.MixedReality.Toolkit.Input;
using System;
using System.Collections;
using System.Collections.Generic;
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
            GameObject ball = Instantiate(BallPrefab, (reflectorShield.transform.localPosition + new Vector3(0f, 0f, 0.1f)), reflectorShield.transform.localRotation).gameObject;
            Rigidbody ballRigidbody = ball.transform.Find("Ball").GetComponent<Rigidbody>();
            ballRigidbody.velocity = transform.TransformDirection(new Vector3(0f, 0f, 1.5f));
            this.OnBallServed.Invoke();
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
