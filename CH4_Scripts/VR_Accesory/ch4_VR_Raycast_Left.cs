using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ch4_VR_Raycast_Left : MonoBehaviour
{
    public static ch4_VR_Raycast_Left Instance;
    private void Awake()
    {
        // 檢查是否已經有一個實例
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // 如果已經有一個實例，銷毀這個重複的實例
            return;
        }

        Instance = this; // 設置 Instance 為當前實例
        DontDestroyOnLoad(gameObject); // 如果希望跨場景保留對象，使用這行
    }

    public XRRayInteractor rayInteractor; // 引用 XR Ray Interactor
    public LayerMask interactableLayer; // 設定可交互的 Layer
    public XRBaseInteractable currentInteractable; // 當前互動的物件

    public bool VRRaycastTag_Silk = false; // 代表是否對到tag為silk的物件
    public bool VRRaycastTag_Spoon = false; // tag 為 spoon 的物件
    public bool VRRaycastTag_TeaPack = false; // tag 為 TeaPack的物件

    void Update()
    {
        // 檢查射線是否檢測到物件
        if (rayInteractor.TryGetCurrent3DRaycastHit(out RaycastHit hit))
        {
            // 獲取檢測到的物件
            XRBaseInteractable interactable = hit.collider.GetComponent<XRBaseInteractable>();

            if (interactable != null)
            {
                if (interactable != currentInteractable)
                {
                    // 開始新的互動
                    if (currentInteractable != null)
                    {
                        // 結束上一次的互動
                        EndInteraction();
                    }

                    currentInteractable = interactable;

                    // 要分別哪一個的tag符合需求，像是silk spoon package
                    if(currentInteractable.gameObject.tag == "Silk")
                    {
                        VRRaycastTag_Silk = true;
                    }
                    else if(currentInteractable.gameObject.tag == "Spoon")
                    {
                        VRRaycastTag_Spoon = true;
                    }
                    else if(currentInteractable.gameObject.tag == "TeaPack")
                    {
                        VRRaycastTag_TeaPack = true;
                    }
                    StartInteraction();
                }
            }
        }
        else if (currentInteractable != null)
        {
            // 射線未檢測到物件，結束互動
            EndInteraction();
        }
    }

    void StartInteraction()
    {
        // 處理開始互動的邏輯，例如更改顏色、播放動畫等
        Debug.Log("Start Interaction with: " + currentInteractable.name);

    }

    void EndInteraction()
    {
        // 處理結束互動的邏輯
        Debug.Log("結束 with: " + currentInteractable.name);
        currentInteractable = null;
        VRRaycastTag_Silk = false;
        VRRaycastTag_Spoon = false;
        VRRaycastTag_TeaPack = false;
    }
}
