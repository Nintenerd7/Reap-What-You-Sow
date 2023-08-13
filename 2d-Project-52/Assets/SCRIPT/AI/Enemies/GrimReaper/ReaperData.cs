using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReaperData : MonoBehaviour
{
    public Transform Target;
    public Rigidbody2D Rb;
    public float Speed;
    public List<Enemy> enemies;

    public Transform visuals;
    public Transform scythe;

    public SpriteRenderer spriteRenderer;

    private void Start()
    {
        Rb = GetComponent<Rigidbody2D>();
        enemies = SpawnManager.Instance.enemies;
    }
}
