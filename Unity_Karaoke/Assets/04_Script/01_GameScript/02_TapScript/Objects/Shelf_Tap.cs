using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shelf_Tap : TapCollider
{
    //閉扉オブジェクト
    public GameObject CloseDoor;
    public GameObject CloseDoorParts1;
    public GameObject CloseDoorParts2;
    public GameObject CloseDoorParts3;

    //開扉オブジェクト
    public GameObject OpenDoor;

    //コライダー
    public GameObject KeyColiider; //初期true
    public GameObject MachineColiider; //初期false
    public GameObject ManualColiider; //初期false

    //ボタンタップ時
    protected override void OnTap()
    {
        base.OnTap();
        //if(true)
        //鍵を選択しているかの判定
        if(ItemManager.Instance.selectItem == "Key1")
        {
            //扉を開ける
            BlockPanel.Instance.ShowBlock();
            AudioManager.Instance.SoundSE("Clear");
            //鍵を消費
            ItemManager.Instance.useItem();
            Invoke(nameof(act1), 1f);
        }
    }

    private void act1()
    {
        //カメラ移動
        CameraManager.Instance.ChangeCameraPosition("Shelf");
        Invoke(nameof(act2), 1f);
    }
    private void act2()
    {
        //閉扉を非表示
        CloseDoor.SetActive(false);
        CloseDoorParts1.SetActive(false);
        CloseDoorParts2.SetActive(false);
        CloseDoorParts3.SetActive(false);
        //開扉を表示
        OpenDoor.SetActive(true);

        //鍵コライダー非表示
        KeyColiider.SetActive(false);
        //棚の中のコライダー表示
        MachineColiider.SetActive(true);
        ManualColiider.SetActive(true);

        AudioManager.Instance.SoundSE("OpenShelf");
        BlockPanel.Instance.HideBlock();

        SaveLoadSystem.Instance.gameData.isOpenShelf = true;
        SaveLoadSystem.Instance.Save();

    }
}
