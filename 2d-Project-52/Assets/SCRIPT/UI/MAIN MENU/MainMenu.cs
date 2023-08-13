using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    public void Play()
    {
        MusicController.Instance.PlayMusic("MainTheme");
        MusicController.Instance.PlaySFX("Button_FX");
        SceneManager.LoadScene(1);//loads main game 
    }

    public void OnApplicationQuit()//quits the game 
    {
        MusicController.Instance.PlaySFX("Button_FX");
        Application.Quit();
    }

    public void PlaySound()
    {
        MusicController.Instance.PlaySFX("Button_FX");
    }
}
