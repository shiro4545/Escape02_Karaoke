using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    //所有アイテム
    public string getItems = "";

    //アイテム取得有無
    public bool isGetPaper1 = false;

    //謎クリア有無
    public bool isSendStarPower = false;
    public bool isSendStepStep = false;
    public bool isSendLovers = false;
    public bool isClearMachine = false;
    public bool isClearOrder = false;


    //オブジェクト状態
    public bool isOpenShelf = false;
    public int DenmokuStatus = 1; //0:電源off,1:ロック状態,2:ロックなし

    //ヒント
    public bool[] hintArray = new bool[12];
}
