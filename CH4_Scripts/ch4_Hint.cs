using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem;

public class ch4_Hint : MonoBehaviour
{
    private Camera mainCamera;
    private float distance; // 玩家與NPC的距離
    private Animator hintAnimator;
    private int pressCount = 0;
    public InputActionReference leftX;
    public InputActionReference leftY;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        // 設置Canvas的主相機
        GetComponent<Canvas>().worldCamera = mainCamera;
        hintAnimator = GetComponent<Animator>();
        GetComponent<CanvasGroup>().enabled = true;
    }

    void Update()
    {
        distance = Vector3.Distance(mainCamera.transform.position, transform.position);

        if (distance < 4)
        {
            if (pressCount == 0)
            {
                hintAnimator.SetBool("Active", true);
                transform.LookAt(mainCamera.transform);
                transform.Rotate(0, 180, 0);
            }

            if ((Input.GetKeyDown(KeyCode.X) || leftY.action.triggered))
            {
                hintAnimator.SetBool("Active", false);
                pressCount += 1;
            }
            if ((Input.GetKeyDown(KeyCode.Z) || leftX.action.triggered))
            {
                hintAnimator.SetBool("Active", false);
                pressCount -= 1;
                if(pressCount < 0)
                {
                    pressCount = 0;
                }
            }
        }
        else
        {
            hintAnimator.SetBool("Active", false);
            pressCount = 0;
        }
    }
}
