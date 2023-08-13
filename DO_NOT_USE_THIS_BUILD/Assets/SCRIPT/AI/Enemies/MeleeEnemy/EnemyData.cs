using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyData : Enemy
{
    public float Speed;
    private float initialSpeed;
    [SerializeField] private float speedRange = 0.5f;
    public float Damage;
    public Rigidbody2D Rb;

    private void OnEnable()
    {
        initialSpeed = Speed;
        Speed = initialSpeed;

        Speed += Random.Range(-speedRange, speedRange);
    }
}
