using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlay: MonoBehaviour 
{
    public AudioClip click;
    public AudioClip errorAlarm;


    internal void ClickSoundPlay(float volume)
    {
        AudioSource.PlayClipAtPoint(click, Vector3.zero, volume);
    }
    internal void ErrorSoundPlay(float volume)
    {
        AudioSource.PlayClipAtPoint(errorAlarm, Vector3.zero, volume);
    }

    internal void MusicControl(float setvolume)
    {
        GlobalVariables.volume = setvolume;
        AudioSource bgm = GetComponent<AudioSource>();
        bgm.volume = setvolume;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
