using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToScene : MonoBehaviour
{
    public void SceneMove(string _sceneName)
    {
        SceneManager.LoadScene(_sceneName);
    }

}
