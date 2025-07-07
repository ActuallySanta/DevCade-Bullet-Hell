using UnityEngine;
using Rewired;
using System.Collections;
using System;
public class PlayerController : MonoBehaviour
{
    public PlayerData data;
    [SerializeField] Animator anim;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] PlayerInputController input;
    [SerializeField] SpriteRenderer sprite;

    private PlayerStateMachine stateMachine;
    private Player player;

    public delegate void OnPlayerHurtEventHandler(object sender, float newHealthVal);
    public event OnPlayerHurtEventHandler onPlayerHurt;

    private bool isInvincible = false;
    public float currHealth;
    [SerializeField] private float flashFreq = .05f;

    //Player Input
    public Vector2 inputVector { get; private set; }

    public Player_IdleState idleState { get; private set; }
    public Player_MoveState moveState { get; private set; }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = input.player;

        currHealth = data.maxHealth;

        stateMachine = new PlayerStateMachine();

        idleState = new Player_IdleState("idle", anim, this, data, stateMachine);
        moveState = new Player_MoveState("moving", anim, this, data, stateMachine);


        stateMachine.Initialize(idleState);
    }

    // Update is called once per frame
    void Update()
    {
        //Get player movement input
        inputVector = new Vector2(player.GetAxisRaw("HorizontalMovement"), player.GetAxisRaw("VerticalMovement"));

        if (Input.GetKeyDown(KeyCode.G)) { TakeDamage(1); }
        
        DamageFlash();
        
        //if (isInvincible)

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

    public void TakeDamage(float _damage)
    {
        if (isInvincible) return;

        currHealth -= _damage;
        onPlayerHurt?.Invoke(gameObject, currHealth);

        if (currHealth <= 0)
        {
            OnPlayerDie();
        }

        isInvincible = true;
        StartCoroutine(nameof(ResetInvincibililty));

    }

    private void OnPlayerDie()
    {
        Debug.Log($"Player {player.id} has died");
    }

    private IEnumerator ResetInvincibililty()
    {
        yield return new WaitForSeconds(data.invincibliltyCooldown);
        isInvincible = false;
        //Reset color
        sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, 255);
    }

    private void DamageFlash()
    {
        sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, Mathf.Cos(Time.time * flashFreq));
    }
}
