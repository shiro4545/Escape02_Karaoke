using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleScript5 : TapCollider
{
   
    //OnTapを引き継ぐ
    protected override void OnTap()
    {
        base.OnTap();

        //オブジェクトを消す
        this.gameObject.SetActive(false);
    }
}
