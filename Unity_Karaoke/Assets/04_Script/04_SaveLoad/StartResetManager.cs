using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartResetManager : MonoBehaviour
{
    private GameData gameData;


    //アイテムオブジェクト**********************
    //public GameObject itemPaper1;


    //ゲーム内オブジェクト**********************
    public Machine_Judge MachineClass;
    public Phone_Judge PhoneClass;
    public Phone_Tap PhoneBtnTop;
    public Phone_Tap PhoneBtnCenter;
    public Phone_Tap PhoneBtnBottom;

    //カラオケ機が入った棚周り
    public GameObject CloseDoor;
    public GameObject CloseDoorParts1;
    public GameObject CloseDoorParts2;
    public GameObject CloseDoorParts3;
    public GameObject OpenDoor;
    public GameObject KeyColiider; //初期true
    public GameObject MachineColiider; //初期false
    public GameObject ManualColiider; //初期false

    //<summary>
    //タイトル画面の「はじめから」の時
    //<summary>
    public void GameStart()
    {
        SaveLoadSystem.Instance.GameStart();
    }

    //<summary>
    //タイトル画面の「続きから」の時
    //<summary>
    public void GameContinue()
    {
        SaveLoadSystem.Instance.Load();
        gameData = SaveLoadSystem.Instance.gameData;


        //カラオケ機棚の開閉状態
        if(gameData.isOpenShelf)
        {
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
        }

        //デンモク状態
        Denmoku_Judge.Instance.DenmokuStatus = gameData.DenmokuStatus;
        switch (gameData.DenmokuStatus)
        {
            case 0: //電源Off
                Denmoku_Judge.Instance.ChangeScreen(100);
                break;
            case 1: //ロック状態
                Denmoku_Judge.Instance.ChangeScreen(101);
                break;
            case 2: //ロックなし
                Denmoku_Judge.Instance.ChangeScreen(102);
                break;
            default:
                break;
        }

        //星の力の予約有無
        if (gameData.isSendStarPower)
        {
            Denmoku_Judge.Instance.isSendStarPower = true;
            MachineClass.LampTop.GetComponent<Renderer>().material.color = Color.red;
        }

        //1歩1歩の予約有無
        if (gameData.isSendStepStep)
        {
            Denmoku_Judge.Instance.isSendStepStep = true;
            MachineClass.LampCenter.GetComponent<Renderer>().material.color = Color.red;
        }


        //九州Loversの予約有無
        if (gameData.isSendLovers)
        {
            Denmoku_Judge.Instance.isSendLovers = true;
            MachineClass.LampTop.GetComponent<Renderer>().material.color = Color.red;
        }


        //カラオケ機
        if (gameData.isClearMachine)
        {
            MachineClass.isClear = true;
            TV_Manager.Instance.ChangeTVScreen("a03");

            //ボタンを正解に
            MachineClass.ButtonTop.Objects[0].SetActive(false);
            MachineClass.ButtonCenter.Objects[0].SetActive(false);
            MachineClass.ButtonBottom.Objects[0].SetActive(false);

            MachineClass.ButtonTop.Objects[4].SetActive(true);
            MachineClass.ButtonCenter.Objects[1].SetActive(true);
            MachineClass.ButtonBottom.Objects[4].SetActive(true);
        }

        //1000円注文のクリア判定
        if (gameData.isClearOrder)
        {
            Denmoku_Judge.Instance.Phone.transform.Translate(new Vector3(0.4f, 0, 0));
            Denmoku_Judge.Instance.PhoneBtnCollider.SetActive(true);
        }

        //電話裏の謎クリア判定
        if(gameData.isClearPhone)
        {
            PhoneClass.isClear = true;
            PhoneBtnTop.Objects[0].SetActive(false);
            PhoneBtnCenter.Objects[0].SetActive(false);
            PhoneBtnBottom.Objects[0].SetActive(false);
            PhoneBtnTop.Objects[4].SetActive(true);
            PhoneBtnCenter.Objects[5].SetActive(true);
            PhoneBtnBottom.Objects[3].SetActive(true);
        }

        //デンモクのスライド判定
        if(gameData.isClearDenmokuSlide)
        {
            Denmoku_Judge.Instance.isSlide = true;
            //デンモクスライド
            Denmoku_Judge.Instance.gameObject.transform.Translate(new Vector3(1.2f, 0, 0));
            //DenmokuColliderスライド
            Denmoku_Judge.Instance.DenmokuCollider.transform.Translate(new Vector3(0, 0, -1.2f));
            //DenmokuBackColliderスライド
            Denmoku_Judge.Instance.DenmokuBackCollider.transform.Translate(new Vector3(0, 0, -1.2f));
            //DriverCollider表示
            Denmoku_Judge.Instance.DriverCollider.SetActive(true);
            //DriverDenmoku表示
            Denmoku_Judge.Instance.DriverDenmokuCollider.SetActive(true);
        }



        //保有アイテム
        if (gameData.getItems == "")
            return;
        string[] arr = gameData.getItems.Split(';');
        foreach (var item in arr)
        {
            if (item != "")
                ItemManager.Instance.loadItem(item);
        }
    }


    //<summary>
    //ゲーム進捗の算出
    //<summary>
    public int checkProgress()
    {
        int progress = 0;

        //if (!WashPanelController.Instance.firstIsClear)
        //    //ウォッシュパネル1回目のヒント
        //    progress = 0;
        //else if (!Judge_nazo4.isClear)
        //    //手洗い下の星の謎
        //    progress = 1;
        //else if (!Judge_paper.isClear)
        //    //背面棚右のペーパー置き謎
        //    progress = 2;
        //else if (!BlueBox.isClear)
        //    //背面棚左の「ペンチ」謎
        //    progress = 3;
        //else if (!Gabyo.isPullGabyo)
        //    //画鋲をとる
        //    progress = 4;
        //else if (!WashPanelController.Instance.SecondIsClear)
        //    //ウォシュパネル2回目の謎(天気記号)
        //    progress = 5;
        //else if (!DeButton.isClear)
        //    //「でんち」の謎
        //    progress = 6;
        //else if (!Hole.isClear)
        //    //懐中電灯で穴を照らす
        //    progress = 7;
        //else if (!Judge_nazo3.Instance.isClear)
        //    //手洗い上の水を出すための謎
        //    progress = 8;
        //else if (!TunkCover.isGetClearPanel)
        //    //タンクに水を入れてクリアパネル2を取得
        //    progress = 9;
        //else if (!DoorCP.isClear)
        //    //クリアパネル3枚の置き方
        //    progress = 10;
        //else
        //    //トイレタンクの水の流し方
        //    progress = 11;

        return progress;
    }

}
