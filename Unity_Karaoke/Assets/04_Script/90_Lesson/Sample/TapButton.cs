using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapButton : TapCollider
{
    //ボタン名
    public string ButtonName;
    //通し番号0~4
    public int Index;
    //画像オブジェクトの配列
    public GameObject[] Objects;

    //答え合せクラス
    public Judge_BlueBox JudgeClass;


    //ボタンタップ時
    protected override void OnTap()
    {
        base.OnTap();

        //答えが正解済みの場合は処理しない
        if (JudgeClass.isClear)
            return;

        //効果音
        AudioManager.Instance.SoundSE("TapButton");

        //表示中の画像を非表示に
        Objects[Index].SetActive(false);

        //インデックスを+1
        Index++;

        //インデックスが4の場合、0に戻る
        if (Index >= Objects.Length)
        {
            Index = 0;
        }

        //次の画像を表示
        Objects[Index].SetActive(true);

        //ボタンをへこます(サイズを小さくする)
        this.gameObject.transform.localScale = new Vector3(9, 1, 9);

        //0.1秒後にボタンサイズを元に戻す
        Invoke(nameof(delayButton), 0.1f);

        //答え合せ
        JudgeClass.JudgeAnswer(ButtonName, Index);
    }



    //押されたボタンを戻す
    private void delayButton()
    {
        this.gameObject.transform.localScale = new Vector3(10, 2, 10);
    }
}
