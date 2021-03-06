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


    //カラオケ機クラス
    public Machine_Judge Machine;

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
            "Title",//タイトル画面
            new CameraPositionInfo
            {
                Position=new Vector3(60.7f,4.7f,6.5f),
                Rotate =new Vector3(7,116,0),
                MoveNames=new MoveNames
                {
                },
            }
        },
        {
            "RoomStart",//スタート位置
            new CameraPositionInfo
            {
                Position=new Vector3(23.2f,7.36f,8.1f),
                Rotate =new Vector3(2.3f,-117,0),
                MoveNames=new MoveNames
                {
                    Left="RoomTV",
                    Right="RoomSofa",
                },
                hideObjectsName = new string[]{"Hanger","HallWall","Desk","TambarinArea"}
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
                    Left="RoomStart",
                    Right="RoomDoor",
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
                    Left="RoomDoor",
                    Right="RoomStart",
                },
            }
        },
        {
            "RoomDoor",//ドア方向
            new CameraPositionInfo
            {
                Position=new Vector3(-11.5f,5.4f,-7.8f),
                Rotate =new Vector3(0,75f,0),
                MoveNames=new MoveNames
                {
                    Left="RoomSofa",
                    Right="RoomTV",
                },
                hideObjectsName = new string[]{"Shelf","KaraokeMachine","Tambarin_Shikaku" }
            }
        },
        {
            "Door",//ドア
            new CameraPositionInfo
            {
                Position=new Vector3(8.2f,6,-5f),
                Rotate =new Vector3(3,90,0),
                MoveNames=new MoveNames
                {
                    Back="RoomDoor",
                }
            }
        },
        {
            "Phone",//電話
            new CameraPositionInfo
            {
                Position=new Vector3(14f,6,-4f),
                Rotate =new Vector3(3,64,0),
                MoveNames=new MoveNames
                {
                    Back="RoomDoor",
                }
            }
        },
        {
            "Hanger",//ハンガー
            new CameraPositionInfo
            {
                Position=new Vector3(10f,7.5f,-9.2f),
                Rotate =new Vector3(3,30,0),
                MoveNames=new MoveNames
                {
                    Back="RoomDoor",
                }
            }
        },
        {
            "Desk",//デスク
            new CameraPositionInfo
            {
                Position=new Vector3(15.1f,7.38f,0.8f),
                Rotate =new Vector3(25,0,0),
                MoveNames=new MoveNames
                {
                    Back="Hanger",
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
            "DenmokuBack",//デンモク裏側(電源ボタン)
            new CameraPositionInfo
            {
                Position=new Vector3(-1.8f,3.1f,-1.1f),
                Rotate =new Vector3(-7,0,0),
                MoveNames=new MoveNames
                {
                    Back="Denmoku",
                }
            }
        },
        {
            "Driver",//ドライバー
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
                Position=new Vector3(10.33f,3.7f,3.9f),
                Rotate =new Vector3(20,180,0),
                MoveNames=new MoveNames
                {
                    Back="BlueBox",
                }
            }
        },
        {
            "Shelf",//カラオケ機が入った棚
            new CameraPositionInfo
            {
                Position=new Vector3(-0.2f,7.4f,-4f),
                Rotate =new Vector3(28,252,0),
                MoveNames=new MoveNames
                {
                    Back="RoomStart",
                }
            }
        },
        {
            "ShelfKey",//カラオケ機が入った棚の鍵
            new CameraPositionInfo
            {
                Position=new Vector3(-4.7f,2f,-5f),
                Rotate =new Vector3(22,270,0),
                MoveNames=new MoveNames
                {
                    Back="Shelf",
                }
            }
        },
        {
            "ShelfLeft",//カラオケ機が入った棚の左下
            new CameraPositionInfo
            {
                Position=new Vector3(-3.65f,3f,-5.1f),
                Rotate =new Vector3(15,238,0),
                MoveNames=new MoveNames
                {
                    Back="Shelf",
                }
            }
        },
        {
            "Manual",//カラオケ機が入った棚内のデンモク説明書
            new CameraPositionInfo
            {
                Position=new Vector3(-6.1f,3.5f,-5.78f),
                Rotate =new Vector3(56,270,0),
                MoveNames=new MoveNames
                {
                    Back="Shelf",
                }
            }
        },
        {
            "Machine",//カラオケ機
            new CameraPositionInfo
            {
                Position=new Vector3(-3.9f,4.55f,-6.4f),
                Rotate =new Vector3(4,272,0),
                MoveNames=new MoveNames
                {
                    Back="Shelf",
                }
            }
        },
        {
            "Picture",//絵2枚
            new CameraPositionInfo
            {
                Position=new Vector3(4.2f,8f,2.4f),
                Rotate =new Vector3(12,324,0),
                MoveNames=new MoveNames
                {
                    Back="RoomSofa",
                }
            }
        },
        {
            "Poster",//ポスター周り
            new CameraPositionInfo
            {
                Position=new Vector3(2.35f,7.65f,-1.38f),
                Rotate =new Vector3(8,-55,0),
                MoveNames=new MoveNames
                {
                    Back="RoomSofa",
                },
            }
        },
        {
            "PosterUp",//ポスター寄り
            new CameraPositionInfo
            {
                Position=new Vector3(-3f,6.5f,5.3f),
                Rotate =new Vector3(0,270,0),
                MoveNames=new MoveNames
                {
                    Back="Poster",
                },
            }
        },
        {
            "BoxSofa",//1人掛ソファ寄り
            new CameraPositionInfo
            {
                Position=new Vector3(9.8f,8.3f,-5),
                Rotate =new Vector3(50,302,-5),
                MoveNames=new MoveNames
                {
                    Back="RoomSofa",
                }
            }
        },
        {
            "Hall",//廊下
            new CameraPositionInfo
            {
                Position=new Vector3(21.6f,6f,11.2f),
                Rotate =new Vector3(6,180,0),
                MoveNames=new MoveNames
                {
                    Back="HallBack",
                },
                hideObjectsName = new string[]{"Hole","HallTable","Phone"}
            }
        },
         {
            "PhoneBtn",//電話ボタン寄り
            new CameraPositionInfo
            {
                Position=new Vector3(16.7f,5.85f,-2.25f),
                Rotate =new Vector3(7.55f,90,0),
                MoveNames=new MoveNames
                {
                    Back="Phone",
                }
            }
        },
         {
            "Trash",//ごみ箱寄り
            new CameraPositionInfo
            {
                Position=new Vector3(-5.87f,6.77f,6.33f),
                Rotate =new Vector3(66,316,0),
                MoveNames=new MoveNames
                {
                    Back="Poster",
                }
            }
        },
         {
            "Book",//曲検索本
            new CameraPositionInfo
            {
                Position=new Vector3(1.93f,6.77f,4f),
                Rotate =new Vector3(55,180,0),
                MoveNames=new MoveNames
                {
                    Back="RoomStart",
                }
            }
        },
         {
            "Cop",//コップエリア
            new CameraPositionInfo
            {
                Position=new Vector3(9.73f,6.82f,6.9f),
                Rotate =new Vector3(25,180,0),
                MoveNames=new MoveNames
                {
                    Back="RoomStart",
                }
            }
        },
         {
            "Rimocon",//リモコン寄り
            new CameraPositionInfo
            {
                 Position=new Vector3(-7.5f,7.3f,7.76f),
                 Rotate =new Vector3(13,-90,0),
                 MoveNames=new MoveNames
                 {
                    Back="Poster",
                 }
            }
        },
        {
            "DoorB",//部屋Bの扉
            new CameraPositionInfo
            {
                Position=new Vector3(29f,6.6f,-19.7f),
                Rotate =new Vector3(7,270,0),
                MoveNames=new MoveNames
                {
                    Back="Hall",
                },
                hideObjectsName = new string[]{"DoorC"}
            }
        },
        {
            "DoorBWindow",//部屋Bの扉の窓
            new CameraPositionInfo
            {
                Position=new Vector3(20.2f,6.44f,-19.82f),
                Rotate =new Vector3(0,270,0),
                MoveNames=new MoveNames
                {
                    Back="DoorB",
                },
            }
        },
        {
            "DoorC",//部屋Cの扉
            new CameraPositionInfo
            {
                Position=new Vector3(14.2f,6.6f,-19.7f),
                Rotate =new Vector3(7,90,0),
                MoveNames=new MoveNames
                {
                    Back="Hall",
                },
                hideObjectsName = new string[]{"DoorB"}
            }
        },
        {
            "DoorCWindow",//部屋Cの扉の窓
            new CameraPositionInfo
            {
                Position=new Vector3(23.08f,6.04f,-19.73f),
                Rotate =new Vector3(0,90,0),
                MoveNames=new MoveNames
                {
                    Back="DoorC",
                },
            }
        },
        {
            "DoorD",//部屋Dの扉
            new CameraPositionInfo
            {
                Position=new Vector3(14.2f,6.6f,-5f),
                Rotate =new Vector3(7,90,0),
                MoveNames=new MoveNames
                {
                    Back="Hall",
                },
            }
        },
        {
            "DoorDWindow",//部屋Dの扉の窓
            new CameraPositionInfo
            {
                Position=new Vector3(22.67f,6.04f,-5.3f),
                Rotate =new Vector3(0,83,0),
                MoveNames=new MoveNames
                {
                    Back="DoorD",
                },
            }
        },
        {
            "HallBack",//通路背中の壁
            new CameraPositionInfo
            {
                Position=new Vector3(21.6f,7.3f,-8f),
                Rotate =new Vector3(10,0,0),
                MoveNames=new MoveNames
                {
                    Back="Hall",
                },
            }
        },
        {
            "HallDesk",//通路背中の机
            new CameraPositionInfo
            {
                Position=new Vector3(21.6f,7.75f,2.62f),
                Rotate =new Vector3(37,0,0),
                MoveNames=new MoveNames
                {
                    Back="HallBack",
                },
            }
        },
        {
            "Pentagon",//五角形パズル
            new CameraPositionInfo
            {
                Position=new Vector3(21.6f,6.13f,6.22f),
                Rotate =new Vector3(70,0,0),
                MoveNames=new MoveNames
                {
                    Back="HallDesk",
                },
            }
        },
        {
            "DoorFinal",//通路突き当たりの扉
            new CameraPositionInfo
            {
                Position=new Vector3(21.5f,6.26f,-33.65f),
                Rotate =new Vector3(7,180,0),
                MoveNames=new MoveNames
                {
                    Back="Hall",
                },
            }
        },
        {
            "DoorFinalBtn",//通路突き当たりの扉ボタン
            new CameraPositionInfo
            {
                Position=new Vector3(22.85f,5.38f,-40.6f),
                Rotate =new Vector3(15,180,0),
                MoveNames=new MoveNames
                {
                    Back="DoorFinal",
                },
            }
        },
        {
            "RoomEnd",//脱出クリア後
            new CameraPositionInfo
            {
                Position=new Vector3(69.9f,9.2f,-20f),
                Rotate =new Vector3(-9.6f,136,0),
                MoveNames=new MoveNames
                {
                },
            }
        },

    };

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        ChangeCameraPosition("Title");
        //ChangeCameraPosition("RoomStart");
        //ChangeCameraPosition("Hall");

        //左矢印ボタン押下時
        ButtonLeft.GetComponent<Button>().onClick.AddListener(() =>
        {
            AudioManager.Instance.SoundSE("TapUIBtn");
            ChangeCameraPosition(CameraPositionInfoes[CurrentPositionName].MoveNames.Left);
        });

        //右矢印ボタン押下時
        ButtonRight.GetComponent<Button>().onClick.AddListener(() =>
        {
            AudioManager.Instance.SoundSE("TapUIBtn");
            ChangeCameraPosition(CameraPositionInfoes[CurrentPositionName].MoveNames.Right);
        });

        //下矢印ボタン押下時
        ButtonBack.GetComponent<Button>().onClick.AddListener(() =>
        {
            AudioManager.Instance.SoundSE("TapUIBtn");

            //カラオケ機のボタン初期化用
            if (CurrentPositionName == "Machine")
                Machine.TapBack();

            //デンモク画面の初期化
            if (CurrentPositionName == "Denmoku")
                Denmoku_Judge.Instance.CameraBack();

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

        //デンモクスライド済みの場合は、カメラ位置を補正
        if (positionName == "Denmoku" && Denmoku_Judge.Instance.isSlide)
        {
            GetComponent<Camera>().transform.position = new Vector3(-1.8f, 5.7f, 1.7f);
            GetComponent<Camera>().transform.rotation = Quaternion.Euler(CameraPositionInfoes[CurrentPositionName].Rotate);
        }
        else
        {
            GetComponent<Camera>().transform.position = CameraPositionInfoes[CurrentPositionName].Position;
            GetComponent<Camera>().transform.rotation = Quaternion.Euler(CameraPositionInfoes[CurrentPositionName].Rotate);
        }
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
