using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Goal : MonoBehaviour
{
    public GameObject EndGameCanvas;
    public Text EndGameText;
    public Timer timer;
    public GameObject UICanvas;

    public bool gameover = false;
    
    private float highScore;

    void Start()
    {
        highScore = PlayerPrefs.GetFloat("HighScore", Mathf.Infinity);

        if (timer == null)
        {
            timer = FindObjectOfType<Timer>();
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            EndGameText.text = "Level Complete";
            EndGameCanvas.SetActive(true);
            Time.timeScale = 0f;
            UICanvas.SetActive(false);
            gameover = true;

            if (timer.GetTime() < highScore)
            {
                highScore = timer.GetTime();
                PlayerPrefs.SetFloat("HighScore", highScore);
                timer.updateHighScoreText();
            }
        }
    }
}
