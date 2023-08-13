using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrimReaperAttack : State
{
    [SerializeField] private EnemyStateMachine stateMachine;
    [SerializeField] private ReaperData reaperData;
    [SerializeField] private State follow;
    [SerializeField] private State scytheSwipe;
    private Rigidbody2D rb;

    float followTimerMax = 2f;
    float followTimer = 0;

    float dashVelocity = 30;

    bool isCoolingDown = false;

    Quaternion newAngle = new();
    public override void Enter()
    {
        stateMachine = transform.root.GetComponent<EnemyStateMachine>();
        reaperData = transform.root.GetComponent<ReaperData>();
        rb = reaperData.Rb;
        rb.velocity = Vector2.zero;

        followTimer = 0;

        reaperData.scythe.gameObject.SetActive(true);

        Debug.Log("doing an attack");


        base.Enter();
    }

    public override void Tick()
    {
        if (isCoolingDown)
            return;

        if (followTimer > followTimerMax)
            stateMachine.ChangeState(follow);



        Vector2 _aimDirection = new();
        _aimDirection = reaperData.Target.position - transform.position;
        float _aimAngle = Mathf.Atan2(_aimDirection.y, _aimDirection.x) * (Mathf.Rad2Deg);

        if (reaperData.visuals.localScale.x > 0)
            newAngle.eulerAngles = new Vector3(0, 0, _aimAngle);
        else
            newAngle.eulerAngles = new Vector3(0, 0, -_aimAngle + 180);

        rb.velocity = new Vector2(_aimDirection.normalized.x * dashVelocity, _aimDirection.normalized.y * dashVelocity);



        reaperData.scythe.localRotation = newAngle;

        if (Vector3.Distance(reaperData.transform.position, reaperData.Target.transform.position) < 3 && !isCoolingDown)
            stateMachine.ChangeState(scytheSwipe);

        followTimer += Time.deltaTime;
    }

    IEnumerator Cooldown()
    {
        isCoolingDown = true;
        rb.velocity = Vector2.zero;
        yield return new WaitForSeconds(2);
        isCoolingDown = false;
        stateMachine.ChangeState(scytheSwipe);
    }
}