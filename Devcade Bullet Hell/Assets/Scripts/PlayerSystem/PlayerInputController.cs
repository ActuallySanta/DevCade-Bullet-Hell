using UnityEngine;
using Rewired;
public class PlayerInputController : MonoBehaviour
{
    public int PlayerID  { get; private set; }
    public Player player { get; private set; }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (player == null) InitializePlayer(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitializePlayer(int _playerID)
    {
        PlayerID = _playerID;
        player = ReInput.players.GetPlayer(_playerID);
    }
}
