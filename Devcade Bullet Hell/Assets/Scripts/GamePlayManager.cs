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

public enum PlayerSelectingState
{
    NoPlayerSelecting,
    Player1Selecting,
    Player2Selecting,
    BothPlayersSelected,
}

//Made so that dictionaries show up in the inspector
#region Serializable Dictionary
[Serializable]
public class PlayerDataDictionary
{
    [SerializeField] PlayerDataDictionaryEntry[] entries;

    public Dictionary<string, PlayerData> ToDictionary()
    {
        Dictionary<string, PlayerData> dict = new Dictionary<string, PlayerData>();

        foreach (var item in entries)
        {
            dict.Add(item.name, item.data);
        }
        return dict;
    }
}

[Serializable]
public class PlayerDataDictionaryEntry
{
    [SerializeField] public string name;
    [SerializeField] public PlayerData data;
}
#endregion

public class GamePlayManager : MonoBehaviour
{
    //How many players will be playing the game
    public PlayerMode currMode { get; private set; }

    //An integer representation of the players (used for enemy spawning)
    public int playerCount { get; private set; }

    //Rewired Players (used for input)
    private Player player1;
    private Player player2;

    //How long the player will wait before respawning (in seconds)
    [SerializeField] private float playerRespawnTimer = 3f;

    //References to the empty game objects where player objects will spawn
    [SerializeField] Transform p1Spawnpoint;
    [SerializeField] Transform p2Spawnpoint;

    [SerializeField] GameObject playerPrefab;

    //A list of the active weapons equipped to each player
    public List<PlayerWeaponData> p1Weapons = new List<PlayerWeaponData>();
    public List<PlayerWeaponData> p2Weapons = new List<PlayerWeaponData>();

    //Holds a reference to each player class data along with a string of it's name
    [SerializeField] PlayerDataDictionary PlayerData;

    private Dictionary<string, PlayerData> playerDataDictionary;

    //The data for the class that each player has selected
    public PlayerData p1Data;
    public PlayerData p2Data;

    #region Events
    //Event triggered to update the Score UI
    public delegate void OnScoreUpdateHandler(object sender);
    public OnScoreUpdateHandler OnScoreUpdate;
    public float currentScore { get; private set; }

    //Event triggered when the player has to respawn, used to update UI and subtract from life pool
    public delegate void OnLifeUpdateHandler(object sender);
    public OnLifeUpdateHandler OnLifeUpdate;
    public float currLives { get; private set; }
    #endregion

    #region UI Fields
    //Displays the player(s)' score
    [SerializeField] TMP_Text scoreText;

    //Shows the current health of each player
    [SerializeField] Slider p1HealthBar;
    [SerializeField] Slider p2HealthBar;

    //The parent UI object of all player UI
    [SerializeField] GameObject playerUIGameObject;

    //The object specifically holding each player's UI
    [SerializeField] GameObject p1UIGameObject;
    [SerializeField] GameObject p2UIGameObject;
    #endregion

    //The singleton reference for this script
    public static GamePlayManager Instance;

    //A reference to the player prefab instance
    public GameObject p1 { get; private set; }
    public GameObject p2 { get; private set; }

    //A list of all active player prefab instances
    public List<GameObject> activePlayers = new List<GameObject>();

    //Global bool for if the game is being run, used for pauses
    public bool isPlaying { get; private set; }

    //The inter round cooldown (in seconds)
    [SerializeField] float timeBeforeRoundStart = 3f;

    //The current state of the player select screen (used to decide what data gets affected)
    public PlayerSelectingState CurrPlayerSelectState { get; private set; }

    private void Start()
    {
        //Initialize the manager
        playerDataDictionary = PlayerData.ToDictionary();

        currLives = 3;
        OnLifeUpdate?.Invoke(this);

        CurrPlayerSelectState = PlayerSelectingState.NoPlayerSelecting;

        //TODO Remove after testing
        //SelectGameMode(1f);
        //StartCoroutine(nameof(BeginPlayerSpawning));
    }

    private void Awake()
    {
        //Set the singleton up
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(Instance);

        //Subscribe a method to this event
        SceneManager.activeSceneChanged += SceneManager_activeSceneChanged;
    }

    private void SceneManager_activeSceneChanged(Scene arg0, Scene arg1)
    {
        if (arg1.name == "Game Scene")
        {
            StartCoroutine(nameof(BeginPlayerSpawning));
        }
    }

    /// <summary>
    /// Spawns in players after a set amount of time
    /// </summary>
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
        playerUIGameObject.SetActive(true);

        if (currMode == PlayerMode.SinglePlayer)
        {
            p1 = Instantiate(playerPrefab, p1Spawnpoint);

            p1.transform.parent = null;

            p1.GetComponent<PlayerInputController>().InitializePlayer(0);
            p1.GetComponent<PlayerController>().data = p1Data;
            p1.GetComponent<PlayerController>().onPlayerHurt += UpdatePlayerHealthBars;
            p1.GetComponent<PlayerController>().onPlayerDie += BeginPlayerRespawn;
            p1.GetComponent<PlayerWeaponHandler>().data = p1Data;
            p1.GetComponent<PlayerWeaponHandler>().activeWeapons = p1Weapons;
            p1HealthBar.maxValue = p1Data.maxHealth;
            p1HealthBar.value = p1HealthBar.maxValue;
            p1UIGameObject.SetActive(true);

            activePlayers.Add(p1);
        }
        else if (currMode == PlayerMode.TwoPlayer)
        {
            p1 = Instantiate(playerPrefab, p1Spawnpoint);

            p1.transform.parent = null;

            p1.GetComponent<PlayerInputController>().InitializePlayer(0);
            p1.GetComponent<PlayerController>().data = p1Data;
            p1.GetComponent<PlayerController>().onPlayerHurt += UpdatePlayerHealthBars;
            p1.GetComponent<PlayerController>().onPlayerDie += BeginPlayerRespawn;
            p1.GetComponent<PlayerWeaponHandler>().data = p1Data;
            p1.GetComponent<PlayerWeaponHandler>().activeWeapons = p1Weapons;
            p1HealthBar.maxValue = p1Data.maxHealth;
            p1HealthBar.value = p1HealthBar.maxValue;

            activePlayers.Add(p1);

            p2 = Instantiate(playerPrefab, p2Spawnpoint);

            p2.transform.parent = null;

            p2.GetComponent<PlayerInputController>().InitializePlayer(1);
            p2.GetComponent<PlayerController>().data = p2Data;
            p2.GetComponent<PlayerController>().onPlayerHurt += UpdatePlayerHealthBars;
            p2.GetComponent<PlayerController>().onPlayerDie += BeginPlayerRespawn;
            p2.GetComponent<PlayerWeaponHandler>().data = p2Data;
            p2.GetComponent<PlayerWeaponHandler>().activeWeapons = p2Weapons;
            p2HealthBar.maxValue = p2Data.maxHealth;
            p2HealthBar.value = p2HealthBar.maxValue;

            activePlayers.Add(p2);

            p1UIGameObject.SetActive(true);
            p2UIGameObject.SetActive(true);
        }

    }

    /// <summary>
    /// Used by the UI buttons to select player count
    /// </summary>
    /// <param name="gameMode">how many players are in the game</param>
    public void SelectGameMode(float gameMode)
    {
        if (gameMode == 1)
        {
            currMode = PlayerMode.SinglePlayer;
            playerCount = 1;
        }
        if (gameMode == 2)
        {
            currMode = PlayerMode.TwoPlayer;
            playerCount = 2;
        }

        CurrPlayerSelectState = PlayerSelectingState.Player1Selecting;
    }

    /// <summary>
    /// Select a type of class data for each player (tied to UI Buttons)
    /// </summary>
    /// <param name="className">The name of each class</param>
    public void SelectClass(string className)
    {
        switch (CurrPlayerSelectState)
        {
            case PlayerSelectingState.Player1Selecting:
                playerDataDictionary.TryGetValue(className, out p1Data);
                break;
            case PlayerSelectingState.Player2Selecting:
                playerDataDictionary.TryGetValue(className, out p2Data);
                break;
        }
    }

    /// <summary>
    /// A public method used to affect the global player score
    /// </summary>
    /// <param name="value">The value that will be added to the total</param>
    public void UpdateScore(float value)
    {
        OnScoreUpdate?.Invoke(this);

        currentScore += value;

        scoreText.text = "SCORE: " + currentScore.ToString();
    }

    /// <summary>
    /// Update the health value for specific players
    /// </summary>
    /// <param name="sender">The player game object that called the method</param>
    /// <param name="newHealthVal">The new health value of the player</param>
    public void UpdatePlayerHealthBars(object sender, float newHealthVal)
    {
        Debug.Log("Health bar event");

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

    /// <summary>
    /// Switch to the game scene (used in the menu scene)
    /// </summary>
    public void StartGame()
    {
        SceneManager.LoadScene("Game Scene");
        playerUIGameObject.SetActive(true);
    }

    /// <summary>
    /// Check if the player can respawn and either respawn the player or switch to game over
    /// </summary>
    /// <param name="sender">Which player is being respawned</param>
    private void BeginPlayerRespawn(int sender)
    {
        if (currLives > 0)
        {
            currLives--;
            OnLifeUpdate?.Invoke(this);
            StartCoroutine(RespawnPlayer(sender));
        }
        else
        {
            GameOver();
        }
    }

    /// <summary>
    /// Just switch the scene to the Game Over scene
    /// </summary>
    private void GameOver()
    {
        SceneManager.LoadScene("Game Over");
    }

    private IEnumerator RespawnPlayer(int playerToRespawn)
    {
        if (playerToRespawn == 0) activePlayers.Remove(p1);
        if (playerToRespawn == 1) activePlayers.Remove(p2);

        yield return new WaitForSeconds(playerRespawnTimer);

        if (playerToRespawn == 0)
        {
            p1 = Instantiate(playerPrefab, p1Spawnpoint);

            p1.transform.parent = null;

            p1.GetComponent<PlayerInputController>().InitializePlayer(0);
            p1.GetComponent<PlayerController>().data = p1Data;
            p1.GetComponent<PlayerController>().onPlayerHurt += UpdatePlayerHealthBars;
            p1.GetComponent<PlayerController>().onPlayerDie += BeginPlayerRespawn;
            p1.GetComponent<PlayerWeaponHandler>().data = p1Data;
            p1.GetComponent<PlayerWeaponHandler>().activeWeapons = p1Weapons;
            p1HealthBar.maxValue = p1Data.maxHealth;
            p1HealthBar.value = p1HealthBar.maxValue;
            activePlayers.Add(p1);
        }
        else if (playerToRespawn == 1)
        {
            p2 = Instantiate(playerPrefab, p2Spawnpoint);

            p2.transform.parent = null;

            p2.GetComponent<PlayerInputController>().InitializePlayer(1);
            p2.GetComponent<PlayerController>().data = p2Data;
            p2.GetComponent<PlayerController>().onPlayerHurt += UpdatePlayerHealthBars;
            p2.GetComponent<PlayerController>().onPlayerDie += BeginPlayerRespawn;
            p2.GetComponent<PlayerWeaponHandler>().data = p2Data;
            p2.GetComponent<PlayerWeaponHandler>().activeWeapons = p2Weapons;
            p2HealthBar.maxValue = p2Data.maxHealth;
            p2HealthBar.value = p2HealthBar.maxValue;
            activePlayers.Add(p2);
        }

        foreach (EnemyController enemy in EnemyManager.instance.enemiesLeft)
        {
            if (enemy.targetGameObject == null) enemy.targetGameObject = activePlayers[playerToRespawn].transform;
        }
    }




}
