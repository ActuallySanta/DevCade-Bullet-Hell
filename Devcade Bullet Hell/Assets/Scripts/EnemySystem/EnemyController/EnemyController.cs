using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyController : MonoBehaviour
{
    [SerializeField] protected Animator anim;

    public EnemyData data;

    protected float currHealth;

    [HideInInspector] public List<Transform> patrolPoints = new List<Transform>();
    [HideInInspector] public Transform targetGameObject;

    protected EnemyStateMachine stateMachine;

    public EnemyIdleState idleState { get; protected set; }
    public EnemyMoveState moveState { get; protected set; }
    public EnemyHurtState hurtState { get; protected set; }
    public EnemyDeadState deadState { get; protected set; }
    public EnemyAttackState attackState { get; protected set; }
    [HideInInspector] public bool canBeHurt = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public virtual void Start()
    {
        currHealth = data.maxHealth;

        stateMachine = new EnemyStateMachine();
        idleState = new EnemyIdleState("idle", anim, this, data, stateMachine);
        hurtState = new EnemyHurtState("hurt", anim, this, data, stateMachine);
        deadState = new EnemyDeadState("dead", anim, this, data, stateMachine);
        stateMachine.Initialize(idleState);
    }

    // Update is called once per frame
    public virtual void Update()
    {
        stateMachine.CurrState.DoChecks();
        stateMachine.CurrState.LogicUpdate();
    }

    public virtual void FixedUpdate()
    {
        stateMachine.CurrState.PhysicsUpdate();
    }

    public virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 7)//Player bullet layer
        {
            BulletController bullet = collision.gameObject.GetComponent<BulletController>();

            currHealth -= bullet.data.bulletDamage;

            //Check if you need to die
            if (currHealth <= 0)
            {
                stateMachine.ChangeState(deadState);
            }
            else
            {
                stateMachine.ChangeState(hurtState);
            }
        }
    }

    public virtual void DestroyEnemy()
    {
        Debug.Log("Destroyed: " + name);
        Destroy(gameObject);
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
