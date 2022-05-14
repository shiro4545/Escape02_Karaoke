using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TapDenmoku : TapCollider
{
    //デンモク画面タップ
    protected override void OnTap()
    {
        base.OnTap();

        this.gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Images/10_Denmoku/" + "d01");

    }
}
