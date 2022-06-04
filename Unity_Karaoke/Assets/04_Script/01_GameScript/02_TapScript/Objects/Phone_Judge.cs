using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phone_Judge : MonoBehaviour
{
    //正解したかどうか
    public bool isClear = false;

    //ボタンの3桁
    public string InputNo = "000";

    //答えの3桁
    public string AnswerNo = "453";

    //箱のスライドオブジェクト
    public GameObject UpSlide;
    //答え合わせ
    public void JudgeAnswer(string buttonName, int Index)
    {
        //入力値を更新
        if (buttonName == "Top") //上ボタンの時
        {
            //1桁目をチェンジ
            InputNo = Index + InputNo.Substring(1);
        }
        else if (buttonName == "Center") //中央ボタンの時
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

            //1秒後にカメラ移動
            Invoke(nameof(AfterClear1), 1);

            //最後にセーブ
            SaveLoadSystem.Instance.Save();
        }

    }
    //正解後のスライド開く
    private void AfterClear1()
    {
        //効果音
        AudioManager.Instance.SoundSE("Slide");
        //スライド開く
        UpSlide.transform.Translate(new Vector3(0f, 0f, 0.4f));
        //画面ブロックを解除
        BlockPanel.Instance.HideBlock();

    }
}
