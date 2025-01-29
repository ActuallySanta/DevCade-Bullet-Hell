using UnityEngine;
using Rewired;
public class PlayerController : MonoBehaviour
{
    public PlayerData data;
    [SerializeField] Animator anim;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] PlayerInputController input;

    private PlayerStateMachine stateMachine;
    private Player player;

    //Player Input
    public Vector2 inputVector { get; private set; }

    public Player_IdleState idleState { get; private set; }
    public Player_MoveState moveState { get; private set; }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = input.player;

        stateMachine = new PlayerStateMachine();

        idleState = new Player_IdleState("idle", anim, this, data,stateMachine);
        moveState = new Player_MoveState("moving", anim, this, data, stateMachine);
        
        
        stateMachine.Initialize(idleState);
    }

    // Update is called once per frame
    void Update()
    {
        //Get player movement input
        inputVector = new Vector2(player.GetAxisRaw("HorizontalMovement"), player.GetAxisRaw("VerticalMovement"));
        


        stateMachine.CurrState.DoChecks();
        stateMachine.CurrState.LogicUpdate();
    }

    private void FixedUpdate()
    {
        stateMachine.CurrState.PhysicsUpdate();
    }

    public void SetVelocity(float _vel)
    {
        rb.linearVelocity = inputVector * _vel;
    }
}
