using System;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public string playerName;
    public int highScore;
    public string highScorePlayer;
    public int score;
    public Dictionary<string, int> highscoresDictonary = new Dictionary<string, int>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    private void Start(){
        if(File.Exists(Application.persistentDataPath + "/savefile.json")){
            LoadHighScore();
        }
    }

    [Serializable]
    public class SaveData
    {
        public string savedPlayerName;
        public int highScore;
        public Dictionary<string, int> highscoresDictonary = new Dictionary<string, int>();
    }

    public void SaveHighScore()
    {
        SaveData saveData = new SaveData();
        saveData.savedPlayerName = highScorePlayer;
        saveData.highScore = highScore;
        saveData.highscoresDictonary = highscoresDictonary;

        string savedPlayerNameJSON = JsonUtility.ToJson(saveData);

        string path = Application.persistentDataPath + "/savefile.json";
        File.WriteAllText(path, savedPlayerNameJSON);
    }

    public void LoadHighScore(){
        string path = Application.persistentDataPath + "/savefile.json";
        string savedPlayerNameJSON = File.ReadAllText(path);

        SaveData saveData = JsonUtility.FromJson<SaveData>(savedPlayerNameJSON);
        highScorePlayer = saveData.savedPlayerName;
        highScore = saveData.highScore;
        highscoresDictonary = saveData.highscoresDictonary;
    }


}
