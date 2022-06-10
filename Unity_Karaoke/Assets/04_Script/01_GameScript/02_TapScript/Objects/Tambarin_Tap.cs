using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tambarin_Tap : TapCollider
{
    //場所(0:左,1:真ん中,2:右)
    public int PositionIndex;

    //タンバリンNo (1:丸,2:四角,3:三角)
    private int TambarinNo;

    //タンバリン配列(丸,三角,四角)
    public GameObject[] Objects;

    //答え合わせクラス
    public Tambarin_Judge JudgeClass;

    //ボタンタップ時
    protected override void OnTap()
    {
        base.OnTap();

        if (JudgeClass.isClear)
            return;

        if(JudgeClass.InputStatus.Substring(PositionIndex,1) == "0")
        {
            //何も置かれていない場合

            //選択アイテム名
            string ItemName = ItemManager.Instance.selectItem;

            if (ItemName.IndexOf("Tambarin") != -1)
            {
                //タンバリンを選択している場合
                if (ItemName == "Tambarin_Maru")
                    TambarinNo = 1;
                else if (ItemName == "Tambarin_Shikaku")
                    TambarinNo = 2;
                else 
                    TambarinNo = 3;

                //タンバリンを表示
                Objects[TambarinNo - 1].SetActive(true);
                //アイテム使用
                ItemManager.Instance.useItem();
                AudioManager.Instance.SoundSE("PutTambarin");

                //答えわせ
                JudgeClass.AnswerJudge(PositionIndex, TambarinNo);
            }
        }
        else
        {
            //タンバリンが置かれている場合

            TambarinNo = int.Parse(JudgeClass.InputStatus.Substring(PositionIndex, 1));

            string TambarinName;
            if (TambarinNo == 1)
                TambarinName = "Tambarin_Maru";
            else if(TambarinNo == 2)
                TambarinName = "Tambarin_Shikaku";
            else
                TambarinName = "Tambarin_Sankaku";

            //タンバリンを非表示
            Objects[TambarinNo - 1].SetActive(false);
            //入力値更新
            JudgeClass.AnswerJudge(PositionIndex, 0);
            //アイテム取得
            ItemManager.Instance.getItem(TambarinName);
        }
    }
}
