using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using Rewired;
using Rewired.Components;
public class MenuManager : MonoBehaviour
{
    public enum ActiveMenuState
    {
        MainMenu,
        StartGame,
        HighScores,
        HowToPlay,
    }

    [SerializeField] GameObject[] menuGameObjects;
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

    // Update is called once per frame
    void Update()
    {

    }

    private void SpawnCursors(int playerID)
    {
        GameObject cursor = Instantiate(cursorPrefab, transform);
        CursorMovement cMovement = cursor.GetComponent<CursorMovement>();

        pMouse.ScreenPositionChangedEvent += cMovement.MoveMouse;
    }



    public void ChangeActiveMenuState(string targetMenuState)
    {
        foreach (var menu in menuGameObjects) { menu.SetActive(false); }

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



    public void ExitGame()
    {
        Application.Quit();
    }
}
