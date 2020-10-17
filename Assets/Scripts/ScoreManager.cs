using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    public int recolectedCoins;
    public Text coinScore;
    public GameObject gunImage;
    public GameObject gunInHand;
    public bool weapon1Collected = false;
    public Slider lifeBarSlider;
    private float timeRemaining = 30;
    public bool timerIsRunning = false;
    public Text timeText;
    public bool medicinePicked = false;
    public GameObject endgamePanel;

    // Start is called before the first frame update
    private void Start()
    {
    }
    private void Update()
    {
        coinScore.text = $"Monedas: {recolectedCoins}";
        TimeCounter();
        if (lifeBarSlider.value == 0) {
            timeText.gameObject.SetActive(true);
            timerIsRunning = true;
        }
       
        if (weapon1Collected == true)
        {
            gunImage.gameObject.SetActive(true);
            gunInHand.gameObject.SetActive(true);
        }
    }
    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
    private void TimeCounter() {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                DisplayTime(timeRemaining);
                if (medicinePicked) {
                    timerIsRunning = false;
                    timeRemaining = 20;
                    timeText.gameObject.SetActive(false);

                }
            }
            else
            {
                timeRemaining = 0;
                timerIsRunning = false;
                Time.timeScale = 0f;
                endgamePanel.gameObject.SetActive(true);
            }
        }
    }
}
