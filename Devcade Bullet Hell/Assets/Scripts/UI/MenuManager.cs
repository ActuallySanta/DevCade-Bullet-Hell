using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using Rewired;
using Rewired.Components;
public class MenuManager : MonoBehaviour
{
    //Shows which parent menu is active
    public enum ActiveMenuState
    {
        MainMenu,
        StartGame,
        HighScores,
        HowToPlay,
    }

    //All the menu parent game objects
    [SerializeField] GameObject[] menuGameObjects;

    //A reference to the gamepad controllable cursor
    [SerializeField] GameObject cursorPrefab;

    Rewired.PlayerMouse pMouse;

    /*
     * 0 = main menu
     * 1 = start game
     * 2 = high scores
     * 3 = how to play
     */

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pMouse = GetComponent<Rewired.PlayerMouse>();

        foreach (Player item in ReInput.players.GetPlayers())
        {
            SpawnCursors(item.id);
        }
    }

    /// <summary>
    /// Spawn a game pad controlled cursor and assign it to each player
    /// </summary>
    /// <param name="playerID"></param>
    private void SpawnCursors(int playerID)
    {
        GameObject cursor = Instantiate(cursorPrefab, transform);
        CursorMovement cMovement = cursor.GetComponent<CursorMovement>();

        pMouse.ScreenPositionChangedEvent += cMovement.MoveMouse;
    }


    /// <summary>
    /// Choose a new menu to display (used by UI buttons)
    /// </summary>
    /// <param name="targetMenuState">The name of the menu gameObject you want to activate</param>
    public void ChangeActiveMenuState(string targetMenuState)
    {
        foreach (var menu in menuGameObjects) { menu.SetActive(false); }

        //Convert the string to an enum (only once)
        ActiveMenuState parsedState;
        Enum.TryParse(targetMenuState, out parsedState);

        switch (parsedState)
        {
            case ActiveMenuState.MainMenu:
                menuGameObjects[0].SetActive(true);
                break;

            case ActiveMenuState.StartGame:
                menuGameObjects[1].SetActive(true);
                break;

            case ActiveMenuState.HighScores:
                menuGameObjects[2].SetActive(true);
                break;

            case ActiveMenuState.HowToPlay:
                menuGameObjects[3].SetActive(true);
                break;

            default:
                Debug.LogWarning("No Menu Found");
                break;
        }
    }


    /// <summary>
    /// Close the game
    /// </summary>
    public void ExitGame()
    {
        Application.Quit();
    }
}
