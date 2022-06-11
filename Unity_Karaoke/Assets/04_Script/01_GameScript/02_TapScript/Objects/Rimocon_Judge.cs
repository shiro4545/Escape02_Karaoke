using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rimocon_Judge : MonoBehaviour
{
    //正解したかどうか
    public bool isClear = false;
    //ボタンの4桁
    public string InputNo = "0000";
    //答えの4桁
    public string AnswerNo = "4212";
    


    //答え合わせ
    public void JudgeAnswer(string buttonName, int Index)
    {

        //入力値の更新
        if (buttonName == "ButtonMic")
        {
            InputNo = Index + InputNo.Substring(1);
        }
        else if (buttonName == "ButtonDenmoku")
        {
            InputNo = InputNo.Substring(0, 1) + Index + InputNo.Substring(2);
        }
        else if (buttonName == "ButtonMirrorBall")
        {
            InputNo = InputNo.Substring(0, 2) + Index + InputNo.Substring(3);
        }
        else
        {
            InputNo = InputNo.Substring(0, 3) + Index;
        }

        //答えの判定
        if (InputNo == AnswerNo)
        {
            //クリアの効果音
            AudioManager.Instance.SoundSE("Clear");
            //クリア判定をtrueに
            isClear = true;

            //画面ブロック
            BlockPanel.Instance.ShowBlock();

            //1秒後にカメラ移動
            Invoke(nameof(AfterClear1), 1.5f);

            //最後にセーブ
            SaveLoadSystem.Instance.Save();
        }
    }

    //正解後のカメラ移動
        private void AfterClear1()
        {
            //カメラ移動
            CameraManager.Instance.ChangeCameraPosition("RoomTV");
            //移動してさらに1秒後に画面変更
            Invoke(nameof(AfterClear2), 1.5f);
        }

    //正解後のスライド開く
    private void AfterClear2()
    {
        TV_Manager.Instance.ChangeTVScreen("a02");


        //さらに1秒後に画面変更
        Invoke(nameof(AfterClear3), 1.5f);
    }

    //最後にもとの画面に戻る
    private void AfterClear3()
    {
        CameraManager.Instance.ChangeCameraPosition("Rimocon");

        //画面ブロックを解除
        BlockPanel.Instance.HideBlock();
        
    }

}