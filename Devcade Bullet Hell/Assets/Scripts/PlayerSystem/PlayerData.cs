using UnityEngine;

[CreateAssetMenu(fileName = "New Player Data", menuName = "Data/Create New Player Data")]
public class PlayerData : ScriptableObject
{
    public float moveSpeed = 5f;
    public float maxHealth = 5f;

}
