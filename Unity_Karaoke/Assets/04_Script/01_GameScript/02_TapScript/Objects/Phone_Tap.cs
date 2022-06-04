using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phone_Tap : TapCollider
{
    //�{�^����
    public string ButtonName;
    //�C���f�b�N�X
    public int Index = 0;
    //�I�u�W�F�N�g�z��
    public GameObject[] Objects;

    //���������N���X
    public Phone_Judge JudgeClass;

    //�{�^���^�b�v��
    protected override void OnTap()
    {
        base.OnTap();

        //�����������ς݂̏ꍇ�͏������Ȃ�
        if (JudgeClass.isClear)
            return;

        //���ʉ�
        AudioManager.Instance.SoundSE("TapButton");

        //�\�����̉摜���\����
        Objects[Index].SetActive(false);

        //�C���f�b�N�X��+1
        Index++;

        //�C���f�b�N�X���I�u�W�F�N�g�z��̗v�f���Ɠ����ȏ�̏ꍇ�A0�ɖ߂�
        if (Index >= Objects.Length)
            Index = 0;

        //���̉摜��\��
        Objects[Index].SetActive(true);

        //�{�^�������Ɉړ�
        this.gameObject.transform.Translate(new Vector3(0, 0.02f, 0));

        //0.1�b��Ƀ{�^���ʒu�����ɖ߂�
        Invoke(nameof(delayButton), 0.1f);

        //��������
        JudgeClass.JudgeAnswer(ButtonName, Index);
    }



    //�����ꂽ�{�^����߂�
    private void delayButton()
    {
        this.gameObject.transform.Translate(new Vector3(0, -0.02f, 0));
    }

}

