using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleScript1 : MonoBehaviour
{
    //変数宣言 (基本型)
    public int Number1 = 12;
    public float Number2 = 3.14f;
    public string Moji = "脱出";
    public bool isClear = true; //false or true


    // Start is called before the first frame update
    void Start()
    {
        //ログ出力
        Debug.Log("カラオケ");
        Debug.Log(Moji);



        //①if文
        if (Number1 == 12)
        {
            Debug.Log("①trueです");
        }
        else
        {
            Debug.Log("①falseです");
        }

        //②if文
        if (Number1 != 10 && Number2 > 3)
        {
            Debug.Log("②trueです");
        }
        else
        {
            Debug.Log("②falseです");
        }



        //for文
        for (int i = 1; i < 4 ; i++)
        {
            Debug.Log(i +"ループ目");
        }


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
