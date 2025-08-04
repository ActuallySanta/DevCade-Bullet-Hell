using UnityEngine;
using Rewired;
using System.Collections;
using System;
using Unity.VisualScripting;
using System.Xml;
public class PlayerController : MonoBehaviour
{
    public PlayerData data;
    [SerializeField] Animator anim;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] PlayerInputController input;
    [SerializeField] SpriteRenderer sprite;

    private Color originalColor;

    private PlayerStateMachine stateMachine;
    private Player player;

    public delegate void OnPlayerHurtEventHandler(object sender, float newHealthVal);
    public event OnPlayerHurtEventHandler onPlayerHurt;

    public delegate void OnPlayerDieEventHandler(GameObject sender);
    public event OnPlayerDieEventHandler onPlayerDie;

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

        originalColor = sprite.color;

        stateMachine.Initialize(idleState);
    }

    // Update is called once per frame
    void Update()
    {
        //Get player movement input
        inputVector = new Vector2(player.GetAxisRaw("HorizontalMovement"), player.GetAxisRaw("VerticalMovement"));

        //TODO Remove after testing
        if (Input.GetKeyDown(KeyCode.G)) { TakeDamage(1); }


        if (isInvincible) DamageFlash();

        //Do the current state's update function 
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

    public virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 9)//Enemy bullet layer
        {
            BulletController bullet = collision.gameObject.GetComponent<BulletController>();

            TakeDamage(bullet.data.bulletDamage);
        }
    }

    public void TakeDamage(float _damage)
    {
        if (isInvincible) return;

        currHealth -= _damage;

        //If there are any active listeners for the event, invoke it with the given arguments
        onPlayerHurt?.Invoke(gameObject, currHealth);

        //Check if the player has died
        if (currHealth <= 0)
        {
            OnPlayerDie();
        }

        //Begin the invincibility timer
        isInvincible = true;
        StartCoroutine(nameof(ResetInvincibililty));

    }

    /// <summary>
    /// The method called <see langword="when"/> the player dies
    /// </summary>
    private void OnPlayerDie()
    {
        Debug.Log($"Player {player.id} has died");

        //If there are any active listeners for the event, invoke it with the given arguments
        onPlayerDie?.Invoke(gameObject);

        Destroy(gameObject);
    }

    private IEnumerator ResetInvincibililty()
    {
        yield return new WaitForSeconds(data.invincibliltyCooldown);
        isInvincible = false;

        //Reset color
        sprite.color = originalColor;
    }

    /// <summary>
    /// Fade the alpha value of the color of the sprite with a Cosine wave
    /// </summary>
    private void DamageFlash()
    {
        sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, Math.Abs(Mathf.Cos(Time.time * flashFreq)));
    }
}
