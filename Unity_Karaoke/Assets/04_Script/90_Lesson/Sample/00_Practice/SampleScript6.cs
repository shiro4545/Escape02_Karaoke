using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TapColliderを継承している
//(TapCollider内の関数を使用できる)
public class SampleScript6 : TapCollider
{
   
    //OnTapを引き継ぐ
    protected override void OnTap()
    {
        base.OnTap();
        //↑ここまでを丸コピーでタップ処理が作れる

        //オブジェクトを消す
        this.gameObject.SetActive(false);
    }
}
