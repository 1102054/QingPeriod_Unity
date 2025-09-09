using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;

public class UIHover : MonoBehaviour
{
    // Start is called before the first frame update
    public bool uIHovering = false;
    private GameObject controller;
    private ActionBasedControllerManager controllerScript;
    void Start()
    {
        controller = transform.parent.gameObject;
        controllerScript = controller.GetComponent<ActionBasedControllerManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(uIHovering)
        {
            controllerScript.enabled = false;
        }
        else
        {
            controllerScript.enabled = true;
        }
    }

    public void setuIHovering(bool value)
    {
        uIHovering = value;
        Debug.Log("UI Hovering: " + uIHovering);
    }
}