using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    //Panelオブジェクト
    public GameObject TitlePanel;
    public GameObject GamePanel;
    public GameObject MenuPanel;
    public GameObject HintPanel;
    public GameObject ClearPanel;
    public GameObject ItemPanel;
    public GameObject OtherAppPanel;

    //GamePanel内
    public GameObject GameHeader;
    public GameObject GameFooter;

    //ボタンオブジェクト
    public GameObject BtnTitle_Start;
    public GameObject BtnTitle_Continue;
    public GameObject BtnTitle_OtherApp;
    public GameObject BtnHeader_Menu;
    public GameObject BtnMenu_Hint;
    public GameObject BtnMenu_Title;
    public GameObject BtnMenu_Back;

    //ヒントパネル内
    public GameObject SclHint;
    public GameObject BtnHint_Back;

    //クリアパネル内
    public GameObject BtnClear_OtherApp;

    //他のアプリパネル内
    public GameObject TxtOtherAppTop;
    public GameObject BtnOtherApp_Title;
    public GameObject SclOtherApp;


    public GameObject ItemImage;

    //ヒントオブジェクト
    public HintManager Hint;
    //ゲームスタートオブジェクト
    public StartResetManager StartReset;

    // Start is called before the first frame update
    void Start()
    {
        TitlePanel.SetActive(true);
        GamePanel.SetActive(false);
        //GamePanel.SetActive(true);

        //各パネルを画面サイズごとで変動させる
        GamePanel.GetComponent<RectTransform>().sizeDelta = GetComponent<RectTransform>().sizeDelta;
        ClearPanel.GetComponent<RectTransform>().sizeDelta = GetComponent<RectTransform>().sizeDelta;

        Debug.Log("w:" + Screen.width);
        Debug.Log("h:" + Screen.height);
        Debug.Log("Safe:" + Screen.safeArea);
        //Debug.Log("Device:" + Application.platform);

        if (Application.platform == RuntimePlatform.Android) //Androidの場合
        {
            //ヘッダーフッター
            GameHeader.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -185 - (Screen.height - Screen.safeArea.height));
            GameFooter.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 250);
            //メニューパネル
            MenuPanel.GetComponent<RectTransform>().sizeDelta = new Vector2(0, Screen.height);
            //ヒントパネル
            HintPanel.GetComponent<RectTransform>().sizeDelta = new Vector2(0, -200);
            //他のアプリパネル
            OtherAppPanel.GetComponent<RectTransform>().sizeDelta = new Vector2(0, -200);
            //アイテムパネル
            ItemPanel.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 940);
        }
        else //iOSの場合
        {
            if (Screen.width <= 750) //iPhone8,SE
            {
                //ヘッダーフッター
                GameHeader.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -185);
                GameFooter.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 250);
                //メニューパネル
                MenuPanel.GetComponent<RectTransform>().sizeDelta = new Vector2(0, -200);
                //ヒントパネル
                HintPanel.GetComponent<RectTransform>().sizeDelta = new Vector2(0, -200);
                //他のアプリパネル
                OtherAppPanel.GetComponent<RectTransform>().sizeDelta = new Vector2(0, -100);
                //アイテムパネル
                ItemPanel.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 940);
            }
            //else if (Screen.width == 1242 && Screen.height == 2208)//iPhone7plus,8plus
            else if (Screen.width == 1080 && Screen.height == 1920)//iPhone7plus,8plus
            {
                //ヘッダーフッター
                GameHeader.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -182);
                GameFooter.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 240);
                //メニューパネル
                MenuPanel.GetComponent<RectTransform>().sizeDelta = new Vector2(0, -200);
                //ヒントパネル
                HintPanel.GetComponent<RectTransform>().sizeDelta = new Vector2(0, -200);
                //他のアプリパネル
                OtherAppPanel.GetComponent<RectTransform>().sizeDelta = new Vector2(0, -200);
                //アイテムパネル
                ItemPanel.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 900);
            }
            else if (Screen.width <= 1300)//iPhone10以上
            {
                //ヘッダーフッター
                GameHeader.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -295);
                GameFooter.GetComponent<RectTransform>().anchoredPosition = new Vector2(0,300);
            }
            else //iPad
            {
                //タイトルパネル
                BtnTitle_Start.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -100);
                BtnTitle_Continue.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -250);
                BtnTitle_OtherApp.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -400);
                //ヘッダーフッター
                GameHeader.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -150);
                GameFooter.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 200);
                //メニューパネル
                MenuPanel.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 920);
                BtnMenu_Hint.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -80);
                BtnMenu_Title.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -220);
                BtnMenu_Back.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -370);
                //ヒントパネル
                HintPanel.GetComponent<RectTransform>().sizeDelta = new Vector2(0,-140);
                //アイテムパネル
                ItemPanel.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 680);
                ItemImage.GetComponent<RectTransform>().sizeDelta = new Vector2(500, 500);
                //クリアパネル
                BtnClear_OtherApp.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -350);
                //他のアプリパネル
                OtherAppPanel.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 0);
            }
        }

        //ボタン処理を登録

        //タイトル画面の「はじめから」
        BtnTitle_Start.GetComponent<Button>().onClick.AddListener(() =>
        {
          OnTapStart();
        });
        //タイトル画面の「続きから」
        BtnTitle_Continue.GetComponent<Button>().onClick.AddListener(() =>
        {
            OnTapContinue();
        });
        //タイトル画面の「他の脱出ゲーム」
        BtnTitle_OtherApp.GetComponent<Button>().onClick.AddListener(() =>
        {
            OnTapOtherApp();
        });
        //ヘッダー画面の「MENU」
        BtnHeader_Menu.GetComponent<Button>().onClick.AddListener(() =>
        {
            OnTapMenu();
        });
        //メニュー画面の「ヒント」
        BtnMenu_Hint.GetComponent<Button>().onClick.AddListener(() =>
        {
            OnTapHint();
        });
        //メニュー画面の「タイトルへ」
        BtnMenu_Title.GetComponent<Button>().onClick.AddListener(() =>
        {
            OnTapTitle();
        });
        //メニュー画面の「ゲームに戻る」
        BtnMenu_Back.GetComponent<Button>().onClick.AddListener(() =>
        {
            OnTapMenuBack();
        });
        //ヒント画面の「ゲームに戻る」
        BtnHint_Back.GetComponent<Button>().onClick.AddListener(() =>
        {
            OnTapHintBack();
        });
        //クリア画面の「タイトルへ」
        BtnClear_OtherApp.GetComponent<Button>().onClick.AddListener(() =>
        {
            OnTapClearOtherApp();
        });
        //他の脱出ゲーム画面の「タイトルへ」
        BtnOtherApp_Title.GetComponent<Button>().onClick.AddListener(() =>
        {
            OnTapOtherAppTitle();
        });

        bool isExistFile = SaveLoadSystem.Instance.checkFileExist();
        if (!isExistFile)
            BtnTitle_Continue.GetComponent<Button>().interactable = false;
    }

    //タイトル画面の「はじめから」ボタン
    private void OnTapStart()
    {
        AudioManager.Instance.SoundSE("GameStart");
        SaveLoadSystem.Instance.GameStart();
        Invoke(nameof(HidePanel), 1f);
    }

    //タイトル画面の「続きから」ボタン
    private void OnTapContinue()
    {
        AudioManager.Instance.SoundSE("GameStart");
        StartReset.GameContinue();
        Invoke(nameof(HidePanel), 1f);
    }

    //タイトル画面の「他の脱出ゲーム」ボタン
    private void OnTapOtherApp()
    {
        AudioManager.Instance.SoundSE("TapUIBtn");
        TitlePanel.SetActive(false);
        OtherAppPanel.SetActive(true);
    }

    //ゲーム開始時のパネル表示非表示
    private void HidePanel()
    {
        TitlePanel.SetActive(false);
        GamePanel.SetActive(true);
        CameraManager.Instance.ChangeCameraPosition("RoomStart");
    }

    //ヘッダーの「MENU」ボタン
    private void OnTapMenu()
    {
        AudioManager.Instance.SoundSE("TapUIBtn");
        MenuPanel.SetActive(true);
        //バナー広告表示(長方形)
        // GoogleAds.RequestSquareBanner();
    }
    //メニュー画面の「ヒント」ボタン
    private void OnTapHint()
    {
        AudioManager.Instance.SoundSE("TapUIBtn");
        MenuPanel.SetActive(false);
        HintPanel.SetActive(true);
        //進捗に応じてヒントを表示する
        Hint.SetHint();

    }
    //メニュー画面の「タイトルへ」ボタン
    private void OnTapTitle()
    {
        AudioManager.Instance.SoundSE("TapUIBtn");
        TitlePanel.SetActive(true);
        GamePanel.SetActive(false);
        MenuPanel.SetActive(false);
        CameraManager.Instance.ChangeCameraPosition("Title");

        // GoogleAds.unRequestSquareBanner();
        //シーンのリセット
        Invoke(nameof(LoadScene), 0.5f);
        //BGMの再生
        Invoke(nameof(soundBGM), 1.5f);
    }
    private void LoadScene()
    {
        SceneManager.LoadScene(0);
    }
    private void soundBGM()
    {
        AudioManager.Instance.SoungBGM();
    }

    //メニュー画面の「ゲームに戻る」ボタン
    private void OnTapMenuBack()
    {
        AudioManager.Instance.SoundSE("TapUIBtn");
        MenuPanel.SetActive(false);
        //バナー広告非表表示(長方形)
        // GoogleAds.unRequestSquareBanner();
    }
    //ヒント画面の「ゲームに戻る」ボタン
    private void OnTapHintBack()
    {
        AudioManager.Instance.SoundSE("TapUIBtn");
        HintPanel.SetActive(false);
        Hint.ResetHint();
        //バナー広告非表表示(長方形)
        // GoogleAds.unRequestSquareBanner();
    }

    //クリア画面の「タイトルに戻る」ボタン
    private void OnTapClearOtherApp()
    {
        AudioManager.Instance.SoundSE("TapUIBtn");
        OtherAppPanel.SetActive(true);
        ClearPanel.SetActive(false);
        GamePanel.SetActive(false);
        //シーンのリセット
        SceneManager.LoadScene(0);
        //BGMの再生
        Invoke(nameof(soundBGM), 1.5f);
    }

    //他の脱出ゲーム画面の「タイトルへ」ボタン
    private void OnTapOtherAppTitle()
    {
        AudioManager.Instance.SoundSE("TapUIBtn");
        OtherAppPanel.SetActive(false);
        TitlePanel.SetActive(true);

    }

}
