using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    public GameObject AudioBGM;
    AudioSource audioSE;
    AudioSource audioBGM;

    // Start is called before the first frame update
    void Start()
    {
      Instance = this;
      audioSE = GetComponent<AudioSource>();
      audioBGM = AudioBGM.GetComponent<AudioSource>();
    }

    //<summary>
    //効果音を出す
    //</summary>
    //<param>音源ファイル名</param>
    public void SoundSE(string SEName)
    {
      audioSE.PlayOneShot( Resources.Load("SE/" + SEName ,typeof(AudioClip) ) as AudioClip );
    }

    //<summary>
    //BGMを流す
    //</summary>
    public void SoungBGM()
    {
      audioBGM.Play();
    }


    //<summary>
    //予約曲を流す
    //</summary>
    //<param>音源ファイル名</param>
    public void SoundSong(string SEName)
    {
        audioBGM.volume = 0;
        audioSE.PlayOneShot(Resources.Load("SE/" + SEName, typeof(AudioClip)) as AudioClip);
        Invoke(nameof(delayBGM), 27);
    }
    public void delayBGM()
    {
        audioBGM.volume = 0.1f;

    }
}
