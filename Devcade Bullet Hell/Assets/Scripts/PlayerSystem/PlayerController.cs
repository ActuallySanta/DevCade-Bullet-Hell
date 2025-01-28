using UnityEngine;
using Rewired;
public class PlayerController : MonoBehaviour
{
    [SerializeField] PlayerData data;
    [SerializeField] Animator anim;
    
    PlayerStateMachine stateMachine = new PlayerStateMachine();

    private Player player;

    [SerializeField] private int playerID = 0;

    public Player_IdleState idleState { get; private set; }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = ReInput.players.GetPlayer(playerID);

        idleState = new Player_IdleState("idle", anim, this, data);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
