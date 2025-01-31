using UnityEngine;

public class EnemyStateMachine : MonoBehaviour
{
    public EnemyState CurrState { get; private set; }


    public void Initialize(EnemyState _startingState)
    {
        CurrState = _startingState;
        //Debug.Log(currState);
        CurrState.OnEnter();
    }

    public void ChangeState(EnemyState destinationState)
    {
        if (CurrState != null)
        {
            CurrState.OnExit();
            CurrState = destinationState;
            CurrState.OnEnter();

        }
    }
}
