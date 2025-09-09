using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ch4_DaDaocheng_script_house_2 : MonoBehaviour
{
    [SerializeField] Transform Main_Ca;

    public Collision col;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    /*
    void OnCollisionStay(Collision col)
    {
        Debug.Log("change the scene~~~~");
        SceneManager.LoadScene($"ch4_Scene_Store");
    }
     */

    public void clearSilk()
    {
        for (int i = Main_Ca.transform.childCount - 1; i >= 0; i--)
        {
            Destroy(Main_Ca.transform.GetChild(i).gameObject);
        }
    }
}
