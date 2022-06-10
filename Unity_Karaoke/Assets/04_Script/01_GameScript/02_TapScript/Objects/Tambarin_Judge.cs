using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tambarin_Judge : MonoBehaviour
{
    //クリア判定
    public bool isClear = false;

    //タンバリン3つの配置状態(0:何もなし,1:丸,2:四角,3:三角)
    //初期配置は右に丸がある
    public string InputStatus = "001";

    //引き出し
    public GameObject Slide;

    //答え合わせ
    public void AnswerJudge(int PositionIndex, int TambarinNo)
    {
        //入力値更新
        if (PositionIndex == 0)
            InputStatus = TambarinNo + InputStatus.Substring(1);
        else if (PositionIndex == 1)
            InputStatus = InputStatus.Substring(0, 1) + TambarinNo + InputStatus.Substring(2);
        else
            InputStatus = InputStatus.Substring(0, 2) + TambarinNo;

        //答え合わせ
        if (InputStatus == "312")
        {
            BlockPanel.Instance.ShowBlock();
            AudioManager.Instance.SoundSE("Clear");

            //引き出し開ける
            Invoke(nameof(AfterClear1), 1.5f);

            //ステータス変更
            isClear = true;
            SaveLoadSystem.Instance.gameData.isClearTambarin = true;
        }

        SaveLoadSystem.Instance.gameData.TambarinStatus = InputStatus;

        SaveLoadSystem.Instance.Save();
    }


    //演出
    private void AfterClear1()
    {
        //引き出し開ける
        Slide.transform.Translate(new Vector3(0, 1,0));
        BlockPanel.Instance.HideBlock();
    }
}
