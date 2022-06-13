using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cop_Judge : MonoBehaviour
{
    public bool isClear = false;

    public string InputNo = "03";

    public string AnswerNo = "22";

    public GameObject CloseSofa;
    public GameObject OpenSofa;

    //コライダー
    public GameObject BoxSofaColiider; //初期false
    //public GameObject TambarinColiider; //初期false
    


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
        CameraManager.Instance.ChangeCameraPosition("BoxSofa");

        Invoke(nameof(AfterClear2), 1);
    }

    private void AfterClear2()
    {

        AudioManager.Instance.SoundSE("SetTotte");

        //CloseChair非表示OpenChair表示
        CloseSofa.SetActive(false);
        OpenSofa.SetActive(true);

        //BoxSofaコライダー表示
        BoxSofaColiider.SetActive(true);
        //TambarinColider.SetActive(true);


        Invoke(nameof(AfterClear3), 1);
    }

    private void AfterClear3()
    {
        CameraManager.Instance.ChangeCameraPosition("Cop");

        BlockPanel.Instance.HideBlock();
    }
}
