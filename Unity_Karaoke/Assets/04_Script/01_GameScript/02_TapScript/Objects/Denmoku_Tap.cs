using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Denmoku_Tap : MonoBehaviour
{
    //タップ有効な画面No
    public int EnableScreennNo;

    //ボタン識別
    public int SubInt;
    public string SubStr;

    // Start is called before the first frame update
    void Start()
    {
        var CurrentTrigger = gameObject.AddComponent<EventTrigger>();
        var EntryClick = new EventTrigger.Entry();
        EntryClick.eventID = EventTriggerType.PointerClick;
        EntryClick.callback.AddListener((x) => OnTap());
        CurrentTrigger.triggers.Add(EntryClick);
    }

    // Update is called once per frame
    void Update()
    {
        if(CameraManager.Instance.CurrentPositionName == "Denmoku" && EnableScreennNo == Denmoku_Judge.Instance.CurrentScreenNo)
            GetComponent<BoxCollider>().enabled = true;
        else GetComponent<BoxCollider>().enabled = false;
    }

    public void OnTap()
    {
        if(EnableScreennNo == 101)
        {
            GameObject sprite = Denmoku_Judge.Instance.Sprite101[Denmoku_Judge.Instance.Answer101.Length];
            sprite.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Images/03_Number/" + SubInt);

        }
    }
}
