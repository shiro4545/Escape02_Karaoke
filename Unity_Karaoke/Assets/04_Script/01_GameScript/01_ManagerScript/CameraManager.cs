using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class CameraManager : MonoBehaviour
{
    public static CameraManager Instance { get; private set; }

    //<summary>現在のカメラの位置名</summary>
    public string CurrentPositionName { get; private set; }

    private bool isStart = false;

    //矢印ボタンオブジェクト
    public GameObject ButtonLeft;
    public GameObject ButtonRight;
    public GameObject ButtonBack;
    //非表示オブジェクトの配列
    public GameObject[] hideObjects;

    //<summary>
    //カメラの位置情報クラス
    //</summary>
    private class CameraPositionInfo
    {
        //<summary>カメラの位置</summary>
        public Vector3 Position { get; set; }
        //<summary>カメラの角度</summary>
        public Vector3 Rotate { get; set; }
        //<summary>ボタンの移動先</summary>
        public MoveNames MoveNames { get; set; }
        //<summary>非表示にするオブジェクト名</summary>
        public string[] hideObjectsName  { get; set; }
    }

    //<summary>
    //ボタンの移動先クラス
    //</summary>
    private class MoveNames
    {
        public string Left { get; set; }
        public string Right { get; set; }
        public string Back { get; set; }
    }

    //<summary>
    //全カメラ位置情報
    //</summary>
    private Dictionary<string, CameraPositionInfo> CameraPositionInfoes = new Dictionary<string, CameraPositionInfo>
    {
        {
            "RoomStart",//スタート位置
            new CameraPositionInfo
            {
                Position=new Vector3(23,7.36f,8.2f),
                Rotate =new Vector3(2.3f,-113,0),
                MoveNames=new MoveNames
                {
                    Left="RoomSofa",
                    Right="RoomTV",
                },
                //hideObjectsName = new string[]{"nazo3","tearai"}
            }
        },
        {
            "RoomSofa",//ソファ方向
            new CameraPositionInfo
            {
                Position=new Vector3(16,5.4f,-10.4f),
                Rotate =new Vector3(0,-48,0),
                MoveNames=new MoveNames
                {
                    Left="RoomDoor",
                    Right="RoomStart",
                }
            }
        },
        {
            "RoomTV",//TV方向
            new CameraPositionInfo
            {
                Position=new Vector3(-7.5f,5.3f,10),
                Rotate =new Vector3(0,-208,0),
                MoveNames=new MoveNames
                {
                    Left="RoomStart",
                    Right="RoomDoor",
                },
            }
        },
        {
            "RoomDoor",//ドア方向
            new CameraPositionInfo
            {
                Position=new Vector3(-8.25f,5.4f,-7),
                Rotate =new Vector3(0,76.5f,0),
                MoveNames=new MoveNames
                {
                    Left="RoomTV",
                    Right="RoomSofa",
                }
            }
        },
        {
            "Door",//ドア
            new CameraPositionInfo
            {
                Position=new Vector3(7,6,-3.5f),
                Rotate =new Vector3(3,90,0),
                MoveNames=new MoveNames
                {
                    Back="RoomDoor",
                }
            }
        },
        {
            "Denmoku",//デンモク
            new CameraPositionInfo
            {
                Position=new Vector3(-1.8f,5.7f,2.9f),
                Rotate =new Vector3(65,180,0),
                MoveNames=new MoveNames
                {
                    Back="RoomTV",
                }
            }
        },
        {
            "BlueBox",//青箱
            new CameraPositionInfo
            {
                Position=new Vector3(10.4f,5.4f,5.7f),
                Rotate =new Vector3(30,180,0),
                MoveNames=new MoveNames
                {
                    Back="RoomStart",
                }
            }
        },
        {
            "BlueBoxBtn",//青箱ボタン
            new CameraPositionInfo
            {
                Position=new Vector3(10.33f,3.4f,4f),
                Rotate =new Vector3(12,180,0),
                MoveNames=new MoveNames
                {
                    Back="BlueBox",
                }
            }
        },
    };

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        ChangeCameraPosition("RoomStart");

        ButtonLeft.GetComponent<Button>().onClick.AddListener(() =>
        {
            AudioManager.Instance.SoundSE("TapUIBtn");
            ChangeCameraPosition(CameraPositionInfoes[CurrentPositionName].MoveNames.Left);
        });
        ButtonRight.GetComponent<Button>().onClick.AddListener(() =>
        {
            AudioManager.Instance.SoundSE("TapUIBtn");
            ChangeCameraPosition(CameraPositionInfoes[CurrentPositionName].MoveNames.Right);
        });
        ButtonBack.GetComponent<Button>().onClick.AddListener(() =>
        {
            AudioManager.Instance.SoundSE("TapUIBtn");
            ChangeCameraPosition(CameraPositionInfoes[CurrentPositionName].MoveNames.Back);
        });
    }

    // Update is called once per frame
    void Update()
    {

    }

    //<summary>
    //カメラ移動
    //</summary>
    //<param>位置名</param>
    public void ChangeCameraPosition(string positionName)
    {
        if(isStart)
        {
          //アイテム拡大画面表示時
          if(ItemManager.Instance.ItemPanel.activeSelf)
          {
            ItemManager.Instance.ItemPanel.SetActive(false);
            positionName = CurrentPositionName;
          }
        }
        isStart = true;

        if (positionName == null) return;

        CurrentPositionName = positionName;

        GetComponent<Camera>().transform.position = CameraPositionInfoes[CurrentPositionName].Position;
        GetComponent<Camera>().transform.rotation = Quaternion.Euler(CameraPositionInfoes[CurrentPositionName].Rotate);

        //iPad対策
        //if(positionName == "RoomRight" && Screen.width > 1300)
        //{
        //  GetComponent<Camera>().transform.position = new Vector3(-4.17f,6.47f,-4);
        //  GetComponent<Camera>().transform.rotation = Quaternion.Euler(new Vector3(18,90,0));
        //}


        //ボタン表示・非表示
        UpdateButtonActive();
        //特定オブジェクトを非表示
        UpdateObjectActive();
    }

    //<summary>
    //ボタン表示非表示の切替
    //</summary>
    private void UpdateButtonActive()
    {
        //左ボタンの表示非表示を切替
        if (CameraPositionInfoes[CurrentPositionName].MoveNames.Left == null)
            ButtonLeft.SetActive(false);
        else ButtonLeft.SetActive(true);
        //右ボタンの表示非表示を切替
        if (CameraPositionInfoes[CurrentPositionName].MoveNames.Right == null)
            ButtonRight.SetActive(false);
        else ButtonRight.SetActive(true);
        //バックボタンの表示非表示を切替
        if (CameraPositionInfoes[CurrentPositionName].MoveNames.Back == null)
            ButtonBack.SetActive(false);
        else ButtonBack.SetActive(true);
    }

    //<summary>
    //特定オブジェクトを非表示
    //</summary>
    private void UpdateObjectActive()
    {
      //既に非表示のオブジェクトを表示する
      foreach (GameObject obj in hideObjects )
      {
        obj.SetActive(true);
      }
      hideObjects = new GameObject[0];

      if (CameraPositionInfoes[CurrentPositionName].hideObjectsName == null)
        return;
      //新たな方向でのオブジェクトを非表示にする
      foreach(string objName in CameraPositionInfoes[CurrentPositionName].hideObjectsName)
      {
        Array.Resize(ref hideObjects, hideObjects.Length + 1);
        hideObjects[hideObjects.Length  - 1] = GameObject.Find(objName);
        GameObject.Find(objName).SetActive(false);
      }
    }
}