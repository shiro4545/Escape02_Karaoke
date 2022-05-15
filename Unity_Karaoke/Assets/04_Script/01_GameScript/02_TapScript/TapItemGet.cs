using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TapItemGet : TapCollider
{
    public string item_Name;
    protected override void OnTap()
    {
      base.OnTap();

      this.gameObject.SetActive(false);
      ItemManager.Instance.getItem(item_Name);
    }
}
