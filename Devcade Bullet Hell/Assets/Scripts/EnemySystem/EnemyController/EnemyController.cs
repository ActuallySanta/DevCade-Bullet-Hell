using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
[RequireComponent(typeof(Animator))]
public class EnemyController : MonoBehaviour
{
    //A reference to the attached animator component
    [SerializeField] protected Animator anim;

    public Collider2D col { get; protected set; }

    //The scriptable object that is tied to this instance of the script
    public EnemyData data;

    //The amount of health that the enemy currently has
    protected float currHealth;

    //If the enemy is a patrol type, a list of the points that the enemy will move between
    [HideInInspector] public List<Transform> patrolPoints = new List<Transform>();

    //If the enemy is a chasing type, a reference to the position that the enemy will attempt to move towards
    [HideInInspector] public Transform targetGameObject;

    //The instance of the stateMachine that handles state transitions, created by each enemy controller script
    protected EnemyStateMachine stateMachine;

    //All of the possible enemy states that the enemy can used (the most generic version so that child versions of the controller can specify more complex behaviours)
    public EnemyIdleState idleState { get; protected set; }
    public EnemyMoveState moveState { get; protected set; }
    public EnemyHurtState hurtState { get; protected set; }
    public EnemyDeadState deadState { get; protected set; }
    public EnemyAttackState attackState { get; protected set; }

    //Detects if the enemy is invincible
    [HideInInspector] public bool canBeHurt = true;

    //The position that the enemy's attacks will originate from
    public Transform firePoint { get; protected set; }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public virtual void Start()
    {
        firePoint = transform.Find("Fire Point").transform;

        col = GetComponent<Collider2D>();

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
        //Check for damage
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

    /// <summary>
    /// Used by the attack states, spawn a bullet on the enemy's fire point position
    /// </summary>
    /// <param name="bulletToSpawn">The bullet prefab that will be spawned</param>
    /// <param name="direction">The vector direction that will show where the bullet is firing</param>
    /// <param name="data">The specific bullet data that the bullet will use</param>
    public void SpawnBullet(GameObject bulletToSpawn, Vector2 direction, BulletData data)
    {
        BulletController controller = Instantiate(bulletToSpawn, transform).GetComponent<BulletController>();

        controller.transform.parent = null;
        
        controller.InitializeBullet(data, gameObject, direction);

    }
}
