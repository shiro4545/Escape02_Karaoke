using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trash_Tap : TapCollider
{

    //タップしたかどうか
    public bool isTap = false;

    //TrashColiderのbool　有効化/無効化するためのもの
    public GameObject obj;

    //ボタンタップ時
    protected override void OnTap()
    {
        base.OnTap();

        //タップを２回目以降しても働かないように
        if (isTap == false)
        {//効果音
            AudioManager.Instance.SoundSE("PutPaper");
            //ごみ箱を倒す
            this.gameObject.transform.Rotate(new Vector3(30,90 , -40));
            this.gameObject.transform.Translate(new Vector3(0, -0.5f, 0.5f));

            //タップを２回目以降しても働かないように
            isTap = true;

            //TrashColiderを有効化
            obj.SetActive(true);
        }
        }
        }
