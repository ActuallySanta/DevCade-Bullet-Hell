using UnityEngine;

public class PlayerStateMachine
{
    public PlayerState CurrState { get; private set; }


    public void Initialize(PlayerState _startingState)
    {
        CurrState = _startingState;
        //Debug.Log(currState);
        CurrState.OnEnter();
    }

    public void ChangeState(PlayerState destinationState)
    {
        if (CurrState != null)
        {
            CurrState.OnExit();
            CurrState = destinationState;
            CurrState.OnEnter();

        }
    }
}
