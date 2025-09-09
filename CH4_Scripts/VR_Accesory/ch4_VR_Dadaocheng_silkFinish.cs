using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ch4_VR_Dadaocheng_silkFinish : MonoBehaviour
{
    public GameObject mainCamera; // ***** ��D�۾� *****
    public GameObject player; // ***** ��collider������A�����Pplayer�������I�� *****
    private GameObject heldObj_silk; // ***** ���۪�����A�h���F�誺�a��� *****
    private Rigidbody heldObjRb_silk; //rigidbody of object we pick up

    public void GaveSilk(GameObject pickUpObj)
    {
        // �b Main Camera �U���W�� holdPosition ���l������ഫ�� GameObject
        GameObject holdPosition = mainCamera.transform.Find("holdPosition")?.gameObject;
        if (holdPosition != null)
        {
            // �b holdPosition ���l���󤤧��W�� silk_1.0 �B���Ҭ� Silk ������
            GameObject silk = holdPosition.transform.Find("silk_1.0")?.gameObject;
            if (silk != null && silk.CompareTag("Silk"))
            {
                heldObj_silk = silk;
                heldObjRb_silk = heldObj_silk.GetComponent<Rigidbody>(); //assign Rigidbody

                Debug.Log("���F����: " + heldObj_silk.name);
            }
            else
            {
                Debug.LogWarning("�䤣��W�� silk_1.0 �B���Ҭ� Silk ������");
            }
        }
        else
        {
            Debug.LogWarning("�䤣��W�� holdPosition ���l����");
        }

        Physics.IgnoreCollision(heldObj_silk.GetComponent<Collider>(), player.GetComponent<Collider>(), false);
        heldObj_silk.layer = 0; //object assigned back to default layer
        heldObjRb_silk.isKinematic = false;
        heldObj_silk.transform.parent = null; //unparent object
        heldObj_silk = null; //undefine game object
        Destroy(pickUpObj);
        MissionControlver2_Edited.Instance.M6Check();
    }
}
