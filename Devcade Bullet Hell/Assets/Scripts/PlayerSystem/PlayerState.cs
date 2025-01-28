using Unity.VisualScripting;
using UnityEngine;

public abstract class PlayerState
{
    public string animName;
    public float startTime;
    
    protected Animator anim;
    protected PlayerController controller;

    public PlayerState(string animName, float startTime, Animator anim, PlayerController controller)
    {
        this.animName = animName;
        this.startTime = Time.time;
        this.anim = anim;
        this.controller = controller;
    }

    public abstract void DoChecks();

    public abstract void LogicUpdate();

    public abstract void PhysicsUpdate();

    public abstract void OnEnter();

    public abstract void OnExit();
}
