using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideItem_Tap : TapCollider
{
    //アイテム名
    public string Item_Name;
    //アイテム取得有無
    public bool isGetItem = false;
    //引き出しクラス
    public DeskSlide_Tap SlideClass;


    //タップ時
    protected override void OnTap()
    {
        base.OnTap();

        //アイテム非表示
        this.gameObject.SetActive(false);
        //引き出しコライダー表示
        SlideClass.gameObject.SetActive(true);

        if(Item_Name == "Key1")
            SaveLoadSystem.Instance.gameData.isGetKey1 = true;
        else
            SaveLoadSystem.Instance.gameData.isGetStraw = true;

        ItemManager.Instance.getItem(Item_Name);
    }
}
