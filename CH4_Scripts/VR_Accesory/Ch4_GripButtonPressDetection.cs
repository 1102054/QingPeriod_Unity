using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR;
using System.Reflection;
using System;
using TMPro;
//using Palmmedia.ReportGenerator.Core.Parser.Analysis;

public class Ch4_GripButtonPressDetection : MonoBehaviour
{
    public static Ch4_GripButtonPressDetection Instance;
    //public float pickUpRange = 20f; //how far the player can pickup the object from

    public InputActionReference VR_Input_Grip_Reference;// Input Action 
    public InputAction VR_Input_Grip;

    public InputActionReference VR_Input_right_A_Reference; // ***** 右手A鍵 *****
    public InputAction VR_Input_right_A;

    public InputActionReference VR_Input_right_B_Reference; // ***** 右手B鍵 *****
    public InputAction VR_Input_right_B;


    // ***** *****
    public GameObject player; // 玩家本人
    public GameObject cameraSpoon; // 湯匙的相機
    public GameObject rayOriginObject; // raycast的射線起點，放camera
    public GameObject heldObj; // object which we pick up
    public bool SilkPick = false;  // check silk pick up
    bool teaFinish = false; // check tea put 
    public Transform holdPos; // 是拿著東西(child)位置的 parent
    private Rigidbody heldObjRb; //rigidbody of object we pick up
    private int LayerNumber; //layer index
    private bool isSpoon = false; // 是否有拿著湯匙
    public bool light_spoon = false; // 湯匙是否發亮
    public bool light_bad = false; // 壞葉子是否發亮
    int badLeafCount = 0; // 計算壞的葉子
    public bool light_nice = false; // 好的葉子整坨發亮
    private bool BadLeafOnSpoon = false; // 壞的葉子在湯匙上
    private bool leafOnSpoon = false; // 葉子在湯匙上
    public bool TeaPackTook = false; // 包好的茶包有沒有被拿
    private int teaCount = 0; //只能從籃子拿10次，數量到了就不能放了
    private int teaPos = 0; // 葉子的位置
    public int teaNum = 0; // 葉子的數量
    public Transform SpoonOriPos; // ***** 湯匙原本的位置 *****
    public GameObject teaLeaf_plate; // 在盤子上的葉子，先關掉，之後再打開
    public GameObject BadLeaf_Trash; // 在垃圾桶的壞葉子
    public int BadteaNum = 0; // 壞葉子的數量

    public bool light_trash = false; // 垃圾桶否發亮
    public bool light_plate = false; // 秤盤是否發亮

    private int tuoPos = 0; //砝碼位置
    public bool finish = false; // ***** 砝碼完成 *****
    public GameObject badLeaf;
    public GameObject waiterTalk; // ***** waiter 要打開的talk，原本關著 *****

    bool M10Finish = false;
    bool M11Finish = false;
    bool M12Finish = false;

    public GameObject Plate_place;
    public GameObject Weight_place;
    public GameObject Branch;
    public GameObject Noose;

    private Vector3 ini_Plate_pos;
    private Vector3 ini_Weight_pos;
    private Vector3 ini_Noose_pos;
    private Vector3 ini_Branch_pos;
    private Quaternion ini_Branch_rot;

    private int teaNow = 0;//PickOrPut 現在的葉子

    void Start()
    {
        VR_Input_Grip = VR_Input_Grip_Reference.action;
        VR_Input_Grip.performed += VR_Input_Grip_function;

        VR_Input_right_A = VR_Input_right_A_Reference.action;
        VR_Input_right_A.performed += VR_Input_right_A_function;

        VR_Input_right_B = VR_Input_right_B_Reference.action;
        VR_Input_right_B.performed += VR_Input_right_B_function;

        LayerNumber = LayerMask.NameToLayer("eee"); // ***** 找到 eee 在哪一個layer *****
        ini_Plate_pos = Plate_place.transform.position;
        ini_Weight_pos = Weight_place.transform.position;
        ini_Noose_pos = Noose.transform.position;
        ini_Branch_pos = Branch.transform.position;
        ini_Branch_rot = Branch.transform.rotation;
        badLeafCount = badLeaf.transform.childCount;
    }

    void Update()
    {
        CheckBalance();
        EndTea();
    }

    void VR_Input_Grip_function(InputAction.CallbackContext context) // left X
    {
        /*
        RaycastHit hit;
        //int layerRaycast1 = LayerMask.GetMask("eee"); // layer Raycast
        Ray ray = new Ray(rayOriginObject.transform.position, rayOriginObject.transform.forward);
        float maxDistance = 10f; // Mathf.Infinity
         */

        /*
        if (Physics.Raycast(ray, out hit, maxDistance))
        {
            GameObject hitObject = hit.collider.gameObject;
            Debug.Log(hitObject.name);
        */

            if (heldObj == null) //if currently not holding anything
            {
                //Debug.Log(hitObject.tag);
                //Debug.Log(ch4_VR_Raycast.Instance.VRRaycastTag_Silk);
                if (ch4_VR_Raycast_Left.Instance.VRRaycastTag_Silk && MissionControlver2_Edited.Instance.take1)
                {
                    //Debug.Log(ch4_VR_Raycast.Instance.currentInteractable.gameObject);
                    TakeSilk(ch4_VR_Raycast_Left.Instance.currentInteractable.gameObject);
                    SilkPick = true;
                    MissionControlver2_Edited.Instance.M2Check();
                }
                else if (ch4_VR_Raycast_Left.Instance.VRRaycastTag_Spoon && MissionControlver2_Edited.Instance.take2) //撿湯匙
                {
                    if (!teaFinish)
                    {
                        TakeSpoon(ch4_VR_Raycast_Left.Instance.currentInteractable.gameObject);
                        isSpoon = true;
                        light_spoon = false;
                        light_bad = true;
                        if (badLeafCount == 0)
                        {
                            light_nice = true;
                        }
                    }
                }
                else if (ch4_VR_Raycast_Left.Instance.VRRaycastTag_TeaPack)
                {
                    //Debug.Log("有嗎??????");
                    TakeTeaPack(ch4_VR_Raycast_Left.Instance.currentInteractable.gameObject);
                    TeaPackTook = true;
                    MissionControlver2_Edited.Instance.M14Check();
                }
                /*
                */

            }
            else
            {
                if (BadLeafOnSpoon == false && leafOnSpoon == false && isSpoon) //放湯匙
                {
                    putBackSpoon();
                    isSpoon = false;
                    if (teaCount == 5)
                    {
                        light_spoon = false;
                    }
                    else
                    {
                        light_spoon = true;
                        light_bad = false;
                        light_nice = false;
                    }
                    Debug.Log("放回湯匙");
                }
            }
    }

    // ***** 按下右手A鍵會執行的事 right A *****
    void VR_Input_right_A_function(InputAction.CallbackContext context)
    {
        /*
        RaycastHit hit;
        //int layerRaycast1 = LayerMask.GetMask("silkRaycast"); // layer Raycast
        Ray ray = new Ray(rayOriginObject.transform.position, rayOriginObject.transform.forward);
        float maxDistance = 10f; // Mathf.Infinity
         */

        if (heldObj != null) //if player is holding object
        {
            MoveObject();
            if (heldObj.transform.gameObject.tag == "Spoon")
            {
                //print(hit.transform.gameObject.tag);
                StartCoroutine(SpoonAni());
                // ***** 啟動協程（Coroutine)，協程是一種特殊的函數，可以在多幀之間執行操作，而不會阻塞主線程。 *****
                // 這對於需要等待某些事件完成或分步執行某些操作（例如，等待一段時間、執行異步操作、逐幀執行動作等）非常有用。

                if (ch4_VR_Raycast_Right.Instance.VRRaycastTag_BadLeaf) //撿黃茶葉
                {
                    if (!BadLeafOnSpoon)
                    {
                        Destroy(ch4_VR_Raycast_Right.Instance.currentInteractable.gameObject);
                        BadTeaToSpoon();
                        BadLeafOnSpoon = true;
                        light_trash = true;
                        light_bad = false;
                        //Debug.Log("拾取黃葉子");
                    }
                }
                if (badLeafCount == 0 && ch4_VR_Raycast_Right.Instance.VRRaycastTag_Basket)
                {
                    //Debug.Log("目前茶葉數量" + teaCount);
                    if (teaNow == 0 && teaCount < 10)
                    {
                        if (!teaFinish)
                        {
                            TeaToSpoon(); //裝茶葉到湯匙
                            leafOnSpoon = true;
                            light_nice = false;
                            light_plate = true;
                            //Debug.Log("茶葉硬了，燈要關");
                        }
                    }
                }
                if (ch4_VR_Raycast_Right.Instance.VRRaycastTag_Plate)
                {
                    if (leafOnSpoon == true)
                    {
                        TeaLeaveSpoon();
                        TeaOnPlate(); //放茶葉到盤子
                        leafOnSpoon = false;
                        teaCount++;
                        teaPos++;
                        light_nice = true;
                        light_plate = false;
                        //Debug.Log("茶葉登出，請開燈");
                    }
                }
                if (ch4_VR_Raycast_Right.Instance.VRRaycastTag_Trash && BadLeafOnSpoon)
                {
                    BadTeaLeaveSpoon();
                    BadToTrash();
                    leafOnSpoon = false;
                    BadLeafOnSpoon = false;
                    badLeafCount--;
                    light_trash = false;
                    light_bad = true;
                }
            }
        }

        if (ch4_VR_Raycast_Right.Instance.VRRaycastTag_Tuo)
        {
            if (!isSpoon)
            {
                if (tuoPos != 10)
                {
                    if (!finish)
                    {
                        tuoPos++;
                        teaPos--;
                    }
                    //Debug.Log("向右移秤砣");
                }
            }
        }
        /*
         */
    }
        
    // ***** 按下右手B鍵會執行的事 *****
    void VR_Input_right_B_function(InputAction.CallbackContext context)
    {
        /*
        RaycastHit hit;
        Ray ray = new Ray(rayOriginObject.transform.position, rayOriginObject.transform.forward);
        float maxDistance = 10f; // Mathf.Infinity
         */

        if (ch4_VR_Raycast_Right.Instance.VRRaycastTag_Tuo)
        {
            if (!isSpoon)
            {
                if (tuoPos != 0)
                {
                    if (!finish)
                    {
                        tuoPos--;
                        teaPos++;
                    }
                    //Debug.Log("向左移秤砣");
                }
            }
        }
    }


    // ***** 拿起絲綢 *****
    public void TakeSilk(GameObject pickUpObj)
    {
        if (pickUpObj.GetComponent<Rigidbody>()) //make sure the object has a RigidBody
        {
            heldObj = pickUpObj; //assign heldObj to the object that was hit by the raycast (no longer == null)
            heldObjRb = pickUpObj.GetComponent<Rigidbody>(); //assign Rigidbody
            heldObjRb.isKinematic = true;
            heldObjRb.transform.parent = holdPos.transform; //parent object to holdposition
            // ***** 相對父物件的轉換，如果是世界座標，就直接用position rotation scale *****
            heldObj.transform.localPosition = Vector3.zero;  // 將位置歸零
            heldObj.transform.localRotation = Quaternion.identity;  // 將旋轉歸零
            //heldObj.transform.localScale = Vector3.one;  // 將縮放設為原始大小（1,1,1）
            heldObj.layer = LayerNumber; //change the object layer to the holdLayer
            //make sure object doesnt collide with player, it can cause weird bugs
            Physics.IgnoreCollision(heldObj.GetComponent<Collider>(), player.GetComponent<Collider>(), true);
        }
    }

    // ***** 拿起湯匙 *****
    void TakeSpoon(GameObject pickUpObj)
    {
        if (pickUpObj.GetComponent<Rigidbody>()) //make sure the object has a RigidBody
        {
            heldObj = pickUpObj; //assign heldObj to the object that was hit by the raycast (no longer == null)
            heldObjRb = pickUpObj.GetComponent<Rigidbody>(); //assign Rigidbody
            heldObjRb.isKinematic = true;
            heldObjRb.transform.parent = holdPos.transform; //parent object to holdposition

            heldObj.transform.localPosition = Vector3.zero;  // 將位置歸零
            heldObj.transform.localRotation = Quaternion.identity;  // 將旋轉歸零
            //heldObj.transform.rotation = holdPos.rotation;
            heldObj.layer = LayerNumber; //change the object layer to the holdLayer
            int childCount = heldObj.transform.childCount;
            for (int i = 0; i < childCount; i++)
            {
                heldObj.transform.GetChild(i).gameObject.layer = LayerNumber;
            }
            //make sure object doesnt collide with player, it can cause weird bugs
            Physics.IgnoreCollision(heldObj.GetComponent<Collider>(), player.GetComponent<Collider>(), true);
        }
    }

    // ***** 拿起茶包 *****
    void TakeTeaPack(GameObject pickUpObj)
    {
        pickUpObj.SetActive(false);
    }
    
    // ***** 放回湯匙 *****
    void putBackSpoon()
    {
        Physics.IgnoreCollision(heldObj.GetComponent<Collider>(), player.GetComponent<Collider>(), false);
        heldObj.layer = 0; //object assigned back to default layer
        heldObjRb.isKinematic = false;
        heldObj.transform.parent = null; //unparent object
        heldObj.transform.position = SpoonOriPos.position;
        heldObj.transform.rotation = SpoonOriPos.rotation;
        heldObj = null; //undefine game object
    }

    // ***** 壞的葉子到湯匙 *****
    void BadTeaToSpoon()
    {
        if (teaNow == 0)
        {
            Transform spoonPos = cameraSpoon.transform.Find("holdPosition");
            Transform teaSpoon = spoonPos.transform.Find("ch4_store_teaSpoon");
            Transform teaLeaf = teaSpoon.Find("spoonBadLeaf");

            if (teaLeaf != null)
            {
                teaLeaf.gameObject.SetActive(true);
                teaNow++;
            }
        }
    }

    // ***** 壞的葉子離開湯匙 *****
    void BadTeaLeaveSpoon()
    {
        if (teaNow != 0)
        {
            Transform spoonPos = cameraSpoon.transform.Find("holdPosition");
            Transform teaSpoon = spoonPos.transform.Find("ch4_store_teaSpoon");
            Transform teaLeaf = teaSpoon.Find("spoonBadLeaf");
            if (teaLeaf != null)
            {
                teaLeaf.gameObject.SetActive(false);
                teaNow--;
            }

        }
    }

    // ***** 葉子到湯匙上 *****
    void TeaToSpoon()
    {
        if (teaNow == 0)
        {
            Transform spoonPos = cameraSpoon.transform.Find("holdPosition");
            Transform teaSpoon = spoonPos.transform.Find("ch4_store_teaSpoon");
            Transform teaLeaf = teaSpoon.Find("spoonLeaf");

            if (teaLeaf != null)
            {
                teaLeaf.gameObject.SetActive(true);
                teaNow++;
            }
        }
    }

    // ***** 葉子離開湯匙 *****
    void TeaLeaveSpoon()
    {
        if (teaNow != 0)
        {
            Transform spoonPos = cameraSpoon.transform.Find("holdPosition");
            Transform teaSpoon = spoonPos.transform.Find("ch4_store_teaSpoon");
            Transform teaLeaf = teaSpoon.Find("spoonLeaf");
            if (teaLeaf != null)
            {
                teaLeaf.gameObject.SetActive(false);
                teaNow--;
            }

        }
    }

    // ***** 放葉子到盤子上 *****
    void TeaOnPlate()
    {
        int childCount = teaLeaf_plate.transform.childCount;
        Transform[] leaf_child = new Transform[childCount];

        leaf_child[teaNum] = teaLeaf_plate.transform.GetChild(teaNum);
        leaf_child[teaNum].gameObject.SetActive(true);
        teaNum++;
        print("這裡嗎????");
        print(teaNum);
        MissionControlver2_Edited.Instance.MissionPanel[10].GetComponentInChildren<TMP_Text>().text = "盛裝所需份量的茶葉到秤盤上 ( " + teaNum.ToString() + " / 5 )";
    }

    // ***** 壞的葉子到垃圾筒 *****
    void BadToTrash()
    {
        int childCount = BadLeaf_Trash.transform.childCount;
        Transform[] leaf_child = new Transform[childCount];

        leaf_child[BadteaNum] = BadLeaf_Trash.transform.GetChild(BadteaNum);
        leaf_child[BadteaNum].gameObject.SetActive(true);
        BadteaNum++;

        MissionControlver2_Edited.Instance.MissionPanel[9].GetComponentInChildren<TMP_Text>().text = "拿取茶杓把全部的黃色茶葉丟到旁邊的垃圾桶 ( " + BadteaNum.ToString() + " / 3 )";
    }

    // ***** 實現物體動畫 平滑的效果，在多幀之間平滑過渡 *****
    IEnumerator SpoonAni()
    {
        float moveDistance = 3.0f; // 定義物體要移動的距離
        float moveDuration = 0.5f; // 定義移動所需的時間
        float elapsedTime = 0f; // 計時器，用於追蹤已經過的時間

        Vector3 originalPosition = heldObj.transform.position; // 記錄物體的原始位置
        Vector3 forwardDirection = holdPos.transform.forward; // 取得物體的前方方向
        Vector3 targetPosition = originalPosition + forwardDirection * moveDistance; // 計算目標位置

        // 平滑移動到目標位置
        while (elapsedTime < moveDuration)
        {
            heldObj.transform.position = Vector3.Lerp(originalPosition, targetPosition, elapsedTime / moveDuration);
            elapsedTime += Time.deltaTime; // 更新已經過的時間
            yield return null; // 暫停協程到下一幀再繼續
        }

        heldObj.transform.position = targetPosition; // 確保物體精確地到達目標位置

        // 重置計時器
        elapsedTime = 0f;

        // 等待1秒再移回原位
        yield return new WaitForSeconds(0.1f);

        // 平滑移動回原始位置
        while (elapsedTime < moveDuration)
        {
            heldObj.transform.position = Vector3.Lerp(targetPosition, originalPosition, elapsedTime / moveDuration);
            elapsedTime += Time.deltaTime; // 更新已經過的時間
            yield return null; // 暫停協程到下一幀再繼續
        }

        heldObj.transform.position = originalPosition; // 確保物體精確地回到原位
    }

    void MoveObject()
    {
        heldObj.transform.position = holdPos.transform.position;
    }

    // ***** 確認秤有沒有達平衡 ***** 
    void CheckBalance() 
    {
        PlatePosition();
        StonePosition();
    }

    void PlatePosition()
    {
        float plate_Y = -0.25f * teaPos;
        Plate_place.transform.position = ini_Plate_pos + new Vector3(0f, plate_Y, 0f);

        float noose_Y = -0.13f * teaPos;
        Noose.transform.position = ini_Noose_pos + new Vector3(0f, noose_Y, 0f);

        float branch_rot_X = 3f * teaPos;
        float branch_pos_Y = 0.065f * teaPos;
        float branch_pos_Z = 0.08f * teaPos;
        Branch.transform.position = ini_Branch_pos + new Vector3(0f, branch_pos_Y, branch_pos_Z);
        Branch.transform.rotation = ini_Branch_rot * Quaternion.Euler(branch_rot_X, 0f, 0f);
    }
    void StonePosition()
    {
        float weight_Z = -0.6f * tuoPos;
        Weight_place.transform.position = ini_Weight_pos + new Vector3(0f, 0f, weight_Z);
    }

    void EndTea()
    {
        if (badLeafCount == 0 && M10Finish == false)
        {
            MissionControlver2_Edited.Instance.M10Check();
            M10Finish = true;
            light_nice = true;
        }
        if (teaCount == 5 && M11Finish == false)
        {
            teaFinish = true;
            MissionControlver2_Edited.Instance.M11Check();
            M11Finish = true;
            light_spoon = false;
            light_nice = false;
        }
        if (teaCount == 5 && tuoPos == 5 && M12Finish == false)
        {
            finish = true;
            MissionControlver2_Edited.Instance.M12Check();
            waiterTalk.SetActive(true);
            M12Finish = true;
        }
    }
}
