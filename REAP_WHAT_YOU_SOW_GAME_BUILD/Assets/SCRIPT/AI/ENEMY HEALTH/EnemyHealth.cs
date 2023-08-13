using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, IDamagable
{
    public int HitPoints;
    public int maxHitPoints = 5;
    public SpriteRenderer sprite;
    [SerializeField] private Color initialColor = new();

    bool isFlashing = false;
    bool isDying = false;

    public Rigidbody2D rb;

    [SerializeField] AudioClip damage;

    void OnEnable()
    {
        isFlashing = false;
        isDying = false;
        HitPoints = maxHitPoints;
        
        if(initialColor == new Color())
            initialColor = sprite.color;

        sprite.color = initialColor;
    }

    public IEnumerator FlashRed()
    {
        isFlashing = true;
        sprite.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        sprite.color = initialColor;
        isFlashing = false;
    }

    public void Damage(int _amount)
    {
        AudioSource.PlayClipAtPoint(damage, transform.position);

        HitPoints -= _amount;
        if(!isFlashing)
            StartCoroutine(FlashRed());

        if (HitPoints <= 0 )
            if(!isDying)
                GetComponent<Enemy>().TurnIntoCorpse();
       
    }

  

}
