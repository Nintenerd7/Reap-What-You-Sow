using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCorpse : State
{
    [SerializeField] private EnemyStateMachine stateMachine;
    [SerializeField] private EnemyData enemyData;
    private Rigidbody2D rb;
    [SerializeField] private Sprite tombstone;


    public override void Enter()
    {
        rb = enemyData.Rb;

        enemyData.GetComponent<Collider2D>().isTrigger = true;

        anim = stateMachine.anim;
        anim.Play("Die");
        rb.velocity = Vector2.zero;

        enemyData.gameObject.tag = "EnemyCorpse";

        base.Enter();
    }

    public override void Tick()
    {

        
    }


}
