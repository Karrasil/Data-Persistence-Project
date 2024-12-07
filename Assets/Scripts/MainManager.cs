using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    public Text ScoreText;
    public GameObject GameOverText;
    public Text BestScoreText;

    private bool m_Started = false;
    private int m_Points;

    private bool m_GameOver = false;
    [SerializeField] private highscores highscores;
    public UnityEvent onGameOver;




    // Start is called before the first frame update
    void Start()
    {
        BestScoreText.text = $"Best score: {GameManager.Instance.highScorePlayer} : {GameManager.Instance.highScore}";

        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);

        int[] pointCountArray = new[] { 1, 1, 2, 2, 5, 5 };
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
            }
        }
    }

    private void Update()
    {
        if (!m_Started)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_Started = true;
                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                Ball.transform.SetParent(null);
                Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }
        }
        else if (m_GameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
            else if(Input.GetKeyDown(KeyCode.Escape)){
                GameManager.Instance.SaveHighScore();
                SceneManager.LoadScene(0);
            }
        }

        HighScoreCheck();
    }

    private void HighScoreCheck()
    {
        if (m_Points > GameManager.Instance.highScore)
        {
            GameManager.Instance.highScore = m_Points;
            GameManager.Instance.highScorePlayer = GameManager.Instance.playerName;
            BestScoreText.text = $"Best score: {GameManager.Instance.highScorePlayer} : {GameManager.Instance.highScore}";
        }
        GameManager.Instance.score = m_Points;
    }

    void AddPoint(int point)
    {
        m_Points += point;
        ScoreText.text = $"Score : {m_Points}";
    }

    public void GameOver()
    {   
        CheckHighscore();

        GameManager.Instance.SaveHighScore();

        m_GameOver = true;
        GameOverText.SetActive(true);
    }

    private void Exit(){
        GameManager.Instance.SaveHighScore();
        Application.Quit();
    }

    public void CheckHighscore(){
        if(GameManager.Instance.highscoresDictonary.ContainsKey(GameManager.Instance.playerName)){
            if(GameManager.Instance.highscoresDictonary[GameManager.Instance.playerName] < GameManager.Instance.score){
                GameManager.Instance.highscoresDictonary[GameManager.Instance.playerName] = GameManager.Instance.score;
            }
        }
        else{
            GameManager.Instance.highscoresDictonary.Add(GameManager.Instance.playerName, GameManager.Instance.score);
        }
    }
}
