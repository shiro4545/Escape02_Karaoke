using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Denmoku_Judge : MonoBehaviour
{
    public static Denmoku_Judge Instance { get; set; }

    //ロック画面状態(0:電源off,1:ロック状態,2:ロックなし)
    public int DenmokuStatus = 1;

    //デンモクがスライドしたか
    public bool isSlide = false;

    //現在の画面No
    public int CurrentScreenNo;
    //1つ前の画像No
    public int PrevScreenNo;

    //メイン画面
    public GameObject MainScreen;
    //メッセージ画像
    public GameObject Msg601; //パスワードが違う
    public GameObject Msg701; //注文を受け付けた
    public GameObject Msg801; //検索中
    public GameObject Msg802; //検索にヒットしない
    public GameObject Msg803; //曲番号が正しくない
    public GameObject Msg901; //送信中
    public GameObject Msg902; //予約を受け付けました
    public GameObject Msg903; //もう1度予約してください

    //ロック画面用(101)
    public GameObject[] ImageArray101;
    private string UserNo101 = "";

    //曲番号画面用(102)
    public GameObject[] ImageArray201;
    private string UserNo201 = "";
    public bool isSendStarPower = false;

    //りれき画面用
    public bool isSendStepStep = false;

    //歌手検索画面用(501)
    public bool isSendLovers = false;
    public GameObject[] ImageArray501;
    private string UserNo501 = "";

    //注文画面用(401)
    public GameObject[] ImageArray401;
    private string UserNo401 = "0000";

    //カラオケ機
    public Machine_Judge Machine;
    //電話クラス
    public Phone_Judge PhoneClass;
    //電話
    public GameObject Phone;
    //電話裏ボタンのコライダー
    public GameObject PhoneBtnCollider;

    //ドライバー用スライド対応
    public GameObject DenmokuCollider;
    public GameObject DenmokuBackCollider;
    public GameObject DriverCollider;
    public GameObject DriverDenmokuCollider;



    // Start is called before the first frame update
    void Start()
    {
        Instance = this;

        //初期はロック画面
        ChangeScreen(101);
        //ChangeScreen(102);
    }


    //************************************************************************************
    //<summary>
    //デンモク画面の変更
    //</summary>
    //<param>画面No</param>
    public void ChangeScreen(int ScreenNo)
    {
        //画面切替
        MainScreen.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Images/10_Denmoku/" + ScreenNo);
        PrevScreenNo = CurrentScreenNo;
        CurrentScreenNo = ScreenNo;

        //注文画面の場合
        if(CurrentScreenNo == 401)
        {
            //画像表示
            for (int i = 0; i < ImageArray401.Length; i++)
            {
                if(i == 1 && Machine.isClear )
                    //割引画像
                    ImageArray401[1].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Images/10_Denmoku/432");
                else
                    //通常画像
                    ImageArray401[i].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Images/10_Denmoku/41" + (i+1));
            }
        }
        else
        {
            //食べ物選択クリア
            UserNo401 = "0000";
            //食べ物画像クリア
            foreach (var img in ImageArray401)
                img.GetComponent<SpriteRenderer>().sprite = null;
        }
    }


    //************************************************************************************
    //<summary>
    //戻るボタン
    //</summary>
    //<param>画面No</param>
    public void TapBack()
    {
        //曲番号の入力クリア
        if (CurrentScreenNo == 201)
        {
            //5桁消去
            UserNo201 = "";
            //数字画像クリア
            foreach (var img in ImageArray201)
                img.GetComponent<SpriteRenderer>().sprite = null;
        }

        //歌手名検索の入力クリア
        if (CurrentScreenNo == 501)
        {
            //4文字消去
            UserNo501 = "";
            //文字画像クリア
            foreach (var img in ImageArray501)
                img.GetComponent<SpriteRenderer>().sprite = null;
        }

        //画面切替え
        switch (CurrentScreenNo)
        {
            case 502:  //きゃなるしてぃ〜ずの曲一覧画面の場合
                //歌手名検索画面へ
                ChangeScreen(501);
                break;
            case 201: //番号画面
            case 301: //りれき画面
            case 501: //歌手名検索画面
                //メニュー画面へ
                ChangeScreen(102);
                break;
            default:
                //1つ前の画面へ
                ChangeScreen(PrevScreenNo);
                break;
        }
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

        //答え合わせ
        //if(true)
        if(UserNo101 == "2369" && PhoneClass.isClear && !isSlide)
        {
            //ドライバーとる時
            AudioManager.Instance.SoundSE("Clear");
            Invoke(nameof(ClearRock), 1.5f);

            //セーブ
            isSlide = true;
            DenmokuStatus = 2;

            SaveLoadSystem.Instance.gameData.isClearDenmokuSlide = true;
            SaveLoadSystem.Instance.gameData.DenmokuStatus = 2;
            SaveLoadSystem.Instance.Save();

        }
        else if (UserNo101 == "6278")
        {
            //ただのロック解除
            AudioManager.Instance.SoundSE("Clear");
            ChangeScreen(102);
            DenmokuStatus = 2;

            BlockPanel.Instance.HideBlock();

            SaveLoadSystem.Instance.gameData.DenmokuStatus = 2;
            SaveLoadSystem.Instance.Save();
        }
        else
        {
            AudioManager.Instance.SoundSE("NotClear");
            Msg601.SetActive(true);
            Invoke(nameof(HideMsg601), 3f);
            BlockPanel.Instance.HideBlock();
        }

        UserNo101 = "";
    }

    //メッセージ非表示
    public void HideMsg601()
    {
        Msg601.SetActive(false);
    }

    //ドライバーとるロック解除の演出
    private void ClearRock()
    {
        ChangeScreen(102);
        CameraManager.Instance.ChangeCameraPosition("Driver");

        Invoke(nameof(AfterClearRock), 1);
    }
    private void AfterClearRock()
    {
        //デンモクスライド
        this.gameObject.transform.Translate(new Vector3(1.2f, 0, 0));
        //DenmokuColliderスライド
        DenmokuCollider.transform.Translate(new Vector3(0, 0, -1.2f));
        //DenmokuBackColliderスライド
        DenmokuBackCollider.transform.Translate(new Vector3(0, 0, -1.2f));

        //DriverCollider表示
        DriverCollider.SetActive(true);
        //DriverDenmoku表示
        DriverDenmokuCollider.SetActive(true);

        BlockPanel.Instance.HideBlock();
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
        switch (UserNo201)
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
        ImageArray501[UserNo501.Length / 2].GetComponent<SpriteRenderer>().sprite
            = Resources.Load<Sprite>("Images/04_Moji/" + SubStr);

        UserNo501 += SubStr;

        //検索中
        if (UserNo501.Length / 2 >= ImageArray501.Length)
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
            ChangeScreen(502);
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

        ImageArray501[UserNo501.Length / 2 - 1].GetComponent<SpriteRenderer>().sprite = null;

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
            //受け付けました
            Invoke(nameof(act1), 2f);

    }
    //予約成功時
    public void act1()
    {
        Msg901.SetActive(false);
        //予約を受け付けました
        Msg902.SetActive(true);
        //テレビ画面の曲スタート
        TV_Manager.Instance.StartSong(CurrentScreenNo);
        //
        //カラオケ機のランプ点灯とフラグ切替
        switch (CurrentScreenNo)
        {
            case 211:
                Machine.LampTop.GetComponent<Renderer>().material.color = Color.red;
                isSendStarPower = true;
                SaveLoadSystem.Instance.gameData.isSendStarPower = true;
                break;
            case 314:
                Machine.LampCenter.GetComponent<Renderer>().material.color = Color.red;
                isSendStepStep = true;
                SaveLoadSystem.Instance.gameData.isSendStepStep = true;
                break;
            case 515:
                Machine.LampBottom.GetComponent<Renderer>().material.color = Color.red;
                isSendLovers = true;
                SaveLoadSystem.Instance.gameData.isSendLovers = true;
                break;
            default:
                break;
        }

        SaveLoadSystem.Instance.Save();
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


    //************************************************************************************
    //<summary>
    //注文画面の食べ物選択(401)
    //</summary>
    public void TapFood(int SubInt)
    {
        BlockPanel.Instance.ShowBlock();

        int AfterNo ; //UserNo用(0:非選択,1:選択)
        int ImageNo ; //画像番号の2桁目 (1:非選択画像,2:選択画像,3:未選択割引,4:選択割引)

        //現在の入力値を確認し、値セット
        if (UserNo401.Substring(SubInt - 1, 1) == "1")
        {
            AfterNo = 0;
            //ポテトか
            if (SubInt == 2)
                ImageNo = Machine.isClear ? 3 : 1;
            else
                ImageNo = 1;
        }
        else
        {
            AfterNo = 1;
            if (SubInt == 2)
                ImageNo = Machine.isClear ? 4 : 2;
            else
                ImageNo = 2;
        }

        //入力値変更
        switch (SubInt)
        {
            case 1:
                UserNo401 = AfterNo + UserNo401.Substring(1);
                break;
            case 2:
                UserNo401 = UserNo401.Substring(0,1) + AfterNo + UserNo401.Substring(2);
                break;
            case 3:
                UserNo401 = UserNo401.Substring(0, 2) + AfterNo + UserNo401.Substring(3);
                break;
            case 4:
                UserNo401 = UserNo401.Substring(0, 3) + AfterNo;
                break;
            default:
                break;
        }

        //食べ物画像チェンジ
        ImageArray401[SubInt - 1].GetComponent<SpriteRenderer>().sprite
            = Resources.Load<Sprite>("Images/10_Denmoku/4" + ImageNo + SubInt);


        BlockPanel.Instance.HideBlock();

    }


    //<summary>
    //注文画面の注文ボタン(401)
    //</summary>
    public void TapOrder()
    {
        BlockPanel.Instance.ShowBlock();
        //送信中
        Msg901.SetActive(true);
        Invoke(nameof(act41), 2f);
    }
    private void act41()
    {
        Msg901.SetActive(false);
        //注文を受け付けました
        Msg701.SetActive(true);
        AudioManager.Instance.SoundSE("Clear");
        Invoke(nameof(JudgeAnswer401), 1f);
    }

     private void JudgeAnswer401()
    {
        if (UserNo401 == "0101" && Machine.isClear)
        {
            //正解演出
            //カメラ移動
            CameraManager.Instance.ChangeCameraPosition("Phone");
            Msg701.SetActive(false);
            Invoke(nameof(AfterClear41), 1.5f);

            //セーブ
            SaveLoadSystem.Instance.gameData.isClearOrder = true;
            SaveLoadSystem.Instance.Save();
        }
        else
        {
            Invoke(nameof(act42), 1f);
            BlockPanel.Instance.HideBlock();
        }
    }

    private void act42()
    {
        Msg701.SetActive(false);
    }

    //正解後の演出
    private void AfterClear41()
    {
        //電話移動
        Phone.transform.Translate(new Vector3(0.4f, 0, 0));
        AudioManager.Instance.SoundSE("Slide");
        Invoke(nameof(AfterClear42), 2.5f);
    }   
    private void AfterClear42()
    {
        //カメラ移動
        CameraManager.Instance.ChangeCameraPosition("Denmoku");
        //電話コライダー表示
        PhoneBtnCollider.SetActive(true);
        BlockPanel.Instance.HideBlock();
    }


    //************************************************************************************
    //<summary>
    //デンモク画面で画面BACKボタン押下
    //</summary>
    public void CameraBack()
    {
        switch (DenmokuStatus)
        {
            case 1: //ロック状態
                //4桁消去
                UserNo101 = "";
                foreach (var img in ImageArray101)
                    img.GetComponent<SpriteRenderer>().sprite = null;
                break;

            case 2: //ロックなし
                //曲番号の入力クリア
                if (CurrentScreenNo == 201)
                {
                    //5桁消去
                    UserNo201 = "";
                    //数字画像クリア
                    foreach (var img in ImageArray201)
                        img.GetComponent<SpriteRenderer>().sprite = null;
                }

                //歌手名検索の入力クリア
                if (CurrentScreenNo == 501)
                {
                    //4文字消去
                    UserNo501 = "";
                    //文字画像クリア
                    foreach (var img in ImageArray501)
                        img.GetComponent<SpriteRenderer>().sprite = null;
                }

                //注文画面の入力クリアは画面切替時に行う↓
                ChangeScreen(102);
                break;
            default:
                break;
        }
    }

    //************************************************************************************
    //<summary>
    //電源OFFの時
    //</summary>
    public void PowerOff()
    {
        //ロック画面
        UserNo101 = "";
        foreach (var img in ImageArray101)
            img.GetComponent<SpriteRenderer>().sprite = null;
              
        //曲番号
        UserNo201 = "";
        //数字画像クリア
        foreach (var img in ImageArray201)
            img.GetComponent<SpriteRenderer>().sprite = null;

        //歌手名
        UserNo501 = "";
        //文字画像クリア
        foreach (var img in ImageArray501)
            img.GetComponent<SpriteRenderer>().sprite = null;

        //注文
        UserNo401 = "0000";
        foreach (var img in ImageArray401)
            img.GetComponent<SpriteRenderer>().sprite = null;
    }
}