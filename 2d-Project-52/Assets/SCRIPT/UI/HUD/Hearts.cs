using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Hearts : MonoBehaviour
{
    //variables 
    public int HeartHealth;
    public int HeartCount;

    //UI Variables
    public Image[] hearts;
    public Sprite Full;
    public Sprite Empty;
    public GameObject HealthItem;

    [SerializeField] float invincibilityPeriod = 0.6f;
    bool isInvincible = false;

    int enemiesTouching = 0;

    [SerializeField] AudioClip damage;
    //
    // Update is called once per frame
    void Update()
    {
        if (HeartHealth > HeartCount)
        {
            HeartHealth = HeartCount;
        }

        if (!isInvincible && HeartHealth > 0 && enemiesTouching > 0)
            DealDamage(1);

        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < HeartHealth)
            {
                hearts[i].sprite = Full;
            }
            else
            {
                hearts[i].sprite = Empty;
            }

            if (i < HeartCount)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        int i = 0;//for the ammount of hearts you have displaying on the hud

        switch (other.tag)
        {
            case "Enemy":
                DealDamage(1);
                break;
            case "HealthItem":
                HeartHealth++;
                hearts[i].sprite = Full;
                HealthItem.SetActive(false);
                break;
            case "ReaperScythe":
                DealDamage(2);
                break;
            default:
                break;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
            enemiesTouching--;
        if(collision.tag == "ReaperScythe")
            enemiesTouching--;

        if (enemiesTouching < 0)
            enemiesTouching = 0;
    }

    void DealDamage(int _amount)
    {
        int i = 0;
        enemiesTouching++;
        if (isInvincible)
            return;

        AudioSource.PlayClipAtPoint(damage, transform.position);

        HeartHealth -= _amount;
        hearts[i].sprite = Empty;
        StartCoroutine(Invincibility());

        if (HeartHealth <= 0)
        {
            MusicController.Instance.Music_Src.Stop();//stops music 
            MusicController.Instance.PlaySFX("Game_Over");//plays game over jingle
            SceneManager.LoadScene(2);
            HeartHealth = 3;
        }
    }

    IEnumerator Invincibility()
    {
        isInvincible = true;

        yield return new WaitForSeconds(invincibilityPeriod);
        isInvincible = false;
    }
}
