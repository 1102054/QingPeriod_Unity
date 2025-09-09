using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ch4_Menga_script_pedestrain1 : MonoBehaviour
{
    private CharacterController characterController;
    private Animator anim;
    public float speed = 3f;
    private bool animationStarted = true;
    public Transform targetPlayer;
    public float distanceMoved = 0f;
    private Quaternion originalRotation;
    
    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        anim = GetComponentInChildren<Animator>(); // 確保Animator是子物件
        anim.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(animationStarted)
        {
            Move();
        }
    }

    private void Move()
    {
        float v = speed * Time.deltaTime;
        // 移動NPC
        characterController.Move(v * transform.forward);
        // 更新移動距離計數器
        distanceMoved += v;
        // 如果移動距離達到了指定距離，則向後轉向
        if (distanceMoved >= 50)
        {
            // 旋轉NPC以向後移動
            transform.Rotate(0, 180, 0);
            // 重置移動距離計數器
            distanceMoved = 0;
        }
    }

    public void pedestrain_move()
    {
        anim.enabled = true;
        speed = 3f;
        animationStarted = true;
        transform.rotation = originalRotation;

        Move();
    }

    public void pedestrain_stop()
    {
        originalRotation = transform.rotation;

        Vector3 direction = targetPlayer.position - transform.position;
        direction.y = 0;
        transform.rotation = Quaternion.LookRotation(direction);
        
        anim.enabled = false;
        speed = 0f;
        animationStarted = false;
    }
}
