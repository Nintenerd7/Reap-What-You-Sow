using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Enemy : MonoBehaviour
{
    private Action<Enemy, Enemy> killAction;
    private EnemyStateMachine stateMachine;
    [SerializeField] private Enemy prefab;
    public Transform Target;
    public SpriteRenderer spriteRenderer;
    [SerializeField] GameObject energy;
    public void Init(Action<Enemy, Enemy> _killAction)
    {
        killAction = _killAction;
    }

    public void TurnIntoCorpse()
    {
        Instantiate(energy, transform.position, Quaternion.identity);
        Kill();
    }

    public void Kill()
    {
        killAction(SpawnManager.Instance.enemies[0], this);
    }

}
