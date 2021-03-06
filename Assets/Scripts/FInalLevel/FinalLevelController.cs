﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FinalLevelController : MonoBehaviour
{
    public int recolectedCoins;
    public Text coinScore;
    public GameObject gunImage, shotgunImage, shotgunInHand;
    public GameObject gunInHand;
    public Slider lifeBarSlider;
    public float timeRemaining = 30;
    public bool timerIsRunning = false;
    public Text timeText;
    public bool medicinePicked = false;
    public bool cureWasPicked = false;
    public bool allCoinsCollected = false;
    public bool zombieMode = false, pistolEquiped = false, shotgunEquiped = false, canShoot = false;
    private GameObject player;
    public GameObject respawnPoint;
    private int TOTAL_TIME = 30;
    public GameObject cureText;
    private GameObject[] gameObjects;
    public PosprocesingController posprocesingController;
    public GameObject medicineLocation1;
    public GameObject medicineLocation2;
    public GameObject medicinePrefab;
    public GameObject medicine1;
    public GameObject medicine2;
    private bool medicineWasSpawned = true;


    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        recolectedCoins = PlayerPrefs.GetInt("recolectedCoins");
        RespawnPlayer();
    }

    private void Update()
    {
        coinScore.text = $"{recolectedCoins}";

        TimeCounter();

        if (lifeBarSlider.value == 0)
        {
            zombieMode = true;
            if (medicineWasSpawned) {
                medicine1 = Instantiate(medicinePrefab, medicineLocation1.transform.position, Quaternion.identity);
                medicine1.gameObject.tag = "Cure";
     
                medicine2 = Instantiate(medicinePrefab, medicineLocation2.transform.position, Quaternion.identity);
                medicine2.gameObject.tag = "Cure";

                medicineWasSpawned = false;
            }
           
        }
        if (lifeBarSlider.value > 0)
        {
            zombieMode = false;
            medicineWasSpawned = true;
            Destroy(medicine1);
            Destroy(medicine2);
        }
        if (zombieMode)
        {
            StartZombieMode();
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
    }
    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        cureText.SetActive(true);
        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
    private void TimeCounter()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                DisplayTime(timeRemaining);

                if (medicinePicked)
                {
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
    public void RestartZombieMode()
    {
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
    public void StartZombieMode()
    {
        posprocesingController.EnableColorGrading(true);
        posprocesingController.EnableVignet(true);
        timeText.gameObject.SetActive(true);
        timerIsRunning = true;
        //FindObjectOfType<AudioManager>().Play("ArchieKilled");

    }
    public void RespawnPlayer()
    {
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
