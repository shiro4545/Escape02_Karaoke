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

    //GamePanel内
    public GameObject GameHeader;
    public GameObject GameFooter;

    //ボタンオブジェクト
    public GameObject BtnTitle_Start;
    public GameObject BtnTitle_Continue;
    public GameObject BtnHeader_Menu;
    public GameObject BtnMenu_Hint;
    public GameObject BtnMenu_Title;
    public GameObject BtnMenu_Back;

    public Text Hint1;
    public Text Hint2;
    public GameObject BtnHint_Back;
    public GameObject Hint1_txt;
    public GameObject Hint2_txt;
    public GameObject Hint2_ads;
    public GameObject BtnHint_Hint2;
    public GameObject BtnClear_Title;

    public GameObject ItemImage;

    //広告オブジェクト
    public GoogleAds GoogleAds;
    //ヒントオブジェクト
    public HintManager Hint;
    //ゲームスタートオブジェクト
    public StartResetManager StartReset;

    // Start is called before the first frame update
    void Start()
    {
        //TitlePanel.SetActive(true);
        //GamePanel.SetActive(false);
        //GamePanel.SetActive(true);

        //各パネルを画面サイズごとで変動させる
        GamePanel.GetComponent<RectTransform>().sizeDelta = GetComponent<RectTransform>().sizeDelta;
        ClearPanel.GetComponent<RectTransform>().sizeDelta = GetComponent<RectTransform>().sizeDelta;

        //Debug.Log("w:" + Screen.width);
        //Debug.Log("h:" + Screen.height);
        //Debug.Log("Device:" + Application.platform);
        //Debug.Log("Safe:" + Screen.safeArea);

        if (Application.platform == RuntimePlatform.Android) //Androidの場合
        {
            //ヘッダーフッター
            GameHeader.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -185 - (Screen.height - Screen.safeArea.height));
            GameFooter.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 250);
            //メニューパネル
            MenuPanel.GetComponent<RectTransform>().sizeDelta = new Vector2(0, Screen.height);
            //ヒントパネル
            HintPanel.GetComponent<RectTransform>().sizeDelta = new Vector2(0, Screen.height);
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
                //アイテムパネル
                ItemPanel.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 940);
            }
            else if (Screen.width == 1242 && Screen.height == 2208)//iPhone7plus,8plus
            //else if (Screen.width == 1080 && Screen.height == 1920)//iPhone7plus,8plus
                    {
                //ヘッダーフッター
                GameHeader.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -182);
                GameFooter.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 240);
                //メニューパネル
                MenuPanel.GetComponent<RectTransform>().sizeDelta = new Vector2(0, -200);
                //ヒントパネル
                HintPanel.GetComponent<RectTransform>().sizeDelta = new Vector2(0, -200);
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
                TitlePanel.gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/UI/Title_wide");
                BtnTitle_Start.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -200);
                BtnTitle_Continue.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -350);
                //ヘッダーフッター
                GameHeader.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -150);
                GameFooter.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 200);
                //メニューパネル
                MenuPanel.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 920);
                BtnMenu_Hint.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -80);
                BtnMenu_Title.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -220);
                BtnMenu_Back.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -370);
                //ヒントパネル
                HintPanel.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 920);
                Hint1.fontSize = 44;
                Hint1.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 280);
                Hint1_txt.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 230);
                Hint2.fontSize = 44;
                Hint2.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 100);
                Hint2_ads.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 50);
                Hint2_txt.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 50);
                BtnHint_Hint2.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -120);
                BtnHint_Hint2.GetComponent<RectTransform>().sizeDelta = new Vector2(250, 100);
                BtnHint_Back.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -370);
                //アイテムパネル
                ItemPanel.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 680);
                ItemImage.GetComponent<RectTransform>().sizeDelta = new Vector2(500, 500);
                //クリアパネル
                BtnClear_Title.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -350);
            }
        }

        //ボタン処理を登録

        //タイトル画面の「はじめから」
        BtnTitle_Start.GetComponent<Button>().onClick.AddListener(() =>
      {
          onTapStart();
      });
        //タイトル画面の「続きから」
        BtnTitle_Continue.GetComponent<Button>().onClick.AddListener(() =>
        {
            onTapContinue();
        });
        //ヘッダー画面の「MENU」
        BtnHeader_Menu.GetComponent<Button>().onClick.AddListener(() =>
        {
            onTapMenu();
        });
        //メニュー画面の「ヒント」
        BtnMenu_Hint.GetComponent<Button>().onClick.AddListener(() =>
        {
            onTapHint();
        });
        //メニュー画面の「タイトルへ」
        BtnMenu_Title.GetComponent<Button>().onClick.AddListener(() =>
        {
            onTapTitle();
        });
        //メニュー画面の「ゲームに戻る」
        BtnMenu_Back.GetComponent<Button>().onClick.AddListener(() =>
        {
            onTapMenuBack();
        });
        //ヒント画面の「動画広告を視聴する」
        BtnHint_Hint2.GetComponent<Button>().onClick.AddListener(() =>
        {
            GoogleAds.ShowReawrd();
        });
        //ヒント画面の「ゲームに戻る」
        BtnHint_Back.GetComponent<Button>().onClick.AddListener(() =>
        {
            onTapHintBack();
        });
        //クリア画面の「タイトルへ」
        BtnClear_Title.GetComponent<Button>().onClick.AddListener(() =>
        {
            onTapClearTitle();
        });

        bool isExistFile = SaveLoadSystem.Instance.checkFileExist();
        if (!isExistFile)
            BtnTitle_Continue.GetComponent<Button>().interactable = false;
    }

    //タイトル画面の「はじめから」ボタン
    private void onTapStart()
    {
        AudioManager.Instance.SoundSE("GameStart");
        SaveLoadSystem.Instance.GameStart();
        Invoke(nameof(hidePanel), 1f);
    }

    //タイトル画面の「続きから」ボタン
    private void onTapContinue()
    {
        AudioManager.Instance.SoundSE("GameStart");
        StartReset.GameContinue();
        Invoke(nameof(hidePanel), 1f);
    }

    //
    private void hidePanel()
    {
        TitlePanel.SetActive(false);
        GamePanel.SetActive(true);
    }

    //ヘッダーの「MENU」ボタン
    private void onTapMenu()
    {
        AudioManager.Instance.SoundSE("TapUIBtn");
        MenuPanel.SetActive(true);
        //バナー広告表示(長方形)
        // GoogleAds.RequestSquareBanner();
    }
    //メニュー画面の「ヒント」ボタン
    private void onTapHint()
    {
        AudioManager.Instance.SoundSE("TapUIBtn");
        MenuPanel.SetActive(false);
        HintPanel.SetActive(true);

        bool isWatch = Hint.setHint();
        if (isWatch)
        {
            Hint2_txt.SetActive(true);
            Hint2_ads.SetActive(false);
        }
        else
        {
            Hint2_txt.SetActive(false);
            Hint2_ads.SetActive(true);
        }

    }
    //メニュー画面の「タイトルへ」ボタン
    private void onTapTitle()
    {
        AudioManager.Instance.SoundSE("TapUIBtn");
        TitlePanel.SetActive(true);
        GamePanel.SetActive(false);
        MenuPanel.SetActive(false);

        // GoogleAds.unRequestSquareBanner();
        //シーンのリセット
        Invoke(nameof(loadScene), 0.5f);
        //BGMの再生
        Invoke(nameof(soundBGM), 1.5f);
    }
    private void loadScene()
    {
        SceneManager.LoadScene(0);
    }
    private void soundBGM()
    {
        AudioManager.Instance.SoungBGM();
    }

    //メニュー画面の「ゲームに戻る」ボタン
    private void onTapMenuBack()
    {
        AudioManager.Instance.SoundSE("TapUIBtn");
        MenuPanel.SetActive(false);
        //バナー広告非表表示(長方形)
        // GoogleAds.unRequestSquareBanner();
    }
    //ヒント画面の「ゲームに戻る」ボタン
    private void onTapHintBack()
    {
        AudioManager.Instance.SoundSE("TapUIBtn");
        HintPanel.SetActive(false);
        //バナー広告非表表示(長方形)
        // GoogleAds.unRequestSquareBanner();
    }

    //クリア画面の「タイトルに戻る」ボタン
    private void onTapClearTitle()
    {
        AudioManager.Instance.SoundSE("TapUIBtn");
        TitlePanel.SetActive(true);
        ClearPanel.SetActive(false);
        GamePanel.SetActive(false);
        //シーンのリセット
        SceneManager.LoadScene(0);
        //BGMの再生
        Invoke(nameof(soundBGM), 1.5f);
    }

}
