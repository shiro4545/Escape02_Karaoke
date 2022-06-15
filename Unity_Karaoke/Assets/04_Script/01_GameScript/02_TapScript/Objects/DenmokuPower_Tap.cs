using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DenmokuPower_Tap : TapCollider
{
    //電源スイッチ
    public GameObject PowerSwitch;

    //電話裏ボタンクラス
    public Phone_Judge PhoneClass;

    //タップ時
    protected override void OnTap()
    {
        base.OnTap();

        //効果音
        AudioManager.Instance.SoundSE("Switch");

        //デンモク状態を判定
        if (Denmoku_Judge.Instance.DenmokuStatus == 0)
        {
            //電源OFF→ONの場合
            PowerSwitch.transform.Translate(new Vector3(0.12f, 0, 0));
            Denmoku_Judge.Instance.DenmokuStatus = 1;
            SaveLoadSystem.Instance.gameData.DenmokuStatus = 1;
            //画面切替
            Denmoku_Judge.Instance.ChangeScreen(101);

            if (PhoneClass.isClear)
                SaveLoadSystem.Instance.gameData.isClearPowerOn = true;
        }
        else
        {
            //電源ON→OFFの場合
            PowerSwitch.transform.Translate(new Vector3(-0.12f, 0, 0));
            Denmoku_Judge.Instance.DenmokuStatus = 0;
            SaveLoadSystem.Instance.gameData.DenmokuStatus = 0;
            //入力値クリア
            Denmoku_Judge.Instance.PowerOff();
            //画面切替
            Denmoku_Judge.Instance.ChangeScreen(100);
        }

        SaveLoadSystem.Instance.Save();
    }
}
