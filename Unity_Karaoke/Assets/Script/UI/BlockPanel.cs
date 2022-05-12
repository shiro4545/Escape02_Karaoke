using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockPanel : MonoBehaviour
{
    public GameObject BP;
    public static BlockPanel Instance { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
      Instance = this;
    }

    // Update is called once per frame
    void Update()
    {

    }

    //画面ブロックを表示
    public void ShowBlock()
    {
      BP.SetActive(true);
    }

    //画面ブロックを非表表示
    public void HideBlock()
    {
      BP.SetActive(false);
    }
}
