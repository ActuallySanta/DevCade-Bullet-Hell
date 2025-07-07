using UnityEngine;
using Rewired;
using System.Collections.Generic;
using System.Collections;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

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
    [SerializeField] Slider p1HealthBar;
    [SerializeField] Slider p2HealthBar;
    [SerializeField] GameObject playerUIGameObject;

    public static GamePlayManager Instance;

    private GameObject p1;
    private GameObject p2;

    public bool isPlaying { get; private set; }
    [SerializeField] float timeBeforeRoundStart = 3f;

    private void Start()
    {
        //TODO Remove after testing
        StartCoroutine(nameof(BeginPlayerSpawning));
    }

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

        DontDestroyOnLoad(Instance);
        SceneManager.activeSceneChanged += SceneManager_activeSceneChanged;
    }

    private void SceneManager_activeSceneChanged(Scene arg0, Scene arg1)
    {
        if (arg1.name == "Game Scene")
        {
            StartCoroutine(nameof(BeginPlayerSpawning));
        }
    }

    private IEnumerator BeginPlayerSpawning()
    {
        yield return new WaitForSeconds(timeBeforeRoundStart);
        SpawnPlayers();
    }

    /// <summary>
    /// Spawns in the players
    /// </summary>
    void SpawnPlayers()
    {
        if (currMode == PlayerMode.SinglePlayer)
        {
            p1 = Instantiate(playerPrefab, p1Spawnpoint);

            p1.transform.parent = null;

            p1.GetComponent<PlayerInputController>().InitializePlayer(0);
            p1.GetComponent<PlayerController>().data = p1Data;
            p1.GetComponent<PlayerController>().onPlayerHurt += UpdatePlayerHealthBars;
            p1.GetComponent<PlayerWeaponHandler>().data = p1Data;
            p1.GetComponent<PlayerWeaponHandler>().activeWeapons = p1Weapons;
            p1HealthBar.maxValue = p1Data.maxHealth;
        }
        else if (currMode == PlayerMode.TwoPlayer)
        {
            p1 = Instantiate(playerPrefab, p1Spawnpoint);

            p1.transform.parent = null;

            p1.GetComponent<PlayerInputController>().InitializePlayer(0);
            p1.GetComponent<PlayerController>().data = p1Data;
            p1.GetComponent<PlayerController>().onPlayerHurt += UpdatePlayerHealthBars;
            p1.GetComponent<PlayerWeaponHandler>().data = p1Data;
            p1.GetComponent<PlayerWeaponHandler>().activeWeapons = p1Weapons;
            p1HealthBar.maxValue = p1Data.maxHealth;

            p2 = Instantiate(playerPrefab, p1Spawnpoint);

            p2.transform.parent = null;

            p2.GetComponent<PlayerInputController>().InitializePlayer(1);
            p2.GetComponent<PlayerController>().data = p2Data;
            p2.GetComponent<PlayerController>().onPlayerHurt += UpdatePlayerHealthBars;
            p2.GetComponent<PlayerWeaponHandler>().data = p2Data;
            p2.GetComponent<PlayerWeaponHandler>().activeWeapons = p2Weapons;
            p2HealthBar.maxValue = p2Data.maxHealth;
        }
    }

    /// <summary>
    /// Used by the UI buttons to select player count
    /// </summary>
    /// <param name="gameMode">how many players are in the game</param>
    public void SelectGameMode(float gameMode)
    {
        if (gameMode == 1) currMode = PlayerMode.SinglePlayer;
        if (gameMode == 2) currMode = PlayerMode.TwoPlayer;
    }

    public void UpdateScore(float value)
    {
        OnScoreUpdate(this);

        currentScore += value;

        scoreText.text = "SCORE: " + currentScore.ToString();
    }

    public void UpdatePlayerHealthBars(object sender, float newHealthVal)
    {
        if ((GameObject)sender == p1)
        {
            Debug.Log("Updated Player 1's health bar");
            p1HealthBar.value = newHealthVal;
        }

        if ((GameObject)sender == p2)
        {
            Debug.Log("Updated Player 2's health bar");
            p2HealthBar.value = newHealthVal;
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Game Scene");
        playerUIGameObject.SetActive(true);
    }
}
