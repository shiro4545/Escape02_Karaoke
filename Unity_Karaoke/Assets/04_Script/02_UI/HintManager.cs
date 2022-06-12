using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HintManager : MonoBehaviour
{
    //ゲーム進捗
    private int Progress;
    //現進捗のヒントフラグ (
    private string HintFlg;
    //現進捗で動画を視聴した回数
    private int CountWatch;

    //スタートリセットオブジェクト
    public StartResetManager StartResetClass;
    //広告オブジェクト
    public GoogleAds GoogleAds;

    //ヒント親オブジェクト
    public GameObject Hint3;
    public GameObject Hint4;

    //ヒントテキスト
    public Text HintTxt1;
    public Text HintTxt2;
    public Text HintTxt3;
    public Text HintTxt4;

    //ヒントテキストオブジェクト
    public GameObject TxtObject2;
    public GameObject TxtObject3;
    public GameObject TxtObject4;

    //動画説明&ボタンの親オブジェクト
    public GameObject Ads2;
    public GameObject Ads3;
    public GameObject Ads4;


    //「動画視聴しますか」ボタン
    public GameObject BtnAds2;
    public GameObject BtnAds3;
    public GameObject BtnAds4;



    //<summary>
    //ヒントテキスト(進捗,ヒント)
    //</summary>
    private Dictionary<int, string[]> HintDB = new Dictionary<int, string[]>
     {
       {
         1, //手洗い下の星の謎
         new string[]{
           //ヒント1
           "手洗い下の棚に描かれた星と同じものが、トイレのタンクの横側にもあるようだ",
           //ヒント2
           "トイレの便座が開いている時と閉じている時で、見える星の数が違うようだ。手洗い下の棚に書かれた黒の2本線と何か関係があるみたいだ。",
           //ヒント3
           "33333333333333333",
           //ヒント4
           "4444444444444"
         }
       },
       {
         2, //手洗い下の星の謎
         new string[]{
           //ヒント1
           "手洗い下の棚に描かれた星と同じものが、トイレのタンクの横側にもあるようだ",
           //ヒント2
           "トイレの便座が開いている時と閉じている時で、見える星の数が違うようだ。手洗い下の棚に書かれた黒の2本線と何か関係があるみたいだ。",
           //ヒント3
           "33333333333333333",
         }
       },
     };

    // Start is called before the first frame update
    void Start()
    {
        BtnAds2.GetComponent<Button>().onClick.AddListener(() =>
        {
            GoogleAds.ShowReawrd();
        });
        BtnAds3.GetComponent<Button>().onClick.AddListener(() =>
        {
            GoogleAds.ShowReawrd();
        });
        BtnAds4.GetComponent<Button>().onClick.AddListener(() =>
        {
            GoogleAds.ShowReawrd();
        });

        //テキスト表示状態を初期化
        ResetHint();
    }

        //<summary>
        //ヒントをヒントテキストにセットし、動画視聴数に応じてテキスト表示・非表示する
        //</summary>
        public void SetHint()
    {
        //ゲーム進捗を取得
        Progress = StartResetClass.CheckProgress();

        //現進捗のヒントフラグを取得
        HintFlg = SaveLoadSystem.Instance.gameData.HintFlgArray[Progress];

        //ヒント1,2,3,4にテキストをセットする
        HintTxt1.text = HintDB[Progress][0];
        HintTxt2.text = HintDB[Progress][1];
        HintTxt3.text = HintDB[Progress][2];
        HintTxt4.text = HintDB[Progress].Length == 4 ? HintDB[Progress][3] : "";

        //動画視聴した数を取得
        CountWatch = 3;
        for (int i = 1; i < HintDB[Progress].Length; i++)
        {
            if (HintFlg.Substring(i, 1) == "0")
            {
                CountWatch = i - 1;
                break;
            }
        }

        //動画視聴数に合わせてオブジェクトを表示・非表示
        ShowHint();
        
    }


    //<summary>
    //動画視聴後のヒント表示・非表示
    //</summary>
    public void AfterWatch()
    {
        CountWatch++;
        ShowHint();

        //セーブデータのフラグ更新
        string NewHintFlg = "0000";

        if (CountWatch == 1)
            NewHintFlg = "0100";
        else if (CountWatch == 2)
            NewHintFlg = "0110";
        else if (CountWatch == 3)
            NewHintFlg = "0111";

        SaveLoadSystem.Instance.gameData.HintFlgArray[Progress] = NewHintFlg;
        SaveLoadSystem.Instance.Save();

    }



    //<summary>
    //ヒントをヒントテキストにセットし、動画視聴数に応じてテキスト表示・非表示する
    //</summary>
    public void ShowHint()
    {
        //ヒント2の動画を視聴済みの場合
        if (CountWatch >= 1)
        {
            Ads2.SetActive(false);
            TxtObject2.SetActive(true);
            Hint3.SetActive(true);
        }
        //ヒント3の動画を視聴済みの場合
        if (CountWatch >= 2)
        {
            Ads3.SetActive(false);
            TxtObject3.SetActive(true);

            //ヒント4まである場合
            if (HintDB[Progress].Length == 4)
                Hint4.SetActive(true);
        }
        //ヒント4の動画を視聴済みの場合
        if (CountWatch >= 3)
        {
            Ads4.SetActive(false);
            TxtObject4.SetActive(true);
        }
    }


    //<summary>
    //ヒント画面を閉じるときにヒントを初期状態にリセットする
    //</summary>
    public void ResetHint()
    {
        Ads2.SetActive(true);
        Ads3.SetActive(true);
        Ads4.SetActive(true);

        TxtObject2.SetActive(false);
        TxtObject3.SetActive(false);
        TxtObject4.SetActive(false);

        Hint3.SetActive(false);
        Hint4.SetActive(false);
    }
}
