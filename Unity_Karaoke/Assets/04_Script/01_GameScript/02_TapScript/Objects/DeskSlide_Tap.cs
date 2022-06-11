using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeskSlide_Tap : TapCollider
{
    //引き出し
    public GameObject Slide;

    //引き出し状態 (0:全閉,1:全開,2:ちょい開き)
    public int Status = 0;

    //アイテムクラス(ストロー or 鍵2)
    public SlideItem_Tap ItemClass;

    //上引き出し(下引き出し用)
    public DeskSlide_Tap UpperSlide;

    //ハンガークラス
    //public Hanger_Judge HangerClass;

    //タンバリンクラス
    public Tambarin_Judge TambarinClass;


    //ボタンタップ時
    protected override void OnTap()
    {
        base.OnTap();

        //ハンガー回転が未正解なら何もしない
        //if (!HangerClass.isClear)
        //    return;

        //上引き出し用
        if (Slide.name == "Upper")
            if(!TambarinClass.isClear)
               return;

        //下引き出し用
        //if (Slide.name == "Under")
        //if(!HangerClass.isClear)
        //      return;


        if (Status == 0)
        {
            //上引き出しが開いててアイテム未取得の場合、タップが貫通して下のスライドが開いてしまう対策
            if (Slide.name == "Under")
                if (UpperSlide.Status == 1)
                    return;

            //全閉→全開
            Slide.transform.Translate(new Vector3(0, -1.1f, 0));
            Status = 1;
        }
        else if(Status == 1)
        {
            //全開→全閉
            Slide.transform.Translate(new Vector3(0, 1.1f, 0));
            Status = 0;
        }
        else
        {
            //ちょい開→全開
            Slide.transform.Translate(new Vector3(0, -0.95f, 0));
            //引き出しコライダー非表示
            this.gameObject.SetActive(false);
            Status = 1;

        }

        AudioManager.Instance.SoundSE("Slide");

        if (Slide.name == "Upper")
            SaveLoadSystem.Instance.gameData.DeskUpperStatus = Status;
        else
            SaveLoadSystem.Instance.gameData.DeskUnderStatus = Status;

        SaveLoadSystem.Instance.Save();
    }



    //クリア後に自動ちょい開き
    public void OpenSlide()
    {
        AudioManager.Instance.SoundSE("Slide");

        //全閉→全開
        Slide.transform.Translate(new Vector3(0, -0.15f, 0));
        Status = 2;


        if (Slide.name == "Upper")
            SaveLoadSystem.Instance.gameData.DeskUpperStatus = Status;
        else
            SaveLoadSystem.Instance.gameData.DeskUnderStatus = Status;

        SaveLoadSystem.Instance.Save();

    }
}
