using UnityEngine;
using UnityEngine.SceneManagement;

public class UIControl : MonoBehaviour
{
    public void ReadPlayerName(string name)
    {
        GameManager.Instance.playerName = name;
    }

        public void GameStart()
    {
        SceneManager.LoadScene(1);
    }

    public void OpenHighscores(){
        SceneManager.LoadScene(2);
    }

    public void ReturnToMenu(){
        SceneManager.LoadScene(0);
    }
}
