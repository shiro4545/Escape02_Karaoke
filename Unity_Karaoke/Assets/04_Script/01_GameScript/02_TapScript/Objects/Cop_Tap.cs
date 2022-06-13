using System.Collections;
using System.Collections.Generic;
using UnityEngine;



 public class Cop_Tap : TapCollider
    {
        public string CopName;
        public int Index = 0;
        public GameObject[] Objects;
        public Cop_Judge JudgeClass;

        protected override void OnTap()
        {
            base.OnTap();

            if (JudgeClass.isClear) return;

            AudioManager.Instance.SoundSE("IceInGlass");

            Objects[Index].SetActive(false);

            Index++;

            if (Index >= Objects.Length)
                Index = 0;

            Objects[Index].SetActive(true);

            JudgeClass.JudgeAnswer(CopName, Index);

        }

    }
