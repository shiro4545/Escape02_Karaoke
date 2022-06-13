using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cop_Judge : MonoBehaviour
{
    public bool isClear = false;

    public string InputNo = "03";

    public string AnswerNo = "22";

    public GameObject CloseChair;
    public GameObject OpenChair;


    // Start is called before the first frame update
    public void JudgeAnswer(string CopName, int Index)
    {
        if (CopName == "Blue")
        {
            InputNo = Index + InputNo.Substring(1);
        }
        else if (CopName == "White")
        {
            InputNo = InputNo.Substring(0, 1) + Index;
        }
        

        if (InputNo == AnswerNo)
        {
            AudioManager.Instance.SoundSE("Clear");

            isClear = true;

            BlockPanel.Instance.ShowBlock();

            Invoke(nameof(AfterClear1), 1);

            SaveLoadSystem.Instance.Save();
        }

    }

    // Update is called once per frame
    private void AfterClear1()
    {
        CameraManager.Instance.ChangeCameraPosition("BoxChair");

        Invoke(nameof(AfterClear2), 1);
    }

    private void AfterClear2()
    {

        AudioManager.Instance.SoundSE("Slide");

        //CloseChair”ñ•\Ž¦OpenChair•\Ž¦
        CloseChair.SetActive(false);
        OpenChair.SetActive(true);


        Invoke(nameof(AfterClear3), 1);
    }

    private void AfterClear3()
    {
        CameraManager.Instance.ChangeCameraPosition("Cop");

        BlockPanel.Instance.HideBlock();
    }
}
