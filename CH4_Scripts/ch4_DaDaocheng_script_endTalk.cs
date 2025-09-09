using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ch4_DaDaocheng_script_endTalk : MonoBehaviour
{
    public static ch4_DaDaocheng_script_endTalk Instance;

    public GameObject NPCTalk; //  關閉對話 
    public GameObject fisherTalk;
    public GameObject fisherCollider;
    public GameObject foreignerCollider;
    public GameObject waiterCollider;

    void Start()
    {
        fisherCollider.SetActive(false);
        foreignerCollider.SetActive(false);
        waiterCollider.SetActive(false);
    }

    void Update()
    {
        if (MissionControlver2_Edited.Instance.CheckTimes == 3)
        {
            fisherTalk.SetActive(true);
        }
    }

    

    public void close_Talk()
    {
        NPCTalk.SetActive(false);
    }

    public void open_Talk()
    {
        NPCTalk.SetActive(true);
    }

    public GameObject Store_Bag; //  對話完才會出現
    //public GameObject Store_waiter_talk; // 因為會擋到package的raycast所以才要對話完後關掉
    public void giveBag()
    {
        Store_Bag.SetActive(true);
        //Store_waiter_talk.SetActive(false);
    }
}