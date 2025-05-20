using UnityEngine;
using Rewired;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;

public enum PlayerMode
{
    SinglePlayer,
    TwoPlayer
}

public class GamePlayManager : MonoBehaviour
{
    public PlayerMode currMode;

    private Player player1;
    private Player player2;

    [SerializeField] Transform p1Spawnpoint;
    [SerializeField] Transform p2Spawnpoint;

    [SerializeField] GameObject playerPrefab;

    public List<PlayerWeaponData> p1Weapons = new List<PlayerWeaponData>();
    public List<PlayerWeaponData> p2Weapons = new List<PlayerWeaponData>();

    public PlayerData p1Data;
    public PlayerData p2Data;


    public delegate void OnScoreUpdateHandler(object sender);
    public OnScoreUpdateHandler OnScoreUpdate;
    public float currentScore { get; private set; }

    public delegate void OnLifeUpdateHandler(object sender);
    public OnLifeUpdateHandler OnLifeUpdate;
    public float currLives { get; private set; }

    [SerializeField] TMP_Text scoreText;

    public static GamePlayManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (currMode == PlayerMode.SinglePlayer)
        {
            GameObject p1 = Instantiate(playerPrefab, p1Spawnpoint);

            p1.transform.parent = null;

            p1.GetComponent<PlayerInputController>().InitializePlayer(0);
            p1.GetComponent<PlayerController>().data = p1Data;
            p1.GetComponent<PlayerWeaponHandler>().data = p1Data;
            p1.GetComponent<PlayerWeaponHandler>().activeWeapons = p1Weapons;
        }
        else if (currMode == PlayerMode.TwoPlayer)
        {
            GameObject p1 = Instantiate(playerPrefab, p1Spawnpoint);

            p1.transform.parent = null;

            p1.GetComponent<PlayerInputController>().InitializePlayer(0);
            p1.GetComponent<PlayerController>().data = p1Data;
            p1.GetComponent<PlayerWeaponHandler>().data = p1Data;
            p1.GetComponent<PlayerWeaponHandler>().activeWeapons = p1Weapons;

            GameObject p2 = Instantiate(playerPrefab, p1Spawnpoint);

            p2.transform.parent = null;

            p2.GetComponent<PlayerInputController>().InitializePlayer(1);
            p2.GetComponent<PlayerController>().data = p2Data;
            p2.GetComponent<PlayerWeaponHandler>().data = p2Data;
            p2.GetComponent<PlayerWeaponHandler>().activeWeapons = p2Weapons;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpdateScore(float value)
    {
        OnScoreUpdate(this);

        currentScore += value;

        scoreText.text = "SCORE: " + currentScore.ToString();
    }
}
