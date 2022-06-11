using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopPutStraw_Tap : TapCollider
{
    //ストロー
    public GameObject Straw;
    //ストロー回す用のコライダー
    //public GameObject CopRotateCollider;

    //タップ時
    protected override void OnTap()
    {
        base.OnTap();

        if (ItemManager.Instance.selectItem != "Straw")
            return;

        AudioManager.Instance.SoundSE("IceInGlass");

        //ストローを表示
        Straw.SetActive(true);
        //ストローさす用のコライダー非表示
        this.gameObject.SetActive(false);
        //ストロー回す用のコラスイダー表示
        //CopRotateCollider.SetActive(true);

        SaveLoadSystem.Instance.gameData.isSetStraw = true;
        ItemManager.Instance.useItem();
    }
}
