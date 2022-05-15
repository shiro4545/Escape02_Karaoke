using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleScript3 : MonoBehaviour
{
    //他のクラス変数
    public SampleScript2 SS2;


    // Start is called before the first frame update
    void Start()
    {
        //Func()関数を呼び出す
        SS2.Func();

        //他のクラスの変数に代入
        SS2.test1 = "アプリ";
        SS2.Func();

        //publicとprivateの違い
        //SS2.test2 = "アプリ";


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
