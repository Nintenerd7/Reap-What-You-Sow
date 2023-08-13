using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameOver : MonoBehaviour
{
 public void Restart()
    {
        SceneManager.LoadScene(1);
        Energy.EnergyPoints = 0;
        MusicController.Instance.PlayMusic("MainTheme");
        MusicController.Instance.PlaySFX("Button_FX");
    }
    public void Title()
    {
        SceneManager.LoadScene(0);
        MusicController.Instance.PlayMusic("Title");
        MusicController.Instance.PlaySFX("Button_FX");
    }
}
