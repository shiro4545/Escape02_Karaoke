using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Denmoku_Judge : MonoBehaviour
{
    public static Denmoku_Judge Instance { get; private set; }

    //現在の画面No
    public int CurrentScreenNo;
    //メイン画面
    public GameObject MainScreen;

    //ロック画面(101)
    public string Answer101 = "";
    public GameObject[] Sprite101;


    // Start is called before the first frame update
    void Start()
    {
        Instance = this;

        ChangeScreen(101);
    }


    //<summary>
    //デンモク画面の変更
    //</summary>
    //<param>画面No</param>
    public void ChangeScreen(int ScreenNo)
    {

        MainScreen.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Images/10_Denmoku/" + ScreenNo);
        CurrentScreenNo = ScreenNo;

    }
}
