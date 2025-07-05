using System;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class HighScoreManager : MonoBehaviour
{
    public static HighScoreManager instance;

    public Dictionary<string, float> playerScores = new Dictionary<string, float>();

    public const string HIGHSCORE_SAVE_PATH = "/highscores.json";

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }

        DontDestroyOnLoad(instance);
    }

    public static void SaveHighScore(string playerName, float playerScore)
    {
        instance.playerScores.Add(playerName, playerScore);

    }

    public static void UpdateJson()
    {
        //init path
        string FilePathSaveData = Application.persistentDataPath + HIGHSCORE_SAVE_PATH;

        string scoreData = JsonUtility.ToJson(instance.playerScores, true);

        File.WriteAllText(FilePathSaveData, scoreData);
    }

    public static void ReadFromJson()
    {
        //init path
        string FilePathLoadData = Application.persistentDataPath + HIGHSCORE_SAVE_PATH;
        
        //Read all the data from the text file
        string fileContent = File.ReadAllText(FilePathLoadData);

        //Load data into the dictionary
        instance.playerScores = JsonUtility.FromJson<Dictionary<string, float>>(fileContent);
    }
}
