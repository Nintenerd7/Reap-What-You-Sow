using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class State : MonoBehaviour
{
    public enum EVENT
    {
        ENTER, UPDATE, EXIT
    };
    public EVENT stage;


    protected Animator anim;
    protected State nextState;
    protected State previousState;

    public bool isRootState = false;
    protected State currentSuperState;

    [SerializeField] protected State currentSubState;

    public State CurrentSuperState { get { return currentSuperState; } }
    public State CurrentSubState { get { return currentSubState; } }

    public virtual void Enter()
    {
        stage = EVENT.UPDATE;
    }

    public virtual void Tick()
    {
        stage = EVENT.EXIT;
    }

    public virtual void Exit()
    {
        stage = EVENT.ENTER;
        currentSubState = null;
        currentSuperState = null;
    }
    public State Process()
    {
        if (stage == EVENT.ENTER)
        {
            Enter();
        }
        if (stage == EVENT.UPDATE)
        {
            Tick();
        }
        if (stage == EVENT.EXIT)
        {
            Exit();
            return nextState;
        }
        return this;
    }

    public virtual void CheckStateSwitch()
    {

    }

    public virtual void InitializeSubState()
    {

    }

    public virtual void SetSuperState(State _newSuperState)
    {
        currentSuperState = _newSuperState;
    }

    public virtual void SetSubState(State _newSubState)
    {
        currentSubState = _newSubState;
        _newSubState.SetSuperState(this);
        _newSubState.stage = EVENT.ENTER;
    }

    public virtual void UpdateStates()
    {
        Process();
        if (currentSubState != null)
        {
            currentSubState.UpdateStates();
        }
    }

    public virtual void ExitStates()
    {
        stage = EVENT.EXIT;
        if (currentSubState != null)
        {
            currentSubState.ExitStates();
        }
    }
}
