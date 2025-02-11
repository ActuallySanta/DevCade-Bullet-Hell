using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public abstract class EnemyController : MonoBehaviour
{
    [SerializeField] Animator anim;
    
    public EnemyData data;

    public List<Transform> patrolPoints = new List<Transform>();

    private EnemyStateMachine stateMachine;

    public EnemyIdleState idleState { get; private set; }
    public EnemyMoveState moveState { get; private set; }
    public EnemyHurtState hurtState { get; private set; }
    public EnemyDeadState deadState { get; private set; }
    public EnemyAttackState attackState { get; private set; }

    public bool canBeHurt = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        stateMachine = new EnemyStateMachine();
        idleState = new EnemyIdleState("idle", anim, this, data, stateMachine);
        moveState = new EnemyMoveState("move", anim, this, data, stateMachine);
        hurtState = new EnemyHurtState("hurt", anim, this, data, stateMachine);
        deadState = new EnemyDeadState("dead", anim, this, data, stateMachine);

        stateMachine.Initialize(idleState);
    }

    // Update is called once per frame
    void Update()
    {
        stateMachine.CurrState.DoChecks();
        stateMachine.CurrState.LogicUpdate();
    }

    private void FixedUpdate()
    {
        stateMachine.CurrState.PhysicsUpdate();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }

    /// <summary>
    /// Set the navMesh destination for this enemy
    /// </summary>
    /// <param name="patrolPoint">The transform of the patrol point to move to</param>
    public void SetPatrolDestination(Transform patrolPoint)
    {
        transform.position = Vector2.MoveTowards(transform.position, patrolPoint.position, data.moveSpeed * Time.deltaTime);
    }
}
