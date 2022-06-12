using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Book_Tap : TapCollider
{
    //タップしてページを変えたか
    private bool isChangePage = false;

    //初期ページ
    public GameObject Book1;
    //めくったページ
    public GameObject Book2;


    //タップ時
    protected override void OnTap()
    {
        base.OnTap();

        AudioManager.Instance.SoundSE("Book");

        if (isChangePage)
        {
            Book1.SetActive(true);
            Book2.SetActive(false);
            isChangePage = false;
        }
        else
        {
            Book1.SetActive(false);
            Book2.SetActive(true);
            isChangePage = true;
        }

    }
}
