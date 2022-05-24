using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleScript4 : MonoBehaviour
{
    //配列変数
    public string[] Colors;

    // Start is called before the first frame update
    void Start()
    {
        //配列に代入
        Colors = new string[] { "Red", "Blue", "Green" };

        //配列要素の取得
        Debug.Log(Colors[0]); //→ Red
        Debug.Log(Colors[1]); //→ Blue
        Debug.Log(Colors[2]); //→ Green


        //配列の要素数
        Debug.Log(Colors.Length); //→ 3


        //配列のループ処理
        for(int i = 0; i < Colors.Length; i++)
        {
            Debug.Log(Colors[i]);
        }


        //要素に代入
        Debug.Log(Colors[1]); //→ Blue
        Colors[1] = "White";
        Debug.Log(Colors[1]); //→ White


        //【問題】
        //int[]配列を作成し、
        //2,4,6,8,を代入する。
        //配列要素をループ処理し、
        //4の時だけログを出力する
        

    }

   
}
