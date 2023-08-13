using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
public class Volume_Logic : MonoBehaviour
{
    [SerializeField] private AudioMixer Music_Mixer;
    [SerializeField] private Slider Music_Slider;
    [SerializeField] private Slider SFX_Slider;
  
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("musicVolume"))
        {
            LoadVolume();
        }
        else
        {
            Set_Music_Volume();
            Set_SFX_Volume();
        }
    }
    public void Set_Music_Volume()
    {
       float volume = Music_Slider.value;//assigns float to slider value
        Music_Mixer.SetFloat("Music", Mathf.Log10(volume) * 20);//assigns values to mixer group
        PlayerPrefs.SetFloat("musicVolume", volume);//saves volume value after exiting game/playmode
    }
   public void Set_SFX_Volume()
    {
        float volume = SFX_Slider.value;//assigns float to slider value
        Music_Mixer.SetFloat("SFX", Mathf.Log10(volume) * 20);//assigns values to mixer group
        PlayerPrefs.SetFloat("SFXVolume", volume);//saves volume value after exiting game/playmode
    }

    private void LoadVolume()
    {
        Music_Slider.value = PlayerPrefs.GetFloat("musicVolume");
        SFX_Slider.value = PlayerPrefs.GetFloat("SFXVolume");
        Set_Music_Volume();
        Set_SFX_Volume();
    }
}
