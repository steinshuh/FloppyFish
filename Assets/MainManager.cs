using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainManager : MonoBehaviour
{
    public static MainManager Instance;

    public int highScore = 0;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        //dont need to check, returns 0 if highScore is not found
        highScore = PlayerPrefs.GetInt("highScore");
    }

    public void updateHighScore()
    {
        PlayerPrefs.SetInt("highScore", highScore);
        PlayerPrefs.Save();
    }

    public void resetHighScore()
    {
        PlayerPrefs.DeleteAll();
        Instance.highScore = 0;
    }

    public void loadGame()
    {
        SceneManager.LoadScene("fish");
    }
}
