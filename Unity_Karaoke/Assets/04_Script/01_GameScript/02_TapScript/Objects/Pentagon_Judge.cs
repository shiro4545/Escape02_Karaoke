using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pentagon_Judge : MonoBehaviour
{
    //クリア有無
    public bool isClear = false;
    //ピースの入力状態(0:ピースなし,1~5:それぞれのピースが置かれている)
    public string Input = "00000";

    //引き出し
    public GameObject Slide;
    //鍵
    public GameObject Key3;
    //五角形コライダー
    public GameObject PentagonCollider;

    //入力値変更&答え合わせ
    public void ChangeInput(int AreaNo,int NewNo)
    {
        //入力値変更
        switch (AreaNo)
        {
            case 1:
                Input = NewNo + Input.Substring(1);
                break;
            case 2:
                Input = Input.Substring(0,1) + NewNo + Input.Substring(2);
                break;
            case 3:
                Input = Input.Substring(0, 2) + NewNo + Input.Substring(3);
                break;
            case 4:
                Input = Input.Substring(0, 3) + NewNo + Input.Substring(4);
                break;
            case 5:
                Input = Input.Substring(0, 4) + NewNo ;
                break;
            default:
                break;
        }

        SaveLoadSystem.Instance.gameData.PentagonStatus = Input;

        //答え合わせ
        if (Input == "12345")
        {
            //演出
            AudioManager.Instance.SoundSE("Clear");
            BlockPanel.Instance.ShowBlock();

            //ステータス変更
            isClear = true;
            SaveLoadSystem.Instance.gameData.isSendKosho = true;
            SaveLoadSystem.Instance.gameData.isClearPentagon = true;


            //引き出しを開ける
            Invoke(nameof(AfterClear1), 1.5f);
        }

        SaveLoadSystem.Instance.Save();
    }


    //カメラ移動
    private void AfterClear1()
    {
        CameraManager.Instance.ChangeCameraPosition("HallDesk");
        PentagonCollider.SetActive(false);
        Invoke(nameof(AfterClear2), 1f);
    }

    //引き出しを開ける
    private void AfterClear2()
    {
        Key3.SetActive(true);
        AudioManager.Instance.SoundSE("Slide");
        Slide.transform.Translate(new Vector3(0, -0.1f, 0));
        BlockPanel.Instance.HideBlock();
    }
}
