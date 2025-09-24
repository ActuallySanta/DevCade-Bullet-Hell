using Unity.VisualScripting;
using UnityEngine;

public abstract class PlayerState
{
    //The name of the animation clip that the state is tied to, will be played for the duration of the state
    public string animName;

    //The time that the state was entered
    public float startTime;

    protected PlayerStateMachine stateMachine;

    /// <summary>
    /// The animator component that is attached to the central game object
    /// </summary>
    protected Animator anim;

    /// <summary>
    /// The player controller component that is using this state
    /// </summary>
    protected PlayerController controller;

    /// <summary>
    /// The class data 
    /// </summary>
    protected PlayerData data;

    public PlayerState(string animName, Animator anim, PlayerController controller, PlayerData data,PlayerStateMachine stateMachine)
    {
        this.animName = animName;
        this.anim = anim;
        this.controller = controller;
        this.data = data;
        this.stateMachine = stateMachine;
    }

    public abstract void DoChecks();

    public abstract void LogicUpdate();

    public abstract void PhysicsUpdate();

    public virtual void OnEnter()
    {
        startTime = Time.time;
        //anim.SetBool(animName, true);

    }

    public virtual void OnExit()
    {
        //anim.SetBool(animName, false);
    }
}
