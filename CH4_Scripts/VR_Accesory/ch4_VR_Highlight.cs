using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ch4_VR_Highlight : MonoBehaviour
{
    private Transform highlight;
    public float pickUpRange = 20f;
    private Ch4_GripButtonPressDetection pickup;

    public GameObject Silk_hint;
    public GameObject Package_hint;
    public GameObject Spoon_hint;
    public GameObject TeaBad_hint;
    public GameObject TeaNice_hint;
    public GameObject Trash_hint;
    public GameObject Plate_hint;
    public GameObject Tuo_hint;

    void Start()
    {
        MissionControlver2_Edited.Instance = FindObjectOfType<MissionControlver2_Edited>();
        pickup = FindObjectOfType<Ch4_GripButtonPressDetection>();

        Silk_hint.SetActive(false);
        Package_hint.SetActive(false);
        Spoon_hint.SetActive(false);
        TeaBad_hint.SetActive(false);
        TeaNice_hint.SetActive(false);
        Trash_hint.SetActive(false);
        Plate_hint.SetActive(false);
        Tuo_hint.SetActive(false);
    }

    void Update()
    {
        if (MissionControlver2_Edited.Instance.light_silk)
        {
            GameObject silkObject = GameObject.FindWithTag("Silk");
            if (silkObject != null)
            {
                SetOutline(silkObject.transform, true);
                Silk_hint.SetActive(true);
            }
        }

        if (MissionControlver2_Edited.Instance.light_spoon || pickup.light_spoon)
        {
            GameObject spoonObject = GameObject.FindWithTag("Spoon");
            if (spoonObject != null)
            {
                SetOutline(spoonObject.transform, true);
                Spoon_hint.SetActive(true);
            }
        }

        if (MissionControlver2_Edited.Instance.light_teapack)
        {
            GameObject teaPackObject = GameObject.FindWithTag("TeaPack");
            if (teaPackObject != null)
            {
                SetOutline(teaPackObject.transform, true);
                Package_hint.SetActive(true);
            }
        }
        if(pickup.TeaPackTook)
        {
            Package_hint.SetActive(false);
        }

        GameObject[] badObjects = GameObject.FindGameObjectsWithTag("BadLeaf");
        foreach (GameObject badObject in badObjects)
        {
            if (badObject != null)
            {
                if (pickup.light_bad)
                {
                    SetOutline(badObject.transform, true);
                    TeaBad_hint.SetActive(true);
                }
                else
                {
                    SetOutline(badObject.transform, false);
                    TeaBad_hint.SetActive(false);
                }
            }
        }

        GameObject[] niceObjects = GameObject.FindGameObjectsWithTag("Basket");
        foreach (GameObject niceObject in niceObjects)
        {
            if (niceObject != null)
            {
                if (pickup.light_nice)
                {
                    SetOutline(niceObject.transform, true);
                    TeaNice_hint.SetActive(true);
                }
                else
                {
                    SetOutline(niceObject.transform, false);
                    TeaNice_hint.SetActive(false);
                }
            }
        }

        GameObject[] trashObjects = GameObject.FindObjectsOfType<GameObject>();
        foreach (GameObject trashObject in trashObjects)
        {
            if (trashObject.name == "trash_obj")
            {
                if (pickup.light_trash)
                {
                    SetOutline(trashObject.transform, true);
                    Trash_hint.SetActive(true);
                }
                else
                {
                    Trash_hint.SetActive(false);
                    SetOutline(trashObject.transform, false);
                }
            }
        }
       
        GameObject[] plateObjects = GameObject.FindObjectsOfType<GameObject>();
        foreach (GameObject plateObject in plateObjects)
        {
            if (plateObject.name == "plate_obj")
            {
                if (pickup.light_plate)
                {
                    SetOutline(plateObject.transform, true);
                    Plate_hint.SetActive(true);
                }
                else
                {
                    SetOutline(plateObject.transform, false);
                    Plate_hint.SetActive(false);
                }
            }
        }

        GameObject[] tuoObjects = GameObject.FindObjectsOfType<GameObject>();
        foreach (GameObject tuoObject in tuoObjects)
        {
            if (tuoObject.name == "tuo_obj")
            {
                if (MissionControlver2_Edited.Instance.light_tuo)
                {
                    SetOutline(tuoObject.transform, true);
                    Tuo_hint.SetActive(true);
                }
                else
                {
                    SetOutline(tuoObject.transform, false);
                    Tuo_hint.SetActive(false);
                }
            }
        }

        /*
        RaycastHit hit;
        bool raycastHit = Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, pickUpRange);
        */
        if (pickup.heldObj!=null)
        {
            //print(pickup.heldObj);
            /*
            if (raycastHit)
            {
            */
                Debug.Log(ch4_VR_Raycast_Left.Instance.VRRaycastTag_Silk);
                if (ch4_VR_Raycast_Left.Instance.VRRaycastTag_Silk)
                {
                    GameObject silkObject = ch4_VR_Raycast_Left.Instance.currentInteractable.gameObject;
                    SetOutline(silkObject.transform, false);
                    Silk_hint.SetActive(false);
                    MissionControlver2_Edited.Instance.light_silk = false;
                }
                else if (ch4_VR_Raycast_Left.Instance.VRRaycastTag_Spoon)
                {
                    GameObject spoonObject = ch4_VR_Raycast_Left.Instance.currentInteractable.gameObject;
                    SetOutline(spoonObject.transform, false);
                    Spoon_hint.SetActive(false);
                    MissionControlver2_Edited.Instance.light_spoon = false;
                }
                else if (ch4_VR_Raycast_Left.Instance.VRRaycastTag_TeaPack)
                {
                    GameObject teaPackObject = ch4_VR_Raycast_Left.Instance.currentInteractable.gameObject;;
                    SetOutline(teaPackObject.transform, false);
                    MissionControlver2_Edited.Instance.light_teapack = false;
                }
                /*
                */
            
        }
    }

    private void SetOutline(Transform target, bool enable)
    {
        Outline outline = target.gameObject.GetComponent<Outline>();
        if (outline != null)
        {
            outline.enabled = enable;
        }
        else if (enable) // 如果輪廓組件不存在且需要打開輪廓效果，則添加組件
        {
            outline = target.gameObject.AddComponent<Outline>();
            outline.enabled = true;
            outline.OutlineColor = Color.yellow;
            outline.OutlineWidth = 7.0f;
        }
    }
}
