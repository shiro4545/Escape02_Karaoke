using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Denmoku_Judge : MonoBehaviour
{
    public static Denmoku_Judge Instance { get; set; }

    //現在の画面No
    public int CurrentScreenNo;
    //1つ前の画像No
    public int PrevScreenNo;

    //メイン画面
    public GameObject MainScreen;
    //メッセージ画像
    public GameObject Msg601; //パスワードが違う
    public GameObject Msg801; //検索中
    public GameObject Msg802; //検索にヒットしない
    public GameObject Msg803; //曲番号が正しくない
    public GameObject Msg901; //送信中
    public GameObject Msg902; //予約を受け付けました
    public GameObject Msg903; //もう1度予約してください

    //ロック画面用(101)
    public GameObject[] ImageArray101;
    private string UserNo101 = "";
    private string AnswerNo101 = "6278";
    public bool isClear_Rock1 = false;

    //曲番号画面用(102)
    public GameObject[] ImageArray201;
    private string UserNo201 = "";
    public bool isSendStarPower = false;

    //りれき画面用
    public bool isSendStepStep = false;

    //歌手検索画面用(501)
    public GameObject[] ImageArray501;
    private string UserNo501 = "";
    public bool isSendLovers = false;


    // Start is called before the first frame update
    void Start()
    {
        Instance = this;

        //初期はロック画面
        //ChangeScreen(101);
        ChangeScreen(102);
    }


    //************************************************************************************
    //<summary>
    //デンモク画面の変更
    //</summary>
    //<param>画面No</param>
    public void ChangeScreen(int ScreenNo)
    {
        MainScreen.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Images/10_Denmoku/" + ScreenNo);
        PrevScreenNo = CurrentScreenNo;
        CurrentScreenNo = ScreenNo;
    }


    //************************************************************************************
    //<summary>
    //戻るボタン
    //</summary>
    //<param>画面No</param>
    public void TapBack()
    {
        //曲番号の場合
        if (CurrentScreenNo == 201)
        {
            //5桁消去
            UserNo201 = "";
            foreach (var img in ImageArray201)
                img.GetComponent<SpriteRenderer>().sprite = null;
        }
        //歌手名検索の場合
        if (CurrentScreenNo == 501)
        {
            //4文字消去
            UserNo501 = "";
            foreach (var img in ImageArray501)
                img.GetComponent<SpriteRenderer>().sprite = null;
        }

        //きゃなるしてぃ〜ずの曲一覧画面の場合
        if (CurrentScreenNo == 502)
            ChangeScreen(501);

        if (CurrentScreenNo == 201 || CurrentScreenNo == 301 || CurrentScreenNo == 501)
            ChangeScreen(102);
        else
            ChangeScreen(PrevScreenNo);
    }


    //************************************************************************************
    //<summary>
    //ロック画面の入力判定(101)
    //</summary>
    //<param>画面No</param>
    public void Input101(int SubInt)
    {
        BlockPanel.Instance.ShowBlock();
        //数字画像チェンジ
        ImageArray101[UserNo101.Length].GetComponent<SpriteRenderer>().sprite
            = Resources.Load<Sprite>("Images/03_Number/" + SubInt);

        UserNo101 += SubInt;

        //答え合わせ
        if (UserNo101.Length >= ImageArray101.Length)
            Invoke(nameof(JudgeAnswer101), 0.7f);
        else
            BlockPanel.Instance.HideBlock();

    }

    //答え合わせ
    private void JudgeAnswer101()
    {
        //4桁消去
        foreach (var img in ImageArray101)
            img.GetComponent<SpriteRenderer>().sprite = null;

        //答え合わせ 6278
        if (UserNo101 == AnswerNo101)
        {
            AudioManager.Instance.SoundSE("Clear");
            ChangeScreen(102);
            isClear_Rock1 = true;

            SaveLoadSystem.Instance.gameData.isClear_Rock1 = true;
            SaveLoadSystem.Instance.Save();
        }
        else
        {
            AudioManager.Instance.SoundSE("NotClear");
            Msg601.SetActive(true);
            Invoke(nameof(HideMsg601), 3f);
        }

        UserNo101 = "";
        BlockPanel.Instance.HideBlock();
    }

    //メッセージ非表示
    public void HideMsg601()
    {
        Msg601.SetActive(false);
    }

    //************************************************************************************
    //<summary>
    //曲番号検索画面の入力判定(201)
    //</summary>
    //<param>画面No</param>
    public void Input201(int SubInt)
    {
        BlockPanel.Instance.ShowBlock();
        //数字画像チェンジ
        ImageArray201[UserNo201.Length].GetComponent<SpriteRenderer>().sprite
            = Resources.Load<Sprite>("Images/03_Number/" + SubInt);

        UserNo201 += SubInt;

        //答え合わせ
        if (UserNo201.Length >= ImageArray201.Length)
            Invoke(nameof(JudgeAnswer201), 0.5f);
        else
            BlockPanel.Instance.HideBlock();

    }

    //答え合わせ
    private void JudgeAnswer201()
    {
        //5桁消去
        foreach (var img in ImageArray201)
            img.GetComponent<SpriteRenderer>().sprite = null;

        //答え合わせして、選曲画面に
        switch(UserNo201)
        {
            case "77283":
                ChangeScreen(211);
                break;
            case "12171":
                ChangeScreen(212);
                break;
            case "45531":
                ChangeScreen(213);
                break;
            case "93761":
                ChangeScreen(214);
                break;
            default:
                AudioManager.Instance.SoundSE("NotClear");
                Msg801.SetActive(true);
                Invoke(nameof(HideMsg801), 1.5f);
                break;
          
        }
        UserNo201 = "";
        BlockPanel.Instance.HideBlock();
    }

    //メッセージ非表示
    public void HideMsg801()
    {
        Msg801.SetActive(false);
    }

    //<summary>
    //曲番号検索画面の1文字削除(201)
    //</summary>
    public void Delete201()
    {
        if (UserNo201.Length == 0)
            return;

        ImageArray201[UserNo201.Length - 1].GetComponent<SpriteRenderer>().sprite = null;

        UserNo201 = UserNo201.Substring(0, UserNo201.Length - 1);
    }


    //************************************************************************************
    //<summary>
    //歌手検索検索画面の入力判定(501)
    //</summary>
    //<param>画面No</param>
    public void Input501(string SubStr)
    {
        BlockPanel.Instance.ShowBlock();
        //文字画像チェンジ
        ImageArray501[UserNo501.Length/2].GetComponent<SpriteRenderer>().sprite
            = Resources.Load<Sprite>("Images/04_Moji/" + SubStr);

        UserNo501 += SubStr;

        //検索中
        if (UserNo501.Length/2 >= ImageArray501.Length)
            Invoke(nameof(ShowSearch), 0.3f);
        else
            BlockPanel.Instance.HideBlock();
    }

    //検索中
    private void ShowSearch()
    {
        Msg801.SetActive(true);
        //答え合わせ
        Invoke(nameof(JudgeAnswer501), 1.5f);
    }

    //答え合わせ
    private void JudgeAnswer501()
    {
        Msg801.SetActive(false);

        //5桁消去
        foreach (var img in ImageArray501)
            img.GetComponent<SpriteRenderer>().sprite = null;

        //答え合わせ
        if (UserNo501 == "kiyanaru")
            ChangeScreen(515);
        else
        {
            //ヒットなし
            Msg802.SetActive(true);
            Invoke(nameof(HideMsg802), 1.5f);
        }

        UserNo501 = "";
        BlockPanel.Instance.HideBlock();
    }

    //メッセージ非表示
    public void HideMsg802()
    {
        Msg802.SetActive(false);
    }

    //<summary>
    //曲番号検索画面の1文字削除(201)
    //</summary>
    public void Delete501()
    {
        if (UserNo501.Length == 0)
            return;

        ImageArray501[UserNo501.Length/2 - 1].GetComponent<SpriteRenderer>().sprite = null;

        UserNo501 = UserNo501.Substring(0, UserNo501.Length - 2);
    }

    //************************************************************************************
    //<summary>
    //曲予約ボタン(211,314,515)
    //</summary>
    public void TapSendSong()
    {
        BlockPanel.Instance.ShowBlock();
        //送信中
        Msg901.SetActive(true);

        //曲再生中か判定
        if (TV_Manager.Instance.isPlaySong)
            //もう1度予約して
            Invoke(nameof(act7), 2f);
        else
        {
            //受け付けました
            Invoke(nameof(act1), 2f);
            //セーブ
            switch(CurrentScreenNo)
            {
                case 211:
                    isSendStarPower = true;
                    SaveLoadSystem.Instance.gameData.isSendStarPower = true;
                    break;
                case 314:
                    isSendStepStep = true;
                    SaveLoadSystem.Instance.gameData.isSendStepStep = true;
                    break;
                case 515:
                    isSendLovers = true;
                    SaveLoadSystem.Instance.gameData.isSendLovers = true;
                    break;
                default:
                    break;
            }
            SaveLoadSystem.Instance.Save();
        }

    }
    //予約成功時
    public void act1()
    {
        Msg901.SetActive(false);
        Msg902.SetActive(true);
        //テレビ画面の曲スタート
        TV_Manager.Instance.StartSong(CurrentScreenNo);
        //メニュー画面へ
        Invoke(nameof(act2), 2f);
    }
    public void act2()
    {
        Msg902.SetActive(false);
        ChangeScreen(102);
        BlockPanel.Instance.HideBlock();
    }

    //予約失敗時
    public void act7()
    {
        Msg901.SetActive(false);
        Msg903.SetActive(true);
        Invoke(nameof(act8), 3.5f);
    }
    public void act8()
    {
        Msg903.SetActive(false);
        BlockPanel.Instance.HideBlock();

    }


}