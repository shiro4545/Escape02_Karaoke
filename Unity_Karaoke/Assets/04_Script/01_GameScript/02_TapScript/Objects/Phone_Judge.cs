using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phone_Judge : MonoBehaviour
{
    //�����������ǂ���
    public bool isClear = false;

    //�{�^����3��
    public string InputNo = "000";

    //������3��
    public string AnswerNo = "453";

    //���̃X���C�h�I�u�W�F�N�g
    public GameObject UpSlide;
    //�������킹
    public void JudgeAnswer(string buttonName, int Index)
    {
        //���͒l���X�V
        if (buttonName == "Top") //��{�^���̎�
        {
            //1���ڂ��`�F���W
            InputNo = Index + InputNo.Substring(1);
        }
        else if (buttonName == "Center") //�����{�^���̎�
        {
            //2���ڂ��`�F���W
            InputNo = InputNo.Substring(0, 1) + Index + InputNo.Substring(2);
        }
        else //�E�{�^���̎�
        {
            //3���ڂ��`�F���W
            InputNo = InputNo.Substring(0, 2) + Index;
        }


        //��������
        if (InputNo == AnswerNo)
        {
            //�N���A�̌��ʉ�
            AudioManager.Instance.SoundSE("Clear");
            //�N���A�����true��
            isClear = true;

            //��ʃu���b�N
            BlockPanel.Instance.ShowBlock();

            //1�b��ɃJ�����ړ�
            Invoke(nameof(AfterClear1), 1);

            //�Ō�ɃZ�[�u
            SaveLoadSystem.Instance.Save();
        }

    }
    //������̃X���C�h�J��
    private void AfterClear1()
    {
        //���ʉ�
        AudioManager.Instance.SoundSE("Slide");
        //�X���C�h�J��
        UpSlide.transform.Translate(new Vector3(0f, 0f, 0.4f));
        //��ʃu���b�N������
        BlockPanel.Instance.HideBlock();

    }
}
