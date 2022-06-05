using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Machine_Judge : MonoBehaviour
{
    //正解したかどうか
    public bool isClear = false;

    //ボタンの3桁
    public string InputNo = "000";

    //答えの3桁
    private string AnswerNo = "414";

    //タップクラス
    public Machine_Tap ButtonTop;
    public Machine_Tap ButtonCenter;
    public Machine_Tap ButtonBottom;

    //予約有無ランプ
    public GameObject LampTop;
    public GameObject LampCenter;
    public GameObject LampBottom;

    //答え合わせ
    public void JudgeAnswer(string buttonName, int Index)
    {
        //入力値を更新
        if (buttonName == "Top") //左ボタンの時
            //1桁目をチェンジ
            InputNo = Index + InputNo.Substring(1);
        else if (buttonName == "Center") //中央ボタンの時
            //2桁目をチェンジ
            InputNo = InputNo.Substring(0, 1) + Index + InputNo.Substring(2);
        else //右ボタンの時
            //3桁目をチェンジ
            InputNo = InputNo.Substring(0, 2) + Index;


        //対象3曲が予約済みなら答え判定
        if (
                InputNo == AnswerNo &&
                Denmoku_Judge.Instance.isSendStarPower &&
                Denmoku_Judge.Instance.isSendStepStep &&
                Denmoku_Judge.Instance.isSendLovers
            )
        {
            //クリアの効果音
            AudioManager.Instance.SoundSE("Clear");
            //クリア判定をtrueに
            isClear = true;

            //画面ブロック
            BlockPanel.Instance.ShowBlock();

            //1秒後にカメラ移動
            Invoke(nameof(AfterClear1), 1);

            //最後にセーブ
            SaveLoadSystem.Instance.gameData.isClearMachine = true;
            SaveLoadSystem.Instance.Save();
        }

    }



    //正解後のカメラ移動
    private void AfterClear1()
    {
        //カメラ移動
        CameraManager.Instance.ChangeCameraPosition("RoomTV");
        //1秒後にテレビ画面切替
        Invoke(nameof(AfterClear2), 1);
    }
    private void AfterClear2()
    {
        //画面切替
        TV_Manager.Instance.ChangeTVScreen("a03");
        Invoke(nameof(AfterClear3), 1.5f);
    }
    private void AfterClear3()
    {
        //カメラ移動
        CameraManager.Instance.ChangeCameraPosition("Machine");
        //画面ブロックを解除
        BlockPanel.Instance.HideBlock();
    }

    //Backボタン押下時
    public void TapBack()
    {
        if (isClear)
            return;

        //未予約のボタンは初期化
        if (!Denmoku_Judge.Instance.isSendStarPower)
        {
            ButtonTop.Objects[ButtonTop.Index].SetActive(false);
            ButtonTop.Index = 0;
            ButtonTop.Objects[0].SetActive(true);
            InputNo = "0" + InputNo.Substring(1);
        }

        if (!Denmoku_Judge.Instance.isSendStepStep)
        {
            ButtonCenter.Objects[ButtonCenter.Index].SetActive(false);
            ButtonCenter.Index = 0;
            ButtonCenter.Objects[0].SetActive(true);
            InputNo = InputNo.Substring(0, 1) + 0 + InputNo.Substring(2);
        }

        if (!Denmoku_Judge.Instance.isSendLovers)
        {
            ButtonBottom.Objects[ButtonBottom.Index].SetActive(false);
            ButtonBottom.Index = 0;
            ButtonBottom.Objects[0].SetActive(true);
            InputNo = InputNo.Substring(0, 2) + 0;
        }

    }
}
