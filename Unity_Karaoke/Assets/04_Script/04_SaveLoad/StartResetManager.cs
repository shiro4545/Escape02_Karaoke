using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartResetManager : MonoBehaviour
{
    private GameData gameData;


    //アイテムオブジェクト**********************
    public GameObject Hanger;
    public GameObject Tambarin_Sankaku;
    public GameObject Tambarin_Shikaku;
    public GameObject KeyBox;
    public GameObject Driver;
    public GameObject Piece1;
    public GameObject Piece2;
    public GameObject Piece3;
    public GameObject Piece4;
    public GameObject Piece5;
    public GameObject Key3;


    //ゲーム内オブジェクト**********************
    public PutHanger_Tap PutHangerClass;
    public Hanger_judge HangerClass;
    public Hanger_Tap[] HangerTapArray;
    public Book_Tap BookClass;
    public Cop_Tap BlueCopClass;
    public Cop_Tap WhiteCopClass;
    public SlideItem_Tap StrawClass;
    public SlideItem_Tap Key1Class;
    public CopPutStraw_Tap PutCopClass;
    public Cop_Judge CopClass;
    public DeskSlide_Tap UpperSlideClass;
    public DeskSlide_Tap UnderSlideClass;
    public Tambarin_Tap TambarinLeft;
    public Tambarin_Tap TambarinCenter;
    public Tambarin_Tap TambarinRight;
    public Tambarin_Judge TambarinClass;
    public Shelf_Tap ShelfClass;
    public Rimocon_Judge RimoconClass;
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

        //ハンガー取得有無
        if (gameData.isGetHanger)
            Hanger.SetActive(false);


        //ハンガー設置有無
        if (gameData.isSetHanger)
        {
            PutHangerClass.gameObject.SetActive(false);
            PutHangerClass.Hanger.SetActive(true);
        }

        //ハンガー状態
        HangerClass.InputNo = gameData.HangerStatus;
        for (int i = 0; i < HangerTapArray.Length; i++)
        {
            if (HangerClass.InputNo.Substring(i, 1) == "1")
            {
                HangerTapArray[i].Objects[0].SetActive(false);
                HangerTapArray[i].Objects[1].SetActive(true);
                HangerTapArray[i].Index = 1;
            }
        }

        //ハンガークリア有無
        HangerClass.isClear = gameData.isClearHanger;


        //タンバリン三角
        if (gameData.isGetTanbarine_Sankaku)
            Tambarin_Sankaku.SetActive(false);

        //タンバリン四角
        if (gameData.isGetTanbarine_Shikaku)
            Tambarin_Shikaku.SetActive(false);


        //ストロー
        StrawClass.isGetItem = gameData.isGetStraw;
        if (gameData.isGetStraw)
            StrawClass.gameObject.SetActive(false);

        //鍵1
        Key1Class.isGetItem = gameData.isGetKey1;
        if (gameData.isGetKey1)
            Key1Class.gameObject.SetActive(false);


        //検索本
        BookClass.isChangePage = gameData.isChangePage;
        if(BookClass.isChangePage)
        {
            BookClass.Book1.SetActive(false);
            BookClass.Book2.SetActive(true);
        }


        //コップ状態
        CopClass.InputNo = gameData.CopStatus;
        int CopStatus ;

        //青コップにストローさしたか
        if (gameData.isSetStraw)
        {
            PutCopClass.gameObject.SetActive(false);
            PutCopClass.CopRotateCollider.SetActive(true);

            //回転状態
            CopStatus = int.Parse(CopClass.InputNo.Substring(0, 1));
            BlueCopClass.Objects[CopStatus].SetActive(true);
            BlueCopClass.Index = CopStatus;
        }

        //白コップ
        CopStatus = int.Parse(CopClass.InputNo.Substring(1, 1));
        if(CopStatus != 3)
        {
            WhiteCopClass.Index = CopStatus;
            WhiteCopClass.Objects[3].SetActive(false);
            WhiteCopClass.Objects[CopStatus].SetActive(true);
        }


        //コップ回しのクリア有無
        CopClass.isClear = gameData.isClearCop;
        if (CopClass.isClear)
        {
            CopClass.CloseSofa.SetActive(false);
            CopClass.OpenSofa.SetActive(true);
            CopClass.BoxSofaColiider.SetActive(true);
        }

        //上引き出し状態
        UpperSlideClass.Status = gameData.DeskUpperStatus;
        if(UpperSlideClass.Status == 1)
        {
            //全開
            UpperSlideClass.Slide.transform.Translate(new Vector3(0, -1.1f, 0));
            //鍵1を未取得なら引き出しコライダー非表示
            if (!gameData.isGetKey1)
                UpperSlideClass.gameObject.SetActive(false);
        }
        else if(UpperSlideClass.Status == 2)
        {
            //ちょい開き
            UpperSlideClass.Slide.transform.Translate(new Vector3(0, -0.15f, 0));
        }

        //下引き出し状態
        UnderSlideClass.Status = gameData.DeskUnderStatus;
        if (UnderSlideClass.Status == 1)
        {
            //全開
            UnderSlideClass.Slide.transform.Translate(new Vector3(0, -1.1f, 0));
            //ストローを未取得なら引き出しコライダー非表示
            if (!gameData.isGetStraw)
                UnderSlideClass.gameObject.SetActive(false);
        }
        else if (UnderSlideClass.Status == 2)
        {
            //ちょい開き
            UnderSlideClass.Slide.transform.Translate(new Vector3(0, -0.15f, 0));
        }


        //タンバリンクリア有無
        TambarinClass.isClear = gameData.isClearTambarin;
        TambarinClass.InputStatus = gameData.TambarinStatus;

        //タンバリン左
        int status = int.Parse(TambarinClass.InputStatus.Substring(0, 1));
        if(status != 0)
            TambarinLeft.Objects[status -1].SetActive(true);
        //タンバリン中央
        status = int.Parse(TambarinClass.InputStatus.Substring(1, 1));
        if (status != 0)
            TambarinCenter.Objects[status - 1].SetActive(true);
        //タンバリン右
        status = int.Parse(TambarinClass.InputStatus.Substring(2, 1));
        if (status != 1)
            TambarinRight.Objects[0].SetActive(false);
        if(status == 2 || status ==3)
            TambarinRight.Objects[status - 1].SetActive(true);


        //カラオケ機棚の開閉状態
        if (gameData.isOpenShelf)
        {
            ShelfClass.CloseDoor.SetActive(false);
            ShelfClass.CloseDoorParts1.SetActive(false);
            ShelfClass.CloseDoorParts2.SetActive(false);
            ShelfClass.CloseDoorParts3.SetActive(false);
            //開扉を表示
            ShelfClass.OpenDoor.SetActive(true);
            //鍵コライダー非表示
            ShelfClass.KeyColiider.SetActive(false);
            //棚の中のコライダー表示
            ShelfClass.MachineColiider.SetActive(true);
            ShelfClass.ManualColiider.SetActive(true);
            ShelfClass.ShelfLeftColiider.SetActive(true);
        }

        //エアコンのリモコン
        RimoconClass.isClear = SaveLoadSystem.Instance.gameData.isClearRimocon;


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



        //カラオケ機
        if (gameData.isClearMachine)
        {
            MachineClass.isClear = true;

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
            //電源スイッチのコライダー非表示
            Denmoku_Judge.Instance.DenmokuBackCollider.SetActive(false);
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
            DoorClass.DoorColliderClass.MovePositionName = "Hall";
            DoorClass.CloseDoorClass.EnableCameraPositionName = "RoomDoor";

            if(DoorClass.DoorStatus == 0)
                DoorClass.DoorColliderClass.gameObject.SetActive(false);
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


        //こしょう少々の予約有無
        Denmoku_Judge.Instance.isSendKosho = gameData.isSendKosho;


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
        else if (RimoconClass.isClear)
            //四角4つ画面
            TV_Manager.Instance.ChangeTVScreen("a02");
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
    public int CheckProgress()
    {
        //進捗
        int progress;
        //インスタンスを代入(ソース短縮化のため)
        gameData = SaveLoadSystem.Instance.gameData;

        //進捗算出
        if (!gameData.isSetHanger)
            //step1 ハンガーセットしたか
            progress = 1;
        else if (!gameData.isClearHanger)
            //step2 ハンガー4つの回転
            progress = 2;
        else if (!gameData.isSetStraw)
            //step3 ストロー挿したか
            progress = 3;
        else if (!gameData.isClearCop)
            //step4 コップ回転 
            progress = 4;
        else if (!gameData.isClearTambarin)
            //step5 タンバリン並べ
            progress = 5;
        else if (!gameData.isClearRimocon)
            //step7 エアコンのリモコン 
            progress = 7;
        else if (!gameData.isClearDenmokuRock)
            //step8 デンモクロック解除 
            progress = 8;
        else if (!gameData.isSendStarPower)
            //step9 星の力送信
            progress = 9;
        else if (!gameData.isSendStepStep)
            //step10 1歩1歩送信
            progress = 10;
        else if (!gameData.isSendLovers)
            //step11 九州Lovers送信
            progress = 11;
        else if (!gameData.isClearMachine)
            //step12 カラオケ機のボタン
            progress = 12;
        else if (!gameData.isClearOrder)
            //step13 1000円注文
            progress = 13;
        else if (!gameData.isClearPhone)
            //step14 電話裏のボタン
            progress = 14;
        else if (!gameData.isClearPowerOn)
            //step16 デンモクロック再起動
            progress = 16;
        else if (!gameData.isClearDenmokuSlide)
            //step17 デンモクロック解除２回目
            progress = 17;
        else if (!gameData.isGetKey2)
            //step18 ドライバーで鍵箱開ける
            progress = 18;
        else if (!gameData.isSendKosho)
            //step20 こしょう送信 
            progress = 20;
        else if (!gameData.isClearPentagon)
            //step21 五角形
            progress = 21;
        else if (!gameData.isClearFinalBtn)
            //step22 最後の扉のボタン
            progress = 22;
        else
            //あとは脱出するだけ
            progress = 23;

        return progress;
    }

}
