using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleScript2 : MonoBehaviour
{
    //変数
    public string test1 = "パブリック";
    private string test2 = "プライベート";

    private int testNo = 100;

    private int answerNo = 0;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    //関数
    public void Func()
    {
        Debug.Log(test1);
        Debug.Log(test2);
    }

    //足し算関数1
    public void Tashizan1()
    {
        testNo = 100 + 10;
        Debug.Log(testNo);
    }


    //足し算関数2
    public void Tashizan2(int number)
    {
        testNo = 100 + number;
        Debug.Log(testNo);
    }

    //足し算関数3
    public int Tashizan3(int number)
    {
        testNo = 100 + number;
        return testNo;
    }



}
