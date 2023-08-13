using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool IsPaused;

    [SerializeField] GameObject pauseMenu;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (IsPaused)
            {
                resume();
            }
            else
            {
                Paused();
            }
        }
    }
    public void Paused()
    {
        IsPaused = true;
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;//pauses menu
    }


    public void resume()
    {
        IsPaused = false;
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;//menu disapears
        MusicController.Instance.PlaySFX("Button_FX");
    }

    public void Menu()
    {
        IsPaused = false;
        pauseMenu.SetActive(false);
        SceneManager.LoadScene(0);//loads title screen
        MusicController.Instance.PlayMusic("Title");
        MusicController.Instance.PlaySFX("Button_FX");
    }
}