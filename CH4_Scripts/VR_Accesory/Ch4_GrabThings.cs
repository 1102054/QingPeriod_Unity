using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Ch4_GrabThings : MonoBehaviour
{
    public static Ch4_GrabThings Instance;

    public static bool handleHoe;
    private XRGrabInteractable grabInteractable;


    void Awake()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();

        if (grabInteractable != null)
        {
            grabInteractable.selectEntered.AddListener(OnGrab);
            grabInteractable.selectExited.AddListener(OnRelease);
        }
    }

    void OnDestroy()
    {
        if (grabInteractable != null)
        {
            grabInteractable.selectEntered.RemoveListener(OnGrab);
            grabInteractable.selectExited.RemoveListener(OnRelease);
        }
    }

    private void OnGrab(SelectEnterEventArgs args)
    {
        string grabbedObjectName = args.interactableObject.transform.name;
        Debug.Log("Grabbed object name: " + grabbedObjectName);
        string grabbedObjectTag = args.interactableObject.transform.tag;
        /*
        if (grabbedObjectTag == "hoe")
        {
            handleHoe = true;
            if(CH3_MissionControlver2.Instance.missionManagerTalk)
            {
                CH3_MissionControlver2.Instance.M3Check();
            }
        }

        if(grabbedObjectTag == "Seed")
        {
            CH3_HistoryV5.Instance.TakeSeed();
        }
        */
    }

    private void OnRelease(SelectExitEventArgs args)
    {
        
        string releasedObjectName = args.interactableObject.transform.name;
        Debug.Log("Released object name: " + releasedObjectName);
        //handleHoe = false;
        
    }
}