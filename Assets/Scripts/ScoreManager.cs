using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    public int recolectedCoins;
    public Text coinScore, timeText;
    public GameObject gunImage, shotgunImage, gunInHand, shotgunInHand, respawnPoint, cureText;
    public bool weapon1Collected = false, pistolEquiped = false, shotgunEquiped = false,
        timerIsRunning = false, medicinePicked = false, showSignal = false, cureWasPicked = false,
        showStory = false, allCoinsCollected = false, allDiariesCollected = false, zombieMode = false,
        canShoot = false, level1Finished = false, showFinalPanel = false, document1WasPicked = false,
        document2WasPicked = false, document3WasPicked = false, document4WasPicked = false, document5WasPicked = false;
    public Slider lifeBarSlider;
    public float timeRemaining = 30;
    public int diariesCounter = 0;
    private GameObject player;
    private int TOTAL_TIME = 30;
    public int COINS_TARGET = 30;
    private int DIARIES_TARGET = 5;
    private GameObject[] gameObjects;
    public PosprocesingController posprocesingController;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        //RespawnPlayer();
       
    }
    private void Update()
    {
        coinScore.text = $"{recolectedCoins}";

        TimeCounter();

        if (lifeBarSlider.value == 0)
        {
            zombieMode = true;
        }
        if (lifeBarSlider.value > 0) {
            zombieMode = false;
        }
        if (zombieMode)
        {
            StartZombieMode();
        }
        if (weapon1Collected && cureWasPicked)
        {
            gunImage.gameObject.SetActive(true);
            gunInHand.gameObject.SetActive(true);
            shotgunInHand.SetActive(false);
            shotgunImage.SetActive(false);
        }
        if (shotgunEquiped)
        {
            gunInHand.gameObject.SetActive(false);
            shotgunInHand.SetActive(true);
            shotgunImage.SetActive(true);
            gunImage.gameObject.SetActive(false);
        }
        if (pistolEquiped)
        {
            gunInHand.gameObject.SetActive(true);
            shotgunInHand.SetActive(false);
            shotgunImage.SetActive(false);
            gunImage.gameObject.SetActive(true);
        }
        if (recolectedCoins >= COINS_TARGET) {
            allCoinsCollected = true;
        }
        if (diariesCounter == DIARIES_TARGET) {
            allDiariesCollected = true;
        }
        if (allDiariesCollected && allCoinsCollected && weapon1Collected && cureWasPicked) {
            Analytics.CustomEvent("Finish Level", transform.position);
            level1Finished = true;
        }
    }
    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        cureText.SetActive(true);
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
                    RestartZombieMode();
                }
            }
            else
            {
                timeRemaining = 0;
                timerIsRunning = false;
            }
        }
    }
    public void KillAllZombies ()
    {
        gameObjects = GameObject.FindGameObjectsWithTag("Zombie");

        for (var i = 0; i < gameObjects.Length; i++)
            Destroy(gameObjects[i]);
    }
    public void RestartZombieMode() {
        posprocesingController.EnableColorGrading(false);
        posprocesingController.EnableVignet(false);
        timerIsRunning = false;
        timeRemaining = TOTAL_TIME;
        timeText.gameObject.SetActive(false);
        zombieMode = false;
        lifeBarSlider.value = 3;
        medicinePicked = false;
        cureText.gameObject.SetActive(false);
    }
    public void StartZombieMode() {
            posprocesingController.EnableColorGrading(true);
            posprocesingController.EnableVignet(true);
            timeText.gameObject.SetActive(true);
            timerIsRunning = true;

    }
    public void RespawnPlayer() {
        player.transform.position = respawnPoint.transform.position;

    }
    public void QuitGame()
    {
    #if UNITY_EDITOR

            UnityEditor.EditorApplication.isPlaying = false;
    #else
                     Application.Quit();
    #endif
    }
}
