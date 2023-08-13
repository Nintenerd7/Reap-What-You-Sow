using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReaperHearts : MonoBehaviour
{
    //variables 
    public int HeartHealth;
    public int HeartCount;

    //UI Variables
    public Image[] Reaper_Hearts;
    public Sprite Full;
    public Sprite Empty;
    public GameObject HealthItem;
    public GameObject GoalUI;

    [SerializeField] AudioClip damage;
    //
    // Update is called once per frame
    void Update()
    {
        if (HeartHealth > HeartCount)
        {
            HeartHealth = HeartCount;
        }



        for (int i = 0; i < Reaper_Hearts.Length; i++)
        {
            if (i < HeartHealth)
            {
                Reaper_Hearts[i].sprite = Full;
            }
            else
            {
                Reaper_Hearts[i].sprite = Empty;
            }

            if (i < HeartCount)
            {
                Reaper_Hearts[i].enabled = true;
            }
            else
            {
                Reaper_Hearts[i].enabled = false;
            }


        }



    }

    public void DealDamage()
    {
        int i = 0;//for the ammount of hearts you have displaying on the hud

        AudioSource.PlayClipAtPoint(damage, transform.position);
        HeartHealth--;
            Reaper_Hearts[i].sprite = Empty;

            if (HeartHealth == 0)
            {
                GoalUI.SetActive(true);
            MusicController.Instance.PlayMusic("Win");//plays win Jingle
            PauseMenu.IsPaused = true;
                Time.timeScale = 0f;
                //win screen 
            }
        
    }

    /*private void OnTriggerEnter2D(Collider2D other)
    {
        int i = 0;//for the ammount of hearts you have displaying on the hud
        if (other.tag == "HellBullet")
        {
            HeartHealth--;
            Reaper_Hearts[i].sprite = Empty;

            if (HeartHealth == 0)
            {
                GoalUI.SetActive(true);
                PauseMenu.IsPaused = true;
                Time.timeScale = 0f;
                //win screen 
            }
        }

    }*/
}
