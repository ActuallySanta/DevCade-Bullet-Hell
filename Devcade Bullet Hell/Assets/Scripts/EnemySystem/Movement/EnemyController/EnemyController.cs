using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyController : MonoBehaviour
{
    [SerializeField] Animator anim;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] NavMeshAgent navAgent;
    
    public EnemyData data;

    public List<Transform> patrolPoints = new List<Transform>();

    private EnemyStateMachine stateMachine;

    public EnemyIdleState idleState { get; private set; }
    public EnemyMoveState moveState { get; private set; }
    public EnemyHurtState hurtState { get; private set; }
    public EnemyDeadState deadState { get; private set; }
    public EnemyAttackState attackState { get; private set; }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
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

    /// <summary>
    /// Set the navMesh destination for this enemy
    /// </summary>
    /// <param name="patrolPoint">The transform of the patrol point to move to</param>
    public void SetPatrolDestination(Transform patrolPoint)
    {
        navAgent.SetDestination(patrolPoint.position);
    }
}
