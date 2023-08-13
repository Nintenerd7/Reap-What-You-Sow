using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathKillerBullet : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var enemy = collision.gameObject.GetComponent<IDamagable>();
        if (enemy is not null)
        {
            enemy.Damage(10);//enemy takes damage from bullets      
        }
        else if (collision.gameObject.tag == "GrimReaper")
        {
            collision.gameObject.GetComponent<ReaperHearts>().DealDamage();
            Destroy(gameObject);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }
}
