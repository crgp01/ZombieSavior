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
    public float timeRemaining = 30;
    public bool timerIsRunning = false;
    public Text timeText;
    public bool medicinePicked = false;
    public bool showSignal = false;
    public bool cureWasPicked = false;
    public bool showStory = false;
    public bool allCoinsCollected = false;
    public bool allDiariesCollected = false;
    public int diariesCounter = 0;
    private int TOTAL_TIME = 30;
    private int COINS_TARGET = 20;
    private int DIARIES_TARGET = 5;

    
    public PosprocesingController posprocesingController;
    // Start is called before the first frame update
    private void Start()
    {
       
    }
    private void Update()
    {
        coinScore.text = $"Monedas: {recolectedCoins}";

        TimeCounter();

        if (lifeBarSlider.value == 0) {
            posprocesingController.EnableColorGrading(true);
            timeText.gameObject.SetActive(true);
            timerIsRunning = true;
        }
       
        if (weapon1Collected && cureWasPicked)
        {
            gunImage.gameObject.SetActive(true);
            gunInHand.gameObject.SetActive(true);
        }
        if (recolectedCoins == COINS_TARGET) {
            allCoinsCollected = true;
        }
        if (diariesCounter == DIARIES_TARGET) {
            allDiariesCollected = true;
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
                    posprocesingController.EnableColorGrading(false);
                    timerIsRunning = false;
                    timeRemaining = TOTAL_TIME;
                    timeText.gameObject.SetActive(false);

                }
            }
            else
            {
                timeRemaining = 0;
                timerIsRunning = false;
            }
        }
    }
}
