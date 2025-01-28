using Unity.VisualScripting;
using UnityEngine;

public abstract class PlayerState
{
    public string animName;
    public float startTime;
    
    protected Animator anim;
    protected PlayerController controller;
    protected PlayerData data;

    public PlayerState(string animName, Animator anim, PlayerController controller, PlayerData data)
    {
        this.animName = animName;
        this.anim = anim;
        this.controller = controller;
        this.data = data; 
    }

    public abstract void DoChecks();

    public abstract void LogicUpdate();

    public abstract void PhysicsUpdate();

    public virtual void OnEnter()
    {
        startTime = Time.time;
    }

    public abstract void OnExit();
}
