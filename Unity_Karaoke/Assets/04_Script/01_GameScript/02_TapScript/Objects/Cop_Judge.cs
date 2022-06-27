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

    //?R???C?_?[
    public GameObject BoxSofaColiider; //????false
    public GameObject BoxSofaColiider2; //????false
    //public GameObject TambarinColiider; //????false



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

            Invoke(nameof(AfterClear1), 1.5f);

            SaveLoadSystem.Instance.gameData.isClearCop = true;
        }

        SaveLoadSystem.Instance.gameData.CopStatus = InputNo;
        SaveLoadSystem.Instance.Save();

    }

    // Update is called once per frame
    private void AfterClear1()
    {
        CameraManager.Instance.ChangeCameraPosition("BoxSofa");

        Invoke(nameof(AfterClear2), 1.5f);
    }

    private void AfterClear2()
    {

        AudioManager.Instance.SoundSE("SetTotte");

        //CloseChair???\??OpenChair?\??
        CloseSofa.SetActive(false);
        OpenSofa.SetActive(true);

        //BoxSofa?R???C?_?[?\??
        BoxSofaColiider.SetActive(true);
        BoxSofaColiider2.SetActive(true);
        //TambarinColider.SetActive(true);


        Invoke(nameof(AfterClear3), 1.5f);
    }

    private void AfterClear3()
    {
        CameraManager.Instance.ChangeCameraPosition("Cop");

        BlockPanel.Instance.HideBlock();
    }
}
