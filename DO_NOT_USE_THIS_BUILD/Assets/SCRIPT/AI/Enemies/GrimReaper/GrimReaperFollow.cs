using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrimReaperFollow : State
{
    [SerializeField] private EnemyStateMachine stateMachine;
    [SerializeField] private ReaperData reaperData;
    [SerializeField] private State attack;
    [SerializeField] private State scytheSwipe;
    private Rigidbody2D rb;

    [SerializeField] private float followTimerMax = 20f;
    [SerializeField] private float followTimerMin = 5f;
    private float followTimerGoal;
    [SerializeField] private float followTimer = 0;

    [SerializeField] private float enemyAmountMin = 1;
    [SerializeField] private float enemyAmountMax = 3;

    [SerializeField] private float spawnDistanceMin = 10f;
    [SerializeField] private float spawnDistanceMax = 15f;

    [SerializeField] private float spawnTimerMin = 1f;
    [SerializeField] private float spawnTimerMax = 5f;
    [SerializeField] private float spawnTimer = 0;
    [SerializeField] private float spawnWhen = 1;
    public override void Enter()
    {
        stateMachine = transform.root.GetComponent<EnemyStateMachine>();
        reaperData = transform.root.GetComponent<ReaperData>();
        rb = reaperData.Rb;
        reaperData.scythe.gameObject.SetActive(false);
        anim = stateMachine.anim;

        anim.Play("Walk");

        followTimer = 0;
        followTimerGoal = Random.Range(followTimerMin, followTimerMax);

        base.Enter();
    }

    public override void Tick()
    {
        if (followTimer > followTimerGoal)
            stateMachine.ChangeState(attack);

        if (Vector3.Distance(reaperData.transform.position, reaperData.Target.transform.position) < 3)
            stateMachine.ChangeState(scytheSwipe);

        if (spawnTimer >= spawnWhen)
        {
            int _enemyAmount = (int)Random.Range(enemyAmountMin, enemyAmountMax);
            Vector3 _targetPosition;
            _targetPosition = SpawnEnemies(OffsetSpawn(reaperData.Target.position, spawnDistanceMin, spawnDistanceMax));
            for (int _i = 0; _i < _enemyAmount - 1; _i++)
            {
                _targetPosition = SpawnEnemies(OffsetSpawn(_targetPosition, 0.5f, 1f));
            }

            spawnWhen = Random.Range(spawnTimerMin, spawnTimerMax);
            spawnTimer = 0;
        }

        var _direction = reaperData.Target.position - transform.root.position;
        rb.AddRelativeForce(_direction.normalized * (reaperData.Speed * Time.fixedDeltaTime), ForceMode2D.Force);

        FlipHorizontal();

        followTimer += Time.fixedDeltaTime;
        spawnTimer += Time.fixedDeltaTime;

    }

    Vector3 OffsetSpawn(Vector3 _initialLocation, float _minDistance, float _maxDistance)
    {
        float _angle = Random.Range(-Mathf.PI, Mathf.PI);
        float _distance = Random.Range(_minDistance, _maxDistance);
        Vector3 _offset = new Vector3(Mathf.Cos(_angle), Mathf.Sin(_angle), 0) * _distance;


        _initialLocation += _offset;
        return _initialLocation;
    }

    Vector3 SpawnEnemies(Vector3 _spawnLocation)
    {
        var _enemyInstance = SpawnManager.Instance.enemyDictionary[reaperData.enemies[0]].Get();
        _enemyInstance.transform.position = _spawnLocation;
        return _enemyInstance.transform.position;
    }

    void FlipHorizontal()
    {
        if (reaperData.Target.position.x < reaperData.transform.position.x)
        {
            reaperData.visuals.localScale = new Vector3(-2, reaperData.visuals.localScale.y, reaperData.visuals.localScale.z);
        }
        else
        {
            reaperData.visuals.localScale = new Vector3(2, reaperData.visuals.localScale.y, reaperData.visuals.localScale.z);
        }
    }

    private void OnDrawGizmos()
    {
        //Gizmos.color = Color.red;
        //Gizmos.DrawSphere(transform.position, 4);
    }
}