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
    public Machine_Judge Machine;

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

        //デンモク画面ロック状態
        if(gameData.isClearRock1)
        {
            Denmoku_Judge.Instance.isClear_Rock1 = true;
            Denmoku_Judge.Instance.ChangeScreen(102);
        }

        //星の力の予約有無
        if (gameData.isSendStarPower)
        {
            Denmoku_Judge.Instance.isSendStarPower = true;
            Machine.LampTop.GetComponent<Renderer>().material.color = Color.red;
        }

        //1歩1歩の予約有無
        if (gameData.isSendStepStep)
        {
            Denmoku_Judge.Instance.isSendStepStep = true;
            Machine.LampCenter.GetComponent<Renderer>().material.color = Color.red;
        }


        //九州Loversの予約有無
        if (gameData.isSendLovers)
        {
            Denmoku_Judge.Instance.isSendLovers = true;
            Machine.LampTop.GetComponent<Renderer>().material.color = Color.red;
        }


        //カラオケ機
        if (gameData.isClearMachine)
        {
            Machine.isClear = true;
            TV_Manager.Instance.ChangeTVScreen("a03");

            //ボタンを正解に
            Machine.ButtonTop.Objects[0].SetActive(false);
            Machine.ButtonCenter.Objects[0].SetActive(false);
            Machine.ButtonBottom.Objects[0].SetActive(false);

            Machine.ButtonTop.Objects[4].SetActive(true);
            Machine.ButtonCenter.Objects[1].SetActive(true);
            Machine.ButtonBottom.Objects[4].SetActive(true);
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
