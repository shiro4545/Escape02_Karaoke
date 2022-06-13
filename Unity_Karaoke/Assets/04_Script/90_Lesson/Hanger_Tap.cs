using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hanger_Tap : TapCollider
{
    public string HangerName;
    public int Index = 0;
    public GameObject[] Objects;
    public Hanger_judge JudgeClass;

    protected override void OnTap()
    {
        base.OnTap();

        if (JudgeClass.isClear)return;

        AudioManager.Instance.SoundSE("TapButton");

        Objects[Index].SetActive(false);

        Index++;

        if (Index >= Objects.Length)
            Index = 0;

        Objects[Index].SetActive(true);

        JudgeClass.JudgeAnswer(HangerName, Index);

    }

}

