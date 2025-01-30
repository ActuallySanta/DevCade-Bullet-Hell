using Unity.VisualScripting;
using UnityEngine;

public abstract class EnemyState
{
    public string animName;
    public float startTime;

    protected PlayerStateMachine stateMachine;

    protected Animator anim;
    protected EnemyController controller;
    protected EnemyData data;

    public EnemyState(string animName, Animator anim, EnemyController controller, EnemyData data,PlayerStateMachine stateMachine)
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
