using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PutHanger_Tap : TapCollider
{
    //設置後のハンガー
    public GameObject Hanger;
    //回転用のタップコライダー
    public GameObject TapCollider;

    //ボタンタップ時
    protected override void OnTap()
    {
        base.OnTap();

        if(ItemManager.Instance.selectItem == "Hanger")
        {
            AudioManager.Instance.SoundSE("Click");

            //ハンガー表示
            Hanger.SetActive(true);
            //コライダー切替
            this.gameObject.SetActive(false);
            //TapCollider.SetActive(true);

            SaveLoadSystem.Instance.gameData.isSetHanger = true;
            ItemManager.Instance.useItem();
        }
        
    }
}
