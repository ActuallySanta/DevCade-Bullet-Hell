using UnityEngine;

public class PlayerStateMachine 
{
    public PlayerState currState { get; private set; }

    public void ChangeState(PlayerState destinationState)
    {
        currState.OnExit();
        currState = destinationState;
        currState.OnEnter();
    }
}
