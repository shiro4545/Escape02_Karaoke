using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class SampleScript5 : MonoBehaviour
{
    //他のキューブオブジェクト
    public GameObject Cube2;


    // Start is called before the first frame update
    void Start()
    {

        //***「タップされたらOnTap()関数が実行する」という処理 (理解しなくてOK)**********
        var CurrentTrigger = gameObject.AddComponent<EventTrigger>();
        var EntryClick = new EventTrigger.Entry();
        EntryClick.eventID = EventTriggerType.PointerClick;
        EntryClick.callback.AddListener((x) => OnTap());
        CurrentTrigger.triggers.Add(EntryClick);
        //*********************************************************************

    }

    // Update is called once per frame
    void Update()
    {

    }

    //オブジェクトをタップした時の処理
    public void OnTap()
    {
        Debug.Log("タップされました");

        //オブジェクトを消す
        this.gameObject.SetActive(false);

        //オベジェクト移動
        //this.gameObject.transform.Translate(new Vector3(2, 2, 2));

        //他のオブジェクトを消す
        //Cube2.SetActive(false);

        //2.5秒後にshowObject関数を実行
        //Invoke(nameof(showObject), 2.5f);
    }


    //
    public void showObject()
    {
        Cube2.SetActive(true);

    }
}
