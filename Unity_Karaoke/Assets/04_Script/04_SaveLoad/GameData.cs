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
  public bool isGetPaper2 = false;
  public bool isGetBottle = false;
  public bool isPaperChange = false;
  public bool isGetCP1_deguchi = false;
  public bool isGetCP2 = false;
  public bool isGetCP3 = false;
  public bool isGetKaicyudento = false;
  public bool isGetPenchi = false;
  public bool isGetDenchi = false;
  public bool isGetTotte = false;

  //謎クリア有無
  public bool isClearWashPanel1 = false;
  public bool isClearWashPanel2 = false;
  public bool isClearPaper = false;
  public bool isClearBlueBox = false;
  public bool isClearNazo4 = false;
  public bool isClearNazo3 = false;
  public bool isClearFlow = false;
  public bool isClearDe = false;
  public bool isClearLight = false;
  public bool isClearCPSet = false;

  //オブジェクト状態
  public bool isOpenBenza = false;
  public bool isOpenBenzaCover = false;
  public int statusHaimendanRight = 0;
  public int statusHaimendanLeft = 0;
  public int statusPaperRight = 0;
  public int statusPaperLeft = 0;
  public int statusPaperCenter1 = 0;
  public int statusPaperCenter2 = 0;
  public bool isPullGabyo = false;
  public bool isOpenTunkCover = false;
  public int statusTunk = 0; //0~3:フタ開(各水位)
  public bool isSetTotte= false;
  public int statusCPIndex0 = 0;
  public int statusCPIndex1 = 0;
  public int statusCPIndex2 = 0;

  //
  public bool[] hintArray = new bool[12];
}
