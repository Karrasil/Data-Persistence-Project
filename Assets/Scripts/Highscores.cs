using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class highscores : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI highscoresList;

    private void Start(){
        foreach(KeyValuePair<string, int> scores in GameManager.Instance.highscoresDictonary){
            highscoresList.text += $"{scores.Key} : {scores.Value}\n";
        }
    }


}