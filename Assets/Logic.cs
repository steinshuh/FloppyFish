using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Logic : MonoBehaviour
{

    
    public int score = 0;
    public Text scoreText;
    public Text highScoreText;
    public GameObject gameOverScreen;
    public FishScript fishScript;
    public AudioSource bloopSound;
    public AudioClip bloop;

    private bool highScoreUpdated = false;

    [ContextMenu("Add to score")]
    public void addScore(int s)
    {
        if (fishScript.alive)
        {
            score += s;
            scoreText.text = score.ToString();
            bloopSound.PlayOneShot(bloop);
            if(score > MainManager.Instance.highScore)
            {
                MainManager.Instance.highScore = score;
                highScoreText.text = "High Score: "+MainManager.Instance.highScore.ToString();
                highScoreUpdated = true;
            }
        }
    }

    public void restartGame()
    {
       SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void gameOver()
    {
        if (highScoreUpdated)
        {
            MainManager.Instance.updateHighScore();
        }
        gameOverScreen.SetActive(true);
    }

    // Start is called before the first frame update
    void Start()
    {
        fishScript = GameObject.FindGameObjectWithTag("fish").GetComponent<FishScript>();
        highScoreText.text = "High Score: " + MainManager.Instance.highScore.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
