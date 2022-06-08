using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pentagon_Tap : TapCollider
{
    //ピース位置
    public int PieceAreaNo;
    //ピースオブジェクト
    public GameObject[] PieceArray;
    //Judgeクラス
    public Pentagon_Judge JudgeClass;


    //タップ時
    protected override void OnTap()
    {
        base.OnTap();

        if (JudgeClass.isClear)
            return;

        //既に置かれているピースNo (0の場合は何もなし)
        int OldPieceNo = int.Parse(JudgeClass.Input.Substring(PieceAreaNo - 1,1));
        //次に置くピースNo (0の場合は何もなし)
        int NewPieceNo = 0;

        //既に別のピースが置かれていれば非表示にし、アイテム取得
        if (OldPieceNo != 0)
        {
            //非表示に
            PieceArray[OldPieceNo - 1].SetActive(false);
            //アイテムゲット
            ItemManager.Instance.getItem("Piece" + OldPieceNo);
        }


        if (ItemManager.Instance.selectItem.IndexOf("Piece") != -1)
        {
            AudioManager.Instance.SoundSE("PutItem");
            //選択してるピースNo
            NewPieceNo = int.Parse(ItemManager.Instance.selectItem.Substring(5));
            //アイテムを使う
            ItemManager.Instance.useItem();
            //選択しているピースを表示
            PieceArray[NewPieceNo - 1].SetActive(true);
        }

        //入力変更&答え合わせ
        JudgeClass.ChangeInput(PieceAreaNo, NewPieceNo);

    }


}
