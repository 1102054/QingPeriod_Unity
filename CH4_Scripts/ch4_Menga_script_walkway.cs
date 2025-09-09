using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ch4_Menga_script_walkway : MonoBehaviour
{
    public GameObject player; // 設定玩家遊戲物件
    public GameObject ToPosition; // ***** 
    public GameObject Menga;
    public GameObject Dadaocheng;
    public GameObject Store;
    public Canvas Portal_Canvas;
    public GameObject DaPos;
    public GameObject StPos;


    void Start()
    {
        Menga.SetActive(true);
        Dadaocheng.SetActive(false);
        Store.SetActive(false);

    }

    void Update()
    {
        if (player.transform.position == DaPos.transform.position)
        {
            MissionControlver2_Edited.Instance.M5Check();
            Menga.SetActive(false);
        }
        if (player.transform.position == StPos.transform.position)
        {
            MissionControlver2_Edited.Instance.M8Check();
            Dadaocheng.SetActive(false);
        }

    }

    void OnTriggerEnter(Collider other)
    {
        // 檢查碰撞到觸發器的物件是否是玩家
        if (other.gameObject == player)
        {
            Debug.Log("Player entered the trigger, changing scene");

            if (MissionControlver2_Edited.Instance.move1)
            {
                Invoke("OpenDadaocheng", 0.05f);
                Dadaocheng.SetActive(true);
                Debug.Log("走阿哪次不走");
                MoveToNextScene();
            }
            else if (MissionControlver2_Edited.Instance.move2)
            {
                Store.SetActive(true);
                Debug.Log("走走走走走");
                MoveToNextScene();
            }
            else
            {
                Debug.Log("供威阿");
                StartCoroutine(OpenCanvas(3f));
            }

        }
    }
    private void MoveToNextScene()
    {
        player.transform.position = ToPosition.transform.position;
        Vector3 currentRotation = player.transform.eulerAngles;
        currentRotation.y += 180f; // 將Y軸旋轉增加180度
        player.transform.eulerAngles = currentRotation;
    }

    private IEnumerator OpenCanvas(float seconds)
    {
        Portal_Canvas.gameObject.SetActive(true); // Show the Canvas
        yield return new WaitForSeconds(seconds); // Wait for the specified time
        Portal_Canvas.gameObject.SetActive(false); // Hide the Canvas
    }

    void OpenDadaocheng()
    {
        Dadaocheng.SetActive(true);
        Debug.Log("開城");
    }

}