using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class MusicController : MonoBehaviour
{
    public static MusicController Instance;
    public SoundIndex[] MusicFX_Lable, SFX_Lable;
    public AudioSource Music_Src, SFX_Src;
    #region AWAKE
    private void Awake()
    {
        if (Instance == null)//if there is no instance in the audio source
        {
            Instance = this;//set instance to the index of the sound name.
            DontDestroyOnLoad(gameObject);//do not destroy
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion
    private void Start()
    {
        PlayMusic("Title");
    }
    public void PlayMusic(string Name)
    {
        SoundIndex s = Array.Find(MusicFX_Lable, X => X.Sound_Name == Name);

        if (s == null)
        {
            Debug.Log("Sound Error");
        }
        else
        {
            Music_Src.clip = s.SoundClip;
            Music_Src.Play();
        }
    }

    public void PlaySFX(string Name)
    {
        SoundIndex s = Array.Find(SFX_Lable, X => X.Sound_Name == Name);

        if (s == null)
        {
            Debug.Log("Sound Error");
        }
        else
        {
            SFX_Src.clip = s.SoundClip;
            SFX_Src.Play();
        }
    }
}
