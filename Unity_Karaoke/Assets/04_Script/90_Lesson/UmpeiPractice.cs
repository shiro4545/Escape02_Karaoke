using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UmpeiPractice : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(Umpei("umpei" , "koga"));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public string Umpei(string moji , string moji2)
    {
        string testmoji = moji2 + " " + moji;
        return testmoji;
    }
    
}
