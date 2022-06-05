using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartResetManager : MonoBehaviour
{
    //アイテムオブジェクト
    //public GameObject itemPaper1;

    //ゲーム内オブジェクト
    //public GameObject paperHolder;

    private GameData gameData;

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

     
        //デンモク画面ロック状態
        if(gameData.isClear_Rock1)
        {
            Denmoku_Judge.Instance.isClear_Rock1 = true;
            Denmoku_Judge.Instance.ChangeScreen(102);
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
