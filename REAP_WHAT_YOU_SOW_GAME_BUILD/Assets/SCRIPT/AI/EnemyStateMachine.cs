using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine : MonoBehaviour
{
    public Animator anim;
    public State currentState;
    public State currentSubState;

    [SerializeField] private State initialState = null;
    public GameObject stateParent;

    

    private StateMachine stateMachine;


    private void OnEnable()
    {
        StateMachine.ChangeState(initialState);
    }

    private StateMachine StateMachine
    {
        get
        {
            if (stateMachine != null)
            {
                return stateMachine;
            }

            stateMachine = new StateMachine(initialState);

            return stateMachine;
        }

    }

    public void Tick()
    {
        currentState = StateMachine.CurrentState;
        currentSubState = currentState.CurrentSubState;

        StateMachine.StateMachineTick();

    }

    public void ChangeState(State state)
    {
        StateMachine.ChangeState(state);
    }

}
