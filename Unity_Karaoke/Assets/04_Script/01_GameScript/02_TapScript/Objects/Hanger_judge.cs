using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hanger_judge : MonoBehaviour
{
    public bool isClear = false;

    public string InputNo = "0000";

    public string AnswerNo = "0110";

    public DeskSlide_Tap SlideClass;


    // Start is called before the first frame update
    public void JudgeAnswer(string HangerName,int Index)
    {
        if(HangerName == "LL")
        {
            InputNo = Index + InputNo.Substring(1);
        }
        else if(HangerName == "L")
        {
            InputNo = InputNo.Substring(0, 1) + Index + InputNo.Substring(2);
        }
        else if(HangerName == "R")
        {
            InputNo = InputNo.Substring(0, 2) + Index + InputNo.Substring(3);
        }
        else if (HangerName == "RR")
        {
            InputNo = InputNo.Substring(0, 3) + Index;
        }

        if (InputNo == AnswerNo)
        {
            AudioManager.Instance.SoundSE("Clear");

            isClear = true;

            BlockPanel.Instance.ShowBlock();

            Invoke(nameof(AfterClear1), 1);

            SaveLoadSystem.Instance.gameData.isClearHanger = true;

        }


        SaveLoadSystem.Instance.gameData.HangerStatus = InputNo;
        SaveLoadSystem.Instance.Save();

    }

    // Update is called once per frame
    private void AfterClear1()
    {
        CameraManager.Instance.ChangeCameraPosition("Desk");

        Invoke(nameof(AfterClear2), 1);
    }

    private void AfterClear2()
    {

        AudioManager.Instance.SoundSE("Slide");

        SlideClass.OpenSlide();


        Invoke(nameof(AfterClear3), 1);
    }

    private void AfterClear3()
    {
        CameraManager.Instance.ChangeCameraPosition("Hanger");

        BlockPanel.Instance.HideBlock();
    }
}