using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ch4_Dadaocheng_script_foreigner : MonoBehaviour
{
    private CharacterController characterController;
    private Animator anim;
    public float speed = 4f;
    private bool isTalking = false;
    public Transform targetPlayer;
    public float followDistance = 10f; // 跟随的距离
    private Vector3 originalDirection;
    private bool isFollowing = false;
    private float distanceMoved = 0f;
    private bool stop = false;
    private bool walk = false;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
        originalDirection = transform.forward;
    }

    void Update()
    {
        if (isTalking)
        {
            Standing(); //站著
        }
        else if (isFollowing)
        {
            Following(); //跟隨
        }
        else
        {
            Walking(); //走路
        }
    }

    private void Walking()
    {
        if (distanceMoved >= 50f)
        {
            transform.Rotate(0, 180, 0);
            distanceMoved = 0f;
            originalDirection = transform.forward;
        }

        if (!stop)
        {
            characterController.Move(speed * Time.deltaTime * originalDirection);
            distanceMoved += speed * Time.deltaTime;
        }

        Vector3 direction = targetPlayer.position - transform.position;
        direction.y = 0;

        if (direction.magnitude <= followDistance)
        {
            StopMoving();
        }
        else
        {
            ResumeWalking();
        }
    }

    private void Following()
    {
        Vector3 direction = targetPlayer.position - transform.position;
        direction.y = 0;

        if (direction.magnitude > followDistance)
        {
            walk = true;
            stop = false;
            if (walk)
            {
                anim.SetBool("moveToStand", false);
                anim.SetBool("standToMove", true);
                characterController.Move(direction.normalized * speed * Time.deltaTime);
                transform.rotation = Quaternion.LookRotation(direction);
            }
        }
        else
        {
            StopMoving();
        }
    }

    private void Standing()
    {
        StopMoving();
    }

    private void StopMoving()
    {
        stop = true;
        walk = false;
        anim.SetBool("moveToStand", true);
        anim.SetBool("standToMove", false);
        characterController.Move(Vector3.zero);

        Vector3 direction = targetPlayer.position - transform.position;
        direction.y = 0;
        if (direction.magnitude > 0)
        {
            transform.rotation = Quaternion.LookRotation(direction);
        }
    }

    private void ResumeWalking()
    {
        stop = false;
        walk = true;
        anim.SetBool("moveToStand", false);
        anim.SetBool("standToMove", true);

        transform.rotation = Quaternion.LookRotation(originalDirection);
    }

    public void StartTalking()
    {
        isTalking = true;
        StopMoving();
    }

    public void EndTalking()
    {
        isTalking = false;
        isFollowing = true;
    }
}
