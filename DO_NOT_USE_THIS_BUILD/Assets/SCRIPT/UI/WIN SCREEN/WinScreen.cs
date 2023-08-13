using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class WinScreen : MonoBehaviour
{
  public void Next()
    {
        SceneManager.LoadScene(3);
        Time.timeScale = 1f;
    }
}
