using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    //所有アイテム
    public string getItems = "";

    //アイテム取得有無
    public bool isGetHanger = false;
    public bool isGetTanbarine_Maru = false;
    public bool isGetTanbarine_Sankaku = false;
    public bool isGetTanbarine_Shikaku = false;
    public bool isGetKey1 = false;
    public bool isGetKeyBox = false;
    public bool isGetDriver = false;
    //謎クリア有無
    public bool isSendStarPower = false;
    public bool isSendStepStep = false;
    public bool isSendLovers = false;
    public bool isClearMachine = false;
    public bool isClearOrder = false;
    public bool isClearPhone = false;
    public bool isClearDenmokuSlide = false;


    //オブジェクト状態
    public bool isOpenShelf = false;
    public int DenmokuStatus = 1; //0:電源off,1:ロック状態,2:ロックなし

    //ヒント
    public bool[] hintArray = new bool[12];
}
