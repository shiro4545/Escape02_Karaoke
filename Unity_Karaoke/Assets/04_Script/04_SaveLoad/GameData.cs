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
    public bool isClearRock1 = false; //デンモクロック画面
    public bool isSendStarPower = false;
    public bool isSendStepStep = false;
    public bool isSendLovers = false;
    public bool isClearMachine = false;


    //オブジェクト状態
    public bool isOpenShelf = false;

    //ヒント
    public bool[] hintArray = new bool[12];
}
