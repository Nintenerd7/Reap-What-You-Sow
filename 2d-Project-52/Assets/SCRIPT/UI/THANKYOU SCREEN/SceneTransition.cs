using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneTransition : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;//resumes frames
        StartCoroutine(Transition());//start coroutine
    }
    public IEnumerator Transition()
    {
        yield return new WaitForSeconds(5);//shows screen for  five seconds 
        SceneManager.LoadScene(0);//returns to title screen

    }
}


