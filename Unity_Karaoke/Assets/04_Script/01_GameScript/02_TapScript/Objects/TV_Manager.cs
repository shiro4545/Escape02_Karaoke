using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TV_Manager : MonoBehaviour
{
    public static TV_Manager Instance { get; set; }

    public GameObject TVScreen;
    //public Rimocon_Judge Rimocon;
    public Machine_Judge Machine;

    //テレビ画面名の頭文字(デンモクからの曲予約時)
    private string Initial;
    //曲タイトル
    private string SongTitle;

    //曲再生中フラグ
    public bool isPlaySong = false;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        ChangeTVScreen("a01");
    }

    //<summary>
    //TV画面の変更
    //</summary>
    //<param>画面文字列</param>
    public void ChangeTVScreen(string ScreenStr)
    {
        TVScreen.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Images/11_TV/" + ScreenStr);
    }


    //<summary>
    //曲開始
    //</summary>
    //<param>デンモク画面No</param>
    public void StartSong(int No)
    {
        isPlaySong = true;

        switch(No)
        {
            //星の力
            case 211:
                Initial = "e";
                SongTitle = "StarPower";
                break;
            //1歩1歩
            case 314:
                Initial = "f";
                SongTitle = "StepStep";
                break;
            //Lovers
            case 515:
                Initial = "g";
                SongTitle = "Lovers";
                break;
            default:
                break;
        }
        //予約完了画面
        ChangeTVScreen(Initial + "01");
        if(!Machine.isAct)
            Invoke(nameof(act1), 7f);
    }
    public void act1()
    {
        //曲タイトル+アーティスト名
        ChangeTVScreen(Initial + "02");
        //BGMスタート
        AudioManager.Instance.SoundSong(SongTitle);

        if (!Machine.isAct)
            Invoke(nameof(act2), 6f);
    }
    public void act2()
    {
        //歌詞1
        ChangeTVScreen(Initial + "03");
        if (!Machine.isAct)
            Invoke(nameof(act3), 7f);
    }
    public void act3()
    {
        //歌詞2
        ChangeTVScreen(Initial + "04");
        if (!Machine.isAct)
            Invoke(nameof(act4), 7f);
    }
    public void act4()
    {
        //真っ白画面
        ChangeTVScreen("a00");
        if (!Machine.isAct)
            Invoke(nameof(act5), 2.5f);
    }
    public void act5()
    {
        //採点画面
        ChangeTVScreen(Initial + "05");
        if (!Machine.isAct)
            Invoke(nameof(act6), 10f);
    }
    public void act6()
    {
        //デフォルト画面
        if(Machine.isClear)
                ChangeTVScreen("a03");
        //else if(Rimokon.isClear)
        //    ChangeTVScreen("a02");
        else
            ChangeTVScreen("a01");
        isPlaySong = false;
    }


}
