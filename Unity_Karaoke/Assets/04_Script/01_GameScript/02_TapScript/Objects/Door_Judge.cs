using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door_Judge : MonoBehaviour
{
    //扉開閉有無
    public int DoorStatus = 0; //0:全閉,1:全開,2:ちょい開け
    //鍵を使ったか
    public bool isClear = false;
    //1度でも全開にしたか
    public bool isFullOpen;


    //閉扉
    public GameObject CloseDoor;
    //開扉
    public GameObject OpenDoor;
    //ちょい開け扉
    public GameObject LittleOpenDoor;

    //閉扉クラス
    public Door_Tap CloseDoorClass;

    //カメラコライダークラス
    public  TapCameraMove DoorColliderClass;
}
