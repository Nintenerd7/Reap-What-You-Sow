
public class StateMachine
{
    public StateMachine(State startingState) => ChangeState(startingState);

    public State CurrentState { get; private set; }

    public State PreviousState { get; private set; }

    public void ChangeState(State _state)
    {
        CurrentStateNullCheck();

        if (_state.isRootState)
        {

            CurrentState = _state;

            CurrentState.stage = State.EVENT.ENTER;
        }
        else
        {
            CurrentState.SetSubState(_state);

        }
    }

    void CurrentStateNullCheck()
    {
        if (CurrentState != null)
        {
            CurrentState.stage = State.EVENT.EXIT;

            PreviousState = CurrentState;
        }
    }

    public void StateMachineTick()
    {
        CurrentState.UpdateStates();
    }
}
