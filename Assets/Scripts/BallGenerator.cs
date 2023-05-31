using Microsoft.MixedReality.Toolkit;
using Microsoft.MixedReality.Toolkit.Input;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeGenerator : MonoBehaviour, IMixedRealityPointerHandler
{
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
        Debug.Log("Clicked");        
    }

    public void OnPointerDown(MixedRealityPointerEventData eventData)
    {
        Debug.Log("Pointer Down");
        //throw new System.NotImplementedException();
    }

    public void OnPointerDragged(MixedRealityPointerEventData eventData)
    {
        Debug.Log("Dragged");
        //throw new System.NotImplementedException();
    }

    public void OnPointerUp(MixedRealityPointerEventData eventData)
    {
        Debug.Log("Pointer up");
        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.transform.localPosition = reflectorShield.transform.localPosition;
        cube.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
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
