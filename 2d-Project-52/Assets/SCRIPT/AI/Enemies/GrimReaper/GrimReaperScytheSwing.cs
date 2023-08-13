using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrimReaperScytheSwing : State
{
    [SerializeField] private EnemyStateMachine stateMachine;
    [SerializeField] private ReaperData reaperData;
    [SerializeField] private State follow;
    private Rigidbody2D rb;

    [SerializeField] float timerMax = 3f;
    float timer = 0;

    Quaternion newAngle = new();
    public override void Enter()
    {
        stateMachine = transform.root.GetComponent<EnemyStateMachine>();
        reaperData = transform.root.GetComponent<ReaperData>();
        rb = reaperData.Rb;
        rb.velocity = Vector2.zero;
        anim = stateMachine.anim;

        anim.Play("Swing");
        Vector2 _aimDirection = new();
        _aimDirection = reaperData.Target.position - transform.position;
        float _aimAngle = Mathf.Atan2(_aimDirection.y, _aimDirection.x) * (Mathf.Rad2Deg);

        if (reaperData.visuals.localScale.x > 0)
            newAngle.eulerAngles = new Vector3(0, 0, _aimAngle);
        else
            newAngle.eulerAngles = new Vector3(0, 0, _aimAngle + 180);





        reaperData.scythe.localRotation = newAngle;

        reaperData.scythe.gameObject.SetActive(true);
        timer = 0;


        base.Enter();
    }

    public override void Tick()
    {
        if (timer > timerMax)
            stateMachine.ChangeState(follow);



        timer += Time.deltaTime;
    }
}
