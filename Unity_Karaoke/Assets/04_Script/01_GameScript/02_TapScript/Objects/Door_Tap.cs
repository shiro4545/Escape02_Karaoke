using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door_Tap : TapCollider
{
    //扉クラス
    public Door_Judge DoorClass;
    //扉名
    public string DoorName;



    //ボタンタップ時
    protected override void OnTap()
    {
        base.OnTap();

        if (DoorName == "CloseDoor")
        {
            //全閉の場合
            if (DoorClass.isClear)
            {
                //扉を開ける
                AudioManager.Instance.SoundSE("OpenShelf");
                DoorClass.CloseDoor.SetActive(false);
                DoorClass.OpenDoor.SetActive(true);
                //コライダー修正
                DoorClass.DoorColliderClass.gameObject.SetActive(true);
                //ステータス変更
                DoorClass.DoorStatus = 1;
                SaveLoadSystem.Instance.gameData.DoorStatus = 1;

            }
            else if (ItemManager.Instance.selectItem == "Key2")
            {
                //鍵で扉をちょい開けに
                AudioManager.Instance.SoundSE("Clear");
                //演出
                BlockPanel.Instance.ShowBlock();
                //鍵を使用
                ItemManager.Instance.useItem();
                Invoke(nameof(AfterClear1), 1.5f);
                //ステータス変更
                DoorClass.isClear = true;
                DoorClass.DoorStatus = 2;
                SaveLoadSystem.Instance.gameData.isClearDoor = true;
                SaveLoadSystem.Instance.gameData.DoorStatus = 2;
            }
            else
                //開かない
                AudioManager.Instance.SoundSE("NotOpen");
        }
        //全開の場合
        else if(DoorName == "OpenDoor")
        {
            //扉を閉める
            AudioManager.Instance.SoundSE("CloseDoor");
            DoorClass.OpenDoor.SetActive(false);
            DoorClass.CloseDoor.SetActive(true);
            //コライダー修正
            DoorClass.DoorColliderClass.gameObject.SetActive(false);
            //ステータス変更
            DoorClass.DoorStatus = 0;
            SaveLoadSystem.Instance.gameData.DoorStatus = 0;
        }
        //ちょい開けの場合
        else
        {
            //扉を開ける
            AudioManager.Instance.SoundSE("OpenShelf");
            DoorClass.OpenDoor.SetActive(true);
            DoorClass.LittleOpenDoor.SetActive(false);

            //コライダー修正
            DoorClass.DoorColliderClass.MovePositionName = "Hall";
            DoorClass.CloseDoorClass.EnableCameraPositionName = "RoomDoor";

            //ステータス変更
            DoorClass.isFullOpen = true;
            DoorClass.DoorStatus = 1;
            SaveLoadSystem.Instance.gameData.isFullOpen = true;
            SaveLoadSystem.Instance.gameData.DoorStatus = 1;

        }
        //最後にセーブ
        SaveLoadSystem.Instance.Save();
    }

    //初めて扉を開けた時の演出
    private void AfterClear1()
    {
        //扉ちょい開け
        AudioManager.Instance.SoundSE("OpenShelf");
        DoorClass.CloseDoor.SetActive(false);
        DoorClass.LittleOpenDoor.SetActive(true);

        BlockPanel.Instance.HideBlock();


    }
}
