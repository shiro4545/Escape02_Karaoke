using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalButton_Judge : MonoBehaviour
{
    //正解したかどうか
    public bool isClear = false;

    //ボタンの3桁
    public string InputNo = "000";

    //答えの3桁
    public string AnswerNo = "123";

    //鍵穴のフタ
    public GameObject KeyCover;


    //答え合わせ
    public void JudgeAnswer(int ButtonNo, int Index)
    {
        //入力値を更新
        if (ButtonNo == 1) //左ボタンの時
        {
            //1桁目をチェンジ
            InputNo = Index + InputNo.Substring(1);
        }
        else if (ButtonNo == 2) //中央ボタンの時
        {
            //2桁目をチェンジ
            InputNo = InputNo.Substring(0, 1) + Index + InputNo.Substring(2);
        }
        else //右ボタンの時
        {
            //3桁目をチェンジ
            InputNo = InputNo.Substring(0, 2) + Index;
        }


        //答え判定
        if (InputNo == AnswerNo)
        {
            //クリアの効果音
            AudioManager.Instance.SoundSE("Clear");
            //クリア判定をtrueに
            isClear = true;

            //画面ブロック
            BlockPanel.Instance.ShowBlock();

            //1.5秒後に鍵穴の蓋が開く
            Invoke(nameof(AfterClear1), 1.5f);

            //最後にセーブ
            SaveLoadSystem.Instance.gameData.isClearFinalBtn = true;
            SaveLoadSystem.Instance.Save();
        }

    }



    //正解後のカメラ移動
    private void AfterClear1()
    {
        //効果音
        AudioManager.Instance.SoundSE("Slide");
        //スライド開く
        KeyCover.transform.Translate(new Vector3(0.13f, 0, 0));
        //画面ブロックを解除
        BlockPanel.Instance.HideBlock();
    }
}
