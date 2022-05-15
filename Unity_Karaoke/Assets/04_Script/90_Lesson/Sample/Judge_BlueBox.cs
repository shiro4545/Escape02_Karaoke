using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Judge_BlueBox : MonoBehaviour
{
    //正解したかどうか
    public bool isClear = false;

    //ボタンの3桁
    public string InputNo = "000";

    //答えの3桁
    public string AnswerNo = "123";

    //青箱のスライドオブジェクト
    public GameObject UnderSlide;



    //答え合わせ
    public void JudgeAnswer(string buttonName,int Index)
    {
      //入力値を更新
        if(buttonName == "Left")
        {
            //1桁目をチェンジ
            InputNo = Index + InputNo.Substring(1);
        }
        else if(buttonName == "Center")
        {
            //2桁目をチェンジ
            InputNo = InputNo.Substring(0, 1) + Index + InputNo.Substring(2);
        }
        else
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

        }

    }



    //正解後のカメラ移動
    private void AfterClear1()
    {
        //カメラ移動
        CameraManager.Instance.ChangeCameraPosition("BlueBox");
        //1秒後にスライド開く
        Invoke(nameof(AfterClear2), 1);
    }

    //正解後のスライド開く
    private void AfterClear2()
    {
        //効果音
        AudioManager.Instance.SoundSE("Slide");
        //スライド開く
        UnderSlide.gameObject.transform.Translate(new Vector3(-0.7f, 0, 0));
        //画面ブロックを解除
        BlockPanel.Instance.HideBlock();
    }
}
