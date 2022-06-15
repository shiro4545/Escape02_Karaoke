using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rimocon_Tap : TapCollider
{
    //ボタン名
    public string ButtonName;

    //答え合せクラス
    public Rimocon_Judge JudgeClass;

    //ボタンタップ時
    protected override void OnTap()
    {
        base.OnTap();

        //答えが正解済みの場合は処理しない
        if (JudgeClass.isClear)
            return;

        //効果音
        AudioManager.Instance.SoundSE("TapButton");

        //ボタンを後ろに移動
        this.gameObject.transform.Translate(new Vector3(0, 0, 0.02f));
        //0.1秒後にボタン位置を元に戻す
        Invoke(nameof(delayButton), 0.1f);

        //答え合せ
        JudgeClass.JudgeAnswer(ButtonName);
    }



    //押されたボタンを戻す
    private void delayButton()
    {
        this.gameObject.transform.Translate(new Vector3(0, 0, -0.02f));
    }
}

        