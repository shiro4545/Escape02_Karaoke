using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapObjectChange : TapCollider
{
  public int Index{ get; private set;}
  public GameObject[] Objects;

  protected override void OnTap()
  {
    base.OnTap();

    AudioManager.Instance.SoundSE("TapButton");
    
    Objects[Index].SetActive(false);
    Index++;

    if (Index >= Objects.Length) Index = 0;

    Objects[Index].SetActive(true);
  }
}
