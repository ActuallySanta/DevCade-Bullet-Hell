using UnityEngine;
using Rewired;
public class PlayerInputController : MonoBehaviour
{
    public int PlayerID  { get; private set; }
    public Player player { get; private set; }

    // Update is called once per frame
    void Update()
    {
        if (player == null)
        {
            Debug.Log("NO PLAYER");
            return;
        }
    }

    public void InitializePlayer(int _playerID)
    {
        Debug.Log(player);
        PlayerID = _playerID;
        player = ReInput.players.GetPlayer(PlayerID);
        Debug.Log(player);
    }
}
