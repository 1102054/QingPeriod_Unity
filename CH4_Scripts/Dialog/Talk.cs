using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Animations;
using cherrydev;
using TMPro;
using UnityEngine.InputSystem;

public class Talk : MonoBehaviour
{
    private DialogBehaviour dialogBehaviour; // 對話行為組件
    [SerializeField] private DialogNodeGraph dialogGraph; // 對話節點圖

    private Camera player; // 玩家相機
    private float distance; // 玩家與NPC的距離
    private GameObject talkCanvas; // 對話框Canvas
    private Animator talkAnimator; // 對話框動畫組件
    public InputActionReference leftX;
    public InputActionReference leftY;

    void Start()
    {
        // 初始化對話框、提示和玩家相機
        talkCanvas = transform.Find("Canvas").gameObject;
        talkAnimator = talkCanvas.GetComponent<Animator>();
        player = Camera.main;
        dialogBehaviour = GetComponent<DialogBehaviour>();
        talkCanvas.GetComponent<CanvasGroup>().enabled = true;

        // 分配玩家相機給對話框和約束組件
        AssignCamera(player.gameObject);
    }

    void Update()
    {
        // 更新對話框的朝向和處理距離相關邏輯
        HandleDistance();
    }

    private void HandleDistance()
    {
        // 計算玩家與NPC的距離，並更新提示位置
        distance = Vector3.Distance(player.transform.position, transform.position);

        if (distance < 25 && IsInView())
        {
            if ((Input.GetKeyDown(KeyCode.X) || leftY.action.triggered) && !talkAnimator.GetBool("Active"))
            {
                // 按下A鍵，顯示對話框並開始對話
                talkAnimator.SetBool("Active", true);
                dialogBehaviour.StartDialog(dialogGraph);
            }
            else if (Input.GetKeyDown(KeyCode.Z) || leftX.action.triggered)
            {
                // 按B鍵，返回上一個對話節點或隱藏對話框
                if (dialogBehaviour.GetHistoryCount() > 1)
                {
                    dialogBehaviour.GoToPreviousNode();
                }
                else
                {
                    talkAnimator.SetBool("Active", false);
                }
            }
        }
        else
        {
            talkAnimator.SetBool("Active", false);
        }
    }

    private bool IsInView()
    {
        // 檢查NPC是否在玩家的視野範圍內
        Vector3 directionToNPC = (transform.position - player.transform.position).normalized;
        float dotProduct = Vector3.Dot(player.transform.forward, directionToNPC);
        return dotProduct > 0.5f; // 0.5f 可調整，值越高表示視野範圍越窄
    }


    private void AssignCamera(GameObject source)
    {
        // 將玩家相機分配給對話框的Canvas組件
        talkCanvas.GetComponent<Canvas>().worldCamera = source.GetComponent<Camera>();
    }
}