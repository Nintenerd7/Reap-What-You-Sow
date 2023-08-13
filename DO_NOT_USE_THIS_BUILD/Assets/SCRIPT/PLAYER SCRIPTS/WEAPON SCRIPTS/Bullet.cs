using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var enemy = collision.gameObject.GetComponent<IDamagable>();
        if (enemy is not null)
        {
            enemy.Damage(1);//enemy takes damage from bullets 
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collision2D collision)
    {
        
        
    }
}
