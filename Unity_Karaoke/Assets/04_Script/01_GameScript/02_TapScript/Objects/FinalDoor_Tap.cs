using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalDoor_Tap : TapCollider
{
    //開きドア
    public GameObject OpenDoor;
    //閉まりドア
    public GameObject CloseDoor;

    //最終ボタンクラス
    public FinalButton_Judge FinalBtnClass;

    //エンディングクラス
    public ClearManager Escape;

    //ボタンタップ時
    protected override void OnTap()
    {
        base.OnTap();
        if(true)
        //if(ItemManager.Instance.selectItem == "Key3" && FinalBtnClass.isClear)
        {
            BlockPanel.Instance.ShowBlock();
            AudioManager.Instance.SoundSE("Clear");

            SaveLoadSystem.Instance.gameData.isClearAll = true;

            //鍵を使用
            ItemManager.Instance.useItem();

            Invoke(nameof(act1), 1f);
        }
    }

    //演出
    private void act1()
    {
        //カメラ移動
        CameraManager.Instance.ChangeCameraPosition("DoorFinal");
        Invoke(nameof(act2), 1.5f);
    }
    private void act2()
    {
        //扉開く
        AudioManager.Instance.SoundSE("OpenShelf");
        OpenDoor.SetActive(true);
        CloseDoor.SetActive(false);
        Invoke(nameof(act3), 1.2f);
    }
    //エンディング
    private void act3()
    {
        Escape.Escape();
    }
}
