using Unity.VisualScripting;
using UnityEngine;

public abstract class PlayerState
{
    public string animName;
    public float startTime;

    protected PlayerStateMachine stateMachine;

    protected Animator anim;
    protected PlayerController controller;
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
        anim.SetBool(animName, true);

    }

    public virtual void OnExit()
    {
        anim.SetBool(animName, false);
    }
}
