using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveTowards : State
{
    [SerializeField] private EnemyStateMachine stateMachine;
    [SerializeField] private EnemyData enemyData;
    private Rigidbody2D rb;

    public override void Enter()
    {
        rb = enemyData.Rb;
        enemyData.GetComponent<Collider2D>().isTrigger = false;
        anim = stateMachine.anim;
        enemyData.gameObject.tag = "Enemy";
        anim.Play("Walk");
        base.Enter();
    }

    public override void Tick()
    {

        var _direction = enemyData.Target.position - enemyData.transform.position;
        rb.velocity = (Time.fixedDeltaTime * enemyData.Speed) * _direction.normalized;

        FlipHorizontal();
    }

    void FlipHorizontal()
    {
        if(enemyData.Target.position.x < enemyData.transform.position.x)
        {
            enemyData.spriteRenderer.flipX = true;
        }
        else
        {
            enemyData.spriteRenderer.flipX = false;
        }
    }
}
