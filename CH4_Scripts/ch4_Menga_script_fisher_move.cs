using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class ch4_Menga_script_fisher_move : MonoBehaviour
{
    public float speed = 200f; // 玩家移動速度
    public Transform targetPosition;
    public float stoppingDistance = 0.2f; // 到達目標位置的停止距離
    public Animator fisher_anim; // ***** fisher的動畫 *****

    public static bool start_Move = false;
    public static bool turnAround = true;
    public bool det = true;

    public bool FisherTalk_fin = false;

    // Start is called before the first frame update
    void Start()
    {
        fisher_anim.enabled = false;
    }

    // Update is called once per frame
    public void Update()
    {        
       
    }

    public void FisherMove()
    {
        start_Move = true;
    }

    private void FixedUpdate()
    {
        if (start_Move == true && det == true)
        {
            fisher_anim.SetBool("moveToStay", false);
            StartMove();
        }
    }

    void MoveGameObject(Vector3 direction, float speed)
    {
        // 計算移動的距離，這是方向向量乘以速度和時間步長
        Vector3 movement = direction * speed * Time.fixedDeltaTime;

        // 更新遊戲物件的位置
        transform.position += movement;
    }

    // ***** 在動畫停止後重設角色狀態 *****
    public void StartMove()
    {
        fisher_anim.enabled = true;
        Vector3 direction = (targetPosition.position - transform.position).normalized;
        //rb.MovePosition(transform.position + direction * speed * Time.fixedDeltaTime);
        MoveGameObject(direction,speed);

        if (Vector3.Distance(transform.position, targetPosition.position) <= stoppingDistance)
        {
            // ***** 移動speed可以關掉 *****
            //fisher_anim.speed = 0;
            //fisher_anim.enabled = true;
            fisher_anim.Play("mixamo_com", 0, 0.7f);
            fisher_anim.SetBool("moveToStay", true);
            print("arrive the destination");
            transform.Rotate(0, 180, 0);
            //fisher_anim.enabled = false;
            det = false;
            //print(start_Move);
        }
        if (turnAround == true)
        {
            transform.Rotate(0, 180, 0);
            turnAround = false;
            //rb.isKinematic = false;

        }

    }

    public void talk_fin()
    {
        FisherTalk_fin = true;
    }
}
