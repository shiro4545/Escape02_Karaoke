using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;

public class SaveLoadSystem
{
    #region Singleton
    private static SaveLoadSystem instance = new SaveLoadSystem();
    public static SaveLoadSystem Instance => instance;
    #endregion
    private SaveLoadSystem() { }

    //セーブデータパス
#if UNITY_EDITOR
    public string SaveDataPath => Application.dataPath + "/gameSaveData.json";
#else
    public string SaveDataPath => Application.persistentDataPath + "/gameSaveData.json";
#endif

    public GameData gameData = new GameData();




    //ゲームデータのセーブ
    public void Save()
    {
#if UNITY_ANDROID
        StreamWriter writer = new StreamWriter(SaveDataPath, false, Encoding.GetEncoding("utf-8"));
#else
        StreamWriter writer = new StreamWriter(SaveDataPath,false);
#endif
        string jsonData = JsonUtility.ToJson(gameData);
        writer.WriteLine(jsonData);
        writer.Flush();
        writer.Close();
    }

    //セーブデータのロード
    public void Load()
    {
#if UNITY_ANDROID
        StreamReader reader = new StreamReader(SaveDataPath,Encoding.GetEncoding("utf-8"),false);
#else
        StreamReader reader = new StreamReader(SaveDataPath);
#endif
        string jsonData = reader.ReadToEnd();
        gameData = JsonUtility.FromJson<GameData>(jsonData);
        reader.Close();
    }

    //セーブデータの初期化
    public void GameStart()
    {
        //ゲームデータを初期化
        gameData = new GameData();

        //初期データをセーブデータに保存
        Save();
    }

    //セーブデータの存在チェック
    public bool checkFileExist()
    {
      if(File.Exists(SaveDataPath))
        return true;

      return false;
    }
}
