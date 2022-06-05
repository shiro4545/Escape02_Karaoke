using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Denmoku_Tap : MonoBehaviour
{
    //タップ有効な画面No
    public int EnableScreenNo;

    //ボタン識別
    public int SubInt;
    public string SubStr;

    // Start is called before the first frame update
    void Start()
    {
        var CurrentTrigger = gameObject.AddComponent<EventTrigger>();
        var EntryClick = new EventTrigger.Entry();
        EntryClick.eventID = EventTriggerType.PointerClick;
        EntryClick.callback.AddListener((x) => OnTap());
        CurrentTrigger.triggers.Add(EntryClick);
    }

    // Update is called once per frame
    void Update()
    {
        bool isEnable = false;

        if (CameraManager.Instance.CurrentPositionName == "Denmoku")
        {
            //通常
            if (EnableScreenNo == Denmoku_Judge.Instance.CurrentScreenNo)
                isEnable = true;
            //予約ボタン
            if (EnableScreenNo == 990)
            {
                if (Denmoku_Judge.Instance.CurrentScreenNo == 211 ||
                    Denmoku_Judge.Instance.CurrentScreenNo == 314 ||
                    Denmoku_Judge.Instance.CurrentScreenNo == 515)
                    isEnable = true;
            }
            //戻るボタン用
            if (EnableScreenNo == 999 && Denmoku_Judge.Instance.CurrentScreenNo >= 200)
                isEnable = true;
        }

        if (isEnable)
            GetComponent<BoxCollider>().enabled = true;
        else GetComponent<BoxCollider>().enabled = false;
    }



    public void OnTap()
    {
        AudioManager.Instance.SoundSE("TapDenmoku");

        switch (EnableScreenNo)
        {
            //ロック画面(101) 数字タップ
            case 101:
                Denmoku_Judge.Instance.Input101(SubInt);
                break;

            //メニュー画面(102) カテゴリタップ
            case 102:
                Denmoku_Judge.Instance.ChangeScreen(SubInt);
                break;

            //曲検索画面(201) 数字タップ
            case 201:
                if(SubStr == "Delete")
                    Denmoku_Judge.Instance.Delete201();
                else
                    Denmoku_Judge.Instance.Input201(SubInt);
                break;

            //りれき画面(301) 履歴曲タップ
            case 301:
                Denmoku_Judge.Instance.ChangeScreen(SubInt);
                break;

            //歌手検索画面(501) 文字タップ
            case 501:
                if (SubStr == "Delete")
                    Denmoku_Judge.Instance.Delete501();
                else
                    Denmoku_Judge.Instance.Input501(SubStr);
                break;

            //選曲画面の予約ボタン
            case 990:
                Denmoku_Judge.Instance.TapSendSong();
                break;
            //戻るボタン
            case 999:
                Denmoku_Judge.Instance.TapBack();
                break;

            default:
                break;
              
    }
    }
}
