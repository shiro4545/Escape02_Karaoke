using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Text.RegularExpressions;

public class ItemManager : MonoBehaviour
{
    public static ItemManager Instance { get; private set; }

    //選択中のアイテム名
    public string selectItem;

    public GameObject[] getItemsArray;
    public GameObject ItemPanel;

    //特定アイテムでの透明ボタン
    public GameObject BtnKeyBox;
    public GameObject BtnStrawPackage;


    // Start is called before the first frame update
    void Start()
    {
      Instance = this;

        foreach(var obj in getItemsArray)
        {
          obj.gameObject.GetComponent<Button>().onClick.AddListener(() =>
          {
            AudioManager.Instance.SoundSE("TapUIBtn");
            onTapItemImage(obj);
          });
        }

        //アイテム拡大画面でタップする場合

        //袋入りストローからストローを取り出す
        BtnStrawPackage.GetComponent<Button>().onClick.AddListener(() =>
        {
            GetStraw();
        });
        //鍵入り箱をひっくり返す/ドライバーで開ける
        BtnKeyBox.GetComponent<Button>().onClick.AddListener(() =>
        {
            RotateKeyBox();
        });


    }

    //<summary>
    //アイテム取得
    //</summary>
    //<param>アイテム名</param>
    public void getItem(string itemName)
    {
        AudioManager.Instance.SoundSE("ItemGet");

        for (int i = 0; i < getItemsArray.Length; i++)
        {
            if(getItemsArray[i].gameObject.GetComponent<Image>().sprite == null)
            {
                getItemsArray[i].gameObject.GetComponent<Image>().sprite  = Resources.Load<Sprite>("Images/01_Items/" + itemName);
                getItemsArray[i].SetActive(true);
                break;
            }
        }

        switch(itemName)
        {
            case "Hanger":
                SaveLoadSystem.Instance.gameData.isGetHanger = true;
                break;
            case "Tambarin_Sankaku":
                SaveLoadSystem.Instance.gameData.isGetTanbarine_Sankaku = true;
                break;
            case "Tambarin_Shikaku":
                SaveLoadSystem.Instance.gameData.isGetTanbarine_Shikaku = true;
                break;
            case "KeyBox":
                SaveLoadSystem.Instance.gameData.isGetKeyBox = true;
                break;
            case "Driver":
                SaveLoadSystem.Instance.gameData.isGetDriver = true;
                break;
            case "Piece1":
                SaveLoadSystem.Instance.gameData.isGetPiece1 = true;
                break;
            case "Piece2":
                SaveLoadSystem.Instance.gameData.isGetPiece2 = true;
                break;
            case "Piece3":
                SaveLoadSystem.Instance.gameData.isGetPiece3 = true;
                break;
            case "Piece4":
                SaveLoadSystem.Instance.gameData.isGetPiece4 = true;
                break;
            case "Piece5":
                SaveLoadSystem.Instance.gameData.isGetPiece5 = true;
                break;
            case "Key3":
                SaveLoadSystem.Instance.gameData.isGetKey3 = true;
                break;
            default:
                break;
        }

        SaveLoadSystem.Instance.gameData.getItems += itemName + ";";
        SaveLoadSystem.Instance.Save();
    }

    //<summary>
    //取得アイテムのロード
    //</summary>
    //<param>アイテム名</param>
    public void loadItem(string itemName)
    {
      for(int i = 0; i < getItemsArray.Length; i++)
      {
        if(getItemsArray[i].gameObject.GetComponent<Image>().sprite == null)
        {
          getItemsArray[i].gameObject.GetComponent<Image>().sprite  = Resources.Load<Sprite>("Images/01_Items/" + itemName);
          getItemsArray[i].SetActive(true);
          break;
        }
      }
    }

    //<summary>
    //アイテム選択
    //</summary>
    //<param>アイテムオブジェクト</param>
    private void onTapItemImage(GameObject item)
    {
      //選択済みの場合
      if(item.gameObject.GetComponent<Outline>().enabled)
      {
        showItem(item);
        return;
      }

      //未選択の場合
      foreach(var obj in getItemsArray)
      {
        if(item == obj)
        {
          obj.gameObject.GetComponent<Outline>().enabled = true;
          selectItem = obj.gameObject.GetComponent<Image>().sprite.name;
        }
        else
        {
          obj.gameObject.GetComponent<Outline>().enabled = false;
        }
      }
    }

    //<summary>
    //アイテム拡大画面の表示
    //</summary>
    //<param>アイテムオブジェクト</param>
    private void showItem(GameObject item)
    {
        ItemPanel.SetActive(true);
        ItemPanel.transform.Find("ItemImage").gameObject.GetComponent<Image>().sprite = item.gameObject.GetComponent<Image>().sprite;
        CameraManager.Instance.ButtonLeft.SetActive(false);
        CameraManager.Instance.ButtonRight.SetActive(false);
        CameraManager.Instance.ButtonBack.SetActive(true);

        //透明ボタンを非表示
        BtnKeyBox.SetActive(false);
        BtnStrawPackage.SetActive(false);


        //箱入りの鍵のん場合に透明ボタン表示
        if (ItemPanel.transform.Find("ItemImage").gameObject.GetComponent<Image>().sprite.name == "KeyBox" ||
            ItemPanel.transform.Find("ItemImage").gameObject.GetComponent<Image>().sprite.name == "KeyBox_Back")
            BtnKeyBox.SetActive(true);

        //袋入りストローの場合に透明ボタン表示
        if (ItemPanel.transform.Find("ItemImage").gameObject.GetComponent<Image>().sprite.name == "StrawPackage" )
            BtnStrawPackage.SetActive(true);

    }

    //<summary>
    //アイテム使用時
    //</summary>
    //<param>アイテム名</param>
    public void useItem()
    {
      for(int i = 0; i < getItemsArray.Length; i++)
      {
        if(getItemsArray[i].gameObject.GetComponent<Image>().sprite.name == selectItem)
        {
          //枠線を非表示に
          getItemsArray[i].gameObject.GetComponent<Outline>().enabled = false;

          //持ち物数がMaの時
          if(i == getItemsArray.Length - 1)
          {
            getItemsArray[i].gameObject.GetComponent<Image>().sprite = null;
            getItemsArray[i].SetActive(false);
            break;
          }

          //それ以降のアイテム画像を左に詰める
          for(int j = i + 1; j < getItemsArray.Length; j++)
          {
            if(getItemsArray[j].gameObject.GetComponent<Image>().sprite == null)
            {
              getItemsArray[j - 1].gameObject.GetComponent<Image>().sprite = null;
              getItemsArray[j - 1].SetActive(false);
              break;
            }
            else if(j == getItemsArray.Length - 1)
            {
              getItemsArray[j - 1].gameObject.GetComponent<Image>().sprite = getItemsArray[j].gameObject.GetComponent<Image>().sprite;
              getItemsArray[j].gameObject.GetComponent<Image>().sprite = null;
              getItemsArray[j].SetActive(false);
              break;
            }
            else
            {
              getItemsArray[j - 1].gameObject.GetComponent<Image>().sprite = getItemsArray[j].gameObject.GetComponent<Image>().sprite;
            }
          }
          break;
        }
      }
      //セーブデータ
      SaveLoadSystem.Instance.gameData.getItems = SaveLoadSystem.Instance.gameData.getItems.Replace(selectItem + ";","");
      
      selectItem = "";
      SaveLoadSystem.Instance.Save();
    }

    //<summary>
    //鍵入り箱を裏返す/ドライバーで開ける
    //</summary>
    //<param></param>
    private void RotateKeyBox()
    {
        if (selectItem == "Driver")
        {
            //ドライバーで開ける場合
            AudioManager.Instance.SoundSE("Clear");
            //拡大画面をKey2に変える
            ItemPanel.transform.Find("ItemImage").gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/01_Items/Key2");
            BtnKeyBox.SetActive(false);

            //ドライバーを使う
            useItem();

            //ヘッダーのアイテム画像をKey2に変える
            foreach (var obj in getItemsArray)
            {
                if (obj.gameObject.GetComponent<Image>().sprite.name == "KeyBox" || obj.gameObject.GetComponent<Image>().sprite.name == "KeyBox_Back")
                {
                    //アイテム画像をKey2に変える
                    obj.gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/01_Items/Key2");
                    //Key2を選枠線を表示する
                    obj.gameObject.GetComponent<Outline>().enabled = true;
                    selectItem = "Key2";
                    break;
                }
            }

            SaveLoadSystem.Instance.gameData.isGetKey2 = true;
            SaveLoadSystem.Instance.gameData.getItems = SaveLoadSystem.Instance.gameData.getItems.Replace("KeyBox", "Key2");
            SaveLoadSystem.Instance.Save();
        }
        else if (ItemPanel.transform.Find("ItemImage").gameObject.GetComponent<Image>().sprite.name == "KeyBox")
        {
            //拡大画面が表だったら裏返す場合
            AudioManager.Instance.SoundSE("ItemGet");
            ItemPanel.transform.Find("ItemImage").gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/01_Items/KeyBox_Back");
            
        }
    }


    //<summary>
    //袋入りストローからストローを取り出す
    //</summary>
    //<param></param>
    private void GetStraw()
    {
        AudioManager.Instance.SoundSE("Clear");
        //拡大画面をStrawに変える
        ItemPanel.transform.Find("ItemImage").gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/01_Items/Straw");
        BtnStrawPackage.SetActive(false);

        //ヘッダーのアイテム画像をStrawに変える
        foreach (var obj in getItemsArray)
        {
            if (obj.gameObject.GetComponent<Image>().sprite.name == "StrawPackage" )
            {
                //アイテム画像をKey2に変える
                obj.gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/01_Items/Straw");
                //Strawの枠線を表示する
                obj.gameObject.GetComponent<Outline>().enabled = true;
                selectItem = "Straw";
                break;
            }
        }

        //セーブ
        SaveLoadSystem.Instance.gameData.getItems = SaveLoadSystem.Instance.gameData.getItems.Replace("StrawPackage", "Straw");
        SaveLoadSystem.Instance.Save();

    }

}
