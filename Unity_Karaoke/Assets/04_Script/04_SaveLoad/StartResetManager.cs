using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartResetManager : MonoBehaviour
{
    private GameData gameData;


    //アイテムオブジェクト**********************
    public GameObject KeyBox;
    public GameObject Driver;
    public GameObject Piece1;
    public GameObject Piece2;
    public GameObject Piece3;
    public GameObject Piece4;
    public GameObject Piece5;
    public GameObject Key3;


    //ゲーム内オブジェクト**********************
    public Machine_Judge MachineClass;
    public Phone_Judge PhoneClass;
    public Phone_Tap PhoneBtnTop;
    public Phone_Tap PhoneBtnCenter;
    public Phone_Tap PhoneBtnBottom;
    public Door_Judge DoorClass;
    public DenmokuPower_Tap DenmokuPower;
    public Pentagon_Judge PentagonClass;
    public Pentagon_Tap[] PentaPieceArray;
    public FinalButton_Judge FinalButtonClass;
    public FinalButton_Tap FInalBtn1;
    public FinalButton_Tap FInalBtn2;
    public FinalButton_Tap FInalBtn3;

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
                DenmokuPower.PowerSwitch.transform.Translate(new Vector3(-0.12f, 0, 0));
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
            MachineClass.LampBottom.GetComponent<Renderer>().material.color = Color.red;
        }

        //こしょう少々の予約有無
        if(gameData.isSendKosho)
            Denmoku_Judge.Instance.isSendKosho = true;


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
            PhoneClass.UpSlide.SetActive(false);
            PhoneBtnTop.Objects[0].SetActive(false);
            PhoneBtnCenter.Objects[0].SetActive(false);
            PhoneBtnBottom.Objects[0].SetActive(false);
            PhoneBtnTop.Objects[4].SetActive(true);
            PhoneBtnCenter.Objects[5].SetActive(true);
            PhoneBtnBottom.Objects[3].SetActive(true);
        }


        //アイテム取得有無　ドライバー
        if (gameData.isGetKeyBox)
            KeyBox.SetActive(false);

        //デンモクのスライド判定
        if (gameData.isClearDenmokuSlide)
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

        //アイテム取得有無　ドライバー
        if (gameData.isGetDriver)
            Driver.SetActive(false);

        //扉の開閉
        DoorClass.DoorStatus = gameData.DoorStatus;
        DoorClass.isClear = gameData.isClearDoor;
        DoorClass.isFullOpen = gameData.isFullOpen;

        if (DoorClass.isFullOpen)
        {
            //1度でも扉を開けてたらコライダー修正
            DoorClass.DoorColliderClass.gameObject.SetActive(false);
            DoorClass.DoorColliderClass.MovePositionName = "Hall";
            DoorClass.CloseDoorClass.EnableCameraPositionName = "RoomDoor";
        }

        if (DoorClass.DoorStatus == 1)
        {
            //全開の場合
            DoorClass.CloseDoor.SetActive(false);
            DoorClass.OpenDoor.SetActive(true);
        }
        else if (DoorClass.DoorStatus == 2)
        {
            //ちょい開けの場合
            DoorClass.CloseDoor.SetActive(false);
            DoorClass.LittleOpenDoor.SetActive(true);
        }

        //五角形周りのアイテム
        if (gameData.isGetPiece1)
            Piece1.SetActive(false);
        if (gameData.isGetPiece2)
            Piece2.SetActive(false);
        if (gameData.isGetPiece3)
            Piece3.SetActive(false);
        if (gameData.isGetPiece4)
            Piece4.SetActive(false);
        if (gameData.isGetPiece5)
            Piece5.SetActive(false);
        if (gameData.isGetKey3)
            Key3.SetActive(false);

        //五角形の状態
        PentagonClass.Input = gameData.PentagonStatus;
        int _input;
        for(int i = 0; i < PentaPieceArray.Length; i++)
        {
            _input = int.Parse(PentagonClass.Input.Substring(i, 1));
            if (_input != 0)
                PentaPieceArray[i].PieceArray[_input - 1].SetActive(true);
        }

        //五角形のクリア有無
        PentagonClass.isClear = gameData.isClearPentagon;
        if (PentagonClass.isClear)
        {
            PentagonClass.Slide.transform.Translate(new Vector3(0, -0.1f, 0));
            PentagonClass.PentagonCollider.SetActive(false);
        }

        //最終扉のボタンクリア有無
        FinalButtonClass.isClear = gameData.isClearFinalBtn;
        if(FinalButtonClass.isClear)
        {
            //ボタンを正解状態に
            FInalBtn1.Objects[0].SetActive(false);
            FInalBtn2.Objects[0].SetActive(false);
            FInalBtn3.Objects[0].SetActive(false);
            FInalBtn1.Objects[1].SetActive(true);
            FInalBtn2.Objects[2].SetActive(true);
            FInalBtn3.Objects[3].SetActive(true);

            //鍵穴の蓋を開ける
            FinalButtonClass.KeyCover.transform.Translate(new Vector3(0.13f, 0, 0));
        }





        //テレビ画面
        if (DoorClass.isClear && Denmoku_Judge.Instance.isSendKosho)
            //こしょう少々の採点画面
            TV_Manager.Instance.ChangeTVScreen("h05");
        else if (MachineClass.isClear)
            //ポテト値引き画面
            TV_Manager.Instance.ChangeTVScreen("a03");
        //else if(Rimokon.isClear)
            //四角4つ画面
        //    ChangeTVScreen("a02");
        else
            TV_Manager.Instance.ChangeTVScreen("a01");

        //全クリアの場合、鍵3がない状態で続きからになるため鍵3を付与
        if (gameData.isClearAll)
            gameData.getItems = "Key3;";

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
